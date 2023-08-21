using AdvancedQuester.Quest;
using AdvancedQuester.Quests;
using System.Collections.Generic;

namespace WoWBot.Client.Quest.Quests.Bundles
{
    public class YourPlaceInTheWorldBundle : QuestBundle
    {
        public YourPlaceInTheWorldBundle()
        {
            QuestBundleName = "Your Place In The World Bundle";

            QuestTaskBundle.Clear();

            QuestTaskBundle.AddRange(new List<QuestTask>(){ new YourPlaceInTheWorld() });
        }
    }
}
