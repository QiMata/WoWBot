using AdvancedQuester.Quest;
using AdvancedQuester.TestQuests;
using System.Collections.Generic;

namespace WoWBot.Client.Quest.Quests.Bundles
{
    public class CuttingTeethBundle : QuestBundle
    {
        public CuttingTeethBundle() {
            QuestBundleName = "Cutting Teeth Bundle";

            QuestTaskBundle.Clear();

            QuestTaskBundle.AddRange(new List<QuestTask>(){ new CuttingTeeth() });

            foreach (QuestTask task in QuestTaskBundle)
            {
                QuestObjectives.AddRange(task.QuestObjectives);
            }
        }
    }
}
