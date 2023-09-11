using wManager.Wow.ObjectManager;

namespace WoWBot.Client.Helpers
{
    public static class UnitHelper
    {
        public static void Name()
        {
            var units = ObjectManager.GetObjectWoWUnit();
        }

        public static int GetNpcIdFromGuid(ulong guid)
        {
            return int.Parse(guid.ToString("X").Substring(6, 4), System.Globalization.NumberStyles.HexNumber);
        }
    }
}
