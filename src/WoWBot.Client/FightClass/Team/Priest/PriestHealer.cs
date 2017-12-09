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
        public MonitoredSpell InnerFire = new MonitoredSpell("Inner Fire");
        public MonitoredSpell ShadowProtection = new MonitoredSpell("Shadow Protection");

        //Protection:
        public MonitoredSpell PowerWordShield = new MonitoredSpell("Power Word: Shield");
        public MonitoredSpell Renew = new MonitoredSpell("Renew");
        public MonitoredSpell FlashHeal = new MonitoredSpell("Lesser Heal", 2000);
        public MonitoredSpell DesperatePrayer = new MonitoredSpell("Desperate Prayer");

        public PriestHealer(float range, float healthPercentBeforeHealingThreshold) : base(range, healthPercentBeforeHealingThreshold)
        {
        }

        protected override void Buff()
        {
            ApplyFriendlyBuff(ObjectManager.Me,InnerFire);
            foreach (var player in wManager.Wow.Helpers.Party.GetParty().Union(new []{ ObjectManager.Me}))
            {
                BuffPlayer(player);
            }
        }

        private void BuffPlayer(WoWPlayer player)
        {
            ApplyFriendlyBuff(player,PowerWordFortitude);
            ApplyFriendlyBuff(player, ShadowProtection);
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
                    ApplyFriendlyBuff(woWPlayer, Renew);
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
