using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using robotManager.Products;
using System.Collections.Generic;
using System.Threading;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Helpers;
using wManager;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.States
{

    /* 
     * ==============================================================
     * GoToTheHalfhillMarketState
     * ==============================================================
    */
    class GoToTheHalfhillMarket : State
    {
        public override string DisplayName
        {
            get { return "GoTo The Halfhill Market"; }
        }

        public override int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        private int _priority;

        robotManager.Helpful.Timer _timerGoTo = new robotManager.Helpful.Timer(MyFishingSettings.GoToTheHalfhillMarketTime);
        public override bool NeedToRun
        {
            get
            {
                if (!Usefuls.InGame ||
                    Usefuls.IsLoadingOrConnecting ||
                    ObjectManager.Me.IsDeadMe ||
                    !ObjectManager.Me.IsValid ||
                    !Products.IsStarted)
                    return false;

                return _timerGoTo.IsReady;
            }
        }

        public override List<State> NextStates
        {
            get { return new List<State>(); }
        }

        public override List<State> BeforeStates
        {
            get { return new List<State>(); }
        }

        public override void Run()
        {
            // Combine
            MovementManager.StopMove();
            Thread.Sleep(700);
            while (ObjectManager.Me.IsCast)
            {
                Thread.Sleep(150);
            }
            Thread.Sleep(1000);
            foreach (var item in MyFishingSettings.EmptyContainer)
            {
                for (int i = 1; i <= MyFishingSettings.EmptyContainerByTypeAtBuy; i++)
                {
                    var name = ItemsManager.GetNameById(item);
                    Lua.RunMacroText("/use " + name);
                    Logging.WriteDebug("Combine item " + name);
                    Thread.Sleep(1000);
                    while (ObjectManager.Me.IsCast)
                    {
                        Thread.Sleep(150);
                    }
                    Thread.Sleep(150);
                }
            }

            // Go to Merchant Cheng:
            if (GoToTask.ToPositionAndIntecractWithNpc(MyFishingSettings.MerchantChengPosition, MyFishingSettings.MerchantChengEntry))
            {
                Thread.Sleep(1000);
                foreach (var item in MyFishingSettings.EmptyContainer)
                {
                    var name = ItemsManager.GetNameById(item);
                    if (ItemsManager.GetItemCountByNameLUA(name) < MyFishingSettings.EmptyContainerByTypeAtBuy)
                    {
                        Vendor.BuyItem(name, MyFishingSettings.EmptyContainerByTypeAtBuy);
                        Logging.WriteDebug("Buy item " + name);
                        Thread.Sleep(1000);
                    }
                }

                Thread.Sleep(1500);
            }
            else
            {
                Logging.WriteDebug("No found npc Merchant Cheng");
            }

            // Go to Nam Ironpaw:
            if (GoToTask.ToPositionAndIntecractWithNpc(MyFishingSettings.NamIronpawPosition, MyFishingSettings.NamIronpawEntry))
            {
                Thread.Sleep(1000);
                Lua.LuaDoString("SelectGossipAvailableQuest(1); CompleteQuest(); GetQuestReward();");
                Thread.Sleep(2000);
            }
            else
            {
                Logging.WriteDebug("No found npc Nam Ironpaw");
            }

            if (Products.IsStarted && Usefuls.InGame &&
                !ObjectManager.Me.IsDeadMe &&
                !(ObjectManager.Me.InCombat && !(ObjectManager.Me.IsMounted && (wManagerSetting.CurrentSetting.IgnoreFightGoundMount || Usefuls.IsFlyableArea))))
            {
                // If Ok reset timer
                _timerGoTo = new robotManager.Helpful.Timer(MyFishingSettings.GoToTheHalfhillMarketTime);
            }
        }
    }
}
