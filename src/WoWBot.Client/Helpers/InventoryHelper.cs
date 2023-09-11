using robotManager.Helpful;
using System.Collections.Generic;
using System.Linq;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Database;
using WoWBot.Client.Models;

namespace WoWBot.Client.Helpers
{
    public class InventoryHelper
    {
        public static float GetGearScoreWithItem(Item item)
        {
            CharacterEquip characterEquip = new CharacterEquip();

            switch (item.InventoryType)
            {
                case (int)InventoryType.Head:
                    characterEquip.HeadItem = item;
                    break;
                case (int)InventoryType.Neck:
                    characterEquip.NeckItem = item;
                    break;
                case (int)InventoryType.Cloak:
                    characterEquip.BackItem = item;
                    break;
                case (int)InventoryType.Shoulders:
                    characterEquip.ShoulderItem = item;
                    break;
                case (int)InventoryType.Chest:
                    characterEquip.ChestItem = item;
                    break;
                case (int)InventoryType.Waist:
                    characterEquip.WaistItem = item;
                    break;
                case (int)InventoryType.Legs:
                    characterEquip.LegItem = item;
                    break;
                case (int)InventoryType.Feet:
                    characterEquip.FeetItem = item;
                    break;
                case (int)InventoryType.Wrists:
                    characterEquip.WristItem = item;
                    break;
                case (int)InventoryType.Hands:
                    characterEquip.HandItem = item;
                    break;
                case (int)InventoryType.Finger:
                    characterEquip.Finger1Item = item;
                    break;
                case (int)InventoryType.Trinket:
                    characterEquip.Trinket1Item = item;
                    break;
                case (int)InventoryType.Weapon:
                case (int)InventoryType.MainHand:
                case (int)InventoryType.TwoHander:
                    characterEquip.MainHandItem = item;
                    break;
                case (int)InventoryType.Offhand:
                case (int)InventoryType.Shield:
                case (int)InventoryType.Holdable:
                    if (characterEquip.MainHandItem.ItemClass != ItemClass.AxeTwoHand
                        && characterEquip.MainHandItem.ItemClass != ItemClass.MaceTwoHand
                        && characterEquip.MainHandItem.ItemClass != ItemClass.SwordTwoHand
                        && characterEquip.MainHandItem.ItemClass != ItemClass.Polearm
                        && characterEquip.MainHandItem.ItemClass != ItemClass.Spear
                        && characterEquip.MainHandItem.ItemClass != ItemClass.Staff)
                    {
                        characterEquip.OffHandItem = item;
                    }
                    break;
                case (int)InventoryType.RangedRight:
                case (int)InventoryType.Ranged:
                case (int)InventoryType.Relic:
                    characterEquip.RangedItem = item;
                    break;
                case (int)InventoryType.Thrown:
                case (int)InventoryType.Ammo:
                    characterEquip.AmmoItem = item;
                    break;
            }

            return characterEquip.GetGearScore();
        }
        public static void UpgradeToLatestEquipment()
        {
            List<WoWItem> allWoWItems = EquippedItems.GetEquippedItems();
            allWoWItems.AddRange(Bag.GetBagItem());

            CharacterEquip bestEquip = FindBestGearSetup(allWoWItems);

            EquipItemsInGearSetup(bestEquip);
        }

