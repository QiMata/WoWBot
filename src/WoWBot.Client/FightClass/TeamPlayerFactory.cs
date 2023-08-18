using System;
using wManager.Wow.Enums;
using WoWBot.Client.FightClass.Team;
using WoWBot.Client.FightClass.Team.Druid;
using WoWBot.Client.FightClass.Team.Hunter;
using WoWBot.Client.FightClass.Team.Mage;
using WoWBot.Client.FightClass.Team.Priest;
using WoWBot.Client.FightClass.Team.Rogue;
using WoWBot.Client.FightClass.Team.Shaman;
using WoWBot.Client.FightClass.Team.Warlock;
using WoWBot.Client.FightClass.Team.Warrior;

namespace WoWBot.Client.FightClass
{
    static class TeamPlayerFactory
    {
        public static ITeamPlayer GetByClass(WoWClass wowClass)
        {
            Console.Write(wowClass);
            switch (wowClass)
            {
                case WoWClass.Warrior:
                    return new PaladinTeamPlayer();
                case WoWClass.Paladin:
                    return new PaladinTeamPlayer();
                case WoWClass.Hunter:
                    return new HunterTeamPlayer();
                case WoWClass.Rogue:
                    return new RogueTeamPlayer();
                case WoWClass.Priest:
                    return new PriestTeamPlayer();
                case WoWClass.Shaman:
                    return new ShamanTeamPlayer();
                case WoWClass.Mage:
                    return new MageTeamPlayer();
                case WoWClass.Warlock:
                    return new WarlockTeamPlayer();
                case WoWClass.Druid:
                    return new DruidTeamPlayer();
                default:
                    throw new ArgumentOutOfRangeException(nameof(wowClass), wowClass, null);
            }
        }
    }
}
