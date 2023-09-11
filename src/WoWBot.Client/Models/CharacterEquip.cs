﻿using System;
using System.Text;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Database;

namespace WoWBot.Client.Models
{
    public class CharacterEquip : ICloneable
    {
        public CharacterEquip() {
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_HEAD) != 0)
            {
                HeadItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_HEAD));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_NECK) != 0)
            {
                NeckItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_NECK));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_SHOULDER) != 0)
            {
                ShoulderItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_SHOULDER));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_BACK) != 0)
            {
                BackItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_BACK));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_CHEST) != 0)
            {
                ChestItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_CHEST));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_WRIST) != 0)
            {
                WristItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_WRIST));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_HAND) != 0)
            {
                HandItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_HAND));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_WAIST) != 0)
            {
                WaistItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_WAIST));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_LEGS) != 0)
            {
                LegItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_LEGS));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_FEET) != 0)
            {
                FeetItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_FEET));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_FINGER1) != 0)
            {
                Finger1Item = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_FINGER1));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_FINGER2) != 0)
            {
                Finger2Item = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_FINGER2));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_TRINKET1) != 0)
            {
                Trinket1Item = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_TRINKET1));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_TRINKET2) != 0)
            {
                Trinket2Item = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_TRINKET2));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_MAINHAND) != 0)
            {
                MainHandItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_MAINHAND));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_OFFHAND) != 0)
            {
                OffHandItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_OFFHAND));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_RANGED) != 0)
            {
                RangedItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_RANGED));
            }
            if (ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_AMMO) != 0)
            {
                AmmoItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_AMMO));
            }
        }
        public Item HeadItem { get; set; }
        public Item NeckItem { get; set; }
        public Item ShoulderItem { get; set; }
        public Item BackItem { get; set; }
        public Item ChestItem { get; set; }
        public Item WristItem { get; set; }
        public Item HandItem { get; set; }
        public Item WaistItem { get; set; }
        public Item LegItem { get; set; }
        public Item FeetItem { get; set; }
        public Item Finger1Item { get; set; }
        public Item Finger2Item { get; set; }
        public Item Trinket1Item { get; set; }
        public Item Trinket2Item { get; set; }
        public Item MainHandItem { get; set; }
        public Item OffHandItem { get; set; }
        public Item RangedItem { get; set; }
        public Item AmmoItem { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (HeadItem != null)
            {
                sb.Append("HeadItem: " + HeadItem.Name + " ");
            }
            if (NeckItem != null)
            {
                sb.Append("NeckItem: " + NeckItem.Name + " ");
            }
            if (ShoulderItem != null)
            {
                sb.Append("ShoulderItem: " + ShoulderItem.Name + " ");
            }
            if (BackItem != null)
            {
                sb.Append("BackItem: " + BackItem.Name + " ");
            }
            if (ChestItem != null)
            {
                sb.Append("ChestItem: " + ChestItem.Name + " ");
            }
            if (WristItem != null)
            {
                sb.Append("WristItem: " + WristItem.Name + " ");
            }
            if (HandItem != null)
            {
                sb.Append("HandItem: " + HandItem.Name + " ");
            }
            if (WaistItem != null)
            {
                sb.Append("WaistItem: " + WaistItem.Name + " ");
            }
            if (LegItem != null)
            {
                sb.Append("LegItem: " + LegItem.Name + " ");
            }
            if (FeetItem != null)
            {
                sb.Append("FeetItem: " + FeetItem.Name + " ");
            }
            if (Finger1Item != null)
            {
                sb.Append("Finger1Item: " + Finger1Item.Name + " ");
            }
            if (Finger2Item != null)
            {
                sb.Append("Finger2Item: " + Finger2Item.Name + " ");
            }
            if (Trinket1Item != null)
            {
                sb.Append("Trinket1Item: " + Trinket1Item.Name + " ");
            }
            if (Trinket2Item != null)
            {
                sb.Append("Trinket2Item: " + Trinket2Item.Name + " ");
            }
            if (MainHandItem != null)
            {
                sb.Append("MainHandItem: " + MainHandItem.Name + " ");
            }
            if (OffHandItem != null)
            {
                sb.Append("OffHandItem: " + OffHandItem.Name + " ");
            }
            if (RangedItem != null)
            {
                sb.Append("RangedItem: " + RangedItem.Name + " ");
            }
            if (AmmoItem != null)
            {
                sb.Append("AmmoItem: " + AmmoItem.Name + " ");
            }
            return sb.ToString();
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public float GetGearScore()
        {
            float gearScore = 0;

            if (HeadItem != null)
            {
                gearScore += HeadItem.GetArmorGearScore();
            }
            if (NeckItem != null)
            {
                gearScore += NeckItem.GetArmorGearScore();
            }
            if (ShoulderItem != null)
            {
                gearScore += ShoulderItem.GetArmorGearScore();
            }
            if (BackItem != null)
            {
                gearScore += BackItem.GetArmorGearScore();
            }
            if (ChestItem != null)
            {
                gearScore += ChestItem.GetArmorGearScore();
            }
            if (WristItem != null)
            {
                gearScore += WristItem.GetArmorGearScore();
            }
            if (HandItem != null)
            {
                gearScore += HandItem.GetArmorGearScore();
            }
            if (WaistItem != null)
            {
                gearScore += WaistItem.GetArmorGearScore();
            }
            if (LegItem != null)
            {
                gearScore += LegItem.GetArmorGearScore();
            }
            if (FeetItem != null)
            {
                gearScore += FeetItem.GetArmorGearScore();
            }
            if (Finger1Item != null)
            {
                gearScore += Finger1Item.GetArmorGearScore();
            }
            if (Finger2Item != null)
            {
                gearScore += Finger2Item.GetArmorGearScore();
            }
            if (Trinket1Item != null)
            {
                gearScore += Trinket1Item.GetArmorGearScore();
            }
            if (Trinket2Item != null)
            {
                gearScore += Trinket2Item.GetArmorGearScore();
            }
            if (MainHandItem != null)
            {
                gearScore += MainHandItem.GetWeaponGearScore();
            }
            if (OffHandItem != null)
            {
                // TODO Differentiate based on item type
                gearScore += OffHandItem.GetArmorGearScore();
            }
            if (RangedItem != null)
            {
                gearScore += RangedItem.GetWeaponGearScore();
            }
            if (AmmoItem != null)
            {
                gearScore += AmmoItem.GetWeaponGearScore();
            }

            return gearScore;
        }
    }
}
