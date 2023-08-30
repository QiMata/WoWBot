using robotManager.Helpful;

namespace WoWBot.Client.NpcBase
{ 
    public abstract class Npc
    {
        public string NpcName { get; set; }
        public int NpcId { get; set; }
        public Vector3 Position { get; set; }
    }
}