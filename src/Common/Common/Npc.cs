using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using WoWBot.Playerstate;

namespace WoWBot.Common
{
    [DataContract]
    public class Npc
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public MapInformation MapInformation { get; set; }

        [DataMember]
        public ICollection<Quest> Quests { get; set; }

        [DataMember]
        public long MaxHealth { get; set; }

        [DataMember]
        public bool MyCharacterInFront { get; set; }


    }
}
