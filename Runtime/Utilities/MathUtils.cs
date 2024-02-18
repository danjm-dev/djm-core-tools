using Unity.Burst;
using Unity.Mathematics;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    public static class MathUtils
    {
        [BurstCompile]
        public static float MinkowskiDistance(in float2 point1, in float2 point2, in float p)
        {
            // manhattan distance is p = 1
            // euclidean distance is p = 2
            
            var deltaX = math.abs(point2.x - point1.x);
            var deltaY = math.abs(point2.y - point1.y);

            var distance = math.pow(math.pow(deltaX, p) + math.pow(deltaY, p), 1 / p);
            return distance;
        }
        
        [BurstCompile]
        public static void RotateDirection(in float2 direction, float angleInRadians, out float2 rotatedDirection)
        {
            var cosTheta = math.cos(angleInRadians);
            var sinTheta = math.sin(angleInRadians);
            rotatedDirection = new float2
            (
                direction.x * cosTheta - direction.y * sinTheta,
                direction.x * sinTheta + direction.y * cosTheta
            );
        }
        
        [BurstCompile]
        public static float GetDirectionOffsetAngleInRadians(in float2 directionA, in float2 directionB)
        {
            return math.acos(math.dot(directionA, directionB));
        }
    }
}