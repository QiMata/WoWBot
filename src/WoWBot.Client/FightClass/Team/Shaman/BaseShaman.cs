using System.Diagnostics;
using WoWBot.Client.Helpers;

namespace WoWBot.Client.FightClass.Team.Shaman
{
    internal class BaseShaman
    {
        public Stopwatch WeaponBuffStopwatch = new Stopwatch();

        // Healing & Rez
        public MonitoredSpell AutoAttack = new MonitoredSpell("Attack", 0);

        // Healing & Rez
        public MonitoredSpell AncestralSpirit = new MonitoredSpell("Ancestral Spirit", 10000);
        public MonitoredSpell HealingWave = new MonitoredSpell("Healing Wave", 3000);
        public MonitoredSpell LesserHealingWave = new MonitoredSpell("Lesser Healing Wave", 2500);
        public MonitoredSpell ChainHeal = new MonitoredSpell("Chain Heal", 2500);
        public MonitoredSpell CurePoison = new MonitoredSpell("Cure Poison", 2500);

        // Elemental
        public MonitoredSpell LightningBolt = new MonitoredSpell("Lightning Bolt", 1500);
        public MonitoredSpell ChainLightning = new MonitoredSpell("Chain Lightning", 2500);

        public MonitoredSpell EarthShock = new MonitoredSpell("Earth Shock", 1500);
        public MonitoredSpell FrostShock = new MonitoredSpell("Frost Shock", 1500);
        public MonitoredSpell FlameShock = new MonitoredSpell("Flame Shock", 1500);

        // Buffs
        public MonitoredSpell AstralRecall = new MonitoredSpell("Astral Recall", 1500);
        public MonitoredSpell WaterWalking = new MonitoredSpell("Water Walking", 1500);
        public MonitoredSpell WaterBreathing = new MonitoredSpell("Water Breathing", 1500);
        public MonitoredSpell CureDisease = new MonitoredSpell("Cure Disease", 1500);

        public MonitoredSpell GhostWolf = new MonitoredSpell("Ghost Wolf", 1500);
        public MonitoredSpell FarSight = new MonitoredSpell("Far Sight", 1500);
        public MonitoredSpell LightningShield = new MonitoredSpell("Lightning Shield", 1500);

        public MonitoredSpell RockbiterWeapon = new MonitoredSpell("Rockbiter Weapon", 1500);
        public MonitoredSpell WindfuryWeapon = new MonitoredSpell("Windfury Weapon", 1500);
        public MonitoredSpell FrostbrandWeapon = new MonitoredSpell("Frostbrand Weapon", 1500);
        public MonitoredSpell FlametongueWeapon = new MonitoredSpell("Flametongue Weapon", 1500);

        // Debuff
        public MonitoredSpell Purge = new MonitoredSpell("Purge", 1500);

        // Totems
        public MonitoredSpell FrostResistanceTotem = new MonitoredSpell("Frost Resistance Totem", 1500);
        public MonitoredSpell FireResistanceTotem = new MonitoredSpell("Fire Resistance Totem", 1500);
        public MonitoredSpell NatureResistanceTotem = new MonitoredSpell("Nature Resistance Totem", 1500);

        public MonitoredSpell GraceOfAirTotem = new MonitoredSpell("Grace of Air Totem", 1500);
        public MonitoredSpell WindwallTotem = new MonitoredSpell("Windwall Totem", 1500);
        public MonitoredSpell WindfuryTotem = new MonitoredSpell("Windfury Totem", 1500);
        public MonitoredSpell TranquilAirTotem = new MonitoredSpell("Tranquil Air Totem", 1500);
        public MonitoredSpell GroundingTotem = new MonitoredSpell("Grounding Totem", 1500);
        public MonitoredSpell SentryTotem = new MonitoredSpell("Sentry Totem", 1500);

        public MonitoredSpell FlametongueTotem = new MonitoredSpell("Flametongue Totem", 1500);
        public MonitoredSpell SearingTotem = new MonitoredSpell("Searing Totem", 1500);
        public MonitoredSpell MagmaTotem = new MonitoredSpell("Magma Totem", 1500);
        public MonitoredSpell FireNovaTotem = new MonitoredSpell("Fire Nova Totem", 1500);

        public MonitoredSpell StrengthOfEarthTotem = new MonitoredSpell("Strength of Earth Totem", 1500);
        public MonitoredSpell StoneclawTotem = new MonitoredSpell("Stoneclaw Totem", 1500);
        public MonitoredSpell StoneskinTotem = new MonitoredSpell("Stoneskin Totem", 1500);
        public MonitoredSpell TremorTotem = new MonitoredSpell("Tremor Totem", 1500);

        public MonitoredSpell ManaSpringTotem = new MonitoredSpell("Mana Spring Totem", 1500);
        public MonitoredSpell HealingStreamTotem = new MonitoredSpell("Healing Stream Totem", 1500);
        public MonitoredSpell PoisonCleansingTotem = new MonitoredSpell("Poison Cleansing Totem", 1500);
        public MonitoredSpell DiseaseCleansingTotem = new MonitoredSpell("Disease Cleansing Totem", 1500);
    }
}
