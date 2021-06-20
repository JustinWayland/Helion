using System;
using System.Collections.Generic;
using Helion.Geometry;
using Helion.Geometry.Vectors;
using Helion.Maps.Specials;
using Helion.Util;
using Helion.Util.Container;
using Helion.Util.Extensions;
using Helion.World.Geometry.Lines;
using Helion.World.Geometry.Sectors;
using Helion.World.Geometry.Sides;
using Helion.World.Geometry.Subsectors;
using Helion.World.Physics;
using static Helion.Util.Assertion.Assert;

namespace Helion.Render.Legacy.Shared.World
{
    public static class WorldTriangulator
    {
        public const double NoOverride = double.MaxValue;

        public static WallVertices HandleOneSided(Side side, in Vec2F textureUVInverse, double tickFraction,
            double overrideFloor = NoOverride, double overrideCeiling = NoOverride, bool isFront = true)
        {
            Precondition(tickFraction >= 0.0 && tickFraction <= 1.0, "Tick interpolation out of unit range");

            Line line = side.Line;
            Sector sector = side.Sector;
            SectorPlane floor = sector.Floor;
            SectorPlane ceiling = sector.Ceiling;

            Vec2D left = isFront ? line.Segment.Start : line.Segment.End;
            Vec2D right = isFront ? line.Segment.End : line.Segment.Start;
            double topZ = overrideCeiling == NoOverride ? ceiling.PrevZ.Interpolate(ceiling.Z, tickFraction) : overrideCeiling;
            double bottomZ = overrideFloor == NoOverride ? floor.PrevZ.Interpolate(floor.Z, tickFraction) : overrideFloor;

            double length = line.Segment.Length;
            double spanZ = topZ - bottomZ;
            WallUV uv = CalculateOneSidedWallUV(line, side, length, textureUVInverse, spanZ, tickFraction);
            
            WorldVertex topLeft = new WorldVertex(left.X, left.Y, topZ, uv.TopLeft.X, uv.TopLeft.Y);
            WorldVertex topRight = new WorldVertex(right.X, right.Y, topZ, uv.BottomRight.X, uv.TopLeft.Y);
            WorldVertex bottomLeft = new WorldVertex(left.X, left.Y, bottomZ, uv.TopLeft.X, uv.BottomRight.Y);
            WorldVertex bottomRight = new WorldVertex(right.X, right.Y, bottomZ, uv.BottomRight.X, uv.BottomRight.Y);
            
            return new WallVertices(topLeft, topRight, bottomLeft, bottomRight);
        }

        public static WallVertices HandleTwoSidedLower(TwoSided facingSide, Side otherSide, 
            in Vec2F textureUVInverse, bool isFrontSide, double tickFraction)
        {
            Precondition(tickFraction >= 0.0 && tickFraction <= 1.0, "Tick interpolation out of unit range");

            Line line = facingSide.Line;
            Sector sector = facingSide.Sector;
            SectorPlane topFlat = otherSide.Sector.Floor;
            SectorPlane bottomFlat = sector.Floor;
            
            Vec2D left = isFrontSide ? line.Segment.Start : line.Segment.End;
            Vec2D right = isFrontSide ? line.Segment.End : line.Segment.Start;
            double topZ = topFlat.PrevZ.Interpolate(topFlat.Z, tickFraction);
            double bottomZ = bottomFlat.PrevZ.Interpolate(bottomFlat.Z, tickFraction);
            
            double length = line.Segment.Length;
            WallUV uv = CalculateTwoSidedLowerWallUV(line, facingSide, length, textureUVInverse, topZ, bottomZ, tickFraction);
            
            WorldVertex topLeft = new WorldVertex(left.X, left.Y, topZ, uv.TopLeft.X, uv.TopLeft.Y);
            WorldVertex topRight = new WorldVertex(right.X, right.Y, topZ, uv.BottomRight.X, uv.TopLeft.Y);
            WorldVertex bottomLeft = new WorldVertex(left.X, left.Y, bottomZ, uv.TopLeft.X, uv.BottomRight.Y);
            WorldVertex bottomRight = new WorldVertex(right.X, right.Y, bottomZ, uv.BottomRight.X, uv.BottomRight.Y);
            
            return new WallVertices(topLeft, topRight, bottomLeft, bottomRight);
        }
        
