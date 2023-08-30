using robotManager.Helpful;
using WoWBot.Client.NpcBase;

namespace WoWBot.Client.Quest.Quests
{
    public class YourPlaceInTheWorld : QuestTask
    {
        public YourPlaceInTheWorld()
        {
            Name = "Your Place In The World";
            QuestId = 4641;

            MinimumLevel = 1;
            MaximumLevel = 5;

            TurnInNpc = new QuestGiver {NpcName = "Gornek", NpcId = 3143, Position = new Vector3(-600.132f, -4186.19f, 41.08913f)};
        }
    }
}