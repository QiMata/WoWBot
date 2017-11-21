using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass
{
    public class CharacterDecisionFightClass : ICustomClass
    {
        private ICharacterDecisionEngine _characterDecisionEngine;

        public void Initialize()
        {
            //Open connection
        }

        public void Dispose()
        {
            //Close connection
        }

        public void ShowConfiguration()
        {
            throw new NotImplementedException();
        }

        public float Range { get; }
    }
}
