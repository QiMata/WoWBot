namespace WoWBot.Client.Models
{
    public class GameObjectLootTemplate
    {
        public int Entry { get; set; }
        public int Item { get; set; }
        public float ChanceOrQuestChance { get; set; }
        public byte GroupId { get; set; }
        public int MinCountOrRef { get; set; }
        public byte MaxCount { get; set; }
        public int ConditionId { get; set; }
        public string Comments { get; set; }
    }
}
