using System.Collections.Generic;
using robotManager.Helpful;
using WoWBot.Client.NpcBase;

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

            TurnInNpc = new QuestGiver
            {
                NpcName = "Foreman Thazz'ril",
                NpcId = 11378,
                Position = new Vector3(-611.6f, -4322.1f, 40f)
            };

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
        }
    }
}