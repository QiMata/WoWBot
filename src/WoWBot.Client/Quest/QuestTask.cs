using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using AdvancedQuester.NpcBase;
using AdvancedQuester.Util;
using robotManager.Helpful;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using Vendor = AdvancedQuester.NpcBase.Vendor;

namespace AdvancedQuester.Quest
{
    public abstract class QuestTask
    {
        public string QuestName { get; set; }
        public int MinimumLevel { get; set; }
        public int MaximumLevel { get; set; }
        public int QuestId { get; set; }
        public QuestGiver PickUpNpc { get; set; }
        public QuestGiver TurnInNpc { get; set; }
        public Vendor AssignedVendor { get; set; }
        public Repair AssignedRepair { get; set; }
        public List<int> Target { get; } = new List<int>();
        public List<Vector3> HotSpots { get; } = new List<Vector3>();
        public List<int> Objective { get; } = new List<int>();
        public List<Vector3> ToArea { get; } = new List<Vector3>();
        public List<Vector3> ToPickUp { get; } = new List<Vector3>();
        public List<Vector3> ToTurnIn { get; } = new List<Vector3>();
        public QuestTask CombineWith { get; set; }

        public bool IsDone()
        {
            for (var objectiveIndex = 1; objectiveIndex <= Objective.Count; ++objectiveIndex)
            {
                if (!wManager.Wow.Helpers.Quest.IsObjectiveComplete(objectiveIndex, QuestId))
                {
                    return false;
                }
            }
            return true;
        }

        public void PickUp()
        {
            Logging.Write("[PickUp] " + QuestName);
            MoveToQuestGiver(PickUpNpc);
            wManager.Wow.Helpers.Quest.AcceptQuest();
        }

        public void TurnIn()
        {
            Logging.Write("[TurnIn] " + QuestName);
            MoveToQuestGiver(TurnInNpc);
            wManager.Wow.Helpers.Quest.SelectGossipActiveQuest(1);
            wManager.Wow.Helpers.Quest.CompleteQuest();
            wManager.Wow.Helpers.Quest.CloseQuestWindow();
        }

        public void Pulse()
        {
            Logging.Write("[Pulse] " + QuestName);
            MoveToArea();
            bool done;
            while (!(done = IsDone()))
                lock (Fight.FightLock)
                {
                    var targets = ObjectManager.GetWoWUnitByEntry(Target);
                    var target = ObjectManager.GetNearestWoWUnit(targets);
                    Logging.WriteDebug("[Attack] " + target.Name + " (lvl " + target.Level + ")");
                    //TODO Add check if is already tagged
                    FightBG.StartFight(target.Guid);
                }
            FightBG.StopFight();
            Logging.WriteDebug("[Quest Progress] " + QuestName + " Completed: " + done);
        }

        private void MoveToQuestGiver(QuestGiver questGiver, int gossip = 1)
        {
            var path = new List<Vector3>();
            if (questGiver.WalkPath.Count > 0)
            {
                path = questGiver.WalkToPath();
            }
            else if (ToArea.Count > 0 && IsDone())
            {
                //TODO Look into angles to determine 'most natural' looking point if not at start position
                //TODO Check if selected point is 'pathable'
                path = ToArea.AsEnumerable().Reverse().ToList();
                var myPosition = ObjectManager.Me.Position;
                var closestPoint = path.MinBy(p => myPosition.DistanceTo(p));
                path.RemoveRange(0, path.IndexOf(closestPoint));

                var pathToClosestPoint = PathFinder.FindPath(ObjectManager.Me.Position, closestPoint);
                pathToClosestPoint.AddRange(path);
                path = pathToClosestPoint;
            }
            else
            {
                path.Add(questGiver.Position);
            }

            if (path.Count > 1)
            {
                MovementManager.Go(path);
                Thread.Sleep(100);
                while (MovementManager.InMovement) ;
            }

            GoToTask.ToPositionAndIntecractWithNpc(questGiver.Position, questGiver.NpcId, gossip);
            Thread.Sleep(1000);
        }

        private void MoveToArea()
        {
            if (ToArea.Count > 0)
            {
                MovementManager.Go(ToArea);
                Thread.Sleep(100);
                while (MovementManager.InMovement) ;
            }
        }
    }
}