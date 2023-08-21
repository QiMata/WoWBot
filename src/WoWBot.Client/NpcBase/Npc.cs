using System.Collections.Generic;
using robotManager.Helpful;
using System.Linq;

namespace AdvancedQuester.NpcBase
{
    public abstract class Npc
    {
        public string NpcName { get; set; }
        public int NpcId { get; set; }
        public Vector3 Position { get; set; }
    }
}