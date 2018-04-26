using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team.Warrior
{
    class WarriorTeamPlayer : ITeamPlayer
    {
        public ICustomClass GetRotationByTeamRole(TeamRole teamRole)
        {
            return new WarriorTank(7);
        }
    }
}
