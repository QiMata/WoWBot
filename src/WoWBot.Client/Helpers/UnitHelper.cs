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
