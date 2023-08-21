using AdvancedQuester.Quests;
using AdvancedQuester.TestQuests;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using robotManager.Helpful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;
using WoWBot.Client.Quest.Quests.Bundles;

namespace AdvancedQuester.Quest
{
    public abstract class QuestBundle
    {
        public string QuestBundleName { get; set; }
        public readonly List<QuestTask> QuestTaskBundle = new List<QuestTask>();
        public readonly List<QuestObjective> QuestObjectives = new List<QuestObjective>();

        public bool IsPickedUp()
        {
            foreach (QuestTask quest in QuestTaskBundle)
            {
                if (!quest.IsPickedUp())
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsTurnedIn()
        {
            foreach (QuestTask quest in QuestTaskBundle)
            {
                if (!quest.IsTurnedIn)
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsReadyToTurnIn()
        {
            foreach (QuestTask quest in QuestTaskBundle)
            {
                if (!quest.IsReadyToTurnIn())
                {
                    return false;
                }
            }
            return true;
        }
        public virtual bool CanDo()
        {
            foreach (QuestTask task in QuestTaskBundle)
            {
                if (!task.CanDo())
                {
                    return false;
                }
            }
            return true;
        }
        public virtual void PickUp()
        {
            foreach (var task in QuestTaskBundle)
            {
                if (!task.IsTurnedIn)
                {
                    task.PickUp();
                }
            }
        }
        public virtual void TurnIn()
        {
            foreach (var task in QuestTaskBundle)
            {
                if (!task.IsTurnedIn)
                {
                    task.TurnIn();
                    task.IsTurnedIn = true;
                }
            }
        }

        public virtual void Pulse()
        {
            Logging.Write("[QuestBundle Pulse] " + QuestBundleName);

            UpdateCompletionCriteria();
            MapDataHelpers.MoveToClosestPosition(QuestObjectives[0].HotSpots);
            MapDataHelpers.RotateThroughArea(QuestObjectives[0].HotSpots);

            bool done;
            while (!(done = IsReadyToTurnIn()))
            {
                UpdateCompletionCriteria();

                lock (Fight.FightLock)
                {
                    var nearbyGameObjects = ObjectManager.GetWoWGameObjectById(QuestObjectives.Select(x => x.TargetId).ToList());
                    var gameObject = ObjectManager.GetNearestWoWGameObject(nearbyGameObjects);

                    var targets = ObjectManager.GetWoWUnitByEntry(QuestObjectives.Select(x => x.TargetId).ToList());
                    var target = ObjectManager.GetNearestWoWUnit(targets);

                    if (gameObject.IsValid && target.IsValid)
                    {
                        Logging.WriteDebug("[QuestBundle Pulse] Detected game object: " + gameObject.Name + " and target: " + target.Name);
                        // If the object is closer to the character than the mob
                        // or the aggro distance (with padding) is still longer than the distance from the mob to the game object
                        if (gameObject.Position.DistanceTo(ObjectManager.Me.Position) < target.Position.DistanceTo(ObjectManager.Me.Position)
                            || target.AggroDistance - 5 > target.Position.DistanceTo(gameObject.Position))
                        {
                            Logging.WriteDebug("[QuestBundle Pulse] Moving to: " + gameObject.Name);
                            MapDataHelpers.MoveToPosition(gameObject.Position);
                            Interact.InteractGameObjectAutoLoot(gameObject.GetBaseAddress);
                            Thread.Sleep(5000);
                        }
                        else
                        {
                            if (target.Position.DistanceTo(ObjectManager.Me.Position) < 30)
                            {
                                // Prioritize fighting mobs first
                                Logging.WriteDebug("[QuestBundle Pulse] Prioritize fighting mobs first: " + target.Name);
                                CombatHelpers.FightTarget(target);
                            }
                            else
                            {
                                MapDataHelpers.MoveToPosition(target.Position, 30);
                            }
                        }
                    }
                    else if (gameObject.IsValid)
                    {
                        Logging.WriteDebug("[QuestBundle Pulse] Moving to: " + gameObject.Name);
                        MapDataHelpers.MoveToPosition(gameObject.Position);
                        Logging.WriteDebug("[QuestBundle Pulse] Interacting with game object: " + gameObject.Name);
                        Interact.InteractGameObjectAutoLoot(gameObject.GetBaseAddress);
                        Thread.Sleep(5000);
                    }
                    else if (target.IsValid)
                    {
                        if (target.Position.DistanceTo(ObjectManager.Me.Position) < 30)
                        {
                            Logging.WriteDebug("[QuestBundle Pulse] Fighting only mobs: " + target.Name);
                            CombatHelpers.FightTarget(target);
                        } else
                        {
                            MapDataHelpers.MoveToPosition(target.Position, 30);
                        }
                    }
                }

                if (Bag.GetContainerNumFreeSlots < 2)
                {
                    CustomProfile.Reevaluate();
                }

                if (MovementManager.CurrentPath.Count == 0)
                {
                    MapDataHelpers.MoveToClosestPosition(QuestObjectives[0].HotSpots);
                    MapDataHelpers.RotateThroughArea(QuestObjectives[0].HotSpots);
                }
            }
            Logging.Write("[QuestBundle Quests Progress] " + QuestBundleName + " Completed: " + done);
        }

        private void UpdateCompletionCriteria()
        {
            if (QuestObjectives.Count > 0)
            {
                var currentFocusedObjective = QuestObjectives[0];
                foreach (QuestTask task in QuestTaskBundle)
                {
                    for (int i = 0; i < task.QuestObjectives.Count; i++)
                    {
                        if (task.IsObjectiveComplete(i + 1) && QuestObjectives.FindAll(x => x.TargetId == task.QuestObjectives[i].TargetId).Count > 0)
                        {
                            Logging.Write("[QuestBundle UpdateCompletionCriteria] Updating completetion progress for quest " + task.QuestId + " target " + task.QuestObjectives[i].TargetId);
                            QuestObjectives.RemoveAll(x => x.TargetId == task.QuestObjectives[i].TargetId);

                            FightBG.StopFight();
                        }
                    }
                }

                if (QuestObjectives.Count > 0 && !currentFocusedObjective.TargetId.Equals(QuestObjectives[0].TargetId))
                {
                    Logging.Write("[QuestBundle Quests Progress] Moving to new hot spot");
                    MapDataHelpers.MoveToClosestPosition(QuestObjectives[0].HotSpots);
                    MapDataHelpers.RotateThroughArea(QuestObjectives[0].HotSpots);
                }
            }
        }
    }
}
