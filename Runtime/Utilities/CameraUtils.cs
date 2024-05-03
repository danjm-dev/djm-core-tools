using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    public static class CameraUtils
    {
        [BurstCompile]
        public static void GetOrthographicViewportSize
        (
            in float orthographicSize, 
            out float2 viewportSize
        )
        {
            var screenHeightInUnits = orthographicSize * 2;
            var screenWidthInUnits = screenHeightInUnits * (Screen.width / (float)Screen.height);
            viewportSize = new float2(screenWidthInUnits, screenHeightInUnits);
        }
    }
}