using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using robotManager.Helpful;
using wManager.Wow.Class;
using wManager.Wow.ObjectManager;
using WoWBot.Client.FightClass.Team.Abstract;
using WoWBot.Client.Helpers;

namespace WoWBot.Client.FightClass.Team.Priest
{
    class PriestHealer : AbstractTeamHealer
    {
        // Buff:
        public MonitoredSpell PowerWordFortitude = new MonitoredSpell("Power Word: Fortitude");
        public Spell InnerFire = new Spell("Inner Fire");
        public MonitoredSpell ShadowProtection = new MonitoredSpell("Shadow Protection");

        //Protection:
        public MonitoredSpell PowerWordShield = new MonitoredSpell("Power Word: Shield");
        public MonitoredSpell Renew = new MonitoredSpell("Renew");
        public MonitoredSpell FlashHeal = new MonitoredSpell("Flash Heal", 1500);
        public Spell DesperatePrayer = new Spell("Desperate Prayer");

        public PriestHealer(float range, float healthPercentBeforeHealingThreshold) : base(range, healthPercentBeforeHealingThreshold)
        {
        }

        protected override void Buff()
        {
            foreach (var player in wManager.Wow.Helpers.Party.GetParty())
            {
                BuffPlayer(player);
            }
        }

        private void BuffPlayer(WoWPlayer player)
        {
            BuffPlayer(player,PowerWordFortitude);
            BuffPlayer(player, ShadowProtection);
        }

        private void BuffPlayer(WoWPlayer player, MonitoredSpell spell)
        {
            player.TargetPlayer();
            if (!wManager.Wow.ObjectManager.ObjectManager.Target.HaveBuff(spell.Name)
                && player.GetDistance < spell.MaxRange)
            {
                spell.Cast();
            }
        }

        protected override void Attack()
        {
            
        }

        protected override void HealPartyMembers(IList<WoWPlayer> membersNeedHealing)
        {
            foreach (var woWPlayer in membersNeedHealing)
            {
                if (woWPlayer.HealthPercent < 80)
                {
                    woWPlayer.TargetPlayer();
                    PowerWordShield.Cast();
                }
                if (woWPlayer.HealthPercent < 80)
                {
                    woWPlayer.TargetPlayer();
                    Renew.Cast();
                }
                if (woWPlayer.HealthPercent < 50)
                {
                    woWPlayer.TargetPlayer();
                    FlashHeal.Cast();
                }
            }
        }
    }
}
