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
        
        public static Vector2 XX(this Vector3 v) => new(v.x, v.x);
        public static Vector2 XY(this Vector3 v) => new(v.x, v.y);
        public static Vector2 XZ(this Vector3 v) => new(v.x, v.z);
        public static Vector2 YX(this Vector3 v) => new(v.y, v.x);
        public static Vector2 YY(this Vector3 v) => new(v.y, v.y);
        public static Vector2 YZ(this Vector3 v) => new(v.y, v.z);
        public static Vector2 ZX(this Vector3 v) => new(v.z, v.x);
        public static Vector2 ZY(this Vector3 v) => new(v.z, v.y);
        public static Vector2 ZZ(this Vector3 v) => new(v.z, v.z);
        
        public static Vector3 WithX(this Vector3 v, float x) => new(x, v.y, v.z);
        public static Vector3 WithY(this Vector3 v, float y) => new(v.x, y, v.z);
        public static Vector3 WithZ(this Vector3 v, float z) => new(v.x, v.y, z);
        
        public static Vector3 AddX(this Vector3 v, float x) => new(v.x + x, v.y, v.z);
        public static Vector3 AddY(this Vector3 v, float y) => new(v.x, v.y + y, v.z);
        public static Vector3 AddZ(this Vector3 v, float z) => new(v.x, v.y, v.z + z);
    }
}