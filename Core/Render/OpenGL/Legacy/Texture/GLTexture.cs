using System;
using Helion.Geometry;
using Helion.Geometry.Vectors;
using Helion.Graphics;
using Helion.Render.OpenGL.Legacy.Context;
using Helion.Render.OpenGL.Legacy.Context.Types;
using static Helion.Util.Assertion.Assert;

namespace Helion.Render.OpenGL.Legacy.Texture
{
    public abstract class GLTexture : IDisposable
    {    
        /// <summary>
        /// The OpenGL texture 'name' handle.
        /// </summary>
        public readonly int TextureId;
        
        /// <summary>
        /// A readable name for this texture.
        /// </summary>
        public readonly string Name;
        
        /// <summary>
        /// A precalculated inverse of the UV coordinates.
        /// </summary>
        public readonly Vec2F UVInverse;
        
        /// <summary>
        /// The dimension of this texture.
        /// </summary>
        public readonly Dimension Dimension;
        
        /// <summary>
        /// What type of texture it is with respect to OpenGL.
        /// </summary>
        public readonly TextureTargetType TextureType;

        public readonly ImageMetadata Metadata;
        
        protected readonly IGLFunctions gl;

        public int Width => Dimension.Width;
        public int Height => Dimension.Height;

        protected GLTexture(int textureId, string name, Dimension dimension, ImageMetadata metadata, IGLFunctions functions, 
            TextureTargetType textureType)
        {
            TextureId = textureId;
            Name = name;
            Dimension = dimension;
            Metadata = metadata;
            UVInverse = Vec2F.One / dimension.Vector.Float;
            gl = functions;
            TextureType = textureType;
        }

        ~GLTexture()
        {
            FailedToDispose(this);
            ReleaseUnmanagedResources();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        protected virtual void ReleaseUnmanagedResources()
        {
            gl.DeleteTexture(TextureId);
        }
    }
}