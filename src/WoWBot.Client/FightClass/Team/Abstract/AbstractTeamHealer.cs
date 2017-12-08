using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoWBot.Client.FightClass.Team.Abstract
{
    class AbstractTeamHealer : BaseClassWithRotation
    {
        public AbstractTeamHealer(float range) : base(range)
        {
        }

        protected override void CombatRotation()
        {
            throw new NotImplementedException();
        }

        protected override bool TeamInCombat()
        {
            throw new NotImplementedException();
        }

        protected override void Buff()
        {
            throw new NotImplementedException();
        }
    }
}
