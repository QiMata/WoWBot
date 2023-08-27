using System.Collections.Generic;
using AdvancedQuester.NpcBase;
using robotManager.Helpful;
using wManager.Wow.Helpers;

namespace WoWBot.Client.Quest.Quests
{
    public class CallOfEarthPart1 : QuestTask
    {
        public CallOfEarthPart1()
        {
            Name = "Call of Earth";
            QuestId = 1516;

            MinimumLevel = 4;
            MaximumLevel = 8;

            PickUpNpc = new QuestGiver
            {
                NpcName = "Canaga Earthcaller",
                NpcId = 5887,
                Position = new Vector3(-630f, -4204.6f, 38.1f)
            };

            TurnInNpc = PickUpNpc;

            QuestObjective questObjective = new QuestObjective
            {
                QuestId = QuestId,
                TargetId = 3102,
                Index = 1
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-154.9f, -4355.2f, 66.3f),
                // Thazz'ril's Pick
                new Vector3(-92.4f, -4279.2f, 65f)
            });

            QuestObjectives.Add(questObjective);

            TurnInPriority = 1;
        }
    }
    public class CallOfEarthPart2 : QuestTask
    {
        public CallOfEarthPart2()
        {
            Name = "Call of Earth";
            QuestId = 1517;

            MinimumLevel = 4;
            MaximumLevel = 8;

            PickUpNpc = new QuestGiver
            {
                NpcName = "Canaga Earthcaller",
                NpcId = 5887,
                Position = new Vector3(-630f, -4204.6f, 38.1f)
            };

            TurnInNpc = new QuestGiver
            {
                NpcName = "Minor Manifestation of Earth",
                NpcId = 5891,
                Position = new Vector3(-877.8f, -4290.4f, 72.6f)
            };

            QuestObjective questObjective = new QuestObjective
            {
                QuestId = QuestId,
                ConsumableItemId = 6635,
                Index = 1
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-879.1f, -4294.2f, 72.7f),
            });

            QuestObjectives.Add(questObjective);
        }
        public override bool IsComplete()
        {
            return !ItemsManager.HasItemById(6635);
        }
    }
    public class CallOfEarthPart3 : QuestTask
    {
        public CallOfEarthPart3()
        {
            Name = "Call of Earth";
            QuestId = 1518;

            MinimumLevel = 4;
            MaximumLevel = 8;

            PickUpNpc = new QuestGiver
            {
                NpcName = "Minor Manifestation of Earth",
                NpcId = 5891,
                Position = new Vector3(-877.8f, -4290.4f, 72.6f)
            };

            TurnInNpc = new QuestGiver
            {
                NpcName = "Canaga Earthcaller",
                NpcId = 5887,
                Position = new Vector3(-630f, -4204.6f, 38.1f)
            };
        }
    }
}