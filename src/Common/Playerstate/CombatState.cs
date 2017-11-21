using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WoWBot.Playerstate
{
    [DataContract(Name = "CombatState")]
    public enum CombatState
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        InCombat = 1,
        [EnumMember]
        OutOfCombat = 2
    }
}
