using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AdvancedQuester.NpcBase;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using robotManager.Helpful;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;
using AdvancedQuester.TestQuests;
using AdvancedQuester.Quests;
using System.Reflection;

namespace AdvancedQuester.Quest
{
    public abstract class QuestTask
    {
        public string Name { get; set; }
        public int MinimumLevel { get; set; }
        public int MaximumLevel { get; set; }
        public int QuestId { get; set; }
        public bool IsActive { get; set; }
        public bool IsTurnedIn { get; set; }
        public QuestGiver PickUpNpc { get; set; }
        public QuestGiver TurnInNpc { get; set; }
        public List<QuestObjective> QuestObjectives { get; } = new List<QuestObjective>();

        public virtual bool CanDo()
        {
            return true;
        }
        public bool IsPickedUp()
        {
            return wManager.Wow.Helpers.Quest.HasQuest(QuestId);
        }
        public bool IsObjectiveComplete(int objectiveIndex = 0)
        {
            return wManager.Wow.Helpers.Quest.IsObjectiveComplete(objectiveIndex, QuestId);
        }
        public bool IsReadyToTurnIn()
        {
            for (var objectiveIndex = 1; objectiveIndex <= QuestObjectives.Count; ++objectiveIndex)
            {
                if (!IsObjectiveComplete(objectiveIndex))
                {
                    return false;
                }
            }
            return true;
        }

        public virtual int QuestRewardSelection()
        {
            return 1;
        }
        public virtual void PickUp()
        {
            Logging.Write("[QuestTask PickUp] " + Name);
            MapDataHelpers.GoTalkToNpc(PickUpNpc);
            Thread.Sleep(500);
            wManager.Wow.Helpers.Quest.SelectGossipAvailableQuest(1);
            Thread.Sleep(500);
            wManager.Wow.Helpers.Quest.AcceptQuest();
            Thread.Sleep(500);
        }

        public virtual void TurnIn()
        {
            Logging.Write("[QuestTask TurnIn] " + Name);
            MapDataHelpers.GoTalkToNpc(TurnInNpc);
            wManager.Wow.Helpers.Quest.SelectGossipActiveQuest(1);
            Thread.Sleep(500);
            wManager.Wow.Helpers.Quest.CompleteQuest(QuestRewardSelection());
            Thread.Sleep(500);
            wManager.Wow.Helpers.Quest.CloseQuestWindow();
            Thread.Sleep(500);
        }

        public virtual void Pulse()
        {
            Logging.Write("[QuestTask Pulse] " + Name);
            Logging.Write("[QuestTask Pulse] MoveToArea");
            MapDataHelpers.MoveToClosestPosition(QuestObjectives[0].HotSpots);
            Logging.Write("[QuestTask Pulse] RotateThroughArea");
            MapDataHelpers.RotateThroughArea(QuestObjectives[0].HotSpots);

            bool done;
            while (!(done = IsReadyToTurnIn()))
            {
                var targets = ObjectManager.GetWoWUnitByEntry(QuestObjectives.Select(x => x.TargetId).ToList());
                var target = ObjectManager.GetNearestWoWUnit(targets);

                if (target.IsAttackable)
                {
                    // Prioritize fighting mobs first
                    CombatHelpers.FightTarget(target);
                }
                else
                {
                    if (target.IsValid)
                    {
                        Logging.Write("[QuestTask Pulse] Interacting with game object: " + target.Name);
                        Interact.InteractGameObject(target.GetBaseAddress);
                    }
                }
                if (Bag.GetContainerNumFreeSlots < 2)
                {
                    CustomProfile.Reevaluate();
                }
            }
            Logging.Write("[QuestBundle Quests Progress] " + Name + " Completed: " + done);
        }
    }
    public class QuestObjective
    {
        public int TargetId { get; set; }
        public List<Vector3> HotSpots { get; } = new List<Vector3>();
    }
}