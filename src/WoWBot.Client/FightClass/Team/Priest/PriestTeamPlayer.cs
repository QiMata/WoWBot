using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team.Priest
{
    class PriestTeamPlayer : ITeamPlayer
    {
        public ICustomClass GetRotationByTeamRole(TeamRole teamRole)
        {
            return new PriestHealer(30);
        }
    }
}
