using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wManager.Wow.Enums;
using WoWBot.Client.FightClass.Team;
using WoWBot.Client.FightClass.Team.Priest;

namespace WoWBot.Client.FightClass
{
    static class TeamPlayerFactory
    {
        public static ITeamPlayer GetByClass(wManager.Wow.Enums.WoWClass wowClass)
        {
            switch (wowClass)
            {
                case WoWClass.Warrior:
                    break;
                case WoWClass.Paladin:
                    break;
                case WoWClass.Hunter:
                    break;
                case WoWClass.Rogue:
                    break;
                case WoWClass.Priest:
                    return new PriestTeamPlayer();
                case WoWClass.Shaman:
                    break;
                case WoWClass.Mage:
                    break;
                case WoWClass.Warlock:
                    break;
                case WoWClass.Druid:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(wowClass), wowClass, null);
            }
            return null;
        }
    }
}
