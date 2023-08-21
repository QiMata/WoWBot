using AdvancedQuester.Quest;
using AdvancedQuester.Quests;
using robotManager.Helpful;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;

namespace WoWBot.Client.Quest.Quests.Bundles
{
    public class Level2Durator : QuestBundle
    {
        public Level2Durator()
        {
            QuestBundleName = "Level 2 Durator Bundle";

            QuestTaskBundle.Clear();

            QuestTaskBundle.AddRange(new List<QuestTask>() { new StingOfTheScorpid(), new RuneInscribedParchment(), new VileFamiliars(), new GalgarsCactusAppleSurprise() });

            foreach (QuestTask task in QuestTaskBundle)
            {
                QuestObjectives.AddRange(task.QuestObjectives);
            }
        }
    }
}
