using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using WoWBot.Playerstate;

namespace WoWBot.Common
{
    [DataContract]
    public class WoWUnit
    {
        [DataMember]
        public bool IsValid { get; set; }
        [DataMember]
        public uint GetDescriptorStartAddress { get; set; }
        [DataMember]
        public ulong Guid { get; set; }
        [DataMember]
        public WoWObjectType Type { get; set; }
        [DataMember]
        public int Entry { get; set; }
        [DataMember]
        public float Scale { get; set; }
        [DataMember]
        public MapCoordinate Position { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public float GetDistance { get; set; }
    }
}
