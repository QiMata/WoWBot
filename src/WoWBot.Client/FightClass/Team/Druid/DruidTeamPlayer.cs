using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team.Druid
{
    class DruidTeamPlayer : ITeamPlayer
    {
        public ICustomClass GetRotationByTeamRole(TeamRole teamRole)
        {
            switch (teamRole)
            {
                case TeamRole.Healer: return new DruidHealer(30);
                case TeamRole.Tank: return new DruidTank(5);
                case TeamRole.Dps: return new DruidDPS(5);
            }
            return new DruidDPS(5);
        }
    }
}
