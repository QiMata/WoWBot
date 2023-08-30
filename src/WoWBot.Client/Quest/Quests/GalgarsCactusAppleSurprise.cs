using System.Collections.Generic;
using robotManager.Helpful;
using WoWBot.Client.NpcBase;

namespace WoWBot.Client.Quest.Quests
{
    public class GalgarsCactusAppleSurprise : QuestTask
    {
        public GalgarsCactusAppleSurprise()
        {
            Name = "Galgar's Cactus Apple Surprise";
            QuestId = 4402;

            MinimumLevel = 1;
            MaximumLevel = 7;

            TurnInNpc = new QuestGiver
            {
                NpcName = "Galgar",
                NpcId = 9796,
                Position = new Vector3(-561.6f, -4221.8f, 41.6f)
            };

            QuestObjective questObjective = new QuestObjective
            {
                QuestId = QuestId,
                TargetId = 171938,
                Index = 1
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-376f, -4332.2f, 52f),
                new Vector3(-452f, -4118f, 51f),
                new Vector3(-346f, -4043f, 51f)
            });

            QuestObjectives.Add(questObjective);
        }
    }
}