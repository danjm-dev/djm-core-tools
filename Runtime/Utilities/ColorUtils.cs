using UnityEngine;

namespace DJM.CoreTools.Utilities
{
    public static class ColorUtils
    {
        public static Color[] GenerateContrastingColors(int count)
        {
            var colors = new Color[count];

            var hueIncrement = 1.0f / count;

            for (var i = 0; i < count; i++)
            {
                var hue = i * hueIncrement;
                var saturation = 0.7f; // You can adjust saturation and lightness based on your preference
                var lightness = 0.6f;

                colors[i] = Color.HSVToRGB(hue, saturation, lightness);
            }

            return colors;
        }
    }
}