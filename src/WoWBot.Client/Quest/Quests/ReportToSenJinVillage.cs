using robotManager.Helpful;
using WoWBot.Client.NpcBase;

namespace WoWBot.Client.Quest.Quests
{
    public class ReportToSenJinVillage : QuestTask
    {
        public ReportToSenJinVillage()
        {
            Name = "Report to Sen'jin Village";
            QuestId = 805;

            MinimumLevel = 1;
            MaximumLevel = 10;

            TurnInNpc = new QuestGiver {NpcName = "Master Gadrin", NpcId = 3188, Position = new Vector3(-825.5f, -4920.9f, 19.4f)};
        }
    }
}