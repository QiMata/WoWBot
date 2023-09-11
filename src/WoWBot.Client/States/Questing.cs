using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Database;
using WoWBot.Client.Helpers;
using WoWBot.Client.Quest;

namespace WoWBot.Client.States
{
    public class Questing : State
    {
        public override bool NeedToRun => true;

        private Vector3 CurrentHotspot = new Vector3();
        private readonly Dictionary<ulong, Stopwatch> TargetGuidBlacklist = new Dictionary<ulong, Stopwatch>();

        public static readonly ObservableCollection<QuestTask> ToDo = new ObservableCollection<QuestTask>();

        public Questing()
        {
            DisplayName = "Questing";
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
                return ToDo.Where(x => !x.IsComplete())
                           .SelectMany(x => x.QuestObjectives)
                               .ToList()
                               .FindAll(x => !x.IsComplete());
            }
        }
        public QuestObjective GetQuestObjectiveByTarget(int target)
        {
            return RemainingQuestObjectives.Find(x => x.CreatureId == target);
        }
        public List<WoWUnit> NearbyQuestGivers
        {
            get
            {
                return ObjectManager.GetObjectWoWUnit()
                                .Where(x => x.NpcMarker.Equals(NpcMarker.YellowExclamation))
                                .OrderBy(x => MovementHelper.FindPathingDistance(x.Position, ObjectManager.Me.Position))
                                .ToList();
            }
        }

        public List<WoWUnit> NearbyQuestTurnIns
        {
            get
            {
                return ObjectManager.GetObjectWoWUnit()
                                .Where(x => x.NpcMarker.Equals(NpcMarker.YellowQuestion))
                                .OrderBy(x => MovementHelper.FindPathingDistance(x.Position, ObjectManager.Me.Position))
                                .ToList();
            }
        }

        private WoWUnit NearestQuestTarget
        {
            get
            {
                var targets = ObjectManager.GetWoWUnitByEntry(RemainingQuestObjectives.Select(x => x.CreatureId).ToList());
                targets.RemoveAll(x => TargetGuidBlacklist.ContainsKey(x.Guid) || x.Position.DistanceTo(ObjectManager.Me.Position) > 50);

                return targets.OrderBy(x => MovementHelper.FindPathingDistance(x.Position, ObjectManager.Me.Position)).FirstOrDefault();
            }
        }

        private WoWGameObject NearestQuestObject
        {
            get
            {
                var nearbyGameObjects = ObjectManager.GetWoWGameObjectById(RemainingQuestObjectives.Select(x => x.GameObjectId).ToList());
                nearbyGameObjects.RemoveAll(x => x.Position.DistanceTo(ObjectManager.Me.Position) > 50);

                return nearbyGameObjects.OrderBy(x => MovementHelper.FindPathingDistance(x.Position, ObjectManager.Me.Position)).FirstOrDefault();
            }
        }

        public override void Run()
        {
            InventoryHelper.UpgradeToLatestEquipment();
            if (Fight.InFight || ObjectManager.Me.InCombatFlagOnly)
            {
                return;
            }

            try
            {
                MovementHelper.DefendAgainstAttackingUnits();
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
                Logging.WriteError($"[Questing] Exception: {ex}");
                Logging.WriteError("[Questing] " + ex.StackTrace);
            }
        }

