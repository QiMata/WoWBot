using AdvancedQuester.NpcBase;
using AdvancedQuester.Quest;
using robotManager.Helpful;
using wManager.Wow.ObjectManager;

namespace AdvancedQuester.Quests
{
    public class RuneInscribedParchment : QuestTask
    {
        public RuneInscribedParchment()
        {
            IsTurnedIn = false;

            Name = "Rune-Inscribed Parchment";
            QuestId = 3089;

            MinimumLevel = 1;
            MaximumLevel = 5;

            PickUpNpc = new QuestGiver { NpcName = "Gornek", NpcId = 3143, Position = new Vector3(-600.132f, -4186.19f, 41.08913f) };
            TurnInNpc = new QuestGiver { NpcName = "Shikrik", NpcId = 3157, Position = new Vector3(-623.9f, -4203.9f, 38.1f) };
        }

        public override bool CanDo()
        {
            return ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Shaman;
        }
    }
}