using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWBot.Dtos;

namespace WoWBot.Server.Services
{
    class PlayerInteractionService : IPlayerInteraction
    {
        public Task<ChatResponse> RespondToChatAsync(ChatMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
