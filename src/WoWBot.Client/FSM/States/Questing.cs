using AdvancedQuester.Quest;
using robotManager.FiniteStateMachine;

namespace AdvancedQuester.FSM.States
{
    public class Questing : State
    {
        public override bool NeedToRun => QuestBoard.Instance.HasQuests;

        public override void Run()
        {
            QuestBoard.Instance.GetNext(out var quest);
            if (quest == null) return;
            quest.PickUp();
            quest.Pulse();
            quest.TurnIn();
            QuestBoard.Instance.MarkComplete(quest);
        }
    }
}