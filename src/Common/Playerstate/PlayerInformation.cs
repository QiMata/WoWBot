using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WoWBot.Playerstate
{
    [DataContract]
    public class PlayerInformation
    {
        [DataMember]
        public MapInformation PlayerPosition { get; set; }

        [DataMember]
        public CombatState CombatState { get; set; }
    }
}
