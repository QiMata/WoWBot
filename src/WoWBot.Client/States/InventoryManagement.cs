using robotManager.FiniteStateMachine;
using System.Threading;
using wManager.Wow.Helpers;

namespace WoWBot.Client.States
{
    public class InventoryManagement : State
    {
        public override bool NeedToRun => true;

        public override void Run()
        {
            Bag.OpenAllBags();
            Thread.Sleep(500);
            Bag.CloseAllBags();
        }
    }
}
