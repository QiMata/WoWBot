using System.Collections.Generic;
using AdvancedQuester.NpcBase;
using AdvancedQuester.Quest;
using robotManager.Helpful;

namespace AdvancedQuester.TestQuests
{
    public class YourPlaceInTheWorld : QuestTask
    {
        public YourPlaceInTheWorld()
        {
            QuestName = "Your Place In The World";
            QuestId = 4641;
            PickUpNpc = new QuestGiver {NpcName = "Kaltunk", NpcId = 10176, Position = new Vector3(-610.073f, -4253.52f, 38.95626f)};
            TurnInNpc = new QuestGiver {NpcName = "Gornek", NpcId = 3143, Position = new Vector3(-600.132f, -4186.19f, 41.08913f)};
        }
    }
}