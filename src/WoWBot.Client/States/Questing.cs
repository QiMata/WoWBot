using Newtonsoft.Json;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Database;
using WoWBot.Client.Helpers;
using WoWBot.Client.Quest;

namespace AdvancedQuester.FSM.States
{
    public class Questing : State
    {
        public override bool NeedToRun => true;

        private Vector3 CurrentHotspot = new Vector3();
        private readonly Dictionary<ulong, Stopwatch> TargetGuidBlacklist = new Dictionary<ulong, Stopwatch>();

        private static readonly List<QuestTask> ToDo = new List<QuestTask>();
        private static readonly List<QuestCompletion> Completed = new List<QuestCompletion>();

        public Questing()
        {
            DisplayName = "Questing";
            LoadQuestProgress();
        }
        public QuestObjective ClosestObjective
        {
            get
            {
                List<Vector3> closestPositions = RemainingQuestObjectives
                                                        .SelectMany(x => x.HotSpots)
                                                        .OrderBy(spot => spot.DistanceTo(ObjectManager.Me.Position))
                                                        .ToList();

                return RemainingQuestObjectives.Find(x => x.HotSpots.Contains(closestPositions[0]));
            }
        }
        public List<QuestObjective> RemainingQuestObjectives
        {
            get
            {
                return ToDo.FindAll(x => !x.IsComplete())
                           .SelectMany(x => x.QuestObjectives)
                           .ToList();
            }
        }
        public List<WoWUnit> NearbyQuestGivers()
        {
            return ObjectManager.GetObjectWoWUnit()
                                .FindAll(x => x.NpcMarker.Equals(NpcMarker.YellowExclamation))
                                .OrderBy(x => x.Position.DistanceTo(ObjectManager.Me.Position))
                                .ToList();
        }

        public List<WoWUnit> NearbyQuestTurnIns()
        {
            return ObjectManager.GetObjectWoWUnit()
                                .FindAll(x => x.NpcMarker.Equals(NpcMarker.YellowQuestion))
                                .OrderBy(x => x.Position.DistanceTo(ObjectManager.Me.Position))
                                .ToList();
        }

        private WoWUnit NearestQuestTarget()
        {
            var targets = ObjectManager.GetWoWUnitByEntry(RemainingQuestObjectives.Select(x => x.TargetId).ToList());
            targets.RemoveAll(x => TargetGuidBlacklist.ContainsKey(x.Guid) || x.Position.DistanceTo(ObjectManager.Me.Position) > 50);
            return ObjectManager.GetNearestWoWUnit(targets);
        }

        private WoWGameObject NearestQuestObject()
        {
            var nearbyGameObjects = ObjectManager.GetWoWGameObjectById(RemainingQuestObjectives.Select(x => x.TargetId).ToList());
            nearbyGameObjects.RemoveAll(x => x.Position.DistanceTo(ObjectManager.Me.Position) > 50);
            return ObjectManager.GetNearestWoWGameObject(nearbyGameObjects);
        }

        public override void Run()
        {
            try
            {
                // Update ToDo list with current quest log
                UpdateToDoWithLogQuests();

                if (CheckForQuestRelatedEntities())
                {
                    LootAllNearbyLootables();
                }
                else
                {
                    RotateThroughClosestHotSpot();
                }
            }
            catch (Exception ex)
            {
                Logging.WriteError("[Questing] " + ex.StackTrace);
            }
        }

