using System;

namespace WoWBot.Client.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Quality { get; set; }
        public int Patch { get; set; }
        public int Class { get; set; }
        public int Subclass { get; set; }
        public string Description { get; set; }
        public int DisplayId { get; set; }
        public int Flags { get; set; }
        public int BuyCount { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
        public int InventoryType { get; set; }
        public int AllowableClass { get; set; }
        public int AllowableRace { get; set; }
        public int ItemLevel { get; set; }
        public int RequiredLevel { get; set; }
        public short RequiredSkill { get; set; }
        public short RequiredSkillRank { get; set; }
        public int RequiredSpell { get; set; }
        public int RequiredHonorRank { get; set; }
        public int RequiredCityRank { get; set; }
        public int RequiredReputationFaction { get; set; }
        public int RequiredReputationRank { get; set; }
        public int MaxCount { get; set; }
        public int Stackable { get; set; }
        public int ContainerSlots { get; set; }
        public int StatType1 { get; set; }
        public int StatValue1 { get; set; }
        public int StatType2 { get; set; }
        public int StatValue2 { get; set; }
        public int StatType3 { get; set; }
        public int StatValue3 { get; set; }
        public int StatType4 { get; set; }
        public int StatValue4 { get; set; }
        public int StatType5 { get; set; }
        public int StatValue5 { get; set; }
        public int StatType6 { get; set; }
        public int StatValue6 { get; set; }
        public int StatType7 { get; set; }
        public int StatValue7 { get; set; }
        public int StatType8 { get; set; }
        public int StatValue8 { get; set; }
        public int StatType9 { get; set; }
        public int StatValue9 { get; set; }
        public int StatType10 { get; set; }
        public int StatValue10 { get; set; }
        public int Delay { get; set; }
        public float RangeMod { get; set; }
        public int AmmoType { get; set; }
        public float DmgMin1 { get; set; }
        public float DmgMax1 { get; set; }
        public int DmgType1 { get; set; }
        public float DmgMin2 { get; set; }
        public float DmgMax2 { get; set; }
        public int DmgType2 { get; set; }
        public float DmgMin3 { get; set; }
        public float DmgMax3 { get; set; }
        public int DmgType3 { get; set; }
        public float DmgMin4 { get; set; }
        public float DmgMax4 { get; set; }
        public int DmgType4 { get; set; }
        public float DmgMin5 { get; set; }
        public float DmgMax5 { get; set; }
        public int DmgType5 { get; set; }
        public int Block { get; set; }
        public int Armor { get; set; }
        public int HolyRes { get; set; }
        public int FireRes { get; set; }
        public int NatureRes { get; set; }
        public int FrostRes { get; set; }
        public int ShadowRes { get; set; }
        public int ArcaneRes { get; set; }
        public int SpellId1 { get; set; }
        public int SpellTrigger1 { get; set; }
        public int SpellCharges1 { get; set; }
        public float SpellPpmRate1 { get; set; }
        public int SpellCooldown1 { get; set; }
        public int SpellCategory1 { get; set; }
        public int SpellCategoryCooldown1 { get; set; }
        public int SpellId2 { get; set; }
        public int SpellTrigger2 { get; set; }
        public int SpellCharges2 { get; set; }
        public float SpellPpmRate2 { get; set; }
        public int SpellCooldown2 { get; set; }
        public int SpellCategory2 { get; set; }
        public int SpellCategoryCooldown2 { get; set; }
        public int SpellId3 { get; set; }
        public int SpellTrigger3 { get; set; }
        public int SpellCharges3 { get; set; }
        public float SpellPpmRate3 { get; set; }
        public int SpellCooldown3 { get; set; }
        public int SpellCategory3 { get; set; }
        public int SpellCategoryCooldown3 { get; set; }
        public int SpellId4 { get; set; }
        public int SpellTrigger4 { get; set; }
        public int SpellCharges4 { get; set; }
        public float SpellPpmRate4 { get; set; }
        public int SpellCooldown4 { get; set; }
        public int SpellCategory4 { get; set; }
        public int SpellCategoryCooldown4 { get; set; }
        public int SpellId5 { get; set; }
        public int SpellTrigger5 { get; set; }
        public int SpellCharges5 { get; set; }
        public float SpellPpmRate5 { get; set; }
        public int SpellCooldown5 { get; set; }
        public int SpellCategory5 { get; set; }
        public int SpellCategoryCooldown5 { get; set; }
        public int Bonding { get; set; }
        public int PageText { get; set; }
        public int PageLanguage { get; set; }
        public int PageMaterial { get; set; }
        public int StartQuest { get; set; }
        public int LockId { get; set; }
        public int Material { get; set; }
        public int Sheath { get; set; }
        public int RandomProperty { get; set; }
        public int SetId { get; set; }
        public int MaxDurability { get; set; }
        public int AreaBound { get; set; }
        public int MapBound { get; set; }
        public int Duration { get; set; }
        public int BagFamily { get; set; }
        public int DisenchantId { get; set; }
        public int FoodType { get; set; }
        public int MinMoneyLoot { get; set; }
        public int MaxMoneyLoot { get; set; }
        public int ExtraFlags { get; set; }
        public int OtherTeamEntry { get; set; }
    }
    public enum StatType
    {
        Agility = 3,
        Strength = 4,
        Intellect = 5,
        Spirit = 6,
        Stamina = 7,
    }
    public enum InventoryType
    {
        Stackable = 0,
        Head = 1,
        Neck = 2,
        Shoulders = 3,
        Shirt = 4,
        Chest = 5,
        Waist = 6,
        Legs = 7,
        Feet = 8,
        Wrists = 9,
        Hands = 10,
        Finger = 11,
        Trinket = 12,
        OneHanderBoE = 13,
        OffhandBoE = 14,
        RangedBowBoE = 15,
        BackBoE = 16,
        TwoHanderBoE = 17,
        Bag = 18,
        Tabard = 19,
        ChestBoE = 20,
        MainHanderBoE = 21,
        OffhandWeaponBoP = 22,
        HeldInOffhandBoE = 23,
        Ammo = 24,
        Thrown = 25,
        RangedGunBoE = 26,
        Relic = 28,
    }
}
