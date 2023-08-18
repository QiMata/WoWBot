using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team.Shaman
{
    class ShamanTeamPlayer : ITeamPlayer
    {
        public ICustomClass GetRotationByTeamRole(TeamRole teamRole)
        {
            switch (teamRole)
            {
                case TeamRole.Dps: return new ShamanDPS(25);
                case TeamRole.Healer: return new ShamanHealer(25);
            }
            return new ShamanDPS(25);
        }
    }
}