        private static CharacterEquip FindBestGearSetup(List<WoWItem> allWoWItems)
        {
            List<Item> headItems = new List<Item>();
            List<Item> neckItems = new List<Item>();
            List<Item> shoulderItems = new List<Item>();
            List<Item> cloakItems = new List<Item>();
            List<Item> chestItems = new List<Item>();
            List<Item> wristItems = new List<Item>();
            List<Item> handsItems = new List<Item>();
            List<Item> waistItems = new List<Item>();
            List<Item> legsItems = new List<Item>();
            List<Item> feetItems = new List<Item>();
            List<Item> fingerItems = new List<Item>();
            List<Item> trinketItems = new List<Item>();
            List<Item> relicItems = new List<Item>();
            List<Item> oneHandedItems = new List<Item>();
            List<Item> mainHandItems = new List<Item>();
            List<Item> twoHandedItems = new List<Item>();
            List<Item> shieldItems = new List<Item>();
            List<Item> offHandItems = new List<Item>();
            List<Item> holdableItems = new List<Item>();
            List<Item> rangedItems = new List<Item>();
            List<Item> thrownItems = new List<Item>();
            List<Item> ammoItems = new List<Item>();

            List<Item> allItems = allWoWItems.Select(x => MangosDb.GetItemById((int)ItemsManager.GetIdByName(x.Name))).ToList();

            foreach (Item item in allItems)
            {
                if (item.RequiredLevel <= ObjectManager.Me.Level && item.CanEquip)
                {
                    switch (item.InventoryType)
                    {
                        case (int)InventoryType.Head:
                            headItems.Add(item);
                            break;
                        case (int)InventoryType.Neck:
                            neckItems.Add(item);
                            break;
                        case (int)InventoryType.Shoulders:
                            shoulderItems.Add(item);
                            break;
                        case (int)InventoryType.Chest:
                            chestItems.Add(item);
                            break;
                        case (int)InventoryType.Waist:
                            waistItems.Add(item);
                            break;
                        case (int)InventoryType.Legs:
                            legsItems.Add(item);
                            break;
                        case (int)InventoryType.Feet:
                            feetItems.Add(item);
                            break;
                        case (int)InventoryType.Wrists:
                            wristItems.Add(item);
                            break;
                        case (int)InventoryType.Hands:
                            handsItems.Add(item);
                            break;
                        case (int)InventoryType.Finger:
                            fingerItems.Add(item);
                            break;
                        case (int)InventoryType.Trinket:
                            trinketItems.Add(item);
                            break;
                        case (int)InventoryType.Weapon:
                            oneHandedItems.Add(item);
                            break;
                        case (int)InventoryType.Shield:
                            shieldItems.Add(item);
                            break;
                        case (int)InventoryType.Ranged:
                            rangedItems.Add(item);
                            break;
                        case (int)InventoryType.Cloak:
                            cloakItems.Add(item);
                            break;
                        case (int)InventoryType.TwoHander:
                            twoHandedItems.Add(item);
                            break;
                        case (int)InventoryType.MainHand:
                            mainHandItems.Add(item);
                            break;
                        case (int)InventoryType.Offhand:
                            offHandItems.Add(item);
                            break;
                        case (int)InventoryType.Holdable:
                            holdableItems.Add(item);
                            break;
                        case (int)InventoryType.Ammo:
                            ammoItems.Add(item);
                            break;
                        case (int)InventoryType.Thrown:
                            thrownItems.Add(item);
                            break;
                        case (int)InventoryType.RangedRight:
                            rangedItems.Add(item);
                            break;
                        case (int)InventoryType.Relic:
                            relicItems.Add(item);
                            break;
                    }
                }
            }

            List<CharacterEquip> characterEquips = PermutateEquips(new List<CharacterEquip>(), headItems);
            Logging.WriteDebug("headItems.Count " + headItems.Count);
            characterEquips = PermutateEquips(characterEquips, neckItems);
            Logging.WriteDebug("neckItems.Count " + neckItems.Count);
            characterEquips = PermutateEquips(characterEquips, shoulderItems);
            Logging.WriteDebug("shoulderItems.Count " + shoulderItems.Count);
            characterEquips = PermutateEquips(characterEquips, chestItems);
            Logging.WriteDebug("chestItems.Count " + chestItems.Count);
            characterEquips = PermutateEquips(characterEquips, wristItems);
            Logging.WriteDebug("wristItems.Count " + wristItems.Count);
            characterEquips = PermutateEquips(characterEquips, handsItems);
            Logging.WriteDebug("handsItems.Count " + handsItems.Count);
            characterEquips = PermutateEquips(characterEquips, waistItems);
            Logging.WriteDebug("waistItems.Count " + waistItems.Count);
            characterEquips = PermutateEquips(characterEquips, legsItems);
            Logging.WriteDebug("legsItems.Count " + legsItems.Count);
            characterEquips = PermutateEquips(characterEquips, feetItems);
            Logging.WriteDebug("feetItems.Count " + feetItems.Count);
            characterEquips = PermutateEquips(characterEquips, fingerItems);
            Logging.WriteDebug("fingerItems.Count " + fingerItems.Count);
            characterEquips = PermutateEquips(characterEquips, trinketItems);
            Logging.WriteDebug("trinketItems.Count " + trinketItems.Count);
            characterEquips = PermutateEquips(characterEquips, twoHandedItems);
            Logging.WriteDebug("twoHandedItems.Count " + twoHandedItems.Count);
            characterEquips = PermutateEquips(characterEquips, mainHandItems);
            Logging.WriteDebug("mainHandItems.Count " + mainHandItems.Count);
            characterEquips = PermutateEquips(characterEquips, oneHandedItems);
            Logging.WriteDebug("oneHandedItems.Count " + oneHandedItems.Count);
            characterEquips = PermutateEquips(characterEquips, shieldItems);
            Logging.WriteDebug("shieldItems.Count " + shieldItems.Count);
            characterEquips = PermutateEquips(characterEquips, offHandItems);
            Logging.WriteDebug("offHandItems.Count " + offHandItems.Count);
            characterEquips = PermutateEquips(characterEquips, holdableItems);
            Logging.WriteDebug("holdableItems.Count " + holdableItems.Count);
            characterEquips = PermutateEquips(characterEquips, rangedItems);
            Logging.WriteDebug("rangedItems.Count " + rangedItems.Count);
            characterEquips = PermutateEquips(characterEquips, relicItems);
            Logging.WriteDebug("relicItems.Count " + relicItems.Count);
            characterEquips = PermutateEquips(characterEquips, thrownItems);
            Logging.WriteDebug("thrownItems.Count " + thrownItems.Count);
            characterEquips = PermutateEquips(characterEquips, ammoItems);
            Logging.WriteDebug("ammoItems.Count " + ammoItems.Count);

            CharacterEquip bestEquip = new CharacterEquip();

            Logging.WriteDebug("Comparing " + characterEquips.Count + " equipment permutations");
            if (characterEquips.Count > 0)
            {
                bestEquip = characterEquips[0];

                foreach (CharacterEquip characterEquip in characterEquips)
                {
                    Logging.WriteDebug("Comparing " + bestEquip.ToString());
                    Logging.WriteDebug("To " + characterEquip.ToString());
                    if (characterEquip.GetGearScore() > bestEquip.GetGearScore())
                    {
                        bestEquip = characterEquip;
                    }
                }
            }

            return bestEquip;
        }

