using robotManager.Helpful;
using System;
using System.Threading;
using wManager.Wow.Helpers;

namespace WoWBot.Client.Helpers
{
    public static class DialogueHelper
    {
        public static void PickupQuestFromNpc()
        {
            Lua.LuaDoString($@"
                QuestFrameAcceptButton:Click();
            ");

            Thread.Sleep(300);
            Lua.LuaDoString($@"
                GossipTitleButton1:Click();
            ");

            Thread.Sleep(300);
            Lua.LuaDoString($@"
                QuestFrameAcceptButton:Click();
            ");

            Thread.Sleep(300);
            Lua.LuaDoString($@"
                QuestFrameAcceptButton:Click();
            ");

            Thread.Sleep(300);
            Lua.LuaDoString($@"
                QuestFrameGoodbyeButton:Click();
            ");
        }
        public static void TurnInQuestFromNpc(string questName, int questReward = 1)
        {
            for (int i = 1; i < 5; i++)
            {
                string questGossipText = GetQuestGossipOption(i);
                string questTitleText = GetQuestTitleOption(i);

                if (questGossipText.Equals(questName, StringComparison.Ordinal) || questTitleText.Equals(questName, StringComparison.Ordinal))
                {
                    Lua.LuaDoString($@"
                            GossipTitleButton{i}:Click()
                        ");

                    Thread.Sleep(300);
                    Lua.LuaDoString($@"
                            QuestTitleButton{i}:Click()
                        ");
                    break;
                }
            }

            CompleteOpenQuestFrame(questReward);
        }

        public static void CompleteOpenQuestFrame(int questReward)
        {
            if (Lua.LuaDoString<bool>($@"return QuestFrameCompleteButton:IsVisible()"))
            {
                Logging.WriteDebug("Clicking QuestFrameCompleteButton");
                Thread.Sleep(300);
                Lua.LuaDoString($@"
                    QuestFrameCompleteButton:Click();
                ");
            }

            Thread.Sleep(300);
            Lua.LuaDoString($@"
                QuestRewardItem{questReward}:Click()
            ");

            Thread.Sleep(300);
            Lua.LuaDoString($@"
                QuestFrameCompleteQuestButton:Click();
            ");

            Thread.Sleep(300);
            Lua.LuaDoString($@"
                QuestFrameAcceptButton:Click();
            ");
        }

        public static string GetQuestGossipOption(int i)
        {
            if (Lua.LuaDoString<bool>($@"return GossipTitleButton{i}:IsVisible()"))
            {
                return Lua.LuaDoString<string>($@"
                    return GossipTitleButton{i}:GetText()
                ");
            }
            return string.Empty;
        }

        public static string GetQuestTitleOption(int i)
        {
            if (Lua.LuaDoString<bool>($@"return QuestTitleButton{i}:IsVisible()"))
            {
                return Lua.LuaDoString<string>($@"
                    return QuestTitleButton{i}:GetText()
                ");
            }
            return string.Empty;
        }
    }
}
