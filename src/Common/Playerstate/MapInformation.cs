using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WoWBot.Playerstate
{
    [DataContract]
    public class MapInformation
    {
        [DataMember]
        public MapZone Zone { get; set; }
        [DataMember]
        public MapCoordinate Coordinate { get; set; }
    }
}
