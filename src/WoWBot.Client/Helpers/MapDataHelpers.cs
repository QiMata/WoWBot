using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using robotManager.Helpful;
using wManager.Wow.Enums;
using WoWBot.Playerstate;

namespace WoWBot.Client.Helpers
{
    public static class MapDataHelpers
    {
        public static MapInformation GetMapInformationForPlayer()
        {
            var pos = wManager.Wow.ObjectManager.ObjectManager.Me.Position;
            var zone = (ContinentId)wManager.Wow.Helpers.Usefuls.ContinentId;

            return new MapInformation
            {
                Coordinate = pos.ToMapCoordinate(),
                Zone = GetZoneFromContientId(zone)
            };
        }

        private static MapZone GetZoneFromContientId(ContinentId zone)
        {
            switch (zone)
            {
                case ContinentId.Kalimdor:
                    return MapZone.Kalimdor;
                case ContinentId.Azeroth:
                    return MapZone.EasternKingdoms;
                default:
                    return MapZone.Unknown;
            }
        }

        private static MapCoordinate ToMapCoordinate(this Vector3 position)
        {
            return new MapCoordinate
            {
                X = position.X,
                Y = position.Y,
                Z = position.Z

            };
        }
    }
}
