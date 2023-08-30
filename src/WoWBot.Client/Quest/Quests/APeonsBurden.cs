using robotManager.Helpful;
using wManager.Wow.ObjectManager;
using WoWBot.Client.NpcBase;

namespace WoWBot.Client.Quest.Quests
{
    public class APeonsBurden : QuestTask
    {
        public APeonsBurden()
        {
            Name = "A Peon's Burden";
            QuestId = 2161;

            MinimumLevel = 1;
            MaximumLevel = 10;

            TurnInNpc = new QuestGiver { NpcName = "Innkeeper Grosk <Innkeeper>", NpcId = 6928, Position = new Vector3(340.4f, -4686.3f, 16.5f) };
        }

        public override int QuestRewardSelection()
        {
            return ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Warrior
                || ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Rogue
                || ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Hunter ? 1 : 2;
        }
    }
}