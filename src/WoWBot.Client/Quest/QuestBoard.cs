using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using wManager.Wow.Class;
using wManager.Wow.ObjectManager;

namespace AdvancedQuester.Quest
{
    public class QuestBoard
    {
        private static readonly List<QuestBundle> ToDo = new List<QuestBundle>();
        private static readonly List<QuestCompletion> Completed = new List<QuestCompletion>();
        private static QuestBoard _instance;

        private QuestBoard()
        {
        }

        public static QuestBoard Instance => _instance ?? (_instance = new QuestBoard());
        public QuestBundle ActiveQuest { get; private set; }
        public bool HasQuests => ToDo.Count > 0;

        public void AddRange(List<QuestBundle> quests)
        {
            ToDo.AddRange(quests);
        }

        public void Add(QuestBundle quest)
        {
            ToDo.Add(quest);
        }

        public void GetNext(out QuestBundle quest)
        {
            quest = null;

            if (ToDo.Count > 0)
            {
                quest = ToDo[0];
                ActiveQuest = quest;
            }
        }

        public void MarkComplete(QuestBundle questBundle)
        {
            foreach (QuestTask questTask in questBundle.QuestTaskBundle)
            {
                Completed.Add(new QuestCompletion() { QuestId = questTask.QuestId, Name = questTask.Name });
            }

            ToDo.Remove(questBundle);

            SaveQuestProgress();
        }
        public static void SaveQuestProgress()
        {
            File.WriteAllText(@Directory.GetCurrentDirectory() + "/" + ObjectManager.Me.Name + "Quest.json", JsonConvert.SerializeObject(Completed));
        }
        public static void LoadQuestProgress()
        {
            if (File.Exists(@Directory.GetCurrentDirectory() + "/" + ObjectManager.Me.Name + "Quest.json"))
            {
                string questCompletionJson = File.ReadAllText(@Directory.GetCurrentDirectory() + "/" + ObjectManager.Me.Name + "Quest.json");
                Completed.AddRange(JsonConvert.DeserializeObject<List<QuestCompletion>>(questCompletionJson));

                foreach(QuestCompletion questCompletion in Completed)
                {
                    foreach(QuestBundle questBundle in ToDo)
                    {
                        questBundle.QuestTaskBundle.RemoveAll(x => x.QuestId == questCompletion.QuestId);
                    }

                    ToDo.RemoveAll(x => x.QuestTaskBundle.Count == 0);
                }
            }
        }
    }
    public class QuestCompletion
    {
        public string Name { get; set; }
        public int QuestId { get; set; }
    }
}