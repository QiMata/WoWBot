using System.Collections.Generic;
using System.Threading;
using AdvancedQuester.NpcBase;
using AdvancedQuester.Quest;
using robotManager.Helpful;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;

namespace AdvancedQuester.Quests
{
    public class VileFamiliars : QuestTask
    {
        public VileFamiliars()
        {
            IsTurnedIn = false;

            Name = "Vile Familiars";
            QuestId = 792;

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
                TargetId = 3101
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-246.7f, -4332.2f, 61.9f),
                new Vector3(-244.7f, -4345f, 63f),
                new Vector3(-224f, -4427f, 63f)
            });

            QuestObjectives.Add(questObjective);
        }

        public override int QuestRewardSelection()
        {
            switch (ObjectManager.Me.WowClass)
            {
                case wManager.Wow.Enums.WoWClass.Rogue:
                    return 2;
                case wManager.Wow.Enums.WoWClass.Hunter:
                case wManager.Wow.Enums.WoWClass.Warrior:
                    return 3;
                case wManager.Wow.Enums.WoWClass.Druid:
                case wManager.Wow.Enums.WoWClass.Mage:
                case wManager.Wow.Enums.WoWClass.Priest:
                case wManager.Wow.Enums.WoWClass.Warlock:
                    return 4;
            }
            return 1;
        }
    }
}