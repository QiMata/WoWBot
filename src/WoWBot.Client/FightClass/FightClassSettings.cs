using System.ComponentModel;
using System.Configuration;
using robotManager.Helpful;
using WoWBot.Client.FightClass.Team;

namespace WoWBot.Client.FightClass
{
    class FightClassSettings : Settings
    {
        [Setting]
        [Category("Party Settings")]
        [DisplayName("Team Role")]
        [Description("Designate the purpose of the party member here")]
        public TeamRole TeamRole { get; set; }
    }
}