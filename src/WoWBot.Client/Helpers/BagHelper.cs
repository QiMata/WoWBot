using wManager.Wow.Helpers;

namespace WoWBot.Client.Helpers
{
    public class BagHelper
    {
        public static bool IsFull()
        {
            return Bag.GetContainerNumFreeSlots == 0;
        }
    }
}