        public static WallVertices HandleTwoSidedMiddle(TwoSided facingSide, 
            in Dimension textureDimension, in Vec2F textureUVInverse, double bottomOpeningZ, double topOpeningZ,
            bool isFrontSide, out bool nothingVisible, double tickFraction)
        {
            if (LineOpening.IsRenderingBlocked(facingSide.Line))
            {
                nothingVisible = true;
                return default;
            }

            Line line = facingSide.Line;
            MiddleDrawSpan drawSpan = CalculateMiddleDrawSpan(line, facingSide, bottomOpeningZ, topOpeningZ, textureDimension);
            if (drawSpan.NotVisible())
            {
                nothingVisible = true;
                return default;
            }

            Vec2D left = isFrontSide ? line.Segment.Start : line.Segment.End;
            Vec2D right = isFrontSide ? line.Segment.End : line.Segment.Start;
            double length = line.Segment.Length;
            WallUV uv = CalculateTwoSidedMiddleWallUV(facingSide, length, drawSpan, textureUVInverse, tickFraction);
            
            WorldVertex topLeft = new WorldVertex(left.X, left.Y, drawSpan.VisibleTopZ, uv.TopLeft.X, uv.TopLeft.Y);
            WorldVertex topRight = new WorldVertex(right.X, right.Y, drawSpan.VisibleTopZ, uv.BottomRight.X, uv.TopLeft.Y);
            WorldVertex bottomLeft = new WorldVertex(left.X, left.Y, drawSpan.VisibleBottomZ, uv.TopLeft.X, uv.BottomRight.Y);
            WorldVertex bottomRight = new WorldVertex(right.X, right.Y, drawSpan.VisibleBottomZ, uv.BottomRight.X, uv.BottomRight.Y);

            nothingVisible = false;
            return new WallVertices(topLeft, topRight, bottomLeft, bottomRight);
        }

        public static WallVertices HandleTwoSidedUpper(Side facingSide, Side otherSide, in Vec2F textureUVInverse, 
            bool isFrontSide, double tickFraction, double overrideTopZ = NoOverride)
        {
            Precondition(tickFraction >= 0.0 && tickFraction <= 1.0, "Tick interpolation out of unit range");

            Line line = facingSide.Line;
            Sector sector = facingSide.Sector;
            SectorPlane topPlane = sector.Ceiling;
            SectorPlane bottomPlane = otherSide.Sector.Ceiling;
            
            Vec2D left = isFrontSide ? line.Segment.Start : line.Segment.End;
            Vec2D right = isFrontSide ? line.Segment.End : line.Segment.Start;
            double topZ = overrideTopZ == NoOverride ? topPlane.PrevZ.Interpolate(topPlane.Z, tickFraction) : overrideTopZ;
            double bottomZ = bottomPlane.PrevZ.Interpolate(bottomPlane.Z, tickFraction);
            
            // TODO: If unchanging, we can pre-calculate the length.
            double length = line.Segment.Length;
            double spanZ = topZ - bottomZ;
            WallUV uv = CalculateTwoSidedUpperWallUV(line, facingSide, length, textureUVInverse, spanZ, tickFraction);
            
            WorldVertex topLeft = new WorldVertex(left.X, left.Y, topZ, uv.TopLeft.X, uv.TopLeft.Y);
            WorldVertex topRight = new WorldVertex(right.X, right.Y, topZ, uv.BottomRight.X, uv.TopLeft.Y);
            WorldVertex bottomLeft = new WorldVertex(left.X, left.Y, bottomZ, uv.TopLeft.X, uv.BottomRight.Y);
            WorldVertex bottomRight = new WorldVertex(right.X, right.Y, bottomZ, uv.BottomRight.X, uv.BottomRight.Y);
            
            return new WallVertices(topLeft, topRight, bottomLeft, bottomRight);
        }

