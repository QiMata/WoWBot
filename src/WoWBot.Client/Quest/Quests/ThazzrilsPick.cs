using System.Collections.Generic;
using AdvancedQuester.NpcBase;
using robotManager.Helpful;

namespace WoWBot.Client.Quest.Quests
{
    public class ThazzrilsPick : QuestTask
    {
        public ThazzrilsPick()
        {
            Name = "Thazz'ril's Pick";
            QuestId = 6394;

            MinimumLevel = 3;
            MaximumLevel = 8;

            PickUpNpc = new QuestGiver
            {
                NpcName = "Foreman Thazz'ril",
                NpcId = 11378,
                Position = new Vector3(-611.6f, -4322.1f, 40f)
            };

            TurnInNpc = PickUpNpc;

            QuestObjective questObjective = new QuestObjective
            {
                QuestId = QuestId,
                TargetId = 178087,
                Index = 1
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-92.8f, -4288.4f, 61.6f)
            });

            QuestObjectives.Add(questObjective);

            TurnInPriority = 3;
        }
    }
}