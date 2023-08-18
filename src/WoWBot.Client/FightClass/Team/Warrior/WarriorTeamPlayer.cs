using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team.Warrior
{
    class PaladinTeamPlayer : ITeamPlayer
    {
        public ICustomClass GetRotationByTeamRole(TeamRole teamRole)
        {
            switch (teamRole) {
                case TeamRole.Tank: return new WarriorTank(5);
                case TeamRole.Dps: return new WarriorDPS(5);
            }
            return new WarriorDPS(5);
        }
    }
}
