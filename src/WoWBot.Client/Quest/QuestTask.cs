using System.Collections.Generic;
using robotManager.Helpful;
using wManager.Wow.ObjectManager;
using WoWBot.Client.NpcBase;

namespace WoWBot.Client.Quest
{
    public abstract class QuestTask
    {
        public int QuestId { get; set; }
        public string Name { get; set; }
        public int MinimumLevel { get; set; }
        public int MaximumLevel { get; set; }
        public QuestGiver TurnInNpc { get; set; }
        public List<QuestObjective> QuestObjectives { get; } = new List<QuestObjective>();

        public virtual bool CanDo()
        {
            return ObjectManager.Me.Level >= MinimumLevel && ObjectManager.Me.Level <= MaximumLevel;
        }
        public virtual bool IsComplete()
        {
            foreach (QuestObjective obj in QuestObjectives)
            {
                if (!obj.IsComplete())
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
    }
    public class QuestObjective
    {
        public int QuestId { get; set; }
        public int Index { get; set; }
        public int TargetId { get; set; }
        public int UsableItemId { get; set; }
        public int ConsumableItemId { get; set; }
        public List<Vector3> HotSpots { get; } = new List<Vector3>();
        public bool IsComplete()
        {
            return wManager.Wow.Helpers.Quest.IsObjectiveComplete(Index, QuestId);
        }
    }
    public class QuestCompletion
    {
        public string Name { get; set; }
        public int QuestId { get; set; }
    }
}