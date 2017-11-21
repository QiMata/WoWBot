using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WoWBot.Playerstate
{
    [DataContract(Name = "MapZone")]
    public enum MapZone
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        Kalimdor = 1,
        [EnumMember]
        EasternKingdoms = 2
    }
}
