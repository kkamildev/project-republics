
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Components.World.Enums;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Exceptions;
using project_republics.Utils.Input;

namespace project_republics.Components.World.Sections;

public class BaseTile : IWorldObject
{
    private readonly Chunk _chunkRef;
    private readonly Sprite _sprite;
    private readonly Vector2 _primaryPosition;
    private readonly Biomes _biome;
    public BaseTile(Chunk chunkRef, Vector2 inChunkPosition, Textures texture, Biomes biome)
    {
        
        _chunkRef = chunkRef;
        _primaryPosition = (_chunkRef.Position.ToVector2() * WorldContainer.CHUNK_SIDE + inChunkPosition) * 32 + MainGame.Resolution / 2;
        _biome = biome;
        _sprite = new(texture, _primaryPosition)
        {
            Scale = 2f
        };
    }

    public BaseTile()
    {
        
    }

    public BaseTile(Textures texture, Biomes biome)
    {
        _biome = biome;
        _sprite = new(texture, Vector2.Zero);
    }

    public virtual void Draw()
    {
        _sprite.Draw();
    }


    public virtual void UpdateGraph()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public string Serialize()
    {
        return $"0>{(int)_sprite.Texture},{(byte)_biome};";
    }

    public IWorldObject Parse(Chunk chunkRef, Vector2 inChunkPosition, string data)
    {
        try
        {
            string[] args = data.Split(",");
            return new BaseTile(chunkRef, inChunkPosition, (Textures)int.Parse(args[0]), (Biomes)byte.Parse(args[1]));
        } catch(Exception)
        {
            throw new WorldObjectParseException("Tile", data);
        }
    }

    public virtual Vector2 Position
    {
        set
        {
            _sprite.Position = _primaryPosition - value;
        }
    }

    public Biomes Biome
    {
        get
        {
            return _biome;
        }
    }
}