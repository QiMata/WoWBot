using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

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
                        //if (Fight.InFight && TeamInCombat())
                        //{
                            CombatRotation();
                        ////}
                    }
                }
            }
            catch (Exception e)
            {
                Logging.WriteError("Lock FC  ERROR: " + e);
            }
            Thread.Sleep(100);
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

        public void ShowConfiguration()
        {
            
        }

        public float Range { get; }
    }
}
