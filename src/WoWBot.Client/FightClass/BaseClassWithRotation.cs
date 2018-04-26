using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;

namespace WoWBot.Client.FightClass
{
    abstract class BaseClassWithRotation : ICustomClass
    {
        private bool _isLaunched;

        public BaseClassWithRotation(float range)
        {
            Range = range;
        }

        public void Initialize()
        {
            Logging.Write("Started Fight Class: " + GetType().FullName);
            _isLaunched = true;
            //while (_isLaunched)
            while (true)
            {
                Rotation();
            }
            Logging.WriteError("Failed to stay in Rotation");
        }

        protected virtual void Rotation()
        {
            try
            {
                if (!Products.InPause)
                {
                    if (!ObjectManager.Me.IsDeadMe)
                    {
                        ManagePet();
                        Buff();
                        CombatRotation();
                    }
                }
            }
            catch (Exception e)
            {
                Logging.WriteError("Lock FC  ERROR: " + e);
            }
            Thread.Sleep(10);
        }

        protected abstract void CombatRotation();

        protected abstract bool TeamInCombat();

        protected abstract void Buff();

        protected virtual void ManagePet()
        {
            
        }

        public void Dispose()
        {
            _isLaunched = false;
        }

        protected void Drink()
        {
            var drinkName = wManager.wManagerSetting.CurrentSetting.DrinkName;
            do
            {
                if (!ObjectManager.Me.HaveBuff("Drink"))
                {
                    wManager.Wow.Helpers.ItemsManager.UseItem(drinkName);
                }
                Thread.Sleep(100);
            } while (NeedMana());
        }

        protected virtual bool NeedMana()
        {
            return false;
        }

        protected void ApplyFriendlyBuff(WoWPlayer player, MonitoredSpell spell, string buffName = null)
        {
            if (buffName == null)
            {
                buffName = spell.Name;
            }

            if (spell.KnownSpell && !player.HaveBuff(buffName)
                && player.GetDistance < spell.MaxRange)
            {
                player.TargetPlayer();
                spell.Cast();
            }
        }

        public void ShowConfiguration()
        {
            
        }

        public float Range { get; protected set; }
    }
}
