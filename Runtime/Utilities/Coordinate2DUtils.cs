﻿using Unity.Burst;
using Unity.Mathematics;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    public static partial class Coordinate2DUtils
    {
        [BurstCompile]
        public static void PositionToCoordinates
        (
            in float2 position, 
            in float2 unitSize, 
            out int2 coordinates, 
            in int2 origin = default
        )
        {
            coordinates = (int2)math.floor(position / unitSize) - origin;
        }
        
        [BurstCompile]
        public static void CoordinatesToPosition        
        (
            in int2 coordinates, 
            in int2 origin,
            in float2 unitSize, 
            in float2 offset,
            out float2 position
        )
        {
            position = (coordinates + origin) * unitSize + offset;
        }
        
        [BurstCompile]
        public static void CoordinatesToPosition        
        (
            in int2 coordinates, 
            in int2 origin,
            in float2 unitSize, 
            in float2 offset,
            out float3 position
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, offset, out float2 position2D);
            position = new float3(position2D.x, 0f, position2D.y);
        }
        
        [BurstCompile]
        public static void CoordinatesToSouthWestPosition
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float2 position, 
            in int2 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, float2.zero, out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToSouthWestPosition
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float3 position, 
            in int2 origin = default,
            in float y = 0f
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, float2.zero, out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToSouthEastPosition
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float2 position, 
            in int2 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, new float2(unitSize.x, 0), out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToSouthEastPosition
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float3 position, 
            in int2 origin = default,
            in float y = 0f
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, new float2(unitSize.x, 0), out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToNorthEastPosition
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float2 position, 
            in int2 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, unitSize, out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToNorthEastPosition
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float3 position, 
            in int2 origin = default,
            in float y = 0f
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, unitSize, out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToNorthWestPosition
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float2 position, 
            in int2 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, new float2(0, unitSize.y), out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToNorthWestPosition
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float3 position, 
            in int2 origin = default,
            in float y = 0f
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, new float2(0, unitSize.y), out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToCenterPosition
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float2 position, 
            in int2 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, unitSize * 0.5f, out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToCenterPosition
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float3 position, 
            in int2 origin = default,
            in float y = 0f
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, unitSize * 0.5f, out position);
        }
    }
    
    public static partial class Coordinate2DUtils
    {
        [BurstCompile]
        public static int IndexToXCoordinate(in int index, int maxXCoordinate)
        {
            return index & maxXCoordinate;
        }
        
        [BurstCompile]
        public static int IndexToYCoordinate(in int index, in int maxYCoordinate, in int xCoordinateResolutionLog2)
        {
            return (index >> xCoordinateResolutionLog2) & maxYCoordinate;
        }
        
        [BurstCompile]
        public static void IndexToCoordinates
        (
            in int index, 
            in int2 maxCoords, 
            in int xCoordinateResolutionLog2, 
            out int2 coords
        )
        {
            coords = new int2
            (
                IndexToXCoordinate(index, maxCoords.x), 
                IndexToYCoordinate(index, maxCoords.y, xCoordinateResolutionLog2)
            );
        }
        
        [BurstCompile]
        public static int CoordinatesToIndex(in int xCoordinate, in int yCoordinate, in int xCoordinateResolutionLog2)
        {
            return xCoordinate + (yCoordinate << xCoordinateResolutionLog2);
        }
        
        [BurstCompile]
        public static int CoordinatesToIndex(in int2 coords, in int xCoordinateResolutionLog2)
        {
            return CoordinatesToIndex(coords.x, coords.y, xCoordinateResolutionLog2);
        }
    }
}