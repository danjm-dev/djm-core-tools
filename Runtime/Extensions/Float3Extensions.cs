using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace DJM.CoreTools.Extensions
{
    public static class Float3Extensions
    {
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AsVector(this float3 v) => new(v.x, v.y, v.z);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XXAsVector(this float3 v) => new(v.x, v.x);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XYAsVector(this float3 v) => new(v.x, v.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 XZAsVector(this float3 v) => new(v.x, v.z);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 YXAsVector(this float3 v) => new(v.y, v.x);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 YYAsVector(this float3 v) => new(v.y, v.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 YZAsVector(this float3 v) => new(v.y, v.z);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ZXAsVector(this float3 v) => new(v.z, v.x);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ZYAsVector(this float3 v) => new(v.z, v.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ZZAsVector(this float3 v) => new(v.z, v.z);
    }
}