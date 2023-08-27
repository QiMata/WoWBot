using AdvancedQuester.NpcBase;
using robotManager.Helpful;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.Quest.Quests
{
    public class RuneInscribedParchment : QuestTask
    {
        public RuneInscribedParchment()
        {
            Name = "Rune-Inscribed Parchment";
            QuestId = 3089;

            MinimumLevel = 1;
            MaximumLevel = 5;

            PickUpNpc = new QuestGiver { NpcName = "Gornek", NpcId = 3143, Position = new Vector3(-600.132f, -4186.19f, 41.08913f) };
            TurnInNpc = new QuestGiver { NpcName = "Shikrik", NpcId = 3157, Position = new Vector3(-623.9f, -4203.9f, 38.1f) };

            TurnInPriority = 4;
        }

        public override bool CanDo()
        {
            return ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Shaman
                && ObjectManager.Me.WowRace == wManager.Wow.Enums.WoWRace.Orc;
        }
    }
    public class RuneInscribedTablet : QuestTask
    {
        public RuneInscribedTablet()
        {
            Name = "Rune-Inscribed Tablet";
            QuestId = 3084;

            MinimumLevel = 1;
            MaximumLevel = 5;

            PickUpNpc = new QuestGiver { NpcName = "Gornek", NpcId = 3143, Position = new Vector3(-600.132f, -4186.19f, 41.08913f) };
            TurnInNpc = new QuestGiver { NpcName = "Shikrik", NpcId = 3157, Position = new Vector3(-623.9f, -4203.9f, 38.1f) };
        }

        public override bool CanDo()
        {
            return ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Shaman
                && ObjectManager.Me.WowRace == wManager.Wow.Enums.WoWRace.Troll;
        }
    }
}