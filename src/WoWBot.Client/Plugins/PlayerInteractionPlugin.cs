using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using robotManager.Helpful;
using wManager.Plugin;

namespace WoWBot.Client.Plugins
{
    public class PlayerInteractionPlugin : IPlugin
    {
        public void Initialize()
        {
            //open pipeline
            wManager.Wow.Helpers.LongMove.LongMoveGo(new Vector3(-5660.33, 755.299, 390.605));
        }

        public void Dispose()
        {
            //close pipeline
        }

        public void Settings()
        {
            throw new NotImplementedException();
        }
    }
}
