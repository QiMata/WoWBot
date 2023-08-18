using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using robotManager;
using robotManager.Helpful;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
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

        public static FightClassSettings CurrentSetting { get; set; }

        FightClassSettings()
        {
            try
            {
                ConfigWinForm(new System.Drawing.Point(400, 400), "FightClass " + Translate.Get("Settings"));
            }
            catch (Exception ex)
            {
                Logging.WriteError("FightClassSettings &gt; Constructor(): " + ex);
            }
        }

        public bool Save()
        {
            try
            {
                CurrentSetting.FetchFromLua();
                return Save(AdviserFilePathAndName("CustomClass-FightClass", ObjectManager.Me.Name + "." + Usefuls.RealmName));
            }
            catch (Exception e)
            {
                Logging.WriteError("FightClassSettings &gt; Save(): " + e);
                return false;
            }
        }

        public static bool Load()
        {
            try
            {
                if (File.Exists(AdviserFilePathAndName("CustomClass-FightClass", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
                {
                    CurrentSetting = Load<FightClassSettings>(AdviserFilePathAndName("CustomClass-FightClass", ObjectManager.Me.Name + "." + Usefuls.RealmName));
                    CurrentSetting.PushToLua();
                    return true;
                }
                CurrentSetting = new FightClassSettings();

            }
            catch (Exception e)
            {
                Logging.WriteError("FightClassSettings &gt; Load(): " + e);
            }
            return false;
        }

        void FetchFromLua()
        {
            TeamRole = Lua.LuaDoString<TeamRole>("return TeamRole");
        }

        void PushToLua()
        {
            Lua.LuaDoString("TeamRole = " + TeamRole);
        }
    }

}