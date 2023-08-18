using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team.Paladin
{
    class PaladinTeamPlayer : ITeamPlayer
    {
        public ICustomClass GetRotationByTeamRole(TeamRole teamRole)
        {
            switch (teamRole)
            {
                case TeamRole.Healer: return new PaladinHealer(30);
                case TeamRole.Tank: return new PaladinTank(5);
                case TeamRole.Dps: return new PaladinDPS(5);
            }
            return new PaladinDPS(5);
        }
    }
}
