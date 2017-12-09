using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wManager.Wow.ObjectManager;

namespace WoWBot.Client.Plugins.Models
{
    class MeData
    {
        public WoWLocalPlayer Me { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
