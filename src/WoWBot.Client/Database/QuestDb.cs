using System.Collections.Generic;
using WoWBot.Client.Quest;
using WoWBot.Client.Quest.Quests;

namespace WoWBot.Client.Database
{
    public static class QuestDb
    {
        private static readonly List<QuestTask> AllQuests = new List<QuestTask>() {
            new APeonsBurden(),
            new BurningBladeMedallion(),
            new CallOfEarthPart1(),
            new CallOfEarthPart2(),
            new CallOfEarthPart3(),
            new CuttingTeeth(),
            new GalgarsCactusAppleSurprise(),
            new LazyPeons(),
            new ReportToSenJinVillage(),
            new RuneInscribedParchment(),
            new SarkothPart1(),
            new SarkothPart2(),
            new StingOfTheScorpid(),  
            new ThazzrilsPick(),
            new VileFamiliars(),
            new YourPlaceInTheWorld()
        };

        public static List<QuestTask> GetAllQuests()
        {
            return AllQuests;
        }

        public static QuestTask GetQuestTaskById(int id)
        {
            foreach(QuestTask task in AllQuests)
            {
                if (task.QuestId == id)
                {
                    return task;
                }
            }
            return new CuttingTeeth();
        }
    }
}
