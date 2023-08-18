using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team.Rogue
{
    class RogueTeamPlayer : ITeamPlayer
    {
        public ICustomClass GetRotationByTeamRole(TeamRole teamRole)
        {
            return new RogueDPS(30);
        }
    }
}