        /// <summary>
        /// Triangulates a subsector by populating the provided dynamic array
        /// of vertices.
        /// </summary>
        /// <param name="subsector">The subsector to triangulate.</param>
        /// <param name="sectorPlane">The flat plane for the subsector.</param>
        /// <param name="textureDimension">The texture dimension.</param>
        /// <param name="tickFraction">The fractional value for interpolating
        /// the subsector.</param>
        /// <param name="verticesToPopulate">An output array where vertices are
        /// written to upon triangulating.</param>
        public static void HandleSubsector(Subsector subsector, SectorPlane sectorPlane, in Dimension textureDimension, 
            double tickFraction, DynamicArray<WorldVertex> verticesToPopulate, double overrideZ = int.MaxValue)
        {
            Precondition(tickFraction >= 0.0 && tickFraction <= 1.0, "Tick interpolation out of unit range");
            Precondition(subsector.ClockwiseEdges.Count >= 3, "Cannot render subsector when it's degenerate (should have 3+ edges)");
            
            List<SubsectorSegment> edges = subsector.ClockwiseEdges;
            verticesToPopulate.Clear();

            if (sectorPlane.Facing == SectorPlaneFace.Ceiling)
            {
                for (int i = 0; i < edges.Count; i++)
                {
                    Vec2D vertex = edges[i].Start;
                    
                    // TODO: Interpolation and slopes needs a slight change in
                    //       how we store sector flat plane information.
                    double z = sectorPlane.PrevZ.Interpolate(sectorPlane.Z, tickFraction);
                    if (overrideZ != int.MaxValue)
                        z = overrideZ;
                    
                    Vec3F position = ((float)vertex.X, (float)vertex.Y, (float)z);
                    Vec2F uv = CalculateFlatUV(vertex, textureDimension);
                    
                    verticesToPopulate.Add(new WorldVertex(position, uv));
                }
            }
            else
            {
                // Because the floor is looked at downwards and because it is
                // clockwise, to get counter-clockwise vertices we reverse the
                // iteration order and go from the end vertex.
                for (int i = edges.Count - 1; i >= 0; i--)
                {
                    Vec2D vertex = edges[i].End;
                    
                    // TODO: Interpolation and slopes needs a slight change in
                    //       how we store sector flat plane information.
                    double z = sectorPlane.PrevZ.Interpolate(sectorPlane.Z, tickFraction);
                    if (overrideZ != int.MaxValue)
                        z = overrideZ;

                    Vec3F position = ((float)vertex.X, (float)vertex.Y, (float)z);
                    Vec2F uv = CalculateFlatUV(vertex, textureDimension);
                    
                    verticesToPopulate.Add(new WorldVertex(position, uv));
                }
            }
        }
        
        private static MiddleDrawSpan CalculateMiddleDrawSpan(Line line, TwoSided facingSide, double bottomOpeningZ, 
            double topOpeningZ, in Dimension textureDimension)
        {
            double topZ = topOpeningZ;
            double bottomZ = topZ - textureDimension.Height;
            if (line.Flags.Unpegged.Lower)
            {
                bottomZ = bottomOpeningZ;
                topZ = bottomZ + textureDimension.Height;
            }

            topZ += facingSide.Offset.Y;
            bottomZ += facingSide.Offset.Y;

            // Check if the lower/upper textures are set. If not then then the middle can be drawn through.
            double visibleTopZ = topZ;
            if (facingSide.Upper.TextureHandle != Constants.NoTextureIndex)
                visibleTopZ = Math.Min(topZ, topOpeningZ);
            double visibleBottomZ = bottomZ;
            if (facingSide.Lower.TextureHandle != Constants.NoTextureIndex)
                visibleBottomZ = Math.Max(bottomZ, bottomOpeningZ);

            return new MiddleDrawSpan(bottomZ, topZ, visibleBottomZ, visibleTopZ);
        }

        private static WallUV CalculateOneSidedWallUV(Line line, Side side, double length, 
            in Vec2F textureUVInverse, double spanZ, double tickFraction)
        {
            Vec2F offsetUV = side.Offset.Float * textureUVInverse;
            if (side.ScrollData != null)
                offsetUV += GetScrollOffset(side.ScrollData, SideScrollData.MiddlePosition, textureUVInverse, tickFraction);
            float wallSpanU = (float)length * textureUVInverse.U;
            float spanV = (float)spanZ * textureUVInverse.V;

            float leftU = offsetUV.U;
            float rightU = offsetUV.U + wallSpanU;
            float topV;
            float bottomV;
            
            if (line.Flags.Unpegged.Lower)
            {
                bottomV = 1.0f + offsetUV.V;
                topV = bottomV - spanV;
            }
            else
            {
                topV = offsetUV.V;
                bottomV = offsetUV.V + spanV;
            }
            
            return new WallUV(new Vec2F(leftU, topV), new Vec2F(rightU, bottomV));   
        }

