using System;
using AdvancedQuester.FSM.States;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using wManager.Wow.Bot.States;
using wManager.Wow.Helpers;

namespace AdvancedQuester.FSM
{
    public class AdvQstFsm
    {
        private static readonly Engine _fsm = new Engine();
        private static AdvQstFsm _instance;

        private AdvQstFsm()
        {
        }

        public static AdvQstFsm Instance => _instance ?? (_instance = new AdvQstFsm());

        public bool Pulse()
        {
            try
            {
                SpellManager.UpdateSpellBook();
                CustomClass.LoadCustomClass();

                _fsm.AddState(new Idle {Priority = 0});
                _fsm.AddState(new Questing {Priority = 1});

                _fsm.StartEngine(18, "AdvancedQuesterFsm", true);

                return true;
            }
            catch (Exception e)
            {
                Logging.WriteError(e.Message);
                return false;
            }
        }

        public void Stop()
        {
            _fsm.StopEngine();
        }
    }
}