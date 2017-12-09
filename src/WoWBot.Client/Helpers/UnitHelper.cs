using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using robotManager.Helpful;
using wManager.Wow.Helpers;

namespace WoWBot.Client.Helpers
{
    public static class UnitHelper
    {
        public static void Name()
        {
            var units = wManager.Wow.ObjectManager.ObjectManager.GetObjectWoWUnit();
        }
    }
}
