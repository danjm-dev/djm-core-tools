using Unity.Burst;
using Unity.Mathematics;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    public static class LineUtils
    {
        [BurstCompile]
        public static bool TryGetIntersection
        (
            in float2 lineAStart, 
            in float2 lineAEnd, 
            in float2 lineBStart, 
            in float2 lineBEnd, 
            out float2 intersection
        )
        {
            var s1 = lineAEnd - lineAStart;
            var s2 = lineBEnd - lineBStart;
            
            var s = (-s1.y * (lineAStart.x - lineBStart.x) + s1.x * (lineAStart.y - lineBStart.y)) / (-s2.x * s1.y + s1.x * s2.y);
            var t = ( s2.x * (lineAStart.y - lineBStart.y) - s2.y * (lineAStart.x - lineBStart.x)) / (-s2.x * s1.y + s1.x * s2.y);

            // No collision
            if (!(s >= 0) || !(s <= 1) || !(t >= 0) || !(t <= 1))
            {
                intersection = default;
                return false; 
            }
            
            // Collision detected
            intersection = lineAStart + t * s1;
            return true;
        }
        
        [BurstCompile]
        public static void GetDirection(in float2 lineStart, in float2 lineEnd, out float2 direction)
        {
            direction = math.normalize(lineEnd - lineStart);
        }

        [BurstCompile]
        public static void GetTangent(in float2 direction, out float2 tangent)
        {
            tangent = new float2(-direction.y, direction.x);
        }
        
        [BurstCompile]
        public static void GetLength(in float2 lineStart, in float2 lineEnd, out float length)
        {
            length = math.length(lineEnd - lineStart);
        }
        
        [BurstCompile]
        public static void GetClosestPointOnLine
        (
            in float2 lineStart, 
            in float2 lineEnd, 
            in float2 targetPoint, 
            out float percentageAlongLine, 
            out float2 closestPoint
        )
        {
            var lineVector = lineEnd - lineStart;
            var lineDirection = math.normalize(lineVector);
            var lineLength = math.length(lineVector);

            if (math.lengthsq(lineDirection) < math.EPSILON)
            {
                closestPoint = lineStart;
                percentageAlongLine = 0f;
                return;
            }
            
            var pointToStart = targetPoint - lineStart;
            var distance = math.clamp(math.dot(pointToStart, lineDirection), 0f, lineLength);

            percentageAlongLine = distance / lineLength;
            closestPoint = lineStart + lineDirection * distance;
        }
    }
}