        private bool CheckForQuestRelatedEntities()
        {
            WoWGameObject questObject = NearestQuestObject();
            WoWUnit questTarget = NearestQuestTarget();
            List<WoWUnit> questGivers = NearbyQuestGivers();
            List<WoWUnit> questTurnIns = NearbyQuestTurnIns();

            if (questGivers.Count > 0 || questTurnIns.Count > 0 || questObject.IsValid || questTarget.IsValid)
            {
                List<WoWUnit> nearbyMobs = ObjectManager.GetObjectWoWUnit()
                                .FindAll(x => x.IsAlive && x.IsAttackable && x.Reaction == Reaction.Hostile)
                                .OrderBy(x => x.Position.DistanceTo(ObjectManager.Me.Position))
                                .ToList();

                MovementManager.StopMove();

                if (questTurnIns.Count > 0 && questTurnIns[0].IsValid)
                {
                    while (questTurnIns.Count > 0 && questTurnIns[0].IsValid)
                    {
                        WoWUnit questNpc = questTurnIns[0];
                        TurnInAllQuestsForNpc(questNpc);

                        questTurnIns = NearbyQuestTurnIns();
                    }
                }
                else if (questGivers.Count > 0 && questGivers[0].IsValid && Quest.GetLogQuestId().Count < 20)
                {
                    while (questGivers.Count > 0 && questGivers[0].IsValid)
                    {
                        WoWUnit questNpc = questGivers[0];
                        PickUpQuestsFromNpc(questNpc);

                        questGivers = NearbyQuestGivers();
                    }
                }
                else if (questObject.IsValid && nearbyMobs.Count > 0 && nearbyMobs[0].IsValid)
                {
                    // If the object is closer to the character than the mob
                    // or the aggro distance (with padding) is still longer than the distance from the mob to the game object
                    if (questObject.Position.DistanceTo(ObjectManager.Me.Position) < nearbyMobs[0].Position.DistanceTo(ObjectManager.Me.Position)
                        || nearbyMobs[0].AggroDistance + 5 > nearbyMobs[0].Position.DistanceTo(questObject.Position))
                    {
                        // Go ahead and get the game object
                        HandleInteractingAndLootingGameObject(questObject);
                    }
                    else
                    {
                        // Handle the mob since we might aggro it while interacting with the game object
                        HandleAttackableTarget(nearbyMobs[0]);
                    }
                }
                else if (questObject.IsValid)
                {
                    HandleInteractingAndLootingGameObject(questObject);
                }
                else if (questTarget.IsValid)
                {
                    HandleAttackableTarget(questTarget);
                }

                if (TargetGuidBlacklist.Keys.Count > 0)
                {
                    List<ulong> keys = TargetGuidBlacklist.Keys.ToList();
                    foreach (var key in keys)
                    {
                        if (TargetGuidBlacklist[key] != null && TargetGuidBlacklist[key].Elapsed > TimeSpan.FromMinutes(2))
                        {
                            TargetGuidBlacklist.Remove(key);
                        }
                    }
                }
                return true;
            }
            return false;
        }

        private void PickUpQuestsFromNpc(WoWUnit npc)
        {
            List<int> questsIdsInPreviousLog = Quest.GetLogQuestId()
                                            .Select(x => x.ID)
                                            .ToList();

            MoveToAndInteractWith(npc);

            DialogueHelper.PickupQuestFromNpc();

            UpdateToDoWithLogQuests();
        }

        private static void UpdateToDoWithLogQuests()
        {
            List<int> questsIdsLog = Quest.GetLogQuestId()
                                                .Select(x => x.ID)
                                                .ToList();

            if (ToDo.Count > 0)
            {
                ToDo.RemoveAll(x => !questsIdsLog.Contains(x.QuestId));
            }

            if (questsIdsLog.Count > 0)
            {
                foreach (var id in questsIdsLog)
                {
                    if (ToDo.FindAll(x => x.QuestId == id).Count == 0)
                    {
                        ToDo.Add(QuestDb.GetQuestTaskById(id));
                    }
                }
            }
        }

        private void TurnInAllQuestsForNpc(WoWUnit npc)
        {
            MoveToAndInteractWith(npc);

            List<int> questsIdsInPreviousLog = Quest.GetLogQuestId()
                                                    .Select(x => x.ID)
                                                    .ToList();

            if (Lua.LuaDoString<bool>($@"return GossipTitleButton1:IsVisible()"))
            {
                for (int i = 1; i < 4; i++)
                {
                    foreach (QuestTask quest in ToDo)
                    {
                        if (quest.Name.Equals(DialogueHelper.GetQuestTitleOption(i)) || quest.Name.Equals(DialogueHelper.GetQuestGossipOption(i)))
                        {
                            DialogueHelper.TurnInQuestFromNpc(quest.Name, quest.QuestRewardSelection());
                            break;
                        }
                    }
                }
            }
            else
            {
                QuestTask potentialQuest = ToDo.Find(x => x.TurnInNpc.NpcId.Equals(GetNpcIdFromGuid(npc)));

                if (potentialQuest != null)
                {
                    DialogueHelper.TurnInQuestFromNpc(potentialQuest.Name, potentialQuest.QuestRewardSelection());
                }
            }


            Thread.Sleep(500);

            List<int> questsIdsInLog = Quest.GetLogQuestId()
                                            .Select(x => x.ID)
                                            .ToList();

            foreach (int questId in questsIdsInPreviousLog)
            {
                if (!questsIdsInLog.Contains(questId) && questsIdsInPreviousLog.Contains(questId))
                {
                    QuestTask completedQuest = ToDo.Find(x => x.QuestId == questId);

                    if (completedQuest != null)
                    {
                        ToDo.Remove(completedQuest);

                        if (Completed.Find(x => x.QuestId == questId) == null)
                        {
                            Completed.Add(new QuestCompletion() { QuestId = completedQuest.QuestId, Name = completedQuest.Name });
                        }

                        File.WriteAllText(@Directory.GetCurrentDirectory() + "/" + ObjectManager.Me.Name + "Quest.json", JsonConvert.SerializeObject(Completed));
                        break;
                    }
                }
            }

            InventoryHelper.UpgradeToLatestEquipment();
        }

