using System;
using Helion.Render.OpenGL.Buffer.Array.Vertex;
using Helion.Render.OpenGL.Context;
using Helion.Render.OpenGL.Shader;
using Helion.Render.OpenGL.Shared;
using Helion.Render.OpenGL.Texture.Legacy;
using Helion.Render.OpenGL.Vertex;
using Helion.Render.OpenGL.Vertex.Attribute;
using Helion.Resources.Archives.Collection;
using Helion.Util.Configs;
using OpenTK.Graphics.OpenGL;
using static Helion.Util.Assertion.Assert;

namespace Helion.Render.OpenGL.Renderers.Legacy.World.Sky.Sphere;

public class SkySphereComponent : ISkyComponent
{
    private static readonly VertexArrayAttributes GeometryAttributes = new(
        new VertexPointerFloatAttribute("pos", 0, 3));

    private readonly IConfig m_config;
    private readonly StreamVertexBuffer<SkyGeometryVertex> m_geometryVbo;
    private readonly VertexArrayObject m_geometryVao;
    private readonly SkySphereGeometryShader m_geometryShaderProgram;
    private readonly SkySphereRenderer m_skySphereRenderer;
    private readonly bool m_flipSkyTexture;

    public bool HasGeometry => !m_geometryVbo.Empty;
    public VertexBufferObject<SkyGeometryVertex> Vbo => m_geometryVbo;

    public SkySphereComponent(IConfig config, ArchiveCollection archiveCollection, LegacyGLTextureManager textureManager, 
        int textureHandle, bool flipSkyTexture)
    {
        m_config = config;
        m_skySphereRenderer = new(archiveCollection, textureManager, textureHandle);

        m_geometryVao = new(GeometryAttributes, "VAO: Sky sphere geometry");
        m_geometryVbo = new(m_geometryVao, "VBO: Sky sphere geometry");
        using (ShaderBuilder builder = SkySphereGeometryShader.MakeBuilder())
            m_geometryShaderProgram = new(builder, GeometryAttributes);

        m_flipSkyTexture = flipSkyTexture;
    }

    ~SkySphereComponent()
    {
        FailedToDispose(this);
        ReleaseUnmanagedResources();
    }

    public void Clear()
    {
        m_geometryVbo.Clear();
    }

    public void Add(SkyGeometryVertex[] vertices, int length)
    {
        m_geometryVbo.Add(vertices, length);
    }

    public void RenderWorldGeometry(RenderInfo renderInfo)
    {
        m_geometryShaderProgram.Bind();

        GL.ActiveTexture(TextureUnit.Texture0);
        m_geometryShaderProgram.Mvp.Set(GLRenderer.CalculateMvpMatrix(renderInfo));

        m_geometryVbo.UploadIfNeeded();

        m_geometryVao.Bind();
        m_geometryVbo.DrawArrays();
        m_geometryVao.Unbind();

        m_geometryShaderProgram.Unbind();
    }

    public void RenderSky(RenderInfo renderInfo)
    {
        m_skySphereRenderer.Render(renderInfo, m_flipSkyTexture);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        m_geometryShaderProgram.Dispose();
        m_geometryVbo.Dispose();
        m_geometryVao.Dispose();

        m_skySphereRenderer.Dispose();
    }
}