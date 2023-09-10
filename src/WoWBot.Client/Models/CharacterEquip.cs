using System;
using wManager.Wow.Helpers;
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
                BaclkItem = MangosDb.GetItemById((int)ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_BACK));
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
        public Item BaclkItem { get; set; }
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
