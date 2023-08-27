using System.Collections.Generic;
using AdvancedQuester.NpcBase;
using robotManager.Helpful;

namespace WoWBot.Client.Quest.Quests
{
    public class LazyPeons : QuestTask
    {
        public LazyPeons()
        {
            Name = "Lazy Peons";
            QuestId = 5441;

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
                TargetId = 10556,
                UsableItemId = 16114,
                Index = 1
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-758.4f, -4324.8f, 45.5f),
                new Vector3(-628.5f, -4340.7f, 41.7f),
                new Vector3(-507.4f, -4376.2f, 46.2f),
                new Vector3(-499f, -4457f, 51.1f),
                new Vector3(-334.5f, -4439.4f, 54.6f),
            });

            QuestObjectives.Add(questObjective);

            TurnInPriority = 2;
        }
    }
}