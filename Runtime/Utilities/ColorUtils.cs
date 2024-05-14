using UnityEngine;

namespace DJM.CoreTools.Utilities
{
    public static class ColorUtils
    {
        public static void GenerateContrastingColors(Color[] colors, float saturation = 0.7f, float brightness = 0.6f)
        {
            var hueIncrement = 1.0f / colors.Length;

            for (var i = 0; i < colors.Length; i++)
            {
                var hue = i * hueIncrement;
                colors[i] = Color.HSVToRGB(hue, saturation, brightness);
            }
        }
    }
}