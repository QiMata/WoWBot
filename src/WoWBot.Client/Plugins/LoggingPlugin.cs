using System;
using System.Threading;
using wManager.Plugin;
using wManager.Wow.ObjectManager;
using WoWBot.Client.Plugins.Logging;
using WoWBot.Client.Plugins.Models;

namespace WoWBot.Client.Plugins
{
    public class LoggingPlugin : IPlugin
    {
        private bool _run;

        private Logger<MeData> _logger;

        public void Initialize()
        {
            _logger = new Logger<MeData>(ObjectManager.Me.Name + "_" + DateTime.Now.ToFileTime() + ".json",100);

            _run = true;
            while (_run)
            {
                LogMeState();
                Thread.Sleep(100);
            }
        }

        private void LogMeState()
        {
            _logger.AddRecord(new MeData
            {
                Me = ObjectManager.Me,
                TimeStamp = DateTime.Now
            });
        }

        public void Dispose()
        {
            _run = false;
        }

        public void Settings()
        {
            
        }
    }
}
