using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.Helpers
{
    public static class Extensions
    {
        public static void TargetPlayer(this WoWPlayer player)
        {
            wManager.Wow.Helpers.Interact.InteractGameObject(player.GetBaseAddress);
        }

        public static void TargetEnemy(this WoWUnit woWUnit)
        {
            wManager.Wow.Helpers.Interact.InteractGameObject(woWUnit.GetBaseAddress);
        }
    }
}
