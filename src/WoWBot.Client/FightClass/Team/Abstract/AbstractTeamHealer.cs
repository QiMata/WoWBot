using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wManager.Wow.Bot.States;
using wManager.Wow.Class;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;
using WoWUnit = WoWBot.Common.WoWUnit;

namespace WoWBot.Client.FightClass.Team.Abstract
{
    abstract class AbstractTeamHealer : BaseClassWithRotation
    {
        private readonly float _healthPercentBeforeHealingThreshold;

        protected AbstractTeamHealer(float range, float healthPercentBeforeHealingThreshold) : base(range)
        {
            _healthPercentBeforeHealingThreshold = healthPercentBeforeHealingThreshold;
        }

        protected override void CombatRotation()
        {
            if (ObjectManager.GetWoWUnitHostile().Any(x => x.IsTargetingMe))
            {
                HandleBeingTarget();
            }

            IEnumerable<WoWPlayer> partyMembersNeedHealing = GetPartyMembersNeedHealing();
            var membersNeedHealing = partyMembersNeedHealing as IList<WoWPlayer> ?? partyMembersNeedHealing.ToList();
            if (membersNeedHealing.Any())
            {
                HealPartyMembers(membersNeedHealing);
            }
            else
            {
                Attack();
            }

            if (NeedMana())
            {
                Drink();
            }
        }

        protected abstract void HandleBeingTarget();


        protected virtual bool NeedMana()
        {
            return ObjectManager.Me.ManaPercentage < 60;
        }

        protected abstract void Attack();

        protected abstract void HealPartyMembers(IList<WoWPlayer> membersNeedHealing);

        private IEnumerable<WoWPlayer> GetPartyMembersNeedHealing()
        {
            return wManager.Wow.Helpers.Party.GetParty()
                .Union(new []{ ObjectManager.Me })
                .Where(x => x.HealthPercent <= _healthPercentBeforeHealingThreshold);
        }

        protected override bool TeamInCombat()
        {
            //i dunno
            return wManager.Wow.Helpers.Party.GetParty().Select(x => x.InCombat).Any(x => x);
        }

        
    }
}
