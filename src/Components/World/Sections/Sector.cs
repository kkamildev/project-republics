
using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using project_republics.Utils.DataStructures;

namespace project_republics.Components.World.Sections;

public class Sector
{
    private readonly WorldContainer _worldRef;
    private readonly SectorData _data;
    private ByteVector2 _position;
    private Vector2 _viewPosition;
    private Chunk[,] _chunks;
    private Sector(WorldContainer worldRef, ByteVector2 position, SectorData data)
    {
        _data = data;
        _worldRef = worldRef;
        _position = position;   
        _chunks = new Chunk[WorldContainer.SECTOR_CHUNKS_SIDE, WorldContainer.SECTOR_CHUNKS_SIDE];
    }

    public static async Task<Sector> GenSector(WorldContainer worldRef, ByteVector2 position)
    {
        SectorData data = await worldRef.Storage.FindSector(position);
        data ??= worldRef.WorldGen.GenSector(position);
        Sector sector = new(worldRef, position, data);
        await sector.GenChunks();
        return sector;
    }

    public async Task GenChunks()
    {
        string currentChunk;
        for(int i = 0;i<_chunks.GetLength(0);i++)
        {
            for(int j = 0;j<_chunks.GetLength(1);j++)
            {
                try
                {
                    currentChunk = _data.ChunksData[i * _chunks.GetLength(0) + j];
                } catch(Exception)
                {
                    throw new Exception((i * _chunks.GetLength(0) + j).ToString());
                }
                _chunks[i, j] = new Chunk(this, new ByteVector2((byte)j, (byte)i), currentChunk);
            }
        }
    }

    public void SetViewPosition(Vector2 position, Action<bool> onChangeChunkVisibility)
    {
        _viewPosition = position;
        for(int i = 0;i<_chunks.GetLength(0);i++)
        {
            for(int j = 0;j<_chunks.GetLength(1);j++)
            {
                _chunks[i, j]?.SetPositionToTiles(_viewPosition, onChangeChunkVisibility);
            }
        }
    }

    public BaseTile GetTile()
    {
        Vector2 blockPos = _viewPosition / 32;
        Vector2 position = new(blockPos.X / WorldContainer.CHUNK_SIDE, blockPos.Y / WorldContainer.CHUNK_SIDE);
        return _chunks[(int)position.Y, (int)position.X].GetTile(blockPos);
    }

    public void Draw()
    {
        for(int i = 0;i<_chunks.GetLength(0);i++)
        {
            for(int j = 0;j<_chunks.GetLength(1);j++)
            {
                _chunks[i, j]?.Draw();
            }
        }
    }

    public void Update()
    {
        for(int i = 0;i<_chunks.GetLength(0);i++)
        {
            for(int j = 0;j<_chunks.GetLength(1);j++)
            {
                _chunks[i, j]?.Update();
            }
        }
    }

    public SectorData Data
    {
        get
        {
            return _data;
        }
    }

    public WorldContainer WorldRef
    {
        get
        {
            return _worldRef;
        }
    }

}