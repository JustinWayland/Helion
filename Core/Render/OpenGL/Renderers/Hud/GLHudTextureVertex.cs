﻿using System.Runtime.InteropServices;
using Helion.Geometry.Vectors;

namespace Helion.Render.OpenGL.Renderers.Hud
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct GLHudTextureVertex
    {
        public readonly Vec3F Pos;
        public readonly Vec2F UV;

        public GLHudTextureVertex(Vec3F pos, Vec2F uv)
        {
            Pos = pos;
            UV = uv;
        }
    }
}