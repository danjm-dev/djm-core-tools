using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace DJM.CoreTools.Utilities
{
    [BurstCompile]
    public static class OrthographicCameraUtils
    {
        [BurstCompile]
        public static void GetViewportSize
        (
            in float orthographicSize, 
            out float2 viewportSize
        )
        {
            var screenHeightInUnits = orthographicSize * 2;
            var screenWidthInUnits = screenHeightInUnits * (Screen.width / (float)Screen.height);
            viewportSize = new float2(screenWidthInUnits, screenHeightInUnits);
        }

        [BurstCompile]
        public static void ScreenToWorldPosition
        (
            in float2 screenPosition, 
            in float2 cameraPosition,
            in float orthographicSize,
            out float2 worldPosition
        )
        {
            ScreenSpaceUtils.GetNormalizedScreenPosition(screenPosition, out var normalizedScreenPosition);
            GetViewportSize(orthographicSize, out var viewportSize);
            worldPosition = cameraPosition + (normalizedScreenPosition - 0.5f) * viewportSize;
        }
    }
}