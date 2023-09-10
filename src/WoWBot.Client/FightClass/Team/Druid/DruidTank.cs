using System;
using System.Diagnostics;
using System.Linq;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;

namespace WoWBot.Client.FightClass.Team.Druid
{
    class DruidTank : Abstract.AbstractTeamTank
    {
        private MonitoredSpell _sunderArmor = new MonitoredSpell("Sunder Armor");
        private MonitoredSpell _shieldSlam = new MonitoredSpell("Shield Bash");
        private MonitoredSpell _taunt = new MonitoredSpell("Taunt");
        private MonitoredSpell _revenge = new MonitoredSpell("Revenge");
        private MonitoredSpell _bloodRage = new MonitoredSpell("Bloodrage");
        private MonitoredSpell DefensiveStance = new MonitoredSpell("Defensive Stance");



        public DruidTank(float range) : base(range)
        {
        }

        protected override void Buff()
        {
            base.Buff();
            DefensiveStance.Cast();
        }

        protected override void PullNextMob()
        {
            var closestHostile = ObjectManager.GetWoWUnitHostile()
                .Where(x => !x.IsDead &&
                !TraceLine.TraceLineGo(ObjectManager.Me.Position, x.Position,CGWorldFrameHitFlags.HitTestSpellLoS))
                .OrderBy(x => x.GetDistance2D)
                .FirstOrDefault();

            if (closestHostile == null)
            {
                return;
            }
            
            closestHostile.TargetEnemy();
            MovementManager.Face(closestHostile);
            MovementManager.StopMove();
            SpellManager.CastSpellByNameLUA("Shoot Crossbow");
        }

        protected override void TankRotation()
        {
            if (ObjectManager.Me.TargetObject.IsCast)
            {
                _shieldSlam.Cast(false);
            }
            _revenge.Cast();
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
