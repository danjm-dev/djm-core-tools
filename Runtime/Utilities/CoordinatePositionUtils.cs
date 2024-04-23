using System;
using Unity.Burst;
using Unity.Mathematics;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    [Obsolete]
    public static class CoordinatePositionUtils
    {
        private static readonly float2 CenterOffset = new (0.5f, 0.5f);
        private static readonly float2 LeftDownOffset = new (0f, 0f);
        private static readonly float2 LeftUpOffset = new (0f, 1f);
        private static readonly float2 RightDownOffset = new (1f, 0f);
        private static readonly float2 RightUpOffset = new (1f, 1f);

        [BurstCompile]
        private static void CoordinateToPosition
        (
            in int2 coordinate, 
            in float unitSize, 
            in float2 offset, 
            out float2 position
        )
        {
            position = (coordinate + offset) * unitSize;
        }
        
        [BurstCompile]
        private static void PositionToCoordinate
        (
            in float2 position, 
            in float unitSize, 
            out int2 coordinate
        )
        {
            coordinate = (int2)math.floor(position / unitSize);
        }
        
        
        
        [BurstCompile]
        public static void GetCoordinate(in float2 position, out int2 coordinate, in float unitSize = 1f)
        {
            PositionToCoordinate(position, unitSize, out coordinate);
        }
        
        [BurstCompile]
        public static void GetCenterPosition(in int2 coordinate, out float2 position, in float unitSize = 1f)
        {
            CoordinateToPosition(coordinate, unitSize, CenterOffset, out position);
        }
        
        [BurstCompile]
        public static void GetLeftDownPosition(in int2 coordinate, out float2 position, in float unitSize = 1f)
        {
            CoordinateToPosition(coordinate, unitSize, LeftDownOffset, out position);
        }
        
        [BurstCompile]
        public static void GetLeftUpPosition(in int2 coordinate, out float2 position, in float unitSize = 1f)
        {
            CoordinateToPosition(coordinate, unitSize, LeftUpOffset, out position);
        }
        
        [BurstCompile]
        public static void GetRightDownPosition(in int2 coordinate, out float2 position, in float unitSize = 1f)
        {
            CoordinateToPosition(coordinate, unitSize, RightDownOffset, out position);
        }
        
        [BurstCompile]
        public static void GetRightUpPosition(in int2 coordinate, out float2 position, in float unitSize = 1f)
        {
            CoordinateToPosition(coordinate, unitSize, RightUpOffset, out position);
        }
    }
}