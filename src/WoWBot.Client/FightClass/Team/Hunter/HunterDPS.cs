using System;
using System.Linq;
using robotManager.Helpful;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.FightClass.Team.Abstract;
using WoWBot.Client.Helpers;

namespace WoWBot.Client.FightClass.Team.Hunter
{
    class HunterDPS : AbstractTeamDPS
    {
        // Buff:
        public MonitoredSpell PowerWordFortitude = new MonitoredSpell("Power Word: Fortitude");
        public MonitoredSpell InnerFire = new MonitoredSpell("Inner Fire");
        public MonitoredSpell ShadowProtection = new MonitoredSpell("Shadow Protection");
        public MonitoredSpell Fade = new MonitoredSpell("Fade");

        //Protection:
        public MonitoredSpell PowerWordShield = new MonitoredSpell("Power Word: Shield");
        public MonitoredSpell Renew = new MonitoredSpell("Renew");
        public MonitoredSpell FlashHeal = new MonitoredSpell("Lesser Heal", 2000);
        public MonitoredSpell DesperatePrayer = new MonitoredSpell("Desperate Prayer");

        public HunterDPS(float range) : base(range)
        {
        }

        protected override void Buff()
        {
            if (!ObjectManager.Me.InCombat)
            {
                Logging.WriteDebug("Buff");
                ApplyFriendlyBuff(ObjectManager.Me, InnerFire);
                foreach (var player in Party.GetParty().Union(new[] {ObjectManager.Me}))
                {
                    BuffPlayer(player);
                }
            }
        }

        private void BuffPlayer(WoWPlayer player)
        {
            ApplyFriendlyBuff(player,PowerWordFortitude);
            ApplyFriendlyBuff(player, ShadowProtection);
        }

        protected override void Attack()
        {
            Logging.WriteDebug("Attack");
        }

        protected override void HandleBeingTarget()
        {
            Logging.WriteDebug("HandleBeingTarget");
            Fade.Cast(false);
        }

        protected override void HealPartyMembers()
        {
            try
            {
                Logging.WriteDebug("HealPartyMembers");
                var woWPlayer = Helpers.Extensions.GetParty().OrderBy(x => x.HealthPercent)
                    .FirstOrDefault();

                if (woWPlayer == null)
                {
                    return;
                }

                if (woWPlayer.HealthPercent < 30 && !woWPlayer.HaveBuff("Weakened Soul"))
                {
                    Logging.WriteDebug("Power Word Shield");
                    woWPlayer.TargetPlayer();
                    PowerWordShield.Cast();
                }
                if (woWPlayer.HealthPercent < 50)
                {
                    Logging.WriteDebug("Flash Heal");
                    woWPlayer.TargetPlayer();
                    FlashHeal.Cast();
                }
                if (woWPlayer.HealthPercent < 80)
                {
                    Logging.WriteDebug("Renew");
                    ApplyFriendlyBuff(woWPlayer, Renew);
                }
            }
            catch (Exception e)
            {
                Logging.WriteError(e.Message);
                throw;
            }
        }

        protected override void CombatRotation()
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
