using System;

namespace ArgUrTMapManager
{
	[Flags]
	public enum SurfaceParms : int
	{
		None = 0,
		SURF_NODAMAGE = 0x1,		// never give falling damage
		SURF_SLICK = 0x2,			// effects game physics
		SURF_SKY = 0x4,				// lighting from environment map
		SURF_LADDER = 0x8,			// make surface climbable
		SURF_NOIMPACT = 0x10,		// don't make missile explosions
		SURF_NOMARKS = 0x20,		// don't leave missile marks
		SURF_FLESH = 0x40,			// make flesh sounds and effects
		SURF_NODRAW = 0x80,			// don't generate a drawsurface at all
		SURF_HINT = 0x100,			// make a primary bsp splitter
		SURF_SKIP = 0x200,			// completely ignore, allowing non-closed brushes
		SURF_NOLIGHTMAP = 0x400,	// surface doesn't need a lightmap
		SURF_POINTLIGHT = 0x800,	// generate lighting info at vertexes
		SURF_METALSTEPS = 0x1000,	// clanking footsteps
		SURF_NOSTEPS = 0x2000,		// no footstep sounds
		SURF_NONSOLID = 0x4000,		// don't collide against curves with this set
		SURF_LIGHTFILTER = 0x8000,	// act as a light filter during q3map -light
		SURF_ALPHASHADOW = 0x10000,	// do per-pixel light shadow casting in q3map
		SURF_NODLIGHT = 0x20000,	// don't dlight even if solid (solid lava, skies)
		SURF_DUST
	}
}
