using System.Collections.Generic;
using System.Linq;
using System.Threading;
using robotManager.Products;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.FightClass.Team.Abstract
{
    abstract class AbstractTeamTank : BaseClassWithRotation
    {
        protected AbstractTeamTank(float range) : base(range)
        {
        }

        protected override void CombatRotation()
        {
            if (TeamInCombat())
            {
                if (LooseMob())
                {
                    GatherLooseMobs();
                }
                else
                {
                    TankRotation();
                }
            }
            else
            {
                while (Party.GetParty()
                    .Select(x => x.ManaPercentage)
                    .Where(x => x != 0)
                    .Any(x => x < 65))
                {
                    if (TeamInCombat())
                    {
                        break;
                    }
                    Products.InPause = true;
                    Thread.Sleep(100);
                }
                Products.InPause = false;
                PullNextMob();
            }
        }

        private bool TargetInRange()
        {
            var closestHostile = ObjectManager.GetWoWUnitHostile()
                .Where(x => !x.IsDead)
                .OrderBy(x => x.GetDistance)
                .FirstOrDefault();

            if (closestHostile == null)
            {
                return false;
            }

            return closestHostile.GetDistance < 30;
        }

        protected abstract void PullNextMob();

        protected abstract void TankRotation();

        protected abstract void GatherLooseMobs();

        protected virtual bool LooseMob()
        {
            return LooseMobs.Any();
        }

        public IEnumerable<WoWUnit> LooseMobs
        {
            get
            {
                return ObjectManager.GetWoWUnitHostile()
                 .Where(x => x.IsTargetingPartyMember);
            }
        }

        protected override bool TeamInCombat()
        {
            return Party.GetParty()
                .Union(new[] {ObjectManager.Me})
                .Select(x => x.InCombat)
                .Any(x => x);
        }

        protected override void Buff()
        {
            //Tank dont buff u
        }
    }
}
