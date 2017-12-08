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
            switch (teamRole)
            {
                case TeamRole.Healer:
                    return new PriestHealer(30,80);
                default:
                    throw new ArgumentOutOfRangeException(nameof(teamRole), teamRole, null);
            }
        }
    }
}
