using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team.Mage
{
    class MageTeamPlayer : ITeamPlayer
    {
        public ICustomClass GetRotationByTeamRole(TeamRole teamRole)
        {
            return new MageDPS(30);
        }
    }
}
