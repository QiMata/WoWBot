using AdvancedQuester.Quest;
using Newtonsoft.Json;
using robotManager.FiniteStateMachine;
using System.Collections.Generic;
using System.IO;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;

namespace AdvancedQuester.FSM.States
{
    public class Questing : State
    {
        public override bool NeedToRun => QuestBoard.Instance.HasQuests;

        public override void Run()
        {
            if (QuestBoard.Instance.HasQuests)
            {
                QuestBoard.Instance.GetNext(out var quests);

                if (quests == null || !quests.CanDo())
                {
                    return;
                }

                if (!quests.IsPickedUp())
                {
                    quests.PickUp();
                }

                if (!quests.IsReadyToTurnIn())
                {
                    quests.Pulse();
                }

                if (!quests.IsTurnedIn())
                {
                    quests.TurnIn();
                }

                QuestBoard.Instance.MarkComplete(quests);
            }

            CustomProfile.Reevaluate();
        }
    }
}