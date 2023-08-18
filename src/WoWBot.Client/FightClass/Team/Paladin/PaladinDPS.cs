using System;
using wManager.Wow.Class;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;

namespace WoWBot.Client.FightClass.Team.Paladin
{
    class PaladinDPS : Abstract.AbstractTeamDPS
    {
        #region Buff Spells

        readonly Spell _battleShout = new Spell("Battle Shout");
        readonly Spell _demoralizingShout = new Spell("Demoralizing Shout");

        readonly Spell _bloodrage = new Spell("Bloodrage");

        #endregion Buff Spells

        #region Offensive Spells

        readonly Spell _heroicStrike = new Spell("Heroic Strike");
        readonly Spell _rend = new Spell("Rend");
        readonly Spell _thunderClap = new Spell("Thunder Clap");
        readonly Spell _overpower = new Spell("Overpower");
        readonly Spell _execute = new Spell("Execute");

        readonly Spell _charge = new Spell("Charge");

        #endregion

        private MonitoredSpell _sunderArmor = new MonitoredSpell("Sunder Armor");
        private MonitoredSpell _shieldBash = new MonitoredSpell("Shield Bash");
        private MonitoredSpell _taunt = new MonitoredSpell("Taunt");

        public PaladinDPS(float range) : base(range)
        {
        }
        protected override void CombatRotation()
        {
            if (_execute.KnownSpell && ObjectManager.Target.HealthPercent < 20)
            {
                _execute.Launch();
            }
            if (_charge.IsSpellUsable && ObjectManager.Target.GetDistance <= 25 && ObjectManager.Target.GetDistance >= 8)
            {
                _charge.Launch();
            }
            if (_overpower.IsSpellUsable
                && ObjectManager.Me.RagePercentage > 5)
            {
                _overpower.Launch();
            }
            if (_bloodrage.IsSpellUsable && ObjectManager.Target.GetDistance <= Range + 2)
            {
                _bloodrage.Launch();
            }
            if (_battleShout.KnownSpell
                && ObjectManager.Me.RagePercentage > 10
                && !ObjectManager.Me.HaveBuff("Battle Shout"))
            {
                _battleShout.Launch();
            }

            if (_rend.KnownSpell
                && !ObjectManager.Target.HaveBuff("Rend")
                && ObjectManager.Me.RagePercentage > 10)
            {
                _rend.Launch();
            }
            if (_heroicStrike.KnownSpell
                && ObjectManager.Me.RagePercentage > 15)
            {
                _heroicStrike.Launch();
            }
        }

        protected override void Attack()
        {
            throw new NotImplementedException();
        }

        protected override void Buff()
        {
            throw new NotImplementedException();
        }


        protected override void HandleBeingTarget()
        {
            throw new NotImplementedException();
        }

        protected override void HealPartyMembers()
        {
            throw new NotImplementedException();
        }

        protected override bool TeamInCombat()
        {
            throw new NotImplementedException();
        }

        protected override void OptimizeGear()
        {
            throw new NotImplementedException();
        }
    }
}
