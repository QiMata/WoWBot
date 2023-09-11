﻿using robotManager.Helpful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.FightClass.Team.Abstract;
using WoWBot.Client.Helpers;

namespace WoWBot.Client.FightClass.Team.Shaman
{
    class ShamanDPS : AbstractTeamDPS
    {
        private readonly BaseShaman _baseShaman;

        public ShamanDPS(float range) : base(range)
        {
            _baseShaman = new BaseShaman();
        }

        protected override void Buff()
        {
            if (ObjectManager.Me.HealthPercent <= 60 && !Fight.InFight)
            {
                MovementManager.StopMoveTo(false, 3000);
                _baseShaman.HealingWave.Launch();
                Usefuls.WaitIsCasting();
            }

            if (_baseShaman.LightningShield.KnownSpell && ObjectManager.Me.ManaPercentage > 70 && !ObjectManager.Me.HaveBuff("Lightning Shield"))
            {
                _baseShaman.LightningShield.Launch();
            }


            if (ObjectManager.Target.IsNpcVendor && !ObjectManager.Me.InCombatFlagOnly && ObjectManager.Me.HaveBuff("Ghost Wolf"))
            {
                _baseShaman.GhostWolf.Launch();
            }

            // break Wolf for the quest man

            if ((ObjectManager.Target.Reaction == wManager.Wow.Enums.Reaction.Friendly) && !ObjectManager.Me.InCombatFlagOnly && ObjectManager.Me.HaveBuff("Ghost Wolf"))
            {
                _baseShaman.GhostWolf.Launch();
            }
            if ((ObjectManager.Target.Reaction == wManager.Wow.Enums.Reaction.Honored) && !ObjectManager.Me.InCombatFlagOnly && ObjectManager.Me.HaveBuff("Ghost Wolf"))
            {
                _baseShaman.GhostWolf.Launch();
            }
            if ((ObjectManager.Target.Reaction == wManager.Wow.Enums.Reaction.Revered) && !ObjectManager.Me.InCombatFlagOnly && ObjectManager.Me.HaveBuff("Ghost Wolf"))
            {
                _baseShaman.GhostWolf.Launch();
            }

            var nodesNearMe = ObjectManager.GetObjectWoWGameObject().FindAll(p => p.GetDistance <= 8 && p.CanOpen);

            // break wolf for the nodes 
            if (nodesNearMe.Count > 0 && !ObjectManager.Me.InCombatFlagOnly && ObjectManager.Me.HaveBuff("Ghost Wolf"))
            {
                _baseShaman.GhostWolf.Launch();
            }
            // break wolf for the trainer man
            if (ObjectManager.Target.IsNpcTrainer && !ObjectManager.Me.InCombatFlagOnly && ObjectManager.Me.HaveBuff("Ghost Wolf"))
            {
                _baseShaman.GhostWolf.Launch();
            }
        }

        protected override void Attack()
        {
            Logging.WriteDebug("Attack");
        }

