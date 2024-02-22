using Unity.Burst;
using Unity.Mathematics;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    public static class CoordinateUtils
    {
        public static readonly int2 Left = new (-1, 0);
        public static readonly int2 Right = new (1, 0);
        public static readonly int2 Up = new (0, 1);
        public static readonly int2 Down = new (0, -1);
        
        [BurstCompile]
        public static void GetLeftCoordinates(in int2 coordinates, out int2 leftCoordinates)
        {
            leftCoordinates = coordinates + Left;
        }
        
        [BurstCompile]
        public static void GetRightCoordinates(in int2 coordinates, out int2 rightCoordinates)
        {
            rightCoordinates = coordinates + Right;
        }
        
        [BurstCompile]
        public static void GetDownCoordinates(in int2 coordinates, out int2 downCoordinates)
        {
            downCoordinates = coordinates + Down;
        }
        
        [BurstCompile]
        public static void GetUpCoordinates(in int2 coordinates, out int2 upCoordinates)
        {
            upCoordinates = coordinates + Up;
        }
        
        
        
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
    }

    // // bounds
    // public static partial class CoordinateUtils
    // {
    //     [BurstCompile]
    //     public static bool IsCoordinateInBoundsInclusive(in int2 boundsMin, in int2 boundsMax, in int2 coordinate)
    //     {
    //         return coordinate >= boundsMin is { x: true, y: true } 
    //                && coordinate <= boundsMax is { x: true, y: true };
    //     }
    //     
    //     [BurstCompile]
    //     public static bool IsCoordinateInBoundsExclusive(in int2 boundsMin, in int2 boundsMax, in int2 coordinate)
    //     {
    //         return coordinate > boundsMin is { x: true, y: true } 
    //                && coordinate < boundsMax is { x: true, y: true };
    //     }
    //     
    //     [BurstCompile]
    //     public static bool IsCoordinateOnBoundsEdge(in int2 boundsMin, in int2 boundsMax, in int2 point)
    //     {
    //         var minimumOnBounds = point == boundsMin;
    //         var maximumOnBounds = point == boundsMax;
    //         return minimumOnBounds.x || minimumOnBounds.y || maximumOnBounds.x || maximumOnBounds.y;
    //     }
    // }
}