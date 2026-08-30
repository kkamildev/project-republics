

using Microsoft.Xna.Framework;
using project_republics.Utils.Input;

namespace project_republics.Components.World.Sections;

public class Chunk
{
    private readonly Sector _sectorRef;
    private ByteVector2 _position;
    private BaseTile[,] _tiles;
    private bool _visible;
    public Chunk(Sector sectorRef, ByteVector2 position)
    {
        _visible = true;
        _sectorRef = sectorRef;
        _position = position;
        _tiles = new BaseTile[WorldContainer.CHUNK_SIDE, WorldContainer.CHUNK_SIDE];
        for(int i = 0;i<16;i++)
        {
            for(int j = 0;j<16;j++)
            {
                _tiles[i, j] = new BaseTile(this, new Vector2(j, i), Textures.GRASS_TILE);
            }
        }
    }

    public void SetPositionToTiles(Vector2 newPosition)
    {
        Vector2 chunkPosition = newPosition / 32 / WorldContainer.CHUNK_SIDE;

        if(Vector2.Distance(chunkPosition, _position.ToVector2()) > 5f)
        {
            _visible = false;
        } else
        {
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