using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using robotManager.Helpful;
using WoWBot.Client.Helpers;
using WoWBot.Client.Models;
using WoWBot.Client.NpcBase;

namespace WoWBot.Client.Quest
{
    public class QuestTask
    {
        public int QuestId { get; set; }
        public string Name { get; set; }
        public Item RewardItem1 { get; set; }
        public Item RewardItem2 { get; set; }
        public Item RewardItem3 { get; set; }
        public Item RewardItem4 { get; set; }
        public Item RewardItem5 { get; set; }
        public Item RewardItem6 { get; set; }
        public QuestGiver TurnInNpc { get; set; }
        public List<QuestObjective> QuestObjectives { get; } = new List<QuestObjective>();

        public virtual bool IsComplete()
        {
            foreach (QuestObjective obj in QuestObjectives)
            {
                if (!obj.IsComplete())
                {
                    return false;
                }
            }
            return true;
        }

        public int QuestRewardSelection()
        {
            int rewardChoice = 1;
            CharacterEquip characterEquip = new CharacterEquip();

            if (RewardItem2 != null)
            {
                if (RewardItem2.CanEquip && InventoryHelper.GetGearScoreWithItem(RewardItem2) > characterEquip.GetGearScore())
                {
                    rewardChoice = 2;
                }
                if (RewardItem3 != null)
                {
                    if (RewardItem3.CanEquip && InventoryHelper.GetGearScoreWithItem(RewardItem3) > characterEquip.GetGearScore())
                    {
                        rewardChoice = 3;
                    }
                    if (RewardItem4 != null)
                    {
                        if (RewardItem4.CanEquip && InventoryHelper.GetGearScoreWithItem(RewardItem4) > characterEquip.GetGearScore())
                        {
                            rewardChoice = 4;
                        }
                        if (RewardItem5 != null)
                        {
                            if (RewardItem5.CanEquip && InventoryHelper.GetGearScoreWithItem(RewardItem5) > characterEquip.GetGearScore())
                            {
                                rewardChoice = 5;
                            }
                            if (RewardItem6 != null)
                            {
                                if (RewardItem6.CanEquip && InventoryHelper.GetGearScoreWithItem(RewardItem6) > characterEquip.GetGearScore())
                                {
                                    rewardChoice = 6;
                                }
                            }
                        }
                    }
                }
            }

            return rewardChoice;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("QuestId: " + QuestId + ", Name: " + Name + ", TurnInNpc: " + JsonConvert.SerializeObject(TurnInNpc) + ", QuestObjectives.Count: " + QuestObjectives.Count);
            if (QuestObjectives.Count > 0)
            {
                stringBuilder.Append(", QuestObjectives[0].TargetId: " + QuestObjectives[0].CreatureId + ", QuestObjectives[0].HotSpots.Count: " + QuestObjectives[0].HotSpots.Count);
            }
            return stringBuilder.ToString();
        }
    }
    public class QuestObjective
    {
        public int QuestId { get; set; }
        public int Index { get; set; }
        public int CreatureId { get; set; }
        public int GameObjectId { get; set; }
        public int UsableItemId { get; set; }
        public int ConsumableItemId { get; set; }
        public List<Vector3> HotSpots { get; } = new List<Vector3>();
        public bool IsComplete()
        {
            bool result;
            try
            {
                result = wManager.Wow.Helpers.Quest.IsObjectiveComplete(Index, QuestId);
            }
            catch (Exception)
            {
                result = true;
            }
            return result;
        }
        public override string ToString()
        {
            return "QuestId: " + QuestId + ", Index: " + Index + ", TargetId: " + CreatureId + ", GameObjectId: " + GameObjectId + ", UsableItemId: " + UsableItemId + ", ConsumableItemId: " + ConsumableItemId + ", HotSpots: " + HotSpots.Count;
        }
    }
    public class QuestCompletion
    {
        public string Name { get; set; }
        public int QuestId { get; set; }
    }
}