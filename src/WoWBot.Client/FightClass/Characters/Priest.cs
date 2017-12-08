using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Wow;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using Timer = robotManager.Helpful.Timer;

class Main : ICustomClass
{
    private readonly uint _wowBase = (uint)Memory.WowMemory.Memory.GetProcess().MainModule.BaseAddress;

    public Spell Shoot = new Spell("Shoot");

    // Buff:
    public Spell PowerWordFortitude = new Spell("Power Word: Fortitude");
    public Spell InnerFire = new Spell("Inner Fire");

    //Protection:
    public Spell PowerWordShield = new Spell("Power Word: Shield");
    public Spell Renew = new Spell("Renew");
    public WoWSpell FlashHeal = new WoWSpell("Flash Heal",1500);
    public Spell DesperatePrayer = new Spell("Desperate Prayer");

    //Attack:
    public Spell ShadowWordPain = new Spell("Shadow Word: Pain");
    public WoWSpell HolyFire = new WoWSpell("Holy Fire",3500);

    private bool _isLaunched;

    public void Initialize()
    {
        _isLaunched = true;
        Logging.Write("Lock FC Is initialized.");
        Rotation();
    }

    private void Rotation()
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
                        Buff();
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

    private void Buff()
    {
        if (PowerWordFortitude.KnownSpell && !ObjectManager.Me.HaveBuff("Power Word: Fortitude"))
        {
            PowerWordFortitude.Launch();
        }
        if (InnerFire.KnownSpell && !ObjectManager.Me.HaveBuff("Inner Fire"))
        {
            InnerFire.Launch();
        }
    }

    public void Dispose()
    {
        _isLaunched = false;
        Logging.Write("Lock FC Stop in progress.");
    }

    public void ShowConfiguration()
    {
        
    }

    public bool WandActive()
    {
        return Memory.WowMemory.Memory.ReadInt32(_wowBase + 0x7E0BA0) > 0;
    }

    public void CombatRotation()
    {
        // auto tag avoid 
        if (Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause && Fight.InFight)
        {
            if (Lua.LuaDoString<bool>(@"return (UnitIsTapped(""target"")) and (not UnitIsTappedByPlayer(""target""));"))
            {
                Fight.StopFight();
                Lua.LuaDoString("ClearTarget();");
                System.Threading.Thread.Sleep(400);
            }
        }
        //Desperate Prayer
        if (ObjectManager.Me.IsAlive && DesperatePrayer.KnownSpell && ObjectManager.Me.HealthPercent <= 10)
        {
            DesperatePrayer.Launch();
            Thread.Sleep(Usefuls.Latency + 500);
        }
        //Power Word: Shield
        if (ObjectManager.Me.IsAlive && PowerWordShield.KnownSpell && ObjectManager.Me.HealthPercent <= 60 && !ObjectManager.Me.HaveBuff("Weakened Soul"))
        {
            PowerWordShield.Launch();
            Thread.Sleep(Usefuls.Latency + 500);
        }
        //Flash Heal
        if (ObjectManager.Me.IsAlive && FlashHeal.KnownSpell && ObjectManager.Me.HealthPercent <= 30)
        {
            FlashHeal.Launch();
            Thread.Sleep(Usefuls.Latency + 500);
        }
        //Renew
        if (ObjectManager.Me.IsAlive && Renew.KnownSpell && ObjectManager.Me.HealthPercent <= 60 && !ObjectManager.Me.HaveBuff("Renew"))
        {
            Renew.Launch();
            Thread.Sleep(Usefuls.Latency + 500);
        }
        //Holy Fire
        if (ObjectManager.Me.IsAlive && HolyFire.KnownSpell && ObjectManager.Target.HealthPercent >= 70 &&
            !ObjectManager.Target.HaveBuff("Holy Fire"))
        {
            HolyFire.Launch();
            Thread.Sleep(Usefuls.Latency + 1000);
        }
        //Shadow Word: Pain
        if (ObjectManager.Me.IsAlive && ShadowWordPain.KnownSpell && ObjectManager.Target.HealthPercent >= 20 &&
            !ObjectManager.Target.HaveBuff("Shadow Word: Pain"))
        {
            ShadowWordPain.Launch();
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

    public float Range { get { return 24.0f; } }

    public class WoWSpell : Spell
    {
        private Timer _timer;

        #region Constructor

        /// <summary>
        /// Creates a new instance of the <see cref="WoWSpell"/> class.
        /// </summary>
        /// <param name="spellNameEnglish">The spell name.</param>
        /// <param name="cooldownTimer">The cooldown time.</param>
        public WoWSpell(string spellNameEnglish, double cooldownTimer)
            : base(spellNameEnglish)
        {
            // Set timer
            this._timer = new Timer(cooldownTimer);
        }

        #endregion

        #region Public

        public bool IsReady
        {
            get
            {
                return this._timer.IsReady;
            }
        }

        /// <summary>
        /// Casts the spell if it is ready.
        /// </summary>
        public new void Launch()
        {
            // Is ready?
            if (!this.IsReady)
            {
                // Return
                return;
            }

            // Call launch
            base.Launch();

            // Reset timer
            this._timer.Reset();
        }

        #endregion
    }
}
