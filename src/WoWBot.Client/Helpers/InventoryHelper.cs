using robotManager.Helpful;
using System;
using System.Collections.Generic;
using System.Threading;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Database;
using WoWBot.Client.Models;

namespace WoWBot.Client.Helpers
{
    public class InventoryHelper
    {
        private static readonly float ItemLevelWeight = 1.0f;

        private static readonly float ArmorWeight = 1.0f;
        private static readonly float BlockWeight = 1.0f;

        private static readonly float RangeModWeight = 1.0f;

        private static readonly float DmgMin1Weight = 1.0f;
        private static readonly float DmgMin2Weight = 1.0f;
        private static readonly float DmgMin3Weight = 1.0f;
        private static readonly float DmgMin4Weight = 1.0f;
        private static readonly float DmgMin5Weight = 1.0f;
        private static readonly float DmgMax1Weight = 1.0f;
        private static readonly float DmgMax2Weight = 1.0f;
        private static readonly float DmgMax3Weight = 1.0f;
        private static readonly float DmgMax4Weight = 1.0f;
        private static readonly float DmgMax5Weight = 1.0f;

        private static readonly float AgilityWeight = 1.0f;
        private static readonly float StrengthWeight = 1.0f;
        private static readonly float IntellectWeight = 1.0f;
        private static readonly float SpiritWeight = 1.0f;
        private static readonly float StaminaWeight = 1.0f;

