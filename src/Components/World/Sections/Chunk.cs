

using System;
using Microsoft.Xna.Framework;
using project_republics.Utils.DataStructures;
using project_republics.Utils.Exceptions;
using project_republics.Utils.Input;

namespace project_republics.Components.World.Sections;

public class Chunk
{
    private readonly Sector _sectorRef;
    private ByteVector2 _position;
    private readonly BaseTile[,] _tiles;
    private bool _visible;
    public Chunk(Sector sectorRef, ByteVector2 position, string chunkData)
    {
        _visible = false;
        _sectorRef = sectorRef;
        _position = position;
        _tiles = new BaseTile[WorldContainer.CHUNK_SIDE, WorldContainer.CHUNK_SIDE];

        ParseChunk(chunkData);
        
    }
    private void ParseChunk(string chunkData)
    {
        // TODO: Create full parser
        string[] blocks = chunkData.Split(";");
        string[] blockData;
        IWorldObject objectSingleton;
        for(int i = 0;i<WorldContainer.CHUNK_SIDE;i++)
        {
            for(int j = 0;j<WorldContainer.CHUNK_SIDE;j++)
            {
                blockData = blocks[i * WorldContainer.CHUNK_SIDE + j].Split(">");
                objectSingleton = GetObjectToParse(blockData);
                _tiles[i, j] = objectSingleton.Parse(this, new Vector2(j, i), blockData[1]) as BaseTile;
            }
        }
        
    }
    private IWorldObject GetObjectToParse(string[] blockData)
    {
        try
        {
            int objectId = int.Parse(blockData[0]);
            return _sectorRef.WorldRef.WorldGen.GetWorldObjectSingleton(objectId);
        } catch(Exception)
        {
            throw new NotSupportedWorldObjectException(string.Join('>', blockData));
        }
    }


    public void SetPositionToTiles(Vector2 newPosition, Action<bool> onChangeChunkVisibility)
    {
        Vector2 chunkPosition = newPosition / 32 / WorldContainer.CHUNK_SIDE;

        if(Vector2.Distance(chunkPosition, _position.ToVector2()) > WorldContainer.PLAYER_GRAPH_RENDER_RANGE)
        {
            if(_visible) onChangeChunkVisibility.Invoke(false);
            _visible = false;
        } else
        {
            if(!_visible) onChangeChunkVisibility.Invoke(true);
            _visible = true;
        }
        if(_visible)
        {
            for(int i = 0;i<_tiles.GetLength(0);i++)
            {
                for(int j = 0;j<_tiles.GetLength(1);j++)
                {
                    _tiles[i, j].Position = newPosition;
                }
            }
        }
    }

    public void Draw()
    {
        if(_visible)
        {
            for(int i = 0;i<_tiles.GetLength(0);i++)
            {
                for(int j = 0;j<_tiles.GetLength(1);j++)
                {
                    _tiles[i, j]?.Draw();
                }
            }
        }
    }

    public void Update()
    {
        for(int i = 0;i<_tiles.GetLength(0);i++)
        {
            for(int j = 0;j<_tiles.GetLength(1);j++)
            {
                _tiles[i, j]?.Update();
                if(_visible) _tiles[i, j]?.UpdateGraph();
            }
        }
    }

    public ByteVector2 Position
    {
        get
        {
            return _position;
        }
    }

    public bool Visible
    {
        get
        {
            return _visible;
        }
    }
}