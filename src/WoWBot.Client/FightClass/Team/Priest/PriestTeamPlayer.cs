using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team.Priest
{
    class PriestTeamPlayer : ITeamPlayer
    {
        public ICustomClass GetRotationByTeamRole(TeamRole teamRole)
        {
            switch (teamRole)
            {
                case TeamRole.Healer: return new PriestHealer(30);
                case TeamRole.Dps: return new PriestDPS(5);
            }
            return new PriestHealer(30);
        }
    }
}
