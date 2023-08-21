using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdvancedQuester.FSM.States;
using AdvancedQuester.Quest;
using Custom_Profile;
using robotManager.Events;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using wManager.Wow.Bot.States;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Quest.Quests.Bundles;
using WoWBot.Client.States;

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

            QuestBoard.Instance.AddRange(new List<QuestBundle>
            {
                new YourPlaceInTheWorldBundle(),
                new CuttingTeethBundle(),
                new Level2Durator()
            });
            
            QuestBoard.LoadQuestProgress();

            FiniteStateMachineEvents.OnAfterRunState += (engine, state) =>
            {
                if (state != null && state.DisplayName == "To Town")
                {
                    Logging.Write("We have completed going To Town State.");

                    Reevaluate();
                }
            };

            FiniteStateMachineEvents.OnBeforeCheckIfNeedToRunState += (engine, state, cancelable) =>
            {
                if (state.DisplayName == "Movement Loop" && ObjectManager.Me.InCombatFlagOnly)
                {
                    Logging.Write("We have completed going To Town State.");

                    cancelable.Cancel = true;
                    Reevaluate();
                }
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

    public static void Reevaluate()
    {
        Logging.Write("Reevaluating priorities...");
        Task.Run(() =>
        {
            Fsm.StopEngine();

            Thread.Sleep(1000);

            Fsm.States.Clear();

            Fsm.AddState(new Relogger { Priority = 200 });
            Fsm.AddState(new StopBotIf { Priority = 100 });
            Fsm.AddState(new Pause { Priority = 16 });
            Fsm.AddState(new Resurrect { Priority = 15 });
            Fsm.AddState(new MyMacro { Priority = 14 });
            Fsm.AddState(new IsAttacked { Priority = 13 });
            Fsm.AddState(new FightHostileTarget { Priority = 12 });
            Fsm.AddState(new Regeneration { Priority = 11 });
            Fsm.AddState(new Looting { Priority = 10 });
            Fsm.AddState(new Farming { Priority = 9 });
            Fsm.AddState(new MillingState { Priority = 8 });
            Fsm.AddState(new ProspectingState { Priority = 7 });
            Fsm.AddState(new ToTown { Priority = 6 });
            Fsm.AddState(new Talents { Priority = 5 });
            Fsm.AddState(new Trainers { Priority = 4 });

            if (Bag.GetContainerNumFreeSlots > 2 && QuestBoard.Instance.HasQuests)
            {
                Fsm.AddState(new Questing { Priority = 3 });
            }

            Fsm.AddState(new Idle { Priority = 0 });

            Logging.Write("Starting finitie state machine again.");
            Fsm.States.Sort();
            Fsm.StartEngine(10);
        });
    }
}


