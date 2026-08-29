
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Input;

namespace project_republics.Components.World.Sections;

public class BaseTile
{
    private readonly Chunk _chunkRef;
    private Sprite _sprite;
    public BaseTile(Chunk chunkRef, Vector2 inChunkPosition, Textures texture)
    {
        _chunkRef = chunkRef;
        _sprite = new(texture, (_chunkRef.Position.ToVector2() * WorldContainer.CHUNK_SIDE + inChunkPosition) * 32)
        {
            Scale = 2f
        };
    }
    public virtual void Draw()
    {
        _sprite.Draw();
    }
}