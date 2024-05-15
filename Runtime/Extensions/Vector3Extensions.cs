using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DJM.CoreTools.Extensions
{
    public static class Vector3Extensions
    {
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XX(this Vector3 v) => new(v.x, v.x);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XY(this Vector3 v) => new(v.x, v.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XZ(this Vector3 v) => new(v.x, v.z);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 YX(this Vector3 v) => new(v.y, v.x);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 YY(this Vector3 v) => new(v.y, v.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 YZ(this Vector3 v) => new(v.y, v.z);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ZX(this Vector3 v) => new(v.z, v.x);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ZY(this Vector3 v) => new(v.z, v.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ZZ(this Vector3 v) => new(v.z, v.z);
        
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithX(this Vector3 v, float x) => new(x, v.y, v.z);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithY(this Vector3 v, float y) => new(v.x, y, v.z);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 WithZ(this Vector3 v, float z) => new(v.x, v.y, z);
        
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddX(this Vector3 v, float x) => new(v.x + x, v.y, v.z);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddY(this Vector3 v, float y) => new(v.x, v.y + y, v.z);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddZ(this Vector3 v, float z) => new(v.x, v.y, v.z + z);
    }
}