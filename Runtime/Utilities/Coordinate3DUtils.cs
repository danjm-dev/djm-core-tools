using Unity.Burst;
using Unity.Mathematics;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    public static partial class Coordinate3DUtils
    {
        [BurstCompile]
        public static void PositionToCoordinates
        (
            in float3 position, 
            in float3 unitSize, 
            out int3 coordinates, 
            in int3 origin = default
        )
        {
            coordinates = (int3)math.floor(position / unitSize) - origin;
        }
        
        [BurstCompile]
        public static void CoordinatesToPosition        
        (
            in int3 coordinates, 
            in int3 origin,
            in float3 unitSize, 
            in float3 offset,
            out float3 position
        )
        {
            position = (coordinates + origin) * unitSize + offset;
        }
        
        [BurstCompile]
        public static void CoordinatesToSouthWestDownPosition
        (
            in int3 coordinates, 
            in float3 unitSize, 
            out float3 position, 
            in int3 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, float3.zero, out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToSouthWestUpPosition
        (
            in int3 coordinates, 
            in float3 unitSize, 
            out float3 position, 
            in int3 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, new float3(0f, unitSize.y, 0f), out position);
        }

        [BurstCompile]
        public static void CoordinatesToSouthEastDownPosition
        (
            in int3 coordinates,
            in float3 unitSize,
            out float3 position,
            in int3 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, new float3(unitSize.x, 0f, 0f), out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToSouthEastUpPosition
        (
            in int3 coordinates,
            in float3 unitSize,
            out float3 position,
            in int3 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, new float3(unitSize.x, unitSize.y, 0f), out position);
        }
        
        
        [BurstCompile]
        public static void CoordinatesToNorthEastDownPosition
        (
            in int3 coordinates, 
            in float3 unitSize, 
            out float3 position, 
            in int3 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, new float3(unitSize.x, 0f, unitSize.z), out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToNorthEastUpPosition
        (
            in int3 coordinates, 
            in float3 unitSize, 
            out float3 position, 
            in int3 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, unitSize, out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToNorthWestDownPosition
        (
            in int3 coordinates, 
            in float3 unitSize, 
            out float3 position, 
            in int3 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, new float3(0f, 0f, unitSize.z), out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToNorthWestUpPosition
        (
            in int3 coordinates, 
            in float3 unitSize, 
            out float3 position, 
            in int3 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, new float3(0f, unitSize.y, unitSize.z), out position);
        }
        
        [BurstCompile]
        public static void CoordinatesToCenterPosition
        (
            in int3 coordinates, 
            in float3 unitSize, 
            out float3 position, 
            in int3 origin = default
        )
        {
            CoordinatesToPosition(coordinates, origin, unitSize, unitSize * 0.5f, out position);
        }
    }
    
    public static partial class Coordinate3DUtils
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
        public static int IndexToZCoordinate(in int index, in int maxZCoordinate, in int xYCoordinateResolutionLog2Sum)
        {
            return (index >> xYCoordinateResolutionLog2Sum) & maxZCoordinate;
        }
        
        [BurstCompile]
        public static void IndexToCoordinates
        (
            in int index, 
            in int3 maxCoords, 
            in int xCoordinateResolutionLog2, 
            in int xYCoordinateResolutionLog2Sum, 
            out int3 coords
        )
        {
            coords = new int3
            (
                IndexToXCoordinate(index, maxCoords.x), 
                IndexToYCoordinate(index, maxCoords.y, xCoordinateResolutionLog2), 
                IndexToZCoordinate(index, maxCoords.z, xYCoordinateResolutionLog2Sum)
            );
        }
        
        [BurstCompile]
        public static int CoordinatesToIndex
        (
            in int xCoordinate,
            in int yCoordinate,
            in int zCoordinate,
            in int xCoordinateResolutionLog2, 
            in int xYCoordinateResolutionLog2Sum
        )
        {
            return xCoordinate + (yCoordinate << xCoordinateResolutionLog2) + (zCoordinate << xYCoordinateResolutionLog2Sum);
        }
        
        [BurstCompile]
        public static int CoordinatesToIndex
        (
            in int3 coords, 
            in int xCoordinateResolutionLog2, 
            in int xYCoordinateResolutionLog2Sum
        )
        {
            return CoordinatesToIndex
            (
                coords.x, 
                coords.y, 
                coords.z, 
                xCoordinateResolutionLog2, 
                xYCoordinateResolutionLog2Sum
            );
        }
    }
}