
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Input;

namespace project_republics.Components.World.Sections;

public class BaseTile
{
    private readonly Chunk _chunkRef;
    private Sprite _sprite;
    private readonly Vector2 _primaryPosition;
    public BaseTile(Chunk chunkRef, Vector2 inChunkPosition, Textures texture)
    {
        _chunkRef = chunkRef;
        _primaryPosition = (_chunkRef.Position.ToVector2() * WorldContainer.CHUNK_SIDE + inChunkPosition) * 32 + MainGame.Resolution / 2;
        _sprite = new(texture, _primaryPosition)
        {
            Scale = 2f
        };
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

    public virtual Vector2 Position
    {
        set
        {
            _sprite.Position = _primaryPosition - value;
        }
    }
}