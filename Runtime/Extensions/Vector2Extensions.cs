using Unity.Mathematics;
using UnityEngine;

namespace DJM.CoreTools.Extensions
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Returns a Boolean indicating whether the current Vector2 is in a given range from another Vector2
        /// </summary>
        /// <param name="current">The current Vector2 position</param>
        /// <param name="target">The Vector2 position to compare against</param>
        /// <param name="range">The range value to compare against</param>
        /// <returns>True if the current Vector2 is in the given range from the target Vector2, false otherwise</returns>
        public static bool InRangeOf(this Vector2 current, Vector2 target, float range)
        {
            return (current - target).sqrMagnitude <= range * range;
        }

        public static Vector3 X0Y(this Vector2 current, float y = 0f)
        {
            return new Vector3(current.x, y, current.y);
        }
        
        public static Vector3 XY0(this Vector2 current, float z = 0f)
        {
            return new Vector3(current.x, current.y, z);
        }
    }
}