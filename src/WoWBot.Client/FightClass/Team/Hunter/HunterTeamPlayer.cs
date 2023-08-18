using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team.Hunter
{
    class HunterTeamPlayer : ITeamPlayer
    {
        public ICustomClass GetRotationByTeamRole(TeamRole teamRole)
        {
            return new HunterDPS(30);
        }
    }
}
