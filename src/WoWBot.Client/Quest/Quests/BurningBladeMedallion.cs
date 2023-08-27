using System.Collections.Generic;
using AdvancedQuester.NpcBase;
using robotManager.Helpful;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.Quest.Quests
{
    public class BurningBladeMedallion : QuestTask
    {
        public BurningBladeMedallion()
        {
            Name = "Burning Blade Medallion";
            QuestId = 794;

            MinimumLevel = 2;
            MaximumLevel = 8;

            PickUpNpc = new QuestGiver
            {
                NpcName = "Zureetha Fargaze",
                NpcId = 3145,
                Position = new Vector3(-629.1f, -4228.1f, 38.2f)
            };

            TurnInNpc = PickUpNpc;

            QuestObjective questObjective = new QuestObjective
            {
                QuestId = QuestId,
                TargetId = 3183,
                Index = 1
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                // Yarrog Baneshadow
                new Vector3(-58.2f, -4220.6f, 62.3f)
            });

            QuestObjectives.Add(questObjective);

            TurnInPriority = 2;
        }

        public override int QuestRewardSelection()
        {
            switch (ObjectManager.Me.WowClass)
            {
                case wManager.Wow.Enums.WoWClass.Warrior:
                    return 2;
                case wManager.Wow.Enums.WoWClass.Druid:
                case wManager.Wow.Enums.WoWClass.Mage:
                case wManager.Wow.Enums.WoWClass.Priest:
                case wManager.Wow.Enums.WoWClass.Warlock:
                    return 3;
            }
            return 1;
        }
    }
}