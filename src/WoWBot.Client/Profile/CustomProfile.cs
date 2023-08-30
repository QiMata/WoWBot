using System;
using System.Threading;
using System.Threading.Tasks;
using AdvancedQuester.FSM.States;
using Custom_Profile;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using wManager;
using wManager.Events;
using wManager.Wow.Bot.States;
using wManager.Wow.Helpers;
using WoWBot.Client.Database;
using WoWBot.Client.Helpers;
using WoWBot.Client.NpcBase;
using WoWBot.Client.States;

class CustomProfile : ICustomProfile
{
    private static readonly Engine Fsm = new Engine();
    public static bool NeedsGearCheck = false;

    public void Pulse()
    {
        try
        {
            // Update spell list
            SpellManager.UpdateSpellBook();

            // Load CC:
            CustomClass.LoadCustomClass();

            //ItemDatabase.GetSampleItems();

            wManagerSetting.CurrentSetting.PathFinderFromServer = false;
            wManagerSetting.CurrentSetting.EquipAvailableBagIfFreeContainerSlot = true;
            wManagerSetting.CurrentSetting.TrainNewSkills = true;
            wManagerSetting.CurrentSetting.AvoidWallWithRays = false;
            wManagerSetting.CurrentSetting.MinFreeBagSlotsToGoToTown = 2;
            wManagerSetting.CurrentSetting.Save();

            ConfigWowForThisBot.ConfigWow();

            LootingEvents.OnLootSuccessful += (unit) =>
            {
                InventoryHelper.UpgradeToLatestEquipment();
            };

            Reevaluate();
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
            Logging.WriteError("CustomProfile > Cause: " + e.InnerException);
        }
    }

    public void Dispose()
    {
        try
        {
            MovementManager.StopMove();
            Fight.StopFight();
            Fsm.StopEngine();
            CustomClass.DisposeCustomClass();
        }
        catch (Exception e)
        {
            Logging.WriteError("CustomProfile > Dispose(): " + e);
        }
    }

    public static void Reevaluate()
    {
        Logging.Write("[CustomProfile] Reevaluate");
        Task.Run(() =>
        {
            Fsm.StopEngine();

            Thread.Sleep(1000);

            Fsm.States.Clear();

            Fsm.AddState(new Resurrect { Priority = 15 });
            Fsm.AddState(new MyMacro { Priority = 14 });
            Fsm.AddState(new FightHostileTarget { Priority = 13 });
            Fsm.AddState(new IsAttacked { Priority = 12 });
            Fsm.AddState(new Regeneration { Priority = 11 });
            Fsm.AddState(new Looting { Priority = 10 });
            Fsm.AddState(new ToTown { Priority = 6 });
            Fsm.AddState(new Talents { Priority = 5 });
            Fsm.AddState(new Trainers { Priority = 4 });
            Fsm.AddState(new Questing { Priority = 2 });

            Fsm.AddState(new Idle { Priority = 0 });

            Logging.Write("[CustomProfile] Starting engine");

            Fsm.States.Sort();
            Fsm.StartEngine(10);
        });
    }
}


