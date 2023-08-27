namespace WoWBot.Client.Models
{
    public abstract class Item
    {
        public string Description { get; set; } = "";
        public ushort DisplayId { get; set; } = 0;
        public uint Flags { get; set; } = 0;
        public byte BuyCount { get; set; } = 1;
        public uint BuyPrice { get; set; } = 0;
        public uint SellPrice { get; set; } = 0;
        public byte InventoryType { get; set; } = 0;
        public int AllowableClass { get; set; } = -1;
        public int AllowableRace { get; set; } = -1;
        public byte ItemLevel { get; set; } = 0;
        public byte RequiredLevel { get; set; } = 0;
        public ushort RequiredSkill { get; set; } = 0;
        public ushort RequiredSkillRank { get; set; } = 0;
        public ushort RequiredSpell { get; set; } = 0;
        public ushort RequiredHonorRank { get; set; } = 0;
        public ushort RequiredCityRank { get; set; } = 0;
        public ushort RequiredReputationFaction { get; set; } = 0;
        public ushort RequiredReputationRank { get; set; } = 0;
        public ushort MaxCount { get; set; } = 0;
        public ushort Stackable { get; set; } = 1;
        public byte ContainerSlots { get; set; } = 0;

        // Stat Properties
        public byte StatType1 { get; set; } = 0;
        public short StatValue1 { get; set; } = 0;
        public byte StatType2 { get; set; } = 0;
        public short StatValue2 { get; set; } = 0;
        public byte StatType3 { get; set; } = 0;
        public short StatValue3 { get; set; } = 0;
        public byte StatType4 { get; set; } = 0;
        public short StatValue4 { get; set; } = 0;
        public byte StatType5 { get; set; } = 0;
        public short StatValue5 { get; set; } = 0;
        public byte StatType6 { get; set; } = 0;
        public short StatValue6 { get; set; } = 0;
        public byte StatType7 { get; set; } = 0;
        public short StatValue7 { get; set; } = 0;
        public byte StatType8 { get; set; } = 0;
        public short StatValue8 { get; set; } = 0;
        public byte StatType9 { get; set; } = 0;
        public short StatValue9 { get; set; } = 0;
        public byte StatType10 { get; set; } = 0;
        public short StatValue10 { get; set; } = 0;

        // Damage Properties
        public float RangeMod { get; set; } = 0;
        public byte AmmoType { get; set; } = 0;
        public float DamageMin1 { get; set; } = 0;
        public float DamageMax1 { get; set; } = 0;
        public byte DamageType1 { get; set; } = 0;
        public float DamageMin2 { get; set; } = 0;
        public float DamageMax2 { get; set; } = 0;
        public byte DamageType2 { get; set; } = 0;
        public float DamageMin3 { get; set; } = 0;
        public float DamageMax3 { get; set; } = 0;
        public byte DamageType3 { get; set; } = 0;
        public float DamageMin4 { get; set; } = 0;
        public float DamageMax4 { get; set; } = 0;
        public byte DamageType4 { get; set; } = 0;
        public float DamageMin5 { get; set; } = 0;
        public float DamageMax5 { get; set; } = 0;
        public byte DamageType5 { get; set; } = 0;
        // ... (up to DamageType5)

        // Resistance Properties
        public ushort Block { get; set; } = 0;
        public short Armor { get; set; } = 0;
        public short HolyResistance { get; set; } = 0;
        public short FireResistance { get; set; } = 0;
        public short NatureResistance { get; set; } = 0;
        public short FrostResistance { get; set; } = 0;
        public short ShadowResistance { get; set; } = 0;
        public short ArcaneResistance { get; set; } = 0;

        // Spell Properties
        public ushort SpellId1 { get; set; } = 0;
        public byte SpellTrigger1 { get; set; } = 0;
        public byte SpellCharges1 { get; set; } = 0;
        public float SpellPpmRate1 { get; set; } = 0;
        public int SpellCooldown1 { get; set; } = -1;
        public ushort SpellCategory1 { get; set; } = 0;
        public int SpellCategoryCooldown1 { get; set; } = -1;
        public ushort SpellId2 { get; set; } = 0;
        public byte SpellTrigger2 { get; set; } = 0;
        public byte SpellCharges2 { get; set; } = 0;
        public float SpellPpmRate2 { get; set; } = 0;
        public int SpellCooldown2 { get; set; } = -1;
        public ushort SpellCategory2 { get; set; } = 0;
        public int SpellCategoryCooldown2 { get; set; } = -1;
        public ushort SpellId3 { get; set; } = 0;
        public byte SpellTrigger3 { get; set; } = 0;
        public byte SpellCharges3 { get; set; } = 0;
        public float SpellPpmRate3 { get; set; } = 0;
        public int SpellCooldown3 { get; set; } = -1;
        public ushort SpellCategory3 { get; set; } = 0;
        public int SpellCategoryCooldown3 { get; set; } = -1;
        public ushort SpellId4 { get; set; } = 0;
        public byte SpellTrigger4 { get; set; } = 0;
        public byte SpellCharges4 { get; set; } = 0;
        public float SpellPpmRate4 { get; set; } = 0;
        public int SpellCooldown4 { get; set; } = -1;
        public ushort SpellCategory4 { get; set; } = 0;
        public int SpellCategoryCooldown4 { get; set; } = -1;
        public ushort SpellId5 { get; set; } = 0;
        public byte SpellTrigger5 { get; set; } = 0;
        public byte SpellCharges5 { get; set; } = 0;
        public float SpellPpmRate5 { get; set; } = 0;
        public int SpellCooldown5 { get; set; } = -1;
        public ushort SpellCategory5 { get; set; } = 0;
        public int SpellCategoryCooldown5 { get; set; } = -1;
        // ... (up to SpellCategoryCooldown5)

        // Other Properties
        public byte Bonding { get; set; } = 0;
        public ushort PageText { get; set; } = 0;
        public byte PageLanguage { get; set; } = 0;
        public byte PageMaterial { get; set; } = 0;
        public ushort StartQuest { get; set; } = 0;
        public ushort LockId { get; set; } = 0;
        public byte Material { get; set; } = 0;
        public byte Sheath { get; set; } = 0;
        public ushort RandomProperty { get; set; } = 0;
        public ushort SetId { get; set; } = 0;
        public ushort MaxDurability { get; set; } = 0;
        public ushort AreaBound { get; set; } = 0;
        public ushort MapBound { get; set; } = 0;
        public ushort Duration { get; set; } = 0;
        public int BagFamily { get; set; } = 0;
        public ushort DisenchantId { get; set; } = 0;
        public byte FoodType { get; set; } = 0;
        public uint MinMoneyLoot { get; set; } = 0;
        public uint MaxMoneyLoot { get; set; } = 0;
        public bool ExtraFlags { get; set; } = false;
        public uint OtherTeamEntry { get; set; } = 1;
    }
}
