using System.Collections.Generic;
using System.Threading;
using AdvancedQuester.NpcBase;
using AdvancedQuester.Quest;
using robotManager.Helpful;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;

namespace AdvancedQuester.Quests
{
    public class StingOfTheScorpid : QuestTask
    {
        public StingOfTheScorpid()
        {
            IsTurnedIn = false;

            Name = "Sting Of The Scorpid";
            QuestId = 789;

            MinimumLevel = 1;
            MaximumLevel = 7;

            PickUpNpc = new QuestGiver
            {
                NpcName = "Gornek",
                NpcId = 3143,
                Position = new Vector3(-600.132f, -4186.19f, 41.08915f)
            };

            TurnInNpc = PickUpNpc;

            QuestObjective questObjective = new QuestObjective
            {
                TargetId = 3124
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-376f, -4332.2f, 52f),
                new Vector3(-452f, -4118f, 51f),
                new Vector3(-346f, -4043f, 51f)
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