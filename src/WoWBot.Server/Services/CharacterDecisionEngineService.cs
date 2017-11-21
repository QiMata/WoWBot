using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWBot.Dtos;

namespace WoWBot.Server.Services
{
    class CharacterDecisionEngineService : ICharacterDecisionEngine
    {
        public Task<CharacterAction> MakeDecision(CharacterState characterState)
        {
            throw new NotImplementedException();
        }
    }
}
