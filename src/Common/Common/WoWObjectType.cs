using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WoWBot.Common
{
    [DataContract(Name = "WoWObjectType")]
    public enum WoWObjectType1
    {
        [EnumMember]
        Object,
        [EnumMember]
        Item,
        [EnumMember]
        Container,
        [EnumMember]
        Unit,
        [EnumMember]
        Player,
        [EnumMember]
        GameObject,
        [EnumMember]
        DynamicObject,
        [EnumMember]
        Corpse,
        [EnumMember]
        AiGroup,
        [EnumMember]
        AreaTrigger,
        [EnumMember]
        None
    }
}
