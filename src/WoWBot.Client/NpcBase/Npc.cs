using robotManager.Helpful;

namespace AdvancedQuester.NpcBase
{
    public abstract class Npc
    {
        public string NpcName { get; set; }
        public int NpcId { get; set; }
        public Vector3 Position { get; set; }
    }
}