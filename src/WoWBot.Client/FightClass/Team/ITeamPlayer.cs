using wManager.Wow.Helpers;

namespace WoWBot.Client.FightClass.Team
{
    interface ITeamPlayer
    {
        ICustomClass GetRotationByTeamRole(TeamRole teamRole);
    }
}
