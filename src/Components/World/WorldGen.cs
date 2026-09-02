
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
        string[] chunksData = new string[WorldContainer.SECTOR_CHUNKS_SIDE * WorldContainer.SECTOR_CHUNKS_SIDE];
        for(int i = 0;i<WorldContainer.SECTOR_CHUNKS_SIDE;i++)
        {
            for(int j = 0;j<WorldContainer.SECTOR_CHUNKS_SIDE;j++)
            {
                chunksData[i * WorldContainer.SECTOR_CHUNKS_SIDE + j] = GenChunk(sectorPosition.ToVector2() * WorldContainer.SECTOR_CHUNKS_SIDE + new Vector2(j, i));
            }
        }
        data.ChunksData = chunksData;
        return data;
    }
    private string GenChunk(Vector2 chunkPosition)
    {
        string data = "";
        // each chunk is represented by one string line
        // 0>32;0>12;0>1;
        for(int i = 0;i<WorldContainer.CHUNK_SIDE;i++)
        {
            for(int j = 0;j<WorldContainer.CHUNK_SIDE;j++)
            {
                if(MainGame.Random.Next(0, 2) == 1)
                {
                    data+= "0>10;";
                } else
                {
                    data+= "0>9;";
                }
                // TODO: generating tiles using perlin noise
            }
        }
        return data;
    }

    public IWorldObject GetWorldObjectSingleton(int index)
    {
        return _singletonArray[index];
    }
}