        private void MoveToAndInteractWith(WoWUnit npc)
        {
            MoveDefensivelyToPosition(npc.Position, npc.InteractDistance);

            GoToTask.ToPositionAndIntecractWithNpc(npc.Position, GetNpcIdFromGuid(npc));
        }

        private static int GetNpcIdFromGuid(WoWUnit npc)
        {
            return int.Parse(npc.Guid.ToString("X").Substring(6, 4), System.Globalization.NumberStyles.HexNumber);
        }

        private void MoveDefensivelyToPosition(Vector3 position, float interactDistance = 3.5f)
        {
            List<Vector3> path = PathFinder.FindPath(position);

            while (path.Count > 0 && path[0].DistanceTo(position) > interactDistance)
            {
                MovementManager.MoveTo(path[0]);

                while (MovementManager.InMovement || ObjectManager.Me.InCombatFlagOnly || ObjectManager.Me.IsLooting())
                {
                    DefendAgainstAttackingUnits();
                    LootAllNearbyLootables();
                    if (path == null || path.Count == 0)
                    {
                        return;
                    }
                }
                path.RemoveAt(0);
            }
            GoToTask.ToPosition(position, interactDistance);
        }
        private void RotateThroughArea(Vector3 position)
        {
            List<Vector3> path = PathFinder.FindPath(position);

            if (path.Count > 0)
            {
                MovementManager.Go(path);

                while (MovementManager.InMovement)
                {
                    DefendAgainstAttackingUnits();

                    if (CheckForQuestRelatedEntities()
                        || UseNearbyHotSpotQuestConsumeables())
                    {
                        LootAllNearbyLootables();
                        break;
                    }
                }
            }
        }

        public static void LoadQuestProgress()
        {
            List<Quest.PlayerQuest> playerQuests = Quest.GetLogQuestId();

            foreach (Quest.PlayerQuest playerQuest in playerQuests)
            {
                if (ToDo.Find(x => x.QuestId.Equals(playerQuest.ID)) == null)
                {
                    ToDo.Add(QuestDb.GetQuestTaskById(playerQuest.ID));
                }
            }

            if (File.Exists(@Directory.GetCurrentDirectory() + "/" + ObjectManager.Me.Name + "Quest.json"))
            {
                string questCompletionJson = File.ReadAllText(@Directory.GetCurrentDirectory() + "/" + ObjectManager.Me.Name + "Quest.json");
                Completed.AddRange(JsonConvert.DeserializeObject<List<QuestCompletion>>(questCompletionJson));
            }

            foreach (QuestCompletion completion in Completed)
            {
                ToDo.RemoveAll(x => x.QuestId == completion.QuestId);
            }
        }

        private static void DefendAgainstAttackingUnits()
        {
            List<WoWUnit> attackingUnits = ObjectManager.GetUnitAttackPlayer();

            while (attackingUnits.Count > 0)
            {
                MovementManager.StopMove();

                var nearestEnemy = ObjectManager.GetNearestWoWUnit(attackingUnits);
                Fight.StartFight(nearestEnemy.Guid);

                while (nearestEnemy.IsAlive)
                {
                    Thread.Sleep(100);
                }
                attackingUnits = ObjectManager.GetUnitAttackPlayer();
            }
            List<Vector3> currentTrajectory = MovementManager.CurrentPath.OrderBy(x => x.DistanceTo(ObjectManager.Me.Position)).ToList();
            List<WoWUnit> localWoWUnits = FindHostileUnitsNearPosition(currentTrajectory[0]);

            if (!ObjectManager.Me.InCombatFlagOnly && currentTrajectory.Count > 0)
            {
                currentTrajectory = MovementManager.CurrentPath.OrderBy(x => x.DistanceTo(ObjectManager.Me.Position)).ToList();
                localWoWUnits = FindHostileUnitsNearPosition(currentTrajectory[0]);

                while (localWoWUnits.Count > 0)
                {
                    Fight.StartFight(ObjectManager.GetNearestWoWUnit(localWoWUnits).Guid);

                    while (Fight.InFight || ObjectManager.Me.InCombatFlagOnly)
                    {
                        Thread.Sleep(100);
                    }
                    LootAllNearbyLootables();

                    localWoWUnits = FindHostileUnitsNearPosition(currentTrajectory[0]);
                }
            }

            LootAllNearbyLootables();

            Fight.StopFight();
        }

