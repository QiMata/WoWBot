using robotManager.Helpful;
using System.Threading;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using WoWBot.Playerstate;
using System.Collections.Generic;
using wManager.Wow.Bot.Tasks;
using System.Linq;
using wManager.Wow.ObjectManager;
using Newtonsoft.Json;
using AdvancedQuester.NpcBase;
using System.IO;

namespace WoWBot.Client.Helpers
{
    public static class MapDataHelpers
    {
        public static MapInformation GetMapInformationForPlayer()
        {
            var pos = ObjectManager.Me.Position;
            var zone = (ContinentId)Usefuls.ContinentId;

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

        public static void RotateThroughArea(List<Vector3> rotationArea)
        {
            if (rotationArea.Count > 0)
            {
                Logging.WriteDebug("[RotateThroughArea] start");

                MovementManager.GoLoop(rotationArea);
            }
        }
        public static void MoveToClosestPosition(List<Vector3> rotationArea)
        {
            List<Vector3> closestPosition = rotationArea
                            .OrderBy(spot => spot.DistanceTo(ObjectManager.Me.Position))
                            .ToList();

            if (closestPosition.Count > 0)
            {
                Logging.WriteDebug("[MoveToClosestPosition] " + closestPosition[0].ToString());
                MoveToPosition(closestPosition[0], 0);
            }
        }

        public static void MoveToPosition(Vector3 position, float interactDistance = 0)
        {
            Logging.WriteDebug("[MoveToPosition] " + position.ToString());
            GoToTask.ToPosition(position, interactDistance);

            while(MovementManager.InMovement)
            {
                List<WoWUnit> assailants = ObjectManager.GetUnitAttackPlayer();

                if (assailants.Count > 0 && assailants[0].Position.DistanceTo(ObjectManager.Me.Position) < assailants[0].AggroDistance)
                {
                    Logging.WriteDebug("[MoveToClosestPosition] Defending against " + assailants[0].Name);
                    MovementManager.StopMove();
                    Thread.Sleep(500);
                    CombatHelpers.FightTarget(assailants[0]);
                    Thread.Sleep(500);
                    GoToTask.ToPosition(position, interactDistance);
                } else
                {
                    FightBG.StopFight();
                }
            }
        }

        public static void GoTalkToNpc(QuestGiver questGiver)
        {
            MoveToPosition(questGiver.Position);

            Thread.Sleep(500);
            GoToTask.ToPositionAndIntecractWithNpc(questGiver.Position, questGiver.NpcId);
            Thread.Sleep(500);
        }
    }
}
