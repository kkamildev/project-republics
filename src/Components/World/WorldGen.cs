
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using project_republics.Components.World.Sections;
using project_republics.Utils.DataStructures;
using project_republics.Utils.Exceptions;
using project_republics.Utils.Generators;

namespace project_republics.Components.World;

public sealed class WorldGen
{
    private readonly IWorldObject[] _singletonArray;
    private readonly Dictionary<float, BaseTile> _generatorTilesRanges;
    private readonly int _seed;
    private readonly PerlinNoise _perlin;
    public WorldGen(int seed)
    {
        _seed = seed;
        _perlin = new(_seed);
        _singletonArray = [
            new BaseTile()
        ];
        _generatorTilesRanges = new Dictionary<float, BaseTile>(){
            {0.5f, new BaseTile(Utils.Input.Textures.GRASS_TILE, Enums.Biomes.PLAINS)},
            {0.6f, new BaseTile(Utils.Input.Textures.DARK_GRASS_TILE, Enums.Biomes.PLAINS)},
            {1f, new BaseTile(Utils.Input.Textures.LIGHT_GRASS_TILE, Enums.Biomes.PLAINS)}
        };
        _generatorTilesRanges = _generatorTilesRanges.OrderBy(kv => kv.Key).ToDictionary(kv => kv.Key, kv => kv.Value);

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
        double noiseValue;
        KeyValuePair<float, BaseTile> foundTile;
        for(int i = 0;i<WorldContainer.CHUNK_SIDE;i++)
        {
            for(int j = 0;j<WorldContainer.CHUNK_SIDE;j++)
            {
                noiseValue = _perlin.Noise((chunkPosition * WorldContainer.CHUNK_SIDE + new Vector2(j, i)) * 0.01f);
                // TODO: generate other structures
                foundTile = _generatorTilesRanges.First((key) => noiseValue < key.Key);
                data += foundTile.Value.Serialize();
            }
        }
        return data;
    }

    public IWorldObject GetWorldObjectSingleton(int index)
    {
        return _singletonArray[index];
    }
}