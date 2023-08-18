using System;
using System.Diagnostics;
using System.Threading;
using wManager.Wow.Class;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.Helpers
{
    public class MonitoredSpell : Spell
    {
        private TimeSpan _castTime;

        public MonitoredSpell(int spellId) : base(spellId)
        {
            _castTime = TimeSpan.FromSeconds(1.5);
        }

        public MonitoredSpell(uint spellId) : base(spellId)
        {
            _castTime = TimeSpan.FromSeconds(1.5);
        }

        public MonitoredSpell(string spellNameEnglish) : base(spellNameEnglish)
        {
            _castTime = TimeSpan.FromSeconds(1.5);
        }

        public MonitoredSpell(int spellId, TimeSpan castTime) : base(spellId)
        {
            _castTime = castTime;
        }

        public MonitoredSpell(uint spellId, TimeSpan castTime) : base(spellId)
        {
            _castTime = castTime;
        }

        public MonitoredSpell(string spellNameEnglish, TimeSpan castTime) : base(spellNameEnglish)
        {
            _castTime = castTime;
        }

        public MonitoredSpell(string spellNameEnglish, bool showLog, TimeSpan castTime) : base(spellNameEnglish, showLog)
        {
            _castTime = castTime;
        }

        public MonitoredSpell(int spellId, int castTime) : base(spellId)
        {
            _castTime = TimeSpan.FromSeconds(castTime);
        }

        public MonitoredSpell(uint spellId, int castTime) : base(spellId)
        {
            _castTime = TimeSpan.FromSeconds(castTime);
        }

        public MonitoredSpell(string spellNameEnglish, int castTime) : base(spellNameEnglish)
        {
            _castTime = TimeSpan.FromSeconds(castTime);
        }

        public MonitoredSpell(string spellNameEnglish, bool showLog, int castTime) : base(spellNameEnglish, showLog)
        {
            _castTime = TimeSpan.FromSeconds(castTime);
        }

        public void Cast(bool stopMove = true)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (IsDistanceGood && KnownSpell && !CooldownEnabled)
            {
                Launch(stopMove);
            }

            while (stopwatch.ElapsedMilliseconds < _castTime.TotalMilliseconds)
            {
                Thread.Sleep(100);
            }
        }

        public bool CooldownEnabled => ObjectManager.Me.CooldownEnabled(Id);
    }
}
