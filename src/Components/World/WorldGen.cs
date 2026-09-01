
using project_republics.Utils.Generators;

namespace project_republics.Components.World;

public sealed class WorldGen
{
    private readonly PerlinNoise _perlin;
    private readonly int _seed;
    public WorldGen(int seed)
    {
        _seed = seed;
        _perlin = new(_seed);
    }
}