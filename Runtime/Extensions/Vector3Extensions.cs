using UnityEngine;

namespace DJM.CoreTools.Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Returns a Boolean indicating whether the current Vector3 is in a given range from another Vector3
        /// </summary>
        /// <param name="current">The current Vector3 position</param>
        /// <param name="target">The Vector3 position to compare against</param>
        /// <param name="range">The range value to compare against</param>
        /// <returns>True if the current Vector3 is in the given range from the target Vector3, false otherwise</returns>
        public static bool InRangeOf(this Vector3 current, Vector3 target, float range) 
        {
            return (current - target).sqrMagnitude <= range * range;
        }
        
        public static Vector2 XY(this Vector3 current) 
        {
            return new Vector2(current.x, current.y);
        }
        
        public static Vector2 XZ(this Vector3 current) 
        {
            return new Vector2(current.x, current.z);
        }
    }
}