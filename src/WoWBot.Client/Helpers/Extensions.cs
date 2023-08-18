using System.Collections.Generic;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.Helpers
{
    public static class Extensions
    {
        public static void TargetPlayer(this WoWPlayer player)
        {
            Interact.InteractGameObject(player.GetBaseAddress);
        }

        public static void TargetEnemy(this WoWUnit woWUnit)
        {
            Interact.InteractGameObject(woWUnit.GetBaseAddress);
        }

        private static IEnumerable<WoWPlayer> _party;
        public static IEnumerable<WoWPlayer> GetParty()
        {
            if (_party == null)
            {
                var party = Party.GetParty();
                party.Add(ObjectManager.Me);
                _party = party;
            }
            return _party;
        }
    }
}
