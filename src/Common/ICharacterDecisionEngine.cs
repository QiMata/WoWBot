using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WoWBot.Dtos;

namespace WoWBot
{
    [ServiceContract]
    public interface ICharacterDecisionEngine
    {
        [OperationContract]
        Task<CharacterAction> MakeDecision(CharacterState characterState);
    }
}
