using System;
using Unity.Burst;
using Unity.Mathematics;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    [Obsolete("Use Coordinate2DUtils or Coordinate3DUtils instead", false)]
    public static class CoordinateUtils
    {
        public static readonly int2 North = new (0, 1);
        public static readonly int2 NorthEast = new (1, 1);
        public static readonly int2 East = new (1, 0);
        public static readonly int2 SouthEast = new (1, -1);
        public static readonly int2 South = new (0, -1);
        public static readonly int2 SouthWest = new (-1, -1);
        public static readonly int2 West = new (-1, 0);
        public static readonly int2 NorthWest = new (-1, 1);
        
        [BurstCompile]
        public static void GetNorthCoordinate(in int2 coordinate, out int2 northCoordinate)
        {
            northCoordinate = coordinate + North;
        }

        [BurstCompile]
        public static void GetNorthEastCoordinate(in int2 coordinate, out int2 northEastCoordinate)
        {
            northEastCoordinate = coordinate + NorthEast;
        }
        
        [BurstCompile]
        public static void GetEastCoordinate(in int2 coordinate, out int2 eastCoordinate)
        {
            eastCoordinate = coordinate + East;
        }
        
        [BurstCompile]
        public static void GetSouthEastCoordinate(in int2 coordinate, out int2 southEastCoordinate)
        {
            southEastCoordinate = coordinate + SouthEast;
        }
        
        [BurstCompile]
        public static void GetSouthCoordinate(in int2 coordinate, out int2 southCoordinate)
        {
            southCoordinate = coordinate + South;
        }
        
        [BurstCompile]
        public static void GetSouthWestCoordinate(in int2 coordinate, out int2 southWestCoordinate)
        {
            southWestCoordinate = coordinate + SouthWest;
        }
        
        [BurstCompile]
        public static void GetWestCoordinate(in int2 coordinate, out int2 westCoordinate)
        {
            westCoordinate = coordinate + West;
        }
        
        [BurstCompile]
        public static void GetNorthWestCoordinate(in int2 coordinate, out int2 northWestCoordinate)
        {
            northWestCoordinate = coordinate + NorthWest;
        }
        
        [BurstCompile]
        public static void GetOrthogonalCoordinates
        (
            in int2 coordinate, 
            out int2 northCoordinate, 
            out int2 eastCoordinate, 
            out int2 southCoordinate, 
            out int2 westCoordinate
        )
        {
            northCoordinate = coordinate + North;
            eastCoordinate = coordinate + East;
            southCoordinate = coordinate + South;
            westCoordinate = coordinate + West;
        }
        
        [BurstCompile]
        public static void GetDiagonalCoordinates
        (
            in int2 coordinate, 
            out int2 northEastCoordinate, 
            out int2 southEastCoordinate, 
            out int2 southWestCoordinate, 
            out int2 northWestCoordinate
        )
        {
            northEastCoordinate = coordinate + NorthEast;
            southEastCoordinate = coordinate + SouthEast;
            southWestCoordinate = coordinate + SouthWest;
            northWestCoordinate = coordinate + NorthWest;
        }
        
        [BurstCompile]
        public static void GetAdjacentCoordinates
        (
            in int2 coordinate, 
            out int2 northCoordinate, 
            out int2 northEastCoordinate, 
            out int2 eastCoordinate, 
            out int2 southEastCoordinate, 
            out int2 southCoordinate, 
            out int2 southWestCoordinate, 
            out int2 westCoordinate, 
            out int2 northWestCoordinate
        )
        {
            northCoordinate = coordinate + North;
            northEastCoordinate = coordinate + NorthEast;
            eastCoordinate = coordinate + East;
            southEastCoordinate = coordinate + SouthEast;
            southCoordinate = coordinate + South;
            southWestCoordinate = coordinate + SouthWest;
            westCoordinate = coordinate + West;
            northWestCoordinate = coordinate + NorthWest;
        }
        
        
        // bounds
        
        [BurstCompile]
        public static bool IsCoordinateInBoundsInclusive(in int2 boundsMin, in int2 boundsMax, in int2 coordinate)
        {
            return coordinate >= boundsMin is { x: true, y: true } 
                   && coordinate <= boundsMax is { x: true, y: true };
        }
        
        [BurstCompile]
        public static bool IsCoordinateInBoundsExclusive(in int2 boundsMin, in int2 boundsMax, in int2 coordinate)
        {
            return coordinate > boundsMin is { x: true, y: true } 
                   && coordinate < boundsMax is { x: true, y: true };
        }
        
        [BurstCompile]
        public static bool IsCoordinateOnBoundsEdge(in int2 boundsMin, in int2 boundsMax, in int2 point)
        {
            var minimumOnBounds = point == boundsMin;
            var maximumOnBounds = point == boundsMax;
            return minimumOnBounds.x || minimumOnBounds.y || maximumOnBounds.x || maximumOnBounds.y;
        }
        
        [BurstCompile]
        public static void GetBoundsCornerCoordinates
        (
            in int2 boundsMin, 
            in int2 boundsMax, 
            out int2 leftDown, 
            out int2 leftUp, 
            out int2 rightDown, 
            out int2 rightUp
        )
        {
            leftDown = boundsMin;
            leftUp = new int2(boundsMin.x, boundsMax.y);
            rightDown = new int2(boundsMax.x, boundsMin.y);
            rightUp = boundsMax;
        }
    }
}