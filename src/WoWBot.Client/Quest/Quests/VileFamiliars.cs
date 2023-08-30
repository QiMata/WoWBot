using System.Collections.Generic;
using robotManager.Helpful;
using wManager.Wow.Enums;
using wManager.Wow.ObjectManager;
using WoWBot.Client.NpcBase;

namespace WoWBot.Client.Quest.Quests
{
    public class VileFamiliars : QuestTask
    {
        public VileFamiliars()
        {
            Name = "Vile Familiars";
            QuestId = 792;

            MinimumLevel = 2;
            MaximumLevel = 8;

            TurnInNpc = new QuestGiver
            {
                NpcName = "Zureetha Fargaze",
                NpcId = 3145,
                Position = new Vector3(-629.1f, -4228.1f, 38.2f)
            };

            QuestObjective questObjective = new QuestObjective
            {
                QuestId = QuestId,
                TargetId = 3101,
                Index = 1
            };
            questObjective.HotSpots.AddRange(new List<Vector3>
            {
                new Vector3(-246.7f, -4332.2f, 61.9f),
                new Vector3(-244.7f, -4345f, 63f),
                new Vector3(-224f, -4427f, 63f),
                new Vector3(-154.9f, -4355.2f, 66.3f)
            });

            QuestObjectives.Add(questObjective);
        }

        public override int QuestRewardSelection()
        {
            if (WoWClass.Shaman == ObjectManager.Me.WowClass)
            {
                return 1;
            }
            else if (WoWClass.Rogue == ObjectManager.Me.WowClass)
            {
                return 2;
            }
            else if (WoWClass.Hunter == ObjectManager.Me.WowClass
                    || WoWClass.Warrior == ObjectManager.Me.WowClass)
            {
                return 3;
            }
            return 4;
        }
    }
}