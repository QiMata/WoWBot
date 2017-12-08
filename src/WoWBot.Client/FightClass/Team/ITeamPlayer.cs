using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team
{
    interface ITeamPlayer
    {
        ICustomClass GetRotationByTeamRole(TeamRole teamRole);
    }
}