        private static WallUV CalculateTwoSidedLowerWallUV(Line line, Side facingSide, double length, 
            in Vec2F textureUVInverse, double topZ, double bottomZ, double tickFraction)
        {
            Vec2F offsetUV = facingSide.Offset.Float * textureUVInverse;
            if (facingSide.ScrollData != null)
                offsetUV += GetScrollOffset(facingSide.ScrollData, SideScrollData.LowerPosition, textureUVInverse, tickFraction);
            float wallSpanU = (float)length * textureUVInverse.U;

            float leftU = offsetUV.U;
            float rightU = offsetUV.U + wallSpanU;
            float topV;
            float bottomV;
            
            if (line.Flags.Unpegged.Lower)
            {
                double ceilZ = facingSide.Sector.Ceiling.Z;
                float topDistFromCeil = (float)(ceilZ - topZ);
                float bottomDistFromCeil = (float)(ceilZ - bottomZ);
                
                topV = offsetUV.V + (topDistFromCeil * textureUVInverse.V);
                bottomV = offsetUV.V + (bottomDistFromCeil * textureUVInverse.V);
            }
            else
            {
                float spanZ = (float)(topZ - bottomZ);
                float spanV = spanZ * textureUVInverse.V;

                topV = offsetUV.V;
                bottomV = offsetUV.V + spanV;
            }
            
            return new WallUV(new Vec2F(leftU, topV), new Vec2F(rightU, bottomV)); 
        }
        
        private static WallUV CalculateTwoSidedMiddleWallUV(Side side, double length, in MiddleDrawSpan drawSpan,
            in Vec2F textureUVInverse, double tickFraction)
        {
            Vec2F offsetUV = side.Offset.Float * textureUVInverse;
            if (side.ScrollData != null)
                offsetUV += GetScrollOffset(side.ScrollData, SideScrollData.MiddlePosition, textureUVInverse, tickFraction);
            float wallSpanU = (float)length * textureUVInverse.U;
            
            float leftU = offsetUV.U;
            float rightU = offsetUV.U + wallSpanU;
            
            // Since we only draw one of the texture, all we need to do is find
            // out where the texture is clamped by and find that value between
            // [0.0, 1.0]. For example if a texture height of 10 only has two
            // pixels available between 6 -> 7 for the line opening, then
            // the top V would be 0.6 and the bottom V would be 0.7.
            double textureHeight = drawSpan.TopZ - drawSpan.BottomZ;
            float topV = 1.0f - (float)((drawSpan.VisibleTopZ - drawSpan.BottomZ) / textureHeight);
            float bottomV = 1.0f - (float)((drawSpan.VisibleBottomZ - drawSpan.BottomZ) / textureHeight);
            
            return new WallUV(new Vec2F(leftU, topV), new Vec2F(rightU, bottomV)); 
        }
        
        private static WallUV CalculateTwoSidedUpperWallUV(Line line, Side side, double length, 
            in Vec2F textureUVInverse, double spanZ, double tickFraction)
        {
            Vec2F offsetUV = side.Offset.Float * textureUVInverse;
            if (side.ScrollData != null)
                offsetUV += GetScrollOffset(side.ScrollData, SideScrollData.UpperPosition, textureUVInverse, tickFraction);
            float wallSpanU = (float)length * textureUVInverse.U;
            float spanV = (float)spanZ * textureUVInverse.V;

            float leftU = offsetUV.U;
            float rightU = offsetUV.U+ wallSpanU;
            float topV;
            float bottomV;
            
            if (line.Flags.Unpegged.Upper)
            {
                topV = offsetUV.V;
                bottomV = topV + spanV;
            }
            else
            {
                bottomV = 1.0f + offsetUV.V;
                topV = bottomV - spanV;
            }
            
            return new WallUV(new Vec2F(leftU, topV), new Vec2F(rightU, bottomV));   
        }

        private static Vec2F GetScrollOffset(SideScrollData scrollData, int position, in Vec2F textureUVInverse, double tickFraction)
        {
            Vec2F lastOffset = scrollData.LastOffset[position].Float;

            Vec2F vec = scrollData.Offset[position].Float - lastOffset;
            vec.X *= (float)tickFraction;
            vec.Y *= (float)tickFraction;

            return (lastOffset + vec) * textureUVInverse;
        }
        
        private static Vec2F CalculateFlatUV(in Vec2D vertex, in Dimension textureDimension)
        {
            // TODO: Sector offsets will go here eventually.
            Vec2F uv = vertex.Float / textureDimension.Vector.Float;
            
            // When we map coordinates to their texture coordinates, because
            // we do division above, a coordinate with Y values of 16 to 32
            // for a 64-dimension texture gets mapped onto 0.25 and 0.5.
            // However the textures are drawn from the top down in vanilla
            // (and all the other ports), which means 16 is effectively 0.75
            // and 32 is 0.5.
            //
            // This means our drawing is inverted along the Y axis, and this is
            // trivially fixed by inverting letting the shader take care of the
            // rest when it clamps it to the image.
            uv.Y = -uv.Y;
            return uv;
        }
    }
}