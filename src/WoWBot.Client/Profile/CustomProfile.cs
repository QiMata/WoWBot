using System;
using System.Collections.Generic;
using System.Threading;
using AdvancedQuester.FSM.States;
using AdvancedQuester.Quest;
using AdvancedQuester.TestQuests;
using Custom_Profile;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using robotManager.Products;
using wManager;
using wManager.Wow.Bot.States;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using Timer = robotManager.Helpful.Timer;

class CustomProfile : ICustomProfile
{
    private static readonly Engine Fsm = new Engine();

    public void Pulse()
    {
        try
        {
            // Update spell list
            SpellManager.UpdateSpellBook();

            // Load CC:
            CustomClass.LoadCustomClass(); 
            
            QuestBoard.Instance.AddRange(new List<QuestTask>
            {
                new YourPlaceInTheWorld(),
                new CuttingTeeth()
            });

            // FSM
            Fsm.States.Clear();

            Fsm.AddState(new Relogger { Priority = 200 });
            Fsm.AddState(new StopBotIf { Priority = 100 });
            Fsm.AddState(new Pause { Priority = 16 });
            Fsm.AddState(new Resurrect { Priority = 14 });
            Fsm.AddState(new MyMacro { Priority = 13 });
            Fsm.AddState(new IsAttacked { Priority = 12 });
            Fsm.AddState(new Regeneration { Priority = 11 });
            Fsm.AddState(new Looting { Priority = 10 });
            Fsm.AddState(new Farming { Priority = 9 });
            Fsm.AddState(new MillingState { Priority = 8 });
            Fsm.AddState(new ProspectingState { Priority = 7 });
            Fsm.AddState(new ToTown { Priority = 6 });
            Fsm.AddState(new Talents { Priority = 5 });
            Fsm.AddState(new Trainers { Priority = 4 });

            Fsm.AddState(new Questing { Priority = 3 });
            Fsm.AddState(new GoToTheHalfhillMarketState { Priority = 2 });
            Fsm.AddState(new FishingState { Priority = 1 });

            Fsm.AddState(new Idle { Priority = 0 });

            Fsm.States.Sort();
            Fsm.StartEngine(10);
        }
        catch (Exception e)
        {
            try
            {
                Dispose();
            }
            catch
            {
            }
            Logging.WriteError("CustomProfile > Pulse(): " + e);
        }
    }

    public void Dispose()
    {
        try
        {
            CustomClass.DisposeCustomClass();
            Fsm.StopEngine();
            Fight.StopFight();
            MovementManager.StopMove();
            FishingTask.StopLoopFish();
        }
        catch (Exception e)
        {
            Logging.WriteError("CustomProfile > Dispose(): " + e);
        }
    }
}

/*
 * SETTINGS
 */
static class MySettings
{
    public const bool UseLure = true;
    public const string LureName = "";
    public static readonly Vector3 FisherbotPosition = new Vector3(-327.8808f, 432.5651f, 148.7676f, "Flying");
    public const float FisherbotRotation = 4.309353f;

    public const int GoToTheHalfhillMarketTime = 1000 * 60 * 10; // 10 minute

    public static readonly Vector3 NamIronpawPosition = new Vector3(-245.1719f, 578.0278f, 167.5478f, "Flying");
    public static readonly int NamIronpawEntry = 64395;

    public static readonly Vector3 MerchantChengPosition = new Vector3(-275.9375f, 599.6597f, 167.5479f, "Flying");
    public static readonly int MerchantChengEntry = 64940;

    public static readonly List<uint> EmptyContainer = new List<uint>
                                                        {
                                                            87686, // Empty Golden Carp Container
                                                            87680, // Empty Emperor Salmon Container
                                                        };

    public static readonly int EmptyContainerByTypeAtBuy = 1;
}

/* 
 * ==============================================================
 * FishingState
 * ==============================================================
*/
class FishingState : State
{
    public override string DisplayName
    {
        get { return "Fishing State"; }
    }

    public override int Priority
    {
        get { return _priority; }
        set { _priority = value; }
    }

    private int _priority;

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

            return true;
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
        // Go to position:
        if (ObjectManager.Me.Position.DistanceTo2D(MySettings.FisherbotPosition) >= 1.5f)
        {
            if (!GoToTask.ToPosition(MySettings.FisherbotPosition, 1))
            {
                Logging.Write("Go to fish position failed");
                return;
            }
        }

        // Stop move
        MovementManager.StopMove();
        MountTask.DismountMount();

        // Face
        ObjectManager.Me.Rotation = MySettings.FisherbotRotation;
        Keybindings.PressKeybindings(wManager.Wow.Enums.Keybindings.STRAFELEFT);
        Keybindings.PressKeybindings(wManager.Wow.Enums.Keybindings.STRAFERIGHT);

        // Good position, start fishing
        FishingTask.LoopFish(0, MySettings.UseLure, MySettings.LureName);

        var timerFishing = new Timer(MySettings.GoToTheHalfhillMarketTime);
        while (Products.IsStarted &&
               !ObjectManager.Me.IsDeadMe &&
               !ObjectManager.Me.InCombat &&
               !timerFishing.IsReady &&
               FishingTask.IsLaunched)
        {
            Thread.Sleep(300);
        }

        FishingTask.StopLoopFish();
    }
}

/* 
 * ==============================================================
 * GoToTheHalfhillMarketState
 * ==============================================================
*/
class GoToTheHalfhillMarketState : State
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

    Timer _timerGoTo = new Timer(MySettings.GoToTheHalfhillMarketTime);
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
        foreach (var item in MySettings.EmptyContainer)
        {
            for (int i = 1; i <= MySettings.EmptyContainerByTypeAtBuy; i++)
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
        if (GoToTask.ToPositionAndIntecractWithNpc(MySettings.MerchantChengPosition, MySettings.MerchantChengEntry))
        {
            Thread.Sleep(1000);
            foreach (var item in MySettings.EmptyContainer)
            {
                var name = ItemsManager.GetNameById(item);
                if (ItemsManager.GetItemCountByNameLUA(name) < MySettings.EmptyContainerByTypeAtBuy)
                {
                    Vendor.BuyItem(name, MySettings.EmptyContainerByTypeAtBuy);
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
        if (GoToTask.ToPositionAndIntecractWithNpc(MySettings.NamIronpawPosition, MySettings.NamIronpawEntry))
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
            _timerGoTo = new Timer(MySettings.GoToTheHalfhillMarketTime);
        }
    }
}
