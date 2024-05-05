using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    public static class ScreenSpaceUtils
    {
        [BurstCompile]
        public static void GetNormalizedScreenPosition(in float2 screenPosition, out float2 normalizedScreenPosition)
        {
            normalizedScreenPosition = new float2
            (
                math.clamp(screenPosition.x / Screen.width, 0f, 1f),
                math.clamp(screenPosition.y / Screen.height, 0f, 1f)
            );
        }
    }
}