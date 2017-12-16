using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;

namespace WoWBot.Client.FightClass.Team.Warrior
{
    class WarriorTank : Abstract.AbstractTeamTank
    {
        private MonitoredSpell _sunderArmor = new MonitoredSpell("Sunder Armor");
        private MonitoredSpell _shieldBash = new MonitoredSpell("Shield Bash");
        private MonitoredSpell _taunt = new MonitoredSpell("Taunt");

        public WarriorTank(float range) : base(range)
        {
        }

        protected override void PullNextMob()
        {
            while (ObjectManager.Me.TargetObject == null)
            {
                Thread.Sleep(100);
            }
            while (!ObjectManager.Me.InCombat)
            {
                Thread.Sleep(100);
            }
        }

        protected override void TankRotation()
        {
            if (ObjectManager.Me.TargetObject.IsCast)
            {
                _shieldBash.Cast(false);
            }
            _sunderArmor.Cast();
        }

        protected override void GatherLooseMobs()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var closestLooseMob = LooseMobs
                .OrderBy(x => x.GetDistance)
                .FirstOrDefault();

            if (closestLooseMob != null)
            {
                while (stopwatch.Elapsed < TimeSpan.FromSeconds(5))
                {
                    closestLooseMob.TargetEnemy();

                    MovementManager.Face(closestLooseMob);
                    MovementManager.MoveTo(closestLooseMob);

                    if (_taunt.CooldownEnabled)
                    {
                        break;
                    }
                    _taunt.Cast();
                }
            }
        }
    }
}
