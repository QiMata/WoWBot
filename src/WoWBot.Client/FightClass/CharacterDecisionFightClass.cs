using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wManager.Wow.Helpers;
using WoWBot.Client.FightClass.Team;

namespace WoWBot.Client.FightClass
{
    public class CharacterDecisionFightClass : ICustomClass
    {
        private ICustomClass _customClass;

        private FightClassSettings _fightClassSettings;

        public void Initialize()
        {
            //Get Team Player
            ITeamPlayer teamPlayer = TeamPlayerFactory.GetByClass(wManager.Wow.ObjectManager.ObjectManager.Me.WowClass);

            //Set custom class
            _customClass = teamPlayer.GetRotationByTeamRole(_fightClassSettings.TeamRole);

            //init custom class
            _customClass.Initialize();
        }

        public void Dispose()
        {
            //dispose yo class
            if (_customClass != null)
            {
                _customClass.Dispose();
            }
        }

        public void ShowConfiguration()
        {
            //show configuration
        }

        public float Range
        {
            get
            {
                if (_customClass == null)
                {
                    return 30;
                }
                return _customClass.Range;
            }
        }
    }
}
