using AdvancedQuester.NpcBase;
using Newtonsoft.Json;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Database;
using WoWBot.Client.Helpers;
using WoWBot.Client.Quest;
using WoWBot.Client.Quest.Quests;

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
        public List<WoWUnit> NearbyQuestGivers
        {
            get
            {
                return ObjectManager.GetObjectWoWUnit()
                                    .FindAll(x => x.NpcMarker.Equals(NpcMarker.YellowExclamation))
                                    .OrderBy(x => x.Position.DistanceTo(ObjectManager.Me.Position))
                                    .ToList();
            }
        }

        public List<WoWUnit> NearbyQuestTurnIns
        {
            get
            {
                return ObjectManager.GetObjectWoWUnit()
                                    .FindAll(x => x.NpcMarker.Equals(NpcMarker.YellowQuestion))
                                    .OrderBy(x => x.Position.DistanceTo(ObjectManager.Me.Position))
                                    .ToList();
            }
        }

        private WoWUnit NearestQuestTarget
        {
            get
            {
                var targets = ObjectManager.GetWoWUnitByEntry(RemainingQuestObjectives.Select(x => x.TargetId).ToList());
                targets.RemoveAll(x => TargetGuidBlacklist.ContainsKey(x.Guid) || x.Position.DistanceTo(ObjectManager.Me.Position) > 50);
                return ObjectManager.GetNearestWoWUnit(targets);
            }
        }

        private WoWGameObject NearestQuestObject
        {
            get
            {
                var nearbyGameObjects = ObjectManager.GetWoWGameObjectById(RemainingQuestObjectives.Select(x => x.TargetId).ToList());
                nearbyGameObjects.RemoveAll(x => x.Position.DistanceTo(ObjectManager.Me.Position) > 50);
                return ObjectManager.GetNearestWoWGameObject(nearbyGameObjects);
            }
        }

        public override void Run()
        {
            try
            {
                Logging.Write("[Questing] Pulse");
                LoadQuestProgress();

                RotateThroughClosestHotSpot();

                Thread.Sleep(500);

                while (MovementManager.InMovement)
                {
                    DefendAgainstAttackingUnits();

                    UseNearbyHotSpotQuestConsumeables();

                    CheckForQuestRelatedEntities();
                }
            }
            catch (Exception ex)
            {
                Logging.WriteError("Questing " + ex.Message);
            }
        }

        private void CheckForQuestRelatedEntities()
        {
            if (NearbyQuestGivers.Count > 0 || NearbyQuestTurnIns.Count > 0 || NearestQuestObject.IsValid || NearestQuestTarget.IsValid)
            {
                MovementManager.StopMove();

                if (NearbyQuestTurnIns.Count > 0 && NearbyQuestTurnIns[0].IsValid)
                {
                    while (NearbyQuestTurnIns.Count > 0)
                    {
                        TurnInAllQuestsForNpc(NearbyQuestTurnIns[0]);
                        Thread.Sleep(100);
                    }
                }
                else if (NearbyQuestGivers.Count > 0 && NearbyQuestGivers[0].IsValid && Quest.GetLogQuestId().Count < 20)
                {
                    while (NearbyQuestGivers.Count > 0 && Quest.GetLogQuestId().Count < 20)
                    {
                        PickUpQuestsFromNpc(NearbyQuestGivers[0]);
                        Thread.Sleep(100);
                    }
                }
                else if (NearestQuestObject.IsValid && NearestQuestTarget.IsValid)
                {

                    // If the object is closer to the character than the mob
                    // or the aggro distance (with padding) is still longer than the distance from the mob to the game object
                    if (NearestQuestObject.Position.DistanceTo(ObjectManager.Me.Position) < NearestQuestTarget.Position.DistanceTo(ObjectManager.Me.Position)
                        || NearestQuestTarget.AggroDistance + 5 > NearestQuestTarget.Position.DistanceTo(NearestQuestObject.Position))
                    {
                        // Go ahead and get the game object
                        HandleInteractingAndLootingGameObject(NearestQuestObject);
                    }
                    else
                    {
                        // Handle the mob since we might aggro it while interacting with the game object
                        HandleAttackableTarget(NearestQuestTarget);
                    }
                }
                else if (NearestQuestObject.IsValid)
                {
                    HandleInteractingAndLootingGameObject(NearestQuestObject);
                }
                else if (NearestQuestTarget.IsValid)
                {
                    HandleAttackableTarget(NearestQuestTarget);
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
            }
        }

        private void PickUpQuestsFromNpc(WoWUnit npc)
        {

            List<int> questsIdsInPreviousLog = Quest.GetLogQuestId()
                                            .Select(x => x.ID)
                                            .ToList();

            MoveToAndInteractWith(npc);

            DialogueHelper.PickupQuestFromNpc();

            List<int> questsIdsInNewLog = Quest.GetLogQuestId()
                                            .Select(x => x.ID)
                                            .ToList();

            foreach (int questId in questsIdsInNewLog)
            {
                if (!questsIdsInPreviousLog.Contains(questId))
                {
                    Logging.WriteDebug("Found new quest " + questId);
                    ToDo.Add(QuestDb.GetQuestTaskById(questId));
                }
            }
        }

        private void TurnInAllQuestsForNpc(WoWUnit npc)
        {
            MoveToAndInteractWith(npc);

            // TODO Find the first quest that is Complete and 
            // that the NPC is the TurnIn for. I can't think
            // of a scenario where this dialogue would pop up
            // for an NPC that has two or more quests on it.
            DialogueHelper.CompleteOpenQuestFrame(1);

            for (int i = 1; i < 4; i++)
            {
                foreach (QuestTask quest in ToDo)
                {
                    if (quest.Name.Equals(DialogueHelper.GetQuestTitleOption(i)) || quest.Name.Equals(DialogueHelper.GetQuestGossipOption(i)))
                    {
                        DialogueHelper.TurnInQuestFromNpc(quest.Name, quest.QuestRewardSelection());
                        MarkComplete(quest);
                        break;
                    }
                };
            }

            List<int> questsIdsInLog = Quest.GetLogQuestId()
                                            .Select(x => x.ID)
                                            .ToList();

            ToDo.RemoveAll(x => !questsIdsInLog.Contains(x.QuestId));
        }

        private void MoveToAndInteractWith(WoWUnit npc)
        {
            MoveDefensivelyToPosition(npc.Position);

            int npcId = int.Parse(npc.Guid.ToString("X").Substring(6, 4), System.Globalization.NumberStyles.HexNumber);

            GoToTask.ToPositionAndIntecractWithNpc(npc.Position, npcId);
        }

        private void MoveDefensivelyToPosition(Vector3 position, float interactDistance = 5)
        {
            while (position.DistanceTo(ObjectManager.Me.Position) > interactDistance)
            {
                GoToTask.ToPosition(position);

                while (MovementManager.InMovement || ObjectManager.Me.InCombat || ObjectManager.Me.IsLooting())
                {
                    Thread.Sleep(100);
                    DefendAgainstAttackingUnits();
                    Thread.Sleep(100);
                    LootAllNearbyLootables();
                }
                Thread.Sleep(500);
            }
        }

        public void MarkComplete(QuestTask questTask)
        {
            Completed.Add(new QuestCompletion() { QuestId = questTask.QuestId, Name = questTask.Name });

            File.WriteAllText(@Directory.GetCurrentDirectory() + "/" + ObjectManager.Me.Name + "Quest.json", JsonConvert.SerializeObject(Completed));
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
        }

        private static void DefendAgainstAttackingUnits()
        {
            List<WoWUnit> attackingUnits = ObjectManager.GetUnitAttackPlayer();

            while (attackingUnits.Count > 0)
            {
                MovementManager.StopMove();

                var nearestEnemy = ObjectManager.GetNearestWoWUnit(attackingUnits);
                Fight.StartFight(ObjectManager.GetNearestWoWUnit(attackingUnits).Guid);

                Thread.Sleep(200);
                while (nearestEnemy.IsAlive)
                {
                    Thread.Sleep(200);
                }
                attackingUnits = ObjectManager.GetUnitAttackPlayer();
                Thread.Sleep(200);
            }
            Fight.StopFight();
        }

        private void UseNearbyHotSpotQuestConsumeables()
        {
            // Use consumables for the quest when in range of its hotspot
            List<QuestObjective> objectivesWithConsumables = RemainingQuestObjectives.FindAll(x => x.ConsumableItemId != 0);

            foreach (QuestObjective objective in objectivesWithConsumables)
            {
                if (objective.HotSpots.Count == 1 && ObjectManager.Me.Position.DistanceTo(objective.HotSpots[0]) < 5 && ItemsManager.HasItemById(Convert.ToUInt32(objective.ConsumableItemId)))
                {
                    ItemsManager.UseItem(ItemsManager.GetNameById(objective.ConsumableItemId));
                    Thread.Sleep(1500);
                }
            }
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
                        CurrentHotspot = ClosestObjective.HotSpots[0];
                    }
                    else
                    {
                        CurrentHotspot = ClosestObjective.HotSpots[index + 1];
                    }
                }

                Task.Run(() => GoToTask.ToPosition(CurrentHotspot));
            }
            else if (ToDo.FindAll(x => x.IsComplete()).Count > 0)
            {
                Task.Run(() => GoToTask.ToPosition(ToDo.FindAll(x => x.IsComplete())
                    .OrderBy(x => x.TurnInNpc.Position.DistanceTo(ObjectManager.Me.Position))
                    .ToArray()[0].TurnInNpc.Position));
            }
            else
            {
                CheckForQuestRelatedEntities();
            }
        }

        private void HandleInteractingAndLootingGameObject(WoWGameObject gameObject)
        {
            Logging.WriteDebug("[QuestBundle] HandleInteractingAndLootingGameObject");

            MoveDefensivelyToPosition(gameObject.Position, gameObject.InteractDistance);

            Interact.InteractGameObjectAutoLoot(gameObject.GetBaseAddress);
            Usefuls.WaitIsCastingAndLooting();

            // Give time to despawn
            Thread.Sleep(500);
        }

        private void HandleAttackableTarget(WoWUnit target)
        {
            Thread.Sleep(100);
            if (ClosestObjective.UsableItemId != 0)
            {
                Logging.WriteDebug("[QuestBundle] HandleAttackableTarget Interact with item");
                ObjectManager.Me.Target = target.Guid;

                GoToTask.ToPosition(target.Position, target.InteractDistance);

                Thread.Sleep(100);
                ItemsManager.UseItem(ItemsManager.GetNameById(ClosestObjective.UsableItemId));
                TargetGuidBlacklist.Add(target.Guid, Stopwatch.StartNew());
                Thread.Sleep(200);
            }
            else
            {
                Logging.WriteDebug("[QuestBundle] HandleAttackableTarget Attack");
                // Prioritize fighting mobs first
                Fight.StartFight(target.Guid);
                Thread.Sleep(200);
            }
        }
    }
}