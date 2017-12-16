using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wManager.Wow.Bot.States;
using wManager.Wow.Class;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Helpers;
using WoWUnit = WoWBot.Common.WoWUnit;

namespace WoWBot.Client.FightClass.Team.Abstract
{
    abstract class AbstractTeamDPS : BaseClassWithRotation
    {

        protected AbstractTeamDPS(float range) : base(range)
        {

        }
    }
}
