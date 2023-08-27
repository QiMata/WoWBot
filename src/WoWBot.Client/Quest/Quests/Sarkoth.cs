﻿using System.Collections.Generic;
using AdvancedQuester.NpcBase;
using robotManager.Helpful;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.Quest.Quests
{
    public class SarkothPart1 : QuestTask
    {
        public SarkothPart1()
        {
            Name = "Sarkoth";
            QuestId = 790;

            MinimumLevel = 1;
            MaximumLevel = 10;

            PickUpNpc = new QuestGiver
            {
                NpcName = "Hana'zua",
                NpcId = 3287,
                Position = new Vector3(-397.8f, -4109f, 50.2f)
            };

            TurnInNpc = PickUpNpc;

            QuestObjective questObjective = new QuestObjective
            {
                QuestId = QuestId,
                TargetId = 3281,
                Index = 1
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-547.3f, -4103.9f, 69.9f),
            });

            QuestObjectives.Add(questObjective);

            TurnInPriority = 1;
        }
    }
    public class SarkothPart2 : QuestTask
    {
        public SarkothPart2()
        {
            Name = "Sarkoth";
            QuestId = 804;

            MinimumLevel = 1;
            MaximumLevel = 10;

            PickUpNpc = new QuestGiver
            {
                NpcName = "Hana'zua",
                NpcId = 3287,
                Position = new Vector3(-397.8f, -4109f, 50.2f)
            };

            TurnInNpc = new QuestGiver
            {
                NpcName = "Gornek",
                NpcId = 3143,
                Position = new Vector3(-600.132f, -4186.19f, 41.08915f)
            };

            TurnInPriority = 2;
        }

        public override int QuestRewardSelection()
        {
            return ObjectManager.Me.WowClass == wManager.Wow.Enums.WoWClass.Warrior ? 2 : 1;
        }
    }
}