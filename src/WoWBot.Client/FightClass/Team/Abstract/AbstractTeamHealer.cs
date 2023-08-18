using System.Linq;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.FightClass.Team.Abstract
{
    abstract class AbstractTeamHealer : BaseClassWithRotation
    {
        protected AbstractTeamHealer(float range) : base(range)
        {
        }

        protected override void CombatRotation()
        {
            if (ObjectManager.GetWoWUnitHostile().Any(x => x.IsTargetingMe))
            {
                HandleBeingTarget();
            }
            HealPartyMembers();
            Attack();

            //if (NeedMana())
            //{
            //    Drink();
            //}
        }

        protected abstract void HandleBeingTarget();

        protected abstract void Attack();

        protected abstract void HealPartyMembers();

        protected override bool TeamInCombat()
        {
            //i dunno
            return wManager.Wow.Helpers.Party.GetParty().Select(x => x.InCombat).Any(x => x);
        }
    }
}
