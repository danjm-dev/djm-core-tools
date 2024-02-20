using Unity.Burst;
using Unity.Mathematics;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    public static class CoordinateIndexUtils
    {
        [BurstCompile]
        public static int GetIndex(in int2 coordinates, in int2 resolution)
        {
            return coordinates.x + coordinates.y * resolution.x;
        }
        
        [BurstCompile]
        public static int OffsetIndex(int index, in int2 resolution, in int2 offset)
        {
            return index + offset.x + offset.y * resolution.x;
        }
        
        [BurstCompile]
        public static bool TryOffsetIndex(int index, in int2 resolution, in int2 offset, out int offsetIndex)
        {
            offsetIndex = OffsetIndex(index, resolution, offset);
            return IsIndexInBounds(offsetIndex, resolution);
        }
        
        [BurstCompile]
        public static bool IsIndexInBounds(int index, in int2 resolution)
        {
            return index >= 0 && index < resolution.x * resolution.y;
        }
        
        [BurstCompile]
        public static bool IsIndexOnEdge(int index, in int2 resolution)
        {
            GetCoordinates(index, resolution, out var coordinates);
            return IsCoordinateOnEdge(coordinates, resolution);
        }
        
        [BurstCompile]
        public static bool TryGetIndex(in int2 coordinates, in int2 resolution, out int index)
        {
            if (!IsCoordinateInBounds(coordinates, resolution))
            {
                index = default;
                return false;
            }
            
            index = GetIndex(coordinates, resolution);
            return true;
        }
        
        [BurstCompile]
        public static void GetCoordinates(int index, in int2 resolution, out int2 coordinates)
        {
            coordinates = new int2
            (
                index % resolution.x, 
                index / resolution.x
            );
        }
        
        [BurstCompile]
        public static bool IsCoordinateInBounds(in int2 coordinates, in int2 resolution)
        {
            return coordinates.x >= 0 
                   && coordinates.x < resolution.x 
                   && coordinates.y >= 0 
                   && coordinates.y < resolution.y;
        }
        
        [BurstCompile]
        public static bool IsCoordinateOnEdge(in int2 coordinates, in int2 resolution)
        {
            return coordinates.x == 0 
                   || coordinates.x == resolution.x - 1 
                   || coordinates.y == 0 
                   || coordinates.y == resolution.y - 1;
        }
        
        [BurstCompile]
        public static bool TryGetCoordinates(int index, in int2 resolution, out int2 coordinates)
        {
            if (!IsIndexInBounds(index, resolution))
            {
                coordinates = default;
                return false;
            }
            
            GetCoordinates(index, resolution, out coordinates);
            return true;
        }
    }
}