        private static List<WoWUnit> FindHostileUnitsNearPosition(Vector3 currentTrajectory)
        {
            return ObjectManager.GetObjectWoWUnit().FindAll(x => x.IsAlive
                                                && x.IsAttackable
                                                && x.Reaction == Reaction.Hostile
                                                && x.AggroDistance + ObjectManager.Me.InteractDistance + 0.5 > x.Position.DistanceTo(currentTrajectory))
                                            .ToList();
        }

        private bool UseNearbyHotSpotQuestConsumeables()
        {
            // Use consumables for the quest when in range of its hotspot
            List<QuestObjective> objectivesWithConsumables = RemainingQuestObjectives.FindAll(x => x.ConsumableItemId != 0);

            foreach (QuestObjective objective in objectivesWithConsumables)
            {
                if (objective.HotSpots.Count == 1 && ObjectManager.Me.Position.DistanceTo(objective.HotSpots[0]) < 5 && ItemsManager.HasItemById(Convert.ToUInt32(objective.ConsumableItemId)))
                {
                    ItemsManager.UseItem(ItemsManager.GetNameById(objective.ConsumableItemId));
                    Thread.Sleep(1500);
                    return true;
                }
            }
            return false;
        }

        private static void LootAllNearbyLootables()
        {
            // Loot all lootables around you
            List<WoWUnit> list = ObjectManager.GetWoWUnitLootable();

            if (list.Count > 0)
            {
                MovementManager.StopMove();

                while (list.Count > 0 && Bag.GetContainerNumFreeSlots > 0)
                {
                    LootingTask.Pulse(list);
                    list = ObjectManager.GetWoWUnitLootable();
                    Usefuls.WaitIsLooting();
                }
            }
        }

        private void RotateThroughClosestHotSpot()
        {
            if (ClosestObjective != null)
            {
                if (!ClosestObjective.HotSpots.Contains(CurrentHotspot))
                {
                    CurrentHotspot = ClosestObjective.HotSpots.OrderBy(x => x.DistanceTo(ObjectManager.Me.Position)).ToArray()[0];
                }
                else
                {
                    int index = ClosestObjective.HotSpots.IndexOf(CurrentHotspot);
                    if (index == ClosestObjective.HotSpots.Count - 1)
                    {
                        ClosestObjective.HotSpots.Reverse();
                        CurrentHotspot = ClosestObjective.HotSpots[0];
                    }
                    else
                    {
                        CurrentHotspot = ClosestObjective.HotSpots[index + 1];
                    }
                }

                RotateThroughArea(CurrentHotspot);
            }
            else if (ToDo.FindAll(x => x.IsComplete()).Count > 0)
            {
                RotateThroughArea(ToDo.FindAll(x => x.IsComplete())
                    .OrderBy(x => x.TurnInNpc.Position.DistanceTo(ObjectManager.Me.Position))
                    .ToArray()[0].TurnInNpc.Position);
            }
            else
            {
                CheckForQuestRelatedEntities();
            }
        }

        private bool HandleInteractingAndLootingGameObject(WoWGameObject gameObject)
        {
            MoveDefensivelyToPosition(gameObject.Position, gameObject.InteractDistance);

            Interact.InteractGameObjectAutoLoot(gameObject.GetBaseAddress);
            Usefuls.WaitIsCastingAndLooting();

            // Give time to despawn
            Thread.Sleep(500);
            return true;
        }

        private bool HandleAttackableTarget(WoWUnit target)
        {
            if (ClosestObjective.UsableItemId != 0 && ClosestObjective.TargetId == target.Entry)
            {
                ObjectManager.Me.Target = target.Guid;

                MoveDefensivelyToPosition(target.Position, target.InteractDistance);

                ItemsManager.UseItem(ItemsManager.GetNameById(ClosestObjective.UsableItemId));
                TargetGuidBlacklist.Add(target.Guid, Stopwatch.StartNew());
            }
            else
            {
                // Prioritize fighting mobs first
                Fight.StartFight(target.Guid);
            }
            return true;
        }
    }
}