using System.Linq;

namespace WoWBot.Client.FightClass.Team.Abstract
{
    abstract class AbstractTeamDPS : BaseClassWithRotation
    {

        protected AbstractTeamDPS(float range) : base(range)
        {

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
