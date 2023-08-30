using robotManager.Helpful;
using wManager.Wow.ObjectManager;
using WoWBot.Client.NpcBase;

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

            TurnInNpc = new QuestGiver { NpcName = "Shikrik", NpcId = 3157, Position = new Vector3(-623.9f, -4203.9f, 38.1f) };
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

            TurnInNpc = new QuestGiver { NpcName = "Shikrik", NpcId = 3157, Position = new Vector3(-623.9f, -4203.9f, 38.1f) };
        }

        public override bool CanDo()
        {
            return ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Shaman
                && ObjectManager.Me.WowRace == wManager.Wow.Enums.WoWRace.Troll;
        }
    }
}