using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace DJM.CoreTools.Extensions
{
    public static class Float2Extensions
    {
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 X0Y(this float2 v, float y = 0f) => new(v.x, y, v.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 XY0(this float2 v, float z = 0f) => new(v.x, v.y, z);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 X0YAsVector(this float2 v, float y = 0f) => new(v.x, y, v.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XY0AsVector(this float2 v, float z = 0f) => new(v.x, v.y, z);
    }
}