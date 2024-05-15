﻿using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace DJM.CoreTools.Extensions
{
    public static class Vector2Extensions
    {
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 X0Y(this Vector2 current, float y = 0f) => new(current.x, y, current.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 XY0(this Vector2 current, float z = 0f) => new(current.x, current.y, z);
        
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 X0YFloat(this Vector2 current, float y = 0f) => new(current.x, y, current.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 XY0Float(this Vector2 current, float z = 0f) => new(current.x, current.y, z);
        
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 WithX(this Vector2 v, float x) => new(x, v.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 WithY(this Vector2 v, float y) => new(v.x, y);
        
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 AddX(this Vector2 v, float x) => new(v.x + x, v.y);
        
        [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 AddY(this Vector2 v, float y) => new(v.x, v.y + y);
    }
}