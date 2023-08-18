using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team.Warlock
{
    class WarlockTeamPlayer : ITeamPlayer
    {
        public ICustomClass GetRotationByTeamRole(TeamRole teamRole)
        {
            return new WarlockDPS();
        }
    }
}