        public static void UpgradeToLatestEquipment()
        {
            List<WoWItem> woWItems = Bag.GetBagItem();

            foreach (WoWItem wowItem in woWItems)
            {
                Item item = ItemDatabase.GetItemById((int)ItemsManager.GetIdByName(wowItem.Name));

                if (item.RequiredLevel <= ObjectManager.Me.Level)
                {
                    switch (item.InventoryType)
                    {
                        case (int)InventoryType.Head:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_HEAD);
                            break;
                        case (int)InventoryType.Neck:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_NECK);
                            break;
                        case (int)InventoryType.Shoulders:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_SHOULDER);
                            break;
                        case (int)InventoryType.BackBoE:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_BACK);
                            break;
                        case (int)InventoryType.Chest:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_CHEST);
                            break;
                        case (int)InventoryType.Wrists:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_WRIST);
                            break;
                        case (int)InventoryType.Hands:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_HAND);
                            break;
                        case (int)InventoryType.Waist:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_WAIST);
                            break;
                        case (int)InventoryType.Legs:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_LEGS);
                            break;
                        case (int)InventoryType.Feet:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_FEET);
                            break;
                        case (int)InventoryType.Finger:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_FINGER1);
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_FINGER2);
                            break;
                        case (int)InventoryType.Trinket:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_TRINKET1);
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_TRINKET2);
                            break;
                        case (int)InventoryType.Relic:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_AMMO);
                            break;
                        case (int)InventoryType.OffhandBoE:
                            EvaluateArmorPieces(wowItem, item, wManager.Wow.Enums.InventorySlot.INVSLOT_OFFHAND);
                            break;
                        default:
                            if (item.InventoryType != (int) InventoryType.Stackable)
                            {
                                EvaluateWeapons(wowItem, item);
                            }
                            break;
                    }
                }
            }
        }

        private static void EvaluateWeapons(WoWItem wowItem, Item item)
        {
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_MAINHAND) == 0)
            {
                ItemsManager.UseItem(wowItem.Name);
            }
            else
            {
                CompareWeapons(ItemDatabase.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_MAINHAND)), item);
            }
        }

        private static void EvaluateArmorPieces(WoWItem wowItem, Item item, wManager.Wow.Enums.InventorySlot inventorySlot)
        {
            if (ObjectManager.Me.GetEquipedItemBySlot(inventorySlot) == 0)
            {
                ItemsManager.UseItem(wowItem.Name);
            }
            else
            {
                CompareArmors(ItemDatabase.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(inventorySlot)), item);
            }
        }

        private static void CompareWeapons(Item item1, Item item2)
        {
            float item1WeightedAverage = item1.ItemLevel * ItemLevelWeight;

            item1WeightedAverage += item1.Armor * ArmorWeight;
            item1WeightedAverage += item1.Block * BlockWeight;

            item1WeightedAverage += item1.RangeMod * RangeModWeight;
            item1WeightedAverage += item1.DmgMin1 * DmgMin1Weight;
            item1WeightedAverage += item1.DmgMin2 * DmgMin2Weight;
            item1WeightedAverage += item1.DmgMin3 * DmgMin3Weight;
            item1WeightedAverage += item1.DmgMin4 * DmgMin4Weight;
            item1WeightedAverage += item1.DmgMin5 * DmgMin5Weight;

            item1WeightedAverage += item1.DmgMax1 * DmgMax1Weight;
            item1WeightedAverage += item1.DmgMax2 * DmgMax2Weight;
            item1WeightedAverage += item1.DmgMax3 * DmgMax3Weight;
            item1WeightedAverage += item1.DmgMax4 * DmgMax4Weight;
            item1WeightedAverage += item1.DmgMax5 * DmgMax5Weight;

            item1WeightedAverage += item1.StatValue1 * GetStatWeightByType(item1.StatType1);
            item1WeightedAverage += item1.StatValue2 * GetStatWeightByType(item1.StatType2);
            item1WeightedAverage += item1.StatValue3 * GetStatWeightByType(item1.StatType3);
            item1WeightedAverage += item1.StatValue4 * GetStatWeightByType(item1.StatType4);
            item1WeightedAverage += item1.StatValue5 * GetStatWeightByType(item1.StatType5);
            item1WeightedAverage += item1.StatValue6 * GetStatWeightByType(item1.StatType6);
            item1WeightedAverage += item1.StatValue7 * GetStatWeightByType(item1.StatType7);
            item1WeightedAverage += item1.StatValue8 * GetStatWeightByType(item1.StatType8);
            item1WeightedAverage += item1.StatValue9 * GetStatWeightByType(item1.StatType9);
            item1WeightedAverage += item1.StatValue10 * GetStatWeightByType(item1.StatType10);

            float item2WeightedAverage = item2.ItemLevel * ItemLevelWeight;

            item2WeightedAverage += item2.Armor * ArmorWeight;
            item2WeightedAverage += item2.Block * BlockWeight;

            item2WeightedAverage += item2.RangeMod * RangeModWeight;
            item2WeightedAverage += item2.DmgMin1 * DmgMin1Weight;
            item2WeightedAverage += item2.DmgMin2 * DmgMin2Weight;
            item2WeightedAverage += item2.DmgMin3 * DmgMin3Weight;
            item2WeightedAverage += item2.DmgMin4 * DmgMin4Weight;
            item2WeightedAverage += item2.DmgMin5 * DmgMin5Weight;

            item2WeightedAverage += item2.DmgMax1 * DmgMax1Weight;
            item2WeightedAverage += item2.DmgMax2 * DmgMax2Weight;
            item2WeightedAverage += item2.DmgMax3 * DmgMax3Weight;
            item2WeightedAverage += item2.DmgMax4 * DmgMax4Weight;
            item2WeightedAverage += item2.DmgMax5 * DmgMax5Weight;

            item2WeightedAverage += item2.StatValue1 * GetStatWeightByType(item2.StatType1);
            item2WeightedAverage += item2.StatValue2 * GetStatWeightByType(item2.StatType2);
            item2WeightedAverage += item2.StatValue3 * GetStatWeightByType(item2.StatType3);
            item2WeightedAverage += item2.StatValue4 * GetStatWeightByType(item2.StatType4);
            item2WeightedAverage += item2.StatValue5 * GetStatWeightByType(item2.StatType5);
            item2WeightedAverage += item2.StatValue6 * GetStatWeightByType(item2.StatType6);
            item2WeightedAverage += item2.StatValue7 * GetStatWeightByType(item2.StatType7);
            item2WeightedAverage += item2.StatValue8 * GetStatWeightByType(item2.StatType8);
            item2WeightedAverage += item2.StatValue9 * GetStatWeightByType(item2.StatType9);
            item2WeightedAverage += item2.StatValue10 * GetStatWeightByType(item2.StatType10); 
            
            if (item2WeightedAverage > item1WeightedAverage)
            {
                ItemsManager.UseItem(item2.Name);
            }
        }

        private static void CompareArmors(Item item1, Item item2)
        {
            float item1WeightedAverage = item1.ItemLevel * ItemLevelWeight;

            item1WeightedAverage += item1.Armor * ArmorWeight;
            item1WeightedAverage += item1.Block * BlockWeight;

            item1WeightedAverage += item1.StatValue1 * GetStatWeightByType(item1.StatType1);
            item1WeightedAverage += item1.StatValue2 * GetStatWeightByType(item1.StatType2);
            item1WeightedAverage += item1.StatValue3 * GetStatWeightByType(item1.StatType3);
            item1WeightedAverage += item1.StatValue4 * GetStatWeightByType(item1.StatType4);
            item1WeightedAverage += item1.StatValue5 * GetStatWeightByType(item1.StatType5);
            item1WeightedAverage += item1.StatValue6 * GetStatWeightByType(item1.StatType6);
            item1WeightedAverage += item1.StatValue7 * GetStatWeightByType(item1.StatType7);
            item1WeightedAverage += item1.StatValue8 * GetStatWeightByType(item1.StatType8);
            item1WeightedAverage += item1.StatValue9 * GetStatWeightByType(item1.StatType9);
            item1WeightedAverage += item1.StatValue10 * GetStatWeightByType(item1.StatType10);

            float item2WeightedAverage = item2.ItemLevel * ItemLevelWeight;

            item2WeightedAverage += item2.Armor * ArmorWeight;
            item2WeightedAverage += item2.Block * BlockWeight;

            item2WeightedAverage += item2.StatValue1 * GetStatWeightByType(item2.StatType1);
            item2WeightedAverage += item2.StatValue2 * GetStatWeightByType(item2.StatType2);
            item2WeightedAverage += item2.StatValue3 * GetStatWeightByType(item2.StatType3);
            item2WeightedAverage += item2.StatValue4 * GetStatWeightByType(item2.StatType4);
            item2WeightedAverage += item2.StatValue5 * GetStatWeightByType(item2.StatType5);
            item2WeightedAverage += item2.StatValue6 * GetStatWeightByType(item2.StatType6);
            item2WeightedAverage += item2.StatValue7 * GetStatWeightByType(item2.StatType7);
            item2WeightedAverage += item2.StatValue8 * GetStatWeightByType(item2.StatType8);
            item2WeightedAverage += item2.StatValue9 * GetStatWeightByType(item2.StatType9);
            item2WeightedAverage += item2.StatValue10 * GetStatWeightByType(item2.StatType10);

            if (item2WeightedAverage > item1WeightedAverage)
            {
                ItemsManager.UseItem(item2.Name);
            }
        }
        private static float GetStatWeightByType(int statType)
        {
            switch (statType)
            {
                case (int)StatType.Agility:
                    return AgilityWeight;
                case (int)StatType.Strength:
                    return StrengthWeight;
                case (int)StatType.Intellect:
                    return IntellectWeight;
                case (int)StatType.Spirit:
                    return SpiritWeight;
                case (int)StatType.Stamina:
                    return StaminaWeight;
            }

            return 1.0f;
        }
    }
}
