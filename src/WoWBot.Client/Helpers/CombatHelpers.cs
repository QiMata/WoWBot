using robotManager.Helpful;
using System.Threading;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.Helpers
{
    public class CombatHelpers
    {
        public static void FightTarget(WoWUnit target, bool loot = true)
        {
            lock (Fight.FightLock)
            {
                Logging.WriteFight("[Attack] " + target.Name + " Distance: " + target.Position.DistanceTo(ObjectManager.Me.Position));

                if (target.Position.DistanceTo(ObjectManager.Me.Position) <= 50)
                {
                    Logging.WriteFight("[Attack] " + target.Name + " (lvl " + target.Level + ")");
                    FightBG.StartFight(target.Guid);
                }
            }

            if (loot)
            {
                LootingTask.Pulse(ObjectManager.GetWoWUnitLootable());
            }
        }
    }
}
