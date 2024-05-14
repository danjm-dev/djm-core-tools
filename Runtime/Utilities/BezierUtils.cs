using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    public static class BezierUtils
    {
        [BurstCompile]
        public static void CubicPosition(in float2 p0, in float2 p1, in float2 p2, in float2 p3, in float t, out float2 position)
        {
            var threeP0 = 3 * p0;
            var threeP1 = 3 * p1;
            var threeP2 = 3 * p2;
    
            position = p0 + 
                       t * (-threeP0 + threeP1) + 
                       math.pow(t, 2) * (threeP0 - 6 * p1 + threeP2) + 
                       math.pow(t, 3) * (-p0 + threeP1 - threeP2 + p3);
        }
        
        [BurstCompile]
        public static void CubicPosition(in float3 p0, in float3 p1, in float3 p2, in float3 p3, in float t, out float3 position)
        {
            var threeP0 = 3 * p0;
            var threeP1 = 3 * p1;
            var threeP2 = 3 * p2;
            
            position = p0 + 
                       t * (-threeP0 + threeP1) + 
                       math.pow(t, 2) * (threeP0 - 6 * p1 + threeP2) + 
                       math.pow(t, 3) * (-p0 + threeP1 - threeP2 + p3);
        }
        
        [BurstCompile]
        public static void CubicDirection(in float2 p0, in float2 p1, in float2 p2, in float2 p3, in float t, out float2 direction)
        {
            var oneMinusT = 1 - t;
            direction = 3 * oneMinusT * oneMinusT * (p1 - p0) +
                        6 * oneMinusT * t * (p2 - p1) +
                        3 * t * t * (p3 - p2);
        }
        
        [BurstCompile]
        public static void CubicDirection(in float3 p0, in float3 p1, in float3 p2, in float3 p3, in float t, out float3 direction)
        {
            var oneMinusT = 1 - t;
            direction = 3 * oneMinusT * oneMinusT * (p1 - p0) +
                        6 * oneMinusT * t * (p2 - p1) +
                        3 * t * t * (p3 - p2);
        }
        
        
        
        [BurstCompile]
        public static void CubicPosition2(in float2 p0, in float2 p1, in float2 p2, in float2 p3, in float t, out float2 position)
        {
            var tPow2 = Mathf.Pow(t, 2);
            var tPow3 = Mathf.Pow(t, 3);
            var threeTPow2 = 3 * tPow2;
            var threeTPow3 = 3 * tPow3;
            var threeT = 3 * t;
        
            position = p0 * (-tPow3 + threeTPow2 - threeT + 1) + 
                       p1 * (threeTPow3 - 2 * threeTPow2 + threeT) + 
                       p2 * (-threeTPow3 + threeTPow2) + 
                       p3 * tPow3;
        }
        
        [BurstCompile]
        public static void CubicPosition2(in float3 p0, in float3 p1, in float3 p2, in float3 p3, in float t, out float3 position)
        {
            var tPow2 = Mathf.Pow(t, 2);
            var tPow3 = Mathf.Pow(t, 3);
            var threeTPow2 = 3 * tPow2;
            var threeTPow3 = 3 * tPow3;
            var threeT = 3 * t;
        
            position = p0 * (-tPow3 + threeTPow2 - threeT + 1) + 
                       p1 * (threeTPow3 - 2 * threeTPow2 + threeT) + 
                       p2 * (-threeTPow3 + threeTPow2) + 
                       p3 * tPow3;
        }
        
        [BurstCompile]
        public static void CubicDirection2(in float2 p0, in float2 p1, in float2 p2, in float2 p3, in float t, out float2 direction)
        {
            var tPower2 = Mathf.Pow(t, 2);
            var threeTPow2 = 3 * tPower2;
            var nineTPow2 = 9 * tPower2;
            var sixT = 6 * t;
        
            direction = p0 * (-threeTPow2 + sixT - 3) + 
                        p1 * (nineTPow2 - 2 * sixT + 3) + 
                        p2 * (-nineTPow2 + sixT) + 
                        p3 * threeTPow2;
        }
        
        [BurstCompile]
        public static void CubicDirection2(in float3 p0, in float3 p1, in float3 p2, in float3 p3, in float t, out float3 direction)
        {
            var tPower2 = Mathf.Pow(t, 2);
            var threeTPow2 = 3 * tPower2;
            var nineTPow2 = 9 * tPower2;
            var sixT = 6 * t;
        
            direction = p0 * (-threeTPow2 + sixT - 3) + 
                        p1 * (nineTPow2 - 2 * sixT + 3) + 
                        p2 * (-nineTPow2 + sixT) + 
                        p3 * threeTPow2;
        }
    }
}