using wManager.Wow.Helpers;
using WoWBot.Client.FightClass;
using WoWBot.Client.FightClass.Team;

public class Main : ICustomClass
{
    private ICustomClass _customClass;

    private FightClassSettings _fightClassSettings;

    public void Initialize()
    {
        if (_fightClassSettings == null)
        {
            FightClassSettings.Load();

            _fightClassSettings = FightClassSettings.CurrentSetting;
        }
        //Get Team Player
        ITeamPlayer teamPlayer = TeamPlayerFactory.GetByClass(wManager.Wow.ObjectManager.ObjectManager.Me.WowClass);

        //Set custom class
        _customClass = teamPlayer.GetRotationByTeamRole(_fightClassSettings.TeamRole);

        //init custom class
        _customClass.Initialize();
    }
    public void Dispose()
    {
        //dispose yo class
        if (_customClass == null)
        {
            return;
        }
        _customClass.Dispose();
    }

    public void ShowConfiguration()
    {
        if (_fightClassSettings == null)
        {
            FightClassSettings.Load();

            _fightClassSettings = FightClassSettings.CurrentSetting;
        }
        //show configuration
        _fightClassSettings.ToForm();
    }

    public float Range
    {
        get
        {
            if (_customClass == null)
            {
                return 30;
            }
            return _customClass.Range;
        }
    }

}