        private static void EquipItemsInGearSetup(CharacterEquip bestEquip)
        {
            if (bestEquip.HeadItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_HEAD) != bestEquip.HeadItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.HeadItem.Name);
            }
            if (bestEquip.NeckItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_NECK) != bestEquip.NeckItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.NeckItem.Name);
            }
            if (bestEquip.ShoulderItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_SHOULDER) != bestEquip.ShoulderItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.ShoulderItem.Name);
            }
            if (bestEquip.ChestItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_CHEST) != bestEquip.ChestItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.ChestItem.Name);
            }
            if (bestEquip.WristItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_WRIST) != bestEquip.WristItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.WristItem.Name);
            }
            if (bestEquip.HandItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_HAND) != bestEquip.HandItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.HandItem.Name);
            }
            if (bestEquip.WaistItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_WAIST) != bestEquip.WaistItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.WaistItem.Name);
            }
            if (bestEquip.LegItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_LEGS) != bestEquip.LegItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.LegItem.Name);
            }
            if (bestEquip.FeetItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_FEET) != bestEquip.FeetItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.FeetItem.Name);
            }
            if (bestEquip.Finger1Item != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_FINGER1) != bestEquip.Finger1Item.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.Finger1Item.Name);
            }
            if (bestEquip.Finger2Item != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_FINGER2) != bestEquip.Finger2Item.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.Finger2Item.Name);
            }
            if (bestEquip.Trinket1Item != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_TRINKET1) != bestEquip.Trinket1Item.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.Trinket1Item.Name);
            }
            if (bestEquip.Trinket2Item != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_TRINKET2) != bestEquip.Trinket2Item.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.Trinket2Item.Name);
            }
            if (bestEquip.MainHandItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_MAINHAND) != bestEquip.MainHandItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.MainHandItem.Name);
            }
            if (bestEquip.OffHandItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_OFFHAND) != bestEquip.OffHandItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.OffHandItem.Name);
            }
            if (bestEquip.RangedItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_RANGED) != bestEquip.RangedItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.RangedItem.Name);
            }
            if (bestEquip.AmmoItem != null && ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_AMMO) != bestEquip.AmmoItem.ItemId)
            {
                ItemsManager.EquipItemByName(bestEquip.AmmoItem.Name);
            }
        }

        public CharacterEquip GetBestEquipmentSetup()
        {
            CharacterEquip bestEquip = new CharacterEquip();

            return bestEquip;
        }

        private static List<CharacterEquip> PermutateEquips(List<CharacterEquip> characterEquips, List<Item> slotItems)
        {
            if (slotItems.Count == 0)
            {
                return characterEquips;
            }

            List<CharacterEquip> newCharacterEquips = new List<CharacterEquip>();

            if (characterEquips.Count > 0)
            {
                foreach (CharacterEquip characterEquip in characterEquips)
                {
                    foreach (Item item in slotItems)
                    {
                        CharacterEquip newCharacterEquip = ApplyItemToEquipmentBuild(characterEquip, item);

                        newCharacterEquips.Add(newCharacterEquip);
                    }
                }
            }
            else
            {
                foreach (Item item in slotItems)
                {
                    CharacterEquip newCharacterEquip = ApplyItemToEquipmentBuild(new CharacterEquip(), item);

                    newCharacterEquips.Add(newCharacterEquip);
                }
            }

            return newCharacterEquips;
        }

        private static CharacterEquip ApplyItemToEquipmentBuild(CharacterEquip characterEquip, Item item)
        {
            CharacterEquip newCharacterEquip = (CharacterEquip)characterEquip.Clone();

            Logging.WriteDebug(item.Name + " " + item.InventoryType);
            switch (item.InventoryType)
            {
                case (int)InventoryType.Head:
                    newCharacterEquip.HeadItem = item;
                    break;
                case (int)InventoryType.Neck:
                    newCharacterEquip.NeckItem = item;
                    break;
                case (int)InventoryType.Shoulders:
                    newCharacterEquip.ShoulderItem = item;
                    break;
                case (int)InventoryType.Cloak:
                    newCharacterEquip.BackItem = item;
                    break;
                case (int)InventoryType.Chest:
                    newCharacterEquip.ChestItem = item;
                    break;
                case (int)InventoryType.Waist:
                    newCharacterEquip.WaistItem = item;
                    break;
                case (int)InventoryType.Legs:
                    newCharacterEquip.LegItem = item;
                    break;
                case (int)InventoryType.Feet:
                    newCharacterEquip.FeetItem = item;
                    break;
                case (int)InventoryType.Wrists:
                    newCharacterEquip.WristItem = item;
                    break;
                case (int)InventoryType.Hands:
                    newCharacterEquip.HandItem = item;
                    break;
                case (int)InventoryType.Finger:
                    newCharacterEquip.Finger1Item = item;
                    break;
                case (int)InventoryType.Trinket:
                    newCharacterEquip.Trinket1Item = item;
                    break;
                case (int)InventoryType.Shield:
                case (int)InventoryType.Offhand:
                    if (newCharacterEquip.MainHandItem.InventoryType != (int)InventoryType.TwoHander)
                    {
                        newCharacterEquip.OffHandItem = item;
                    }
                    break;
                case (int)InventoryType.Ranged:
                case (int)InventoryType.RangedRight:
                case (int)InventoryType.Relic:
                    newCharacterEquip.RangedItem = item;
                    break;
                case (int)InventoryType.TwoHander:
                    newCharacterEquip.MainHandItem = item;
                    newCharacterEquip.OffHandItem = null;
                    break;
                case (int)InventoryType.Weapon:
                case (int)InventoryType.MainHand:
                    newCharacterEquip.MainHandItem = item;
                    break;
                case (int)InventoryType.Holdable:
                    newCharacterEquip.OffHandItem = item;
                    break;
                case (int)InventoryType.Thrown:
                case (int)InventoryType.Ammo:
                    newCharacterEquip.AmmoItem = item;
                    break;
            }

            return newCharacterEquip;
        }
    }
}
