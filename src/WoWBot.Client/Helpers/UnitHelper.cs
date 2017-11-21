using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using robotManager.Helpful;

namespace WoWBot.Client.Helpers
{
    public static class UnitHelper
    {
        public static void GetNearbyUnits()
        {
            var units = wManager.Wow.ObjectManager.ObjectManager.GetObjectWoWUnit();
        }
    }
}
