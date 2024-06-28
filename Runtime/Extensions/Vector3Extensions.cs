using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace DJM.CoreTools.Extensions
{
    public static class Vector3Extensions
    {
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XXX(this Vector3 v) => new(v.x, v.x, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XXY(this Vector3 v) => new(v.x, v.x, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XXZ(this Vector3 v) => new(v.x, v.x, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XYX(this Vector3 v) => new(v.x, v.y, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XYY(this Vector3 v) => new(v.x, v.y, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XZX(this Vector3 v) => new(v.x, v.z, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XZY(this Vector3 v) => new(v.x, v.z, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XZZ(this Vector3 v) => new(v.x, v.z, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YXX(this Vector3 v) => new(v.y, v.x, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YXY(this Vector3 v) => new(v.y, v.x, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YXZ(this Vector3 v) => new(v.y, v.x, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YYX(this Vector3 v) => new(v.y, v.y, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YYY(this Vector3 v) => new(v.y, v.y, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YYZ(this Vector3 v) => new(v.y, v.y, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YZX(this Vector3 v) => new(v.y, v.z, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YZY(this Vector3 v) => new(v.y, v.z, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 YZZ(this Vector3 v) => new(v.y, v.z, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ZXX(this Vector3 v) => new(v.z, v.x, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ZXY(this Vector3 v) => new(v.z, v.x, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ZXZ(this Vector3 v) => new(v.z, v.x, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ZYX(this Vector3 v) => new(v.z, v.y, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ZYY(this Vector3 v) => new(v.z, v.y, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ZYZ(this Vector3 v) => new(v.z, v.y, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ZZX(this Vector3 v) => new(v.z, v.z, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ZZY(this Vector3 v) => new(v.z, v.z, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ZZZ(this Vector3 v) => new(v.z, v.z, v.z);
        
        
        
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
        public static float3 AsFloat(this Vector3 v) => new(v.x, v.y, v.z);
        
        
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 XXXAsFloat(this Vector3 v) => new(v.x, v.x, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 XXYAsFloat(this Vector3 v) => new(v.x, v.x, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 XXZAsFloat(this Vector3 v) => new(v.x, v.x, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 XYXAsFloat(this Vector3 v) => new(v.x, v.y, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 XYYAsFloat(this Vector3 v) => new(v.x, v.y, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 XZXAsFloat(this Vector3 v) => new(v.x, v.z, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 XZYAsFloat(this Vector3 v) => new(v.x, v.z, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 XZZAsFloat(this Vector3 v) => new(v.x, v.z, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 YXXAsFloat(this Vector3 v) => new(v.y, v.x, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 YXYAsFloat(this Vector3 v) => new(v.y, v.x, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 YXZAsFloat(this Vector3 v) => new(v.y, v.x, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 YYXAsFloat(this Vector3 v) => new(v.y, v.y, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 YYYAsFloat(this Vector3 v) => new(v.y, v.y, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 YYZAsFloat(this Vector3 v) => new(v.y, v.y, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 YZXAsFloat(this Vector3 v) => new(v.y, v.z, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 YZYAsFloat(this Vector3 v) => new(v.y, v.z, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 YZZAsFloat(this Vector3 v) => new(v.y, v.z, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ZXXAsFloat(this Vector3 v) => new(v.z, v.x, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ZXYAsFloat(this Vector3 v) => new(v.z, v.x, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ZXZAsFloat(this Vector3 v) => new(v.z, v.x, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ZYXAsFloat(this Vector3 v) => new(v.z, v.y, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ZYYAsFloat(this Vector3 v) => new(v.z, v.y, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ZYZAsFloat(this Vector3 v) => new(v.z, v.y, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ZZXAsFloat(this Vector3 v) => new(v.z, v.z, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ZZYAsFloat(this Vector3 v) => new(v.z, v.z, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ZZZAsFloat(this Vector3 v) => new(v.z, v.z, v.z);
        
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 XXAsFloat(this Vector3 v) => new(v.x, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 XYAsFloat(this Vector3 v) => new(v.x, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 XZAsFloat(this Vector3 v) => new(v.x, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 YXAsFloat(this Vector3 v) => new(v.y, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 YYAsFloat(this Vector3 v) => new(v.y, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 YZAsFloat(this Vector3 v) => new(v.y, v.z);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 ZXAsFloat(this Vector3 v) => new(v.z, v.x);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 ZYAsFloat(this Vector3 v) => new(v.z, v.y);
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 ZZAsFloat(this Vector3 v) => new(v.z, v.z);
        
        
        
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