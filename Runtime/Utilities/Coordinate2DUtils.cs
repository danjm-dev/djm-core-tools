using Unity.Burst;
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
            out int2 coordinates
        )
        {
            coordinates = (int2)math.floor(position / unitSize);
            //coords = (int3)math.floor(position >> (int3)math.log2(unitSize));
        }
        
        [BurstCompile]
        public static void CoordinatesToPositionSW
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float2 position
        )
        {
            position = coordinates * unitSize;
            //position = coords << (int3)math.log2(unitSize);
        }
        
        [BurstCompile]
        public static void GetCoordinateScaleFactor(in float2 fromUnitSize, in float2 toUnitSize, out float2 scaleFactor)
        {
            scaleFactor = fromUnitSize / toUnitSize;
        }
        
        [BurstCompile]
        public static void ScaleCoordinates
        (
            in int2 coordinates, 
            in float2 scaleFactor,
            out int2 scaledCoordinates
        )
        {
            scaledCoordinates = (int2)math.floor(coordinates * scaleFactor);
        }
        
        [BurstCompile]
        public static void ScaleCoordinates
        (
            in int2 coordinates, 
            in float2 fromUnitSize, 
            in float2 toUnitSize, 
            out int2 scaledCoordinates
        )
        {
            GetCoordinateScaleFactor(fromUnitSize, toUnitSize, out var scaleFactor);
            ScaleCoordinates(coordinates, scaleFactor, out scaledCoordinates);
        }
        
        [BurstCompile]
        public static void CoordinatesToPositionSE
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float2 position
        )
        {
            CoordinatesToPositionSW(coordinates, unitSize, out position);
            position += new float2(unitSize.x, 0f);
        }
        
        [BurstCompile]
        public static void CoordinatesToPositionNE
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float2 position
        )
        {
            CoordinatesToPositionSW(coordinates, unitSize, out position);
            position += unitSize;
        }
        
        [BurstCompile]
        public static void CoordinatesToPositionNW
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float2 position
        )
        {
            CoordinatesToPositionSW(coordinates, unitSize, out position);
            position += new float2(0, unitSize.y);
        }
        
        [BurstCompile]
        public static void CoordinatesToPositionCenter
        (
            in int2 coordinates, 
            in float2 unitSize, 
            out float2 position
        )
        {
            CoordinatesToPositionSW(coordinates, unitSize, out position);
            position += unitSize * 0.5f;
        }
    }
    
    public static partial class Coordinate2DUtils
    {
        [BurstCompile]
        public static void LocalCoordinatesToWorldCoordinates
        (
            in int2 localCoordinates, 
            in int2 origin,
            out int2 worldCoordinates
        )
        {
            worldCoordinates = localCoordinates + origin;
        }
        
        [BurstCompile]
        public static void LocalCoordinatesToWorldCoordinates
        (
            ref int2 coordinates, 
            in int2 origin
        )
        {
            coordinates += origin;
        }

        [BurstCompile]
        public static void WorldCoordinatesToLocalCoordinates
        (
            in int2 worldCoordinates, 
            in int2 origin,
            out int2 localCoordinates
        )
        {
            localCoordinates = worldCoordinates - origin;
        }
        
        [BurstCompile]
        public static void WorldCoordinatesToLocalCoordinates
        (
            ref int2 coordinates, 
            in int2 origin
        )
        {
            coordinates -= origin;
        }
    }

    public static partial class Coordinate2DUtils
    {
        [BurstCompile]
        public static bool IsCoordinatesWithinBoundsInclusive(in int2 boundsMin, in int2 boundsMax, in int2 coordinates)
        {
            return coordinates >= boundsMin is { x: true, y: true } 
                   && coordinates <= boundsMax is { x: true, y: true };
        }
        
        [BurstCompile]
        public static bool IsCoordinatesWithinBoundsExclusive(in int2 boundsMin, in int2 boundsMax, in int2 coordinates)
        {
            return coordinates > boundsMin is { x: true, y: true } 
                   && coordinates < boundsMax is { x: true, y: true };
        }
        
        [BurstCompile]
        public static bool IsCoordinatesOnBoundsEdge(in int2 boundsMin, in int2 boundsMax, in int2 coordinates)
        {
            var minimumOnBounds = coordinates == boundsMin;
            if (minimumOnBounds.x || minimumOnBounds.y) return true;
            
            var maximumOnBounds = coordinates == boundsMax;
            return maximumOnBounds.x || maximumOnBounds.y;
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