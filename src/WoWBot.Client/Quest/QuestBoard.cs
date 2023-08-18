using System.Collections.Generic;

namespace AdvancedQuester.Quest
{
    public class QuestBoard
    {
        private static readonly List<QuestTask> ToDo = new List<QuestTask>();
        private static readonly List<QuestTask> Completed = new List<QuestTask>();
        private static QuestBoard _instance;

        private QuestBoard()
        {
        }

        public static QuestBoard Instance => _instance ?? (_instance = new QuestBoard());
        public QuestTask ActiveQuest { get; private set; }
        public bool HasQuests => ToDo.Count > 0;

        public void AddRange(List<QuestTask> quests)
        {
            ToDo.AddRange(quests);
        }

        public void Add(QuestTask quest)
        {
            ToDo.Add(quest);
        }

        public void GetNext(out QuestTask quest)
        {
            quest = null;
            if (ToDo.Count > 0)
            {
                quest = ToDo[0];
                ActiveQuest = quest;
            }
        }

        public void MarkComplete(QuestTask quest)
        {
            if (ToDo.Contains(quest))
                ToDo.Remove(quest);
            Completed.Add(quest);
        }
    }
}