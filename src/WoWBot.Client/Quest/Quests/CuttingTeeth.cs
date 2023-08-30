using System.Collections.Generic;
using robotManager.Helpful;
using wManager.Wow.ObjectManager;
using WoWBot.Client.NpcBase;

namespace WoWBot.Client.Quest.Quests
{
    public class CuttingTeeth : QuestTask
    {
        public CuttingTeeth()
        {
            Name = "Cutting Teeth";
            QuestId = 788;

            MinimumLevel = 1;
            MaximumLevel = 6;

            TurnInNpc = new QuestGiver
            {
                NpcName = "Gornek",
                NpcId = 3143,
                Position = new Vector3(-600.132f, -4186.19f, 41.08915f)
            };

            QuestObjective questObjective = new QuestObjective
            {
                QuestId = QuestId,
                TargetId = 3098,
                Index = 1
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-550.3f, -4337.2f, 43.3f),
                new Vector3(-486.4f, -4381.4f, 48.2f),
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