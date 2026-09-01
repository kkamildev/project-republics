
using Microsoft.Xna.Framework;
using project_republics.Components.World.Sections;
using project_republics.Utils.DataStructures;
using project_republics.Utils.Generators;

namespace project_republics.Components.World;

public sealed class WorldGen
{
    private readonly IWorldObject[] _singletonArray;
    private readonly PerlinNoise _perlin;
    private readonly int _seed;
    public WorldGen(int seed)
    {
        _seed = seed;
        _perlin = new(_seed);
        _singletonArray = [
            new BaseTile()
        ];
    }

    public SectorData GenSector(ByteVector2 sectorPosition)
    {
        SectorData data = new();
        string[] chunksData = new string[WorldContainer.MAP_SIDE * WorldContainer.MAP_SIDE];
        for(int i = 0;i<WorldContainer.MAP_SIDE;i++)
        {
            for(int j = 0;j<WorldContainer.MAP_SIDE;j++)
            {
                chunksData[i * WorldContainer.MAP_SIDE + j] = GenChunk(sectorPosition.ToVector2() * WorldContainer.SECTOR_CHUNKS_SIDE + new Vector2(j, i));
            }
        }

        return data;
    }
    private string GenChunk(Vector2 chunkPosition)
    {
        string data = "";
        for(int i = 0;i<WorldContainer.CHUNK_SIDE;i++)
        {
            for(int j = 0;j<WorldContainer.CHUNK_SIDE;j++)
            {
                // TODO: generating tiles using perlin noise
            }
        }
        return data;
    }

}