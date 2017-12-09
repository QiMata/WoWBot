using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using wManager.Wow.Helpers;
using WoWBot.Client.FightClass;
using WoWBot.Client.FightClass.Team;

public class Main : ICustomClass
{
    private ICustomClass _customClass;

    private readonly FightClassSettings _fightClassSettings = new FightClassSettings();

    public void Initialize()
    {
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
        if (_customClass != null)
        {
            _customClass.Dispose();
        }
    }

    public void ShowConfiguration()
    {
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
