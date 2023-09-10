﻿using robotManager.Helpful;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.Models
{
    public class Item
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
        public int ItemId { get; set; }
        public int Patch { get; set; }
        public int Class { get; set; }
        public int Subclass { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayId { get; set; }
        /// <summary>
        /// 0 [Grey] (Poor), 1 [White] (Common), 2 [Green] (Uncommon), 3 [Blue] (Rare), 4 [Purple] (Epic), 5 [Orange] (Legendary), 6 [Red] (Artifact)
        /// </summary>
        public int Quality { get; set; }
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
        public bool CanEquip
        {
            get
            {
                switch (ItemClass)
                {
                    case ItemClass.AxeOneHand:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Axes) > 0;
                    case ItemClass.AxeTwoHand:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.TwoHandedAxes) > 0;
                    case ItemClass.Bow:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Bows) > 0;
                    case ItemClass.Gun:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Guns) > 0;
                    case ItemClass.MaceOneHand:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Maces) > 0;
                    case ItemClass.MaceTwoHand:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.TwoHandedMaces) > 0;
                    case ItemClass.Polearm:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Polearms) > 0;
                    case ItemClass.SwordOneHand:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Swords) > 0;
                    case ItemClass.SwordTwoHand:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.TwoHandedSwords) > 0;
                    case ItemClass.Staff:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Staves) > 0;
                    case ItemClass.Fist:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.FistWeapons) > 0;
                    case ItemClass.Dagger:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Daggers) > 0;
                    case ItemClass.Thrown:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Thrown) > 0;
                    case ItemClass.Crossbow:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Crossbows) > 0;
                    case ItemClass.Wand:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Wands) > 0;
                    case ItemClass.Cloth:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Cloth) > 0;
                    case ItemClass.Leather:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Leather) > 0;
                    case ItemClass.Mail:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Mail) > 0;
                    case ItemClass.Plate:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.PlateMail) > 0;
                    case ItemClass.Shield:
                        return wManager.Wow.Helpers.Skill.GetValue(wManager.Wow.Enums.SkillLine.Shield) > 0;
                }

                return true;
            }
        }
        public ItemClass ItemClass
        {
            get
            {
                switch (Class)
                {
                    case 0:
                        switch (Subclass)
                        {
                            case 0:
                                return ItemClass.Consumable;
                        }
                        break;
                    case 1:
                        switch (Subclass)
                        {
                            case 0:
                                return ItemClass.Bag;
                            case 1:
                                return ItemClass.SoulBag;
                            case 2:
                                return ItemClass.HerbBag;
                            case 3:
                                return ItemClass.EnchantingBag;
                            case 4:
                                return ItemClass.EngineeringBag;
                        }
                        break;
                    case 2:
                        switch (Subclass)
                        {
                            case 0:
                                return ItemClass.AxeOneHand;
                            case 1:
                                return ItemClass.AxeTwoHand;
                            case 2:
                                return ItemClass.Bow;
                            case 3:
                                return ItemClass.Gun;
                            case 4:
                                return ItemClass.MaceOneHand;
                            case 5:
                                return ItemClass.MaceTwoHand;
                            case 6:
                                return ItemClass.Polearm;
                            case 7:
                                return ItemClass.SwordOneHand;
                            case 8:
                                return ItemClass.SwordTwoHand;
                            case 10:
                                return ItemClass.Staff;
                            case 13:
                                return ItemClass.Fist;
                            case 14:
                                return ItemClass.MiscWeapon;
                            case 15:
                                return ItemClass.Dagger;
                            case 16:
                                return ItemClass.Thrown;
                            case 17:
                                return ItemClass.Spear;
                            case 18:
                                return ItemClass.Crossbow;
                            case 19:
                                return ItemClass.Wand;
                            case 20:
                                return ItemClass.FishingPole;
                        }
                        break;
                    case 4:
                        switch (Subclass)
                        {
                            case 0:
                                return ItemClass.MiscArmor;
                            case 1:
                                return ItemClass.Cloth;
                            case 2:
                                return ItemClass.Leather;
                            case 3:
                                return ItemClass.Mail;
                            case 4:
                                return ItemClass.Plate;
                            case 6:
                                return ItemClass.Shield;
                            case 7:
                                return ItemClass.Libram;
                            case 8:
                                return ItemClass.Idol;
                            case 9:
                                return ItemClass.Totem;
                        }
                        break;
                    case 5:
                        switch (Subclass)
                        {
                            case 0:
                                return ItemClass.Reagent;
                        }
                        break;
                    case 6:
                        switch (Subclass)
                        {
                            case 2:
                                return ItemClass.Arrow;
                            case 3:
                                return ItemClass.Bullet;
                        }
                        break;
                    case 7:
                        switch (Subclass)
                        {
                            case 0:
                                return ItemClass.TradeGood;
                            case 1:
                                return ItemClass.Parts;
                            case 2:
                                return ItemClass.Explosives;
                            case 3:
                                return ItemClass.Devices;
                        }
                        break;
                    case 9:
                        switch (Subclass)
                        {
                            case 0:
                                return ItemClass.ClassBook;
                            case 1:
                                return ItemClass.LeatherworkingRecipe;
                            case 2:
                                return ItemClass.TailoringRecipe;
                            case 3:
                                return ItemClass.EngineeringRecipe;
                            case 4:
                                return ItemClass.BlacksmithingRecipe;
                            case 5:
                                return ItemClass.CookingRecipe;
                            case 6:
                                return ItemClass.AlchemyRecipe;
                            case 7:
                                return ItemClass.FirstAidRecipe;
                            case 8:
                                return ItemClass.EnchantingRecipe;
                            case 9:
                                return ItemClass.FishingRecipe;
                        }
                        break;
                    case 11:
                        switch (Subclass)
                        {
                            case 2:
                                return ItemClass.Quiver;
                            case 3:
                                return ItemClass.AmmoPouch;
                        }
                        break;
                    case 12:
                        switch (Subclass)
                        {
                            case 0:
                                return ItemClass.Quest;
                        }
                        break;
                    case 13:
                        switch (Subclass)
                        {
                            case 0:
                                return ItemClass.Key;
                            case 1:
                                return ItemClass.Lockpick;
                        }
                        break;
                }
                return ItemClass.Junk;
            }
        }

        public float GetWeaponGearScore()
        {
            float itemWeightedAverage = ItemLevel * ItemLevelWeight;

            itemWeightedAverage += Armor * ArmorWeight;
            itemWeightedAverage += Block * BlockWeight;

            itemWeightedAverage += RangeMod * RangeModWeight;

            itemWeightedAverage += DmgMin1 * DmgMin1Weight;
            itemWeightedAverage += DmgMin2 * DmgMin2Weight;
            itemWeightedAverage += DmgMin3 * DmgMin3Weight;
            itemWeightedAverage += DmgMin4 * DmgMin4Weight;
            itemWeightedAverage += DmgMin5 * DmgMin5Weight;

            itemWeightedAverage += DmgMax1 * DmgMax1Weight;
            itemWeightedAverage += DmgMax2 * DmgMax2Weight;
            itemWeightedAverage += DmgMax3 * DmgMax3Weight;
            itemWeightedAverage += DmgMax4 * DmgMax4Weight;
            itemWeightedAverage += DmgMax5 * DmgMax5Weight;

            itemWeightedAverage += StatValue1 * GetStatWeightByType(StatType1);
            itemWeightedAverage += StatValue2 * GetStatWeightByType(StatType2);
            itemWeightedAverage += StatValue3 * GetStatWeightByType(StatType3);
            itemWeightedAverage += StatValue4 * GetStatWeightByType(StatType4);
            itemWeightedAverage += StatValue5 * GetStatWeightByType(StatType5);
            itemWeightedAverage += StatValue6 * GetStatWeightByType(StatType6);
            itemWeightedAverage += StatValue7 * GetStatWeightByType(StatType7);
            itemWeightedAverage += StatValue8 * GetStatWeightByType(StatType8);
            itemWeightedAverage += StatValue9 * GetStatWeightByType(StatType9);
            itemWeightedAverage += StatValue10 * GetStatWeightByType(StatType10);

            return itemWeightedAverage;
        }

        public float GetArmorGearScore()
        {
            float itemWeightedAverage = ItemLevel * ItemLevelWeight;

            itemWeightedAverage += Armor * ArmorWeight;
            itemWeightedAverage += Block * BlockWeight;

            itemWeightedAverage += StatValue1 * GetStatWeightByType(StatType1);
            itemWeightedAverage += StatValue2 * GetStatWeightByType(StatType2);
            itemWeightedAverage += StatValue3 * GetStatWeightByType(StatType3);
            itemWeightedAverage += StatValue4 * GetStatWeightByType(StatType4);
            itemWeightedAverage += StatValue5 * GetStatWeightByType(StatType5);
            itemWeightedAverage += StatValue6 * GetStatWeightByType(StatType6);
            itemWeightedAverage += StatValue7 * GetStatWeightByType(StatType7);
            itemWeightedAverage += StatValue8 * GetStatWeightByType(StatType8);
            itemWeightedAverage += StatValue9 * GetStatWeightByType(StatType9);
            itemWeightedAverage += StatValue10 * GetStatWeightByType(StatType10);

            return itemWeightedAverage;
        }
        public static float GetStatWeightByType(int statType)
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
    public enum ItemClass
    {
        Consumable,
        Bag,
        SoulBag,
        HerbBag,
        EnchantingBag,
        EngineeringBag,
        AxeOneHand,
        AxeTwoHand,
        Bow,
        Gun,
        MaceOneHand,
        MaceTwoHand,
        Polearm,
        SwordOneHand,
        SwordTwoHand,
        Staff,
        Fist,
        MiscWeapon,
        Dagger,
        Thrown,
        Spear,
        Crossbow,
        Wand,
        FishingPole,
        MiscArmor,
        Cloth,
        Leather,
        Mail,
        Plate,
        Shield,
        Libram,
        Idol,
        Totem,
        Reagent,
        Arrow,
        Bullet,
        TradeGood,
        Parts,
        Explosives,
        Devices,
        ClassBook,
        LeatherworkingRecipe,
        TailoringRecipe,
        EngineeringRecipe,
        BlacksmithingRecipe,
        CookingRecipe,
        AlchemyRecipe,
        FirstAidRecipe,
        EnchantingRecipe,
        FishingRecipe,
        Quiver,
        AmmoPouch,
        Quest,
        Key,
        Lockpick,
        Junk
    }
    public enum StatType
    {
        NoStats = 0,
        Health = 1,
        Agility = 3,
        Strength = 4,
        Intellect = 5,
        Spirit = 6,
        Stamina = 7,
    }
    public enum DamageType
    {
        Physical = 0,
        Holy = 1,
        Fire = 2,
        Nature = 3,
        Frost = 4,
        Shadow = 5,
        Arcane = 6
    }
    public enum SpellTrigger
    {
        OnUse = 0,
        OnEquip = 1,
        ChanceOnHit = 2,
        Soulstone = 4,
        OnUseWithoutDelay = 5
    }
    public enum Bonding
    {
        NoBinding = 0,
        BindOnPickup = 1,
        BindOnEquip = 2,
        BindOnUse = 3,
        QuestItem = 4
    }
    public enum Material
    {
        NoBinding = 0,
        BindOnPickup = 1,
        BindOnEquip = 2,
        BindOnUse = 3,
        QuestItem = 4
    }
    public enum InventoryType
    {
        NonEquippable = 0,
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
        Weapon = 13,
        Shield = 14,
        Ranged = 15,
        Cloak = 16,
        TwoHander = 17,
        Bag = 18,
        Tabard = 19,
        Robe = 20,
        MainHand = 21,
        Offhand = 22,
        Holdable = 23,
        Ammo = 24,
        Thrown = 25,
        RangedRight = 26,
        Quiver = 27,
        Relic = 28,
    }
}