        private bool CheckForQuestRelatedEntities()
        {
            try
            {
                WoWGameObject questObject = NearestQuestObject;
                WoWUnit questTarget = NearestQuestTarget;
                List<WoWUnit> questGivers = NearbyQuestGivers;
                List<WoWUnit> questTurnIns = NearbyQuestTurnIns;

                if (questGivers.Count > 0 || questTurnIns.Count > 0 || (questObject != null && questObject.IsValid) || (questTarget != null && questTarget.IsValid))
                {
                    List<WoWUnit> nearbyMobs = ObjectManager.GetObjectWoWUnit()
                                    .FindAll(x => x.IsAlive && x.IsAttackable && x.Reaction == Reaction.Hostile)
                                    .OrderBy(x => MovementHelper.FindPathingDistance(x.Position, ObjectManager.Me.Position))
                                    .ToList();

                    MovementManager.StopMove();

                    if (questTurnIns.Count > 0 && questTurnIns[0].IsValid)
                    {
                        while (questTurnIns.Count > 0 && questTurnIns[0].IsValid)
                        {
                            TurnInQuestForNpc(questTurnIns[0]);

                            questTurnIns = NearbyQuestTurnIns;
                        }
                    }
                    else if (questGivers.Count > 0 && questGivers[0].IsValid && wManager.Wow.Helpers.Quest.GetLogQuestId().Count < 20)
                    {
                        //if (questGivers.Count > 0 && FindNearbyInnkeeper())
                        //{
                        //    AssignHomePointToNearestInn();
                        //}
                        while (questGivers.Count > 0 && questGivers[0].IsValid)
                        {
                            PickUpQuestsFromNpc(questGivers[0]);

                            questGivers = NearbyQuestGivers;
                        }
                    }
                    else if (questObject != null && questObject.IsValid)
                    {
                        // If the object is closer to the character than the mob
                        // or the aggro distance (with padding) is still longer than the distance from the mob to the game object
                        if (nearbyMobs.Count > 0 && nearbyMobs[0].IsValid)
                        {
                            if (MovementHelper.FindPathingDistance(questObject.Position, ObjectManager.Me.Position) < MovementHelper.FindPathingDistance(nearbyMobs[0].Position, ObjectManager.Me.Position)
                                || nearbyMobs[0].AggroDistance + 5 > MovementHelper.FindPathingDistance(nearbyMobs[0].Position, questObject.Position))
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
                        else
                        {
                            HandleInteractingAndLootingGameObject(questObject);
                        }
                    }
                    else if (questTarget != null && questTarget.IsValid)
                    {
                        if (nearbyMobs.Count > 0 && nearbyMobs[0].IsValid)
                        {
                            if (MovementHelper.FindPathingDistance(questTarget.Position, ObjectManager.Me.Position) < MovementHelper.FindPathingDistance(nearbyMobs[0].Position, ObjectManager.Me.Position)
                                || nearbyMobs[0].AggroDistance + 5 > MovementHelper.FindPathingDistance(nearbyMobs[0].Position, questTarget.Position))
                            {
                                HandleAttackableTarget(questTarget);
                            }
                            else
                            {
                                // Handle the mob since we might aggro it while interacting with the game object
                                HandleAttackableTarget(nearbyMobs[0]);
                            }
                        }
                        else
                        {
                            HandleAttackableTarget(questTarget);
                        }
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
            }
            catch (Exception ex)
            {
                Logging.WriteError(ex.Message);
            }
            return false;
        }

        private void AssignHomePointToNearestInn()
        {
            throw new NotImplementedException();
        }

        private bool FindNearbyInnkeeper()
        {
            Logging.WriteDebug(Usefuls.MapZoneName);
            Logging.WriteDebug(Usefuls.SubMapZoneName);

            Logging.WriteDebug(Lua.LuaDoString<string>("bindlocation = GetBindLocation(); return bindlocation;"));

            return false;
        }

        private void PickUpQuestsFromNpc(WoWUnit npc)
        {
            List<int> questsIdsInPreviousLog = wManager.Wow.Helpers.Quest.GetLogQuestId()
                                            .Select(x => x.ID)
                                            .ToList();

            MovementHelper.MoveToAndInteractWith(npc);

            DialogueHelper.PickupQuestFromNpc();

            UpdateToDoWithLogQuests();
        }

        private static void UpdateToDoWithLogQuests()
        {
            List<int> questsIdsLog = wManager.Wow.Helpers.Quest.GetLogQuestId()
                                                .Select(x => x.ID)
                                                .ToList();

            ToDo.Clear();

            if (questsIdsLog.Count > 0)
            {
                foreach (var id in questsIdsLog)
                {
                    if (ToDo.All(x => x.QuestId != id))
                    {
                        ToDo.Add(QuestDb.GetQuestTaskById(id));
                        Logging.WriteDebug("Adding quest to ToDo list: " + id);
                        //Dispatcher.FromThread(CustomProfile.Thread)
                        //    ?.Invoke(() =>
                        //    {
                        //    });
                    }
                }
            }
        }

        private void TurnInQuestForNpc(WoWUnit npc)
        {
            MovementHelper.MoveToAndInteractWith(npc);

            List<int> questsIdsInPreviousLog = wManager.Wow.Helpers.Quest.GetLogQuestId()
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
                QuestTask potentialQuest = ToDo.FirstOrDefault(x => x.TurnInNpc.NpcId.Equals(UnitHelper.GetNpcIdFromGuid(npc.Guid)));

                if (potentialQuest != null)
                {
                    DialogueHelper.TurnInQuestFromNpc(potentialQuest.Name, potentialQuest.QuestRewardSelection());
                }
            }

            Thread.Sleep(500);

            List<int> questsIdsInLog = wManager.Wow.Helpers.Quest.GetLogQuestId()
                                            .Select(x => x.ID)
                                            .ToList();

            foreach (int questId in questsIdsInPreviousLog)
            {
                if (!questsIdsInLog.Contains(questId) && questsIdsInPreviousLog.Contains(questId))
                {
                    QuestTask completedQuest = ToDo.FirstOrDefault(x => x.QuestId == questId);

                    if (completedQuest != null)
                    {
                        Dispatcher.FromThread(CustomProfile.Thread)
                            ?.Invoke(() =>
                            {
                                ToDo.Remove(completedQuest);
                            });

                        break;
                    }
                }
            }

            InventoryHelper.UpgradeToLatestEquipment();
        }
        private void RotateThroughArea(Vector3 position)
        {
            List<Vector3> path = PathFinder.FindPath(position);

            if (path.Count > 0)
            {
                MovementManager.Go(path);

                while (MovementManager.InMovement)
                {
                    MovementHelper.DefendAgainstAttackingUnits();

                    if (CheckForQuestRelatedEntities()
                        || UseNearbyHotSpotQuestConsumeables())
                    {
                        LootAllNearbyLootables();
                        break;
                    }
                }
            }
        }

        private bool UseNearbyHotSpotQuestConsumeables()
        {
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

        private static bool LootAllNearbyLootables()
        {
            bool hasLooted = false;
            // Loot all lootables around you
            List<WoWUnit> list = ObjectManager.GetWoWUnitLootable();

            if (list.Count > 0)
            {
                MovementManager.StopMove();

                while (list.Count > 0 && Bag.GetContainerNumFreeSlots > 0)
                {
                    hasLooted = true;
                    LootingTask.Pulse(list);
                    list = ObjectManager.GetWoWUnitLootable();
                    Usefuls.WaitIsLooting();
                }
            }
            Bag.EquipAvailableBagIfFreeContainerSlot();
            return hasLooted;
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
                    if (ClosestObjective.HotSpots.Count > 1)
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
                    else
                    {
                        CurrentHotspot = RemainingQuestObjectives.FindAll(x => x != ClosestObjective)
                                                        .SelectMany(x => x.HotSpots)
                                                        .OrderBy(spot => spot.DistanceTo(ObjectManager.Me.Position))
                                                        .ToList()[0];
                    }
                }
                Logging.WriteDebug("Quest " + ClosestObjective.QuestId + " " + ClosestObjective.Index);

                RotateThroughArea(CurrentHotspot);
            }
            else if (ToDo.Any(x => x.IsComplete()))
            {
                Logging.WriteDebug("Quest " + ClosestObjective.QuestId + " TurnIn");

                string s = ItemsManager.GetItemSpell(6948);
                bool isUsable = false;

                if (ItemsManager.HasItemById(6948))
                {
                    ItemsManager.UseItem(6948);

                    Logging.WriteDebug("Using Hearthstone");
                    Thread.Sleep(Usefuls.Latency + 10000);

                    RotateThroughArea(ToDo.Where(x => x.IsComplete())
                        .OrderBy(x => MovementHelper.FindPathingDistance(x.TurnInNpc.Position, ObjectManager.Me.Position))
                        .ToArray()[0].TurnInNpc.Position);
                }
            }
            else
            {
                Logging.WriteDebug("Looking for nearby quest entities...");
                CheckForQuestRelatedEntities();
            }
        }

        private bool HandleInteractingAndLootingGameObject(WoWGameObject gameObject)
        {
            MovementHelper.MoveDefensivelyToPosition(gameObject.Position, gameObject.InteractDistance);

            Interact.InteractGameObjectAutoLoot(gameObject.GetBaseAddress);
            Usefuls.WaitIsCastingAndLooting();

            // Give time to despawn
            Thread.Sleep(500);
            return true;
        }

        private void HandleAttackableTarget(WoWUnit target)
        {
            QuestObjective objective = GetQuestObjectiveByTarget(target.Entry);
            ObjectManager.Me.Target = target.Guid;

            while (ObjectManager.Me.IsSwimming != target.IsSwimming || !TraceLine.TraceLineGo(ObjectManager.Me.Position, target.Position, CGWorldFrameHitFlags.HitTestSpellLoS))
            {
                GoToTask.ToPosition(target.Position);
            }

            if (objective != null && objective.UsableItemId != 0 && objective.CreatureId == target.Entry)
            {
                ItemsManager.UseItem(ItemsManager.GetNameById(objective.UsableItemId));
                TargetGuidBlacklist.Add(target.Guid, Stopwatch.StartNew());
            }
            else
            {
                Logging.WriteDebug("Starting attack on quest mob: " + target.Name);
                Fight.StartFight(target.Guid);
            }
        }
    }
}