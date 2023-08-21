using System.Collections.Generic;
using AdvancedQuester.NpcBase;
using AdvancedQuester.Quest;
using robotManager.Helpful;

namespace AdvancedQuester.Quests
{
    public class GalgarsCactusAppleSurprise : QuestTask
    {
        public GalgarsCactusAppleSurprise()
        {
            IsTurnedIn = false;

            Name = "Galgar's Cactus Apple Surprise";
            QuestId = 4402;

            MinimumLevel = 1;
            MaximumLevel = 7;

            PickUpNpc = new QuestGiver
            {
                NpcName = "Galgar",
                NpcId = 9796,
                Position = new Vector3(-561.6f, -4221.8f, 41.6f)
            };

            TurnInNpc = PickUpNpc;

            QuestObjective questObjective = new QuestObjective
            {
                TargetId = 171938
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