using System;
using System.Threading;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using wManager.Wow;
using WoWBot.Client.Helpers;

namespace WoWBot.Client.FightClass.Team.Warlock
{
    public class WarlockDPS : ICustomClass
    {
        public float Range { get { return 24.0f; } }
        private Random _r = new Random();
        private readonly uint _wowBase = (uint)Memory.WowMemory.Memory.GetProcess().MainModule.BaseAddress;
        private bool _isLaunched;
        //private ulong _lastTarget;
        //private ulong _currentTarget;
        //private uint _target;
        //uint oldTarget;

        public void Initialize() // When product started, initialize and launch Fightclass
        {
            _isLaunched = true;
            Logging.Write("Lock FC Is initialized.");
            Rotation();
        }

        public void Dispose() // When product stopped
        {
            _isLaunched = false;
            Logging.Write("Lock FC Stop in progress.");
        }

        public void ShowConfiguration() // When use click on Fight class settings
        {
        }


        // SPELLS:
        //
        // SPELLS:
        //Pet:
        private Spell SummonImp = new Spell("Summon Imp");
        private Spell SummonVoid = new Spell("Summon Voidwalker");
        public Spell HealthFunnel = new Spell("Health Funnel");

        // Buff:
        public Spell DemonSkin = new Spell("Demon Skin");
        public Spell DemonArmor = new Spell("Demon Armor");
        public Spell LifeTap = new Spell("Life Tap");

        // Close Combat:
        public Spell ShadowBolt = new Spell("Shadow Bolt");
        public MonitoredSpell Immolate = new MonitoredSpell("Immolate", 3500);
        public MonitoredSpell Corruption = new MonitoredSpell("Corruption", 2500);
        public MonitoredSpell SiphonLife = new MonitoredSpell("Siphon Life", 2500);
        public MonitoredSpell CurseofAgony = new MonitoredSpell("Curse of Agony", 2500);
        public Spell Shoot = new Spell("Shoot");
        public Spell DrainLife = new Spell("Drain Life");
        public Spell DrainSoul = new Spell("Drain Soul");

        public bool WandActive()
        {
            return Memory.WowMemory.Memory.ReadInt32(_wowBase + 0x7E0BA0) > 0;
        }

        internal void Rotation()
        {
            Logging.Write("Lock FC started.");
            while (_isLaunched)
            {
                try
                {
                    if (!Products.InPause)
                    {
                        if (!ObjectManager.Me.IsDeadMe)
                        {
                            PetManager();
                            Buff();
                            if (ObjectManager.Pet.IsValid && ObjectManager.Me.Target > 0 && ObjectManager.Target.IsAttackable)
                            {
                                Lua.LuaDoString("PetAttack();");
                            }
                            if (Fight.InFight && ObjectManager.Me.Target > 0)
                            {
                                CombatRotation();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logging.WriteError("Lock FC  ERROR: " + e);
                }

                Thread.Sleep(100); // Pause 10 ms to reduce the CPU usage.
            }
            Logging.Write("Lock FC  Is now stopped.");
        }

        internal void PetManager()
        {


            // call void
            if (!ObjectManager.Pet.IsValid && SummonVoid.KnownSpell && !ObjectManager.Me.IsDeadMe && !ObjectManager.Me.IsMounted)
            {
                SummonVoid.Launch(true);
                Thread.Sleep(Usefuls.Latency + 12000);
            }
            // Call imp- assumming void isnt known
            if (!ObjectManager.Pet.IsValid && SummonImp.KnownSpell && !SummonVoid.KnownSpell && !ObjectManager.Me.IsDeadMe && !ObjectManager.Me.IsMounted)
            {
                SummonImp.Launch(true);
                Thread.Sleep(Usefuls.Latency + 12000);
            }
            //try waiting around if no pet??
            if (!ObjectManager.Pet.IsValid && SummonImp.KnownSpell && !ObjectManager.Me.IsDeadMe && !ObjectManager.Me.IsMounted)
            {
                Thread.Sleep(Usefuls.Latency + 12000);
            }

        }


        public void Buff()
        {
            // demon skin
            if (DemonSkin.KnownSpell && !ObjectManager.Me.HaveBuff("Demon Skin") && !DemonArmor.KnownSpell && ObjectManager.Pet.IsValid)
            {
                DemonSkin.Launch();
            }

            // demon armor
            if (DemonArmor.KnownSpell && !ObjectManager.Me.HaveBuff("Demon Armor") && ObjectManager.Pet.IsValid)
            {
                DemonArmor.Launch();
            }

            // Lifetap
            if (LifeTap.KnownSpell && ObjectManager.Me.HealthPercent >= 60 && ObjectManager.Me.ManaPercentage < 40)
            {
                LifeTap.Launch();
            }

            //Health Funnel
            if (ObjectManager.Pet.IsValid && ObjectManager.Me.IsAlive && HealthFunnel.KnownSpell && HealthFunnel.IsDistanceGood && ObjectManager.Pet.HealthPercent <= 50 && ObjectManager.Me.HealthPercent >= 60)
            {
                HealthFunnel.Launch();
                //channeling ?
                return;
            }
        }


        internal void CombatRotation()
        {
            // auto tag avoid 
            if (Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause && Fight.InFight)
            {
                if (Lua.LuaDoString<bool>(@"return (UnitIsTapped(""target"")) and (not UnitIsTappedByPlayer(""target""));"))
                {
                    Fight.StopFight();
                    Lua.LuaDoString("ClearTarget();");
                    Thread.Sleep(400);
                }
            }
            if (ObjectManager.Pet.IsValid && ObjectManager.Me.IsAlive && HealthFunnel.KnownSpell && HealthFunnel.IsDistanceGood && ObjectManager.Pet.HealthPercent <= 30 && ObjectManager.Me.HealthPercent >= 60)
            {
                HealthFunnel.Launch();
                //channeling ?

            }
            if (ObjectManager.Target.HealthPercent >= 20 && ObjectManager.Me.HaveBuff("Shadow trance") && ObjectManager.Me.ManaPercentage > 15)
            {
                ShadowBolt.Launch();
                Thread.Sleep(Usefuls.Latency + 1000);
            }
            if (CurseofAgony.KnownSpell && ObjectManager.Target.HealthPercent >= 40 && !ObjectManager.Target.HaveBuff("Curse of Agony") && ObjectManager.Me.ManaPercentage > 15)
            {
                this.CurseofAgony.Launch();
                Thread.Sleep(Usefuls.Latency + 1200);
            }

            if (Corruption.KnownSpell && ObjectManager.Target.HealthPercent >= 40 && !ObjectManager.Target.HaveBuff("Corruption") && ObjectManager.Me.ManaPercentage > 15 && ObjectManager.Target.GetDistance < 25)
            {
                this.Corruption.Launch();
                Thread.Sleep(Usefuls.Latency + 1200);
            }

            if (SiphonLife.KnownSpell && ObjectManager.Target.GetDistance < 25 && ObjectManager.Target.HealthPercent >= 40 && !ObjectManager.Target.HaveBuff("Siphon Life") && ObjectManager.Me.ManaPercentage > 15)
            {
                this.SiphonLife.Launch();
                Thread.Sleep(Usefuls.Latency + 1200);
            }



            if (Immolate.KnownSpell && ObjectManager.Target.GetDistance < 25 && ObjectManager.Target.HealthPercent >= 50 && !ObjectManager.Target.HaveBuff("Immolate") && ObjectManager.Me.ManaPercentage > 15 && !SiphonLife.KnownSpell)
            {
                this.Immolate.Launch();
                Thread.Sleep(Usefuls.Latency + 1200);
            }

            if (DrainSoul.KnownSpell && ObjectManager.Target.GetDistance < 25 && ObjectManager.Target.HealthPercent <= 25 && ItemsManager.GetItemCountByNameLUA("Soul Shard") <= 3)
            {
                DrainSoul.Launch();
                Thread.Sleep(Usefuls.Latency + 500);
            }

            if (DrainLife.KnownSpell && ObjectManager.Target.GetDistance < 25 && ObjectManager.Me.ManaPercentage > 40 && ObjectManager.Me.HealthPercent <= 60 && ObjectManager.Target.HealthPercent >= 20 && ObjectManager.Target.GetDistance > 8)
            {
                DrainLife.Launch();
                Thread.Sleep(Usefuls.Latency + 500);
            }

            if (LifeTap.KnownSpell && ObjectManager.Me.HealthPercent >= 60 && ObjectManager.Me.ManaPercentage < 30)
            {
                LifeTap.Launch();
            }


            //low lvl showbolt
            if (!SummonVoid.KnownSpell && ObjectManager.Target.GetDistance < 25)
            {
                ShadowBolt.Launch();
                Thread.Sleep(Usefuls.Latency + 1000);
            }

            //shoot
            if (!Lua.LuaDoString<bool>("return IsAutoRepeatAction(" + (SpellManager.GetSpellSlotId(SpellListManager.SpellIdByName("Shoot")) + 1) + ")") && ObjectManager.Me.HealthPercent >= 60)
            {
                if (Shoot.KnownSpell)
                {
                    SpellManager.CastSpellByNameLUA("Shoot");
                    Thread.Sleep(Usefuls.Latency + 1000);
                }
                return;
            }
            //shoot
            if (!Lua.LuaDoString<bool>("return IsAutoRepeatAction(" + (SpellManager.GetSpellSlotId(SpellListManager.SpellIdByName("Shoot")) + 1) + ")") && ObjectManager.Target.GetDistance < 8)
            {
                if (Shoot.KnownSpell)
                {
                    SpellManager.CastSpellByNameLUA("Shoot");
                    Thread.Sleep(Usefuls.Latency + 1000);
                }
                return;
            }
        }
    }
}
