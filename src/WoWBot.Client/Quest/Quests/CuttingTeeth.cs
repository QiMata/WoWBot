using System.Collections.Generic;
using AdvancedQuester.NpcBase;
using AdvancedQuester.Quest;
using robotManager.Helpful;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;

namespace AdvancedQuester.TestQuests
{
    public class CuttingTeeth : QuestTask
    {
        public CuttingTeeth()
        {
            IsTurnedIn = false;

            Name = "Cutting Teeth";
            QuestId = 788;

            MinimumLevel = 1;
            MaximumLevel = 6;

            PickUpNpc = new QuestGiver
            {
                NpcName = "Gornek",
                NpcId = 3143,
                Position = new Vector3(-600.132f, -4186.19f, 41.08915f)
            };

            TurnInNpc = PickUpNpc;

            QuestObjective questObjective = new QuestObjective
            {
                TargetId = 3098
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-513.5054f, -4290.405f, 39.87229f),
                new Vector3(-492.8509f, -4350.517f, 39.30183f),
                new Vector3(-454.5764f, -4246.796f, 49.82771f)
            });

            QuestObjectives.Add(questObjective);
        }

        public override int QuestRewardSelection()
        {
            return ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Rogue
                || ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Warrior
                || ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Shaman
                || ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Hunter
                || ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Druid ? 2 : 1;
        }
    }
}