using System.Runtime.Serialization;
using WoWBot.Playerstate;

namespace WoWBot.Dtos
{
    [DataContract]
    public class CharacterState
    {
        [DataMember]
        public PlayerInformation PlayerInformation { get; set; }
    }
}