        protected override void HandleBeingTarget()
        {
            Logging.WriteDebug("HandleBeingTarget");
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
                if (woWPlayer.HealthPercent < 50)
                {
                    Logging.WriteDebug("Flash Heal");
                    woWPlayer.TargetPlayer();
                    _baseShaman.LesserHealingWave.Launch();
                }
                if (woWPlayer.HealthPercent < 80)
                {
                    Logging.WriteDebug("Renew");
                    woWPlayer.TargetPlayer();
                    _baseShaman.HealingWave.Launch();
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
            List<WoWUnit> targets = ObjectManager.GetObjectWoWUnit().FindAll(x => x.Guid == ObjectManager.Me.Target);

            if (targets.Count > 0)
            {
                WoWUnit target = targets[0];

                if (Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause && Fight.InFight)
                {
                    if (Lua.LuaDoString<bool>(@"return (UnitIsTapped(""target"")) and (not UnitIsTappedByPlayer(""target""));")
                        || target.IsDead)
                    {
                        Fight.StopFight();
                        Lua.LuaDoString("ClearTarget();");
                        return;
                    }
                }

                Lua.RunMacroText("/assist " + ObjectManager.Me.Name);
                MovementManager.Face(ObjectManager.GetObjectByGuid(ObjectManager.Me.Target).Position);

                if (target.Position.DistanceTo(ObjectManager.Me.Position) < 0.5)
                {
                    Move.Backward(Move.MoveAction.DownKey, 50);
                    Thread.Sleep(50);
                }
                else if (target.Position.DistanceTo(ObjectManager.Me.Position) > 28)
                {
                    Move.Forward(Move.MoveAction.DownKey, 50);
                    Thread.Sleep(50);
                }

                // drop ghost wolf
                if (ObjectManager.Me.HaveBuff("Ghost Wolf") && ObjectManager.Me.InCombat)
                {
                    _baseShaman.GhostWolf.Launch();
                }

                if (ObjectManager.Me.HealthPercent <= 40 && ObjectManager.Me.ManaPercentage > 15)
                {
                    _baseShaman.LesserHealingWave.Launch();
                }
                if (!_baseShaman.LesserHealingWave.KnownSpell && ObjectManager.Me.HealthPercent <= 40 && ObjectManager.Me.ManaPercentage > 15)
                {
                    _baseShaman.HealingWave.Launch();
                }

                if (!_baseShaman.WeaponBuffStopwatch.IsRunning || _baseShaman.WeaponBuffStopwatch.Elapsed > TimeSpan.FromMinutes(5))
                {
                    if (!_baseShaman.WindfuryWeapon.KnownSpell)
                    {
                        if (_baseShaman.RockbiterWeapon.KnownSpell)
                        {
                            this._baseShaman.RockbiterWeapon.Launch();
                            _baseShaman.WeaponBuffStopwatch.Restart();
                        }
                    }
                    else
                    {
                        this._baseShaman.WindfuryWeapon.Launch();
                        _baseShaman.WeaponBuffStopwatch.Restart();
                    }
                }
                if (_baseShaman.FlameShock.KnownSpell
                    && !ObjectManager.Target.HaveBuff("Flame Shock")
                    && ObjectManager.Me.ManaPercentage > 37
                    && ObjectManager.Target.GetDistance < 17
                    && !ObjectManager.Target.HaveBuff("Stormstrike"))
                {
                    _baseShaman.FlameShock.Launch();
                }

                if (_baseShaman.EarthShock.KnownSpell
                    && ObjectManager.Me.ManaPercentage > 50
                    && ObjectManager.Me.Level < 12
                    && ObjectManager.Target.GetDistance < 17
                    && ObjectManager.Target.IsCast)
                {
                    _baseShaman.EarthShock.Launch();
                }

                if (_baseShaman.SearingTotem.KnownSpell
                    && ObjectManager.Me.ManaPercentage > 40
                    && ObjectManager.Target.HealthPercent >= 80
                    && ObjectManager.Target.GetDistance < 9)
                {
                    _baseShaman.SearingTotem.Launch();
                }

                if (_baseShaman.StoneskinTotem.KnownSpell
                    && ObjectManager.Me.ManaPercentage > 10
                    && !ObjectManager.Me.HaveBuff("Stoneskin")
                    && HostileUnitsInRange(15.0f) > 1)
                {
                    _baseShaman.StoneskinTotem.Launch();
                }

                if (_baseShaman.ManaSpringTotem.KnownSpell
                    && ObjectManager.Me.ManaPercentage > 10
                    && !ObjectManager.Me.HaveBuff("Mana Spring")
                    && HostileUnitsInRange(15.0f) > 1)
                {
                    _baseShaman.ManaSpringTotem.Launch();
                }

                if (_baseShaman.LightningBolt.KnownSpell && ObjectManager.Me.ManaPercentage > 50 && ObjectManager.Me.Position.DistanceTo(target.Position) < 30)
                {
                    _baseShaman.LightningBolt.Launch();
                }

                if (ObjectManager.Me.ManaPercentage < 25 && ObjectManager.Me.Position.DistanceTo(target.Position) > ObjectManager.Me.InteractDistance)
                {
                    Move.Forward(Move.MoveAction.DownKey, 50);
                    Thread.Sleep(50);
                }
            }
        }

        public static int HostileUnitsInRange(float range)
        {
            int hostileUnitsInRange = ObjectManager.GetUnitAttackPlayer().Count(u => u.GetDistance <= range);
            return hostileUnitsInRange;
        }

        protected override bool TeamInCombat()
        {
            return false;
        }
    }
}
