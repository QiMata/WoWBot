using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using robotManager.Products;
using System.Collections.Generic;
using System.Threading;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.States
{
    /* 
 * ==============================================================
 * FishingState
 * ==============================================================
*/
    public class GoneFishing : State
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
            if (ObjectManager.Me.Position.DistanceTo2D(MyFishingSettings.FisherbotPosition) >= 1.5f)
            {
                if (!GoToTask.ToPosition(MyFishingSettings.FisherbotPosition, 1))
                {
                    Logging.Write("Go to fish position failed");
                    return;
                }
            }

            // Stop move
            MovementManager.StopMove();
            MountTask.DismountMount();

            // Face
            ObjectManager.Me.Rotation = MyFishingSettings.FisherbotRotation;
            Keybindings.PressKeybindings(wManager.Wow.Enums.Keybindings.STRAFELEFT);
            Keybindings.PressKeybindings(wManager.Wow.Enums.Keybindings.STRAFERIGHT);

            // Good position, start fishing
            FishingTask.LoopFish(0, MyFishingSettings.UseLure, MyFishingSettings.LureName);

            var timerFishing = new robotManager.Helpful.Timer(MyFishingSettings.GoToTheHalfhillMarketTime);
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

}


/*
 * SETTINGS
 */
static class MyFishingSettings
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