
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Input;

namespace project_republics.Utils.Components.Sprites;

public class RawTextureSprite : Sprite
{
    private Texture2D _rawTexture;
    public RawTextureSprite(Texture2D rawTexture, Vector2 position) : base(Textures.BACKGROUND, position)
    {
        _rawTexture = rawTexture;
    }

    public override void Draw()
    {
        MainGame.Batch.Draw(_rawTexture, _position, null, Color, 0f, Vector2.Zero, Scale, SpriteEffects.None, LayerDepth);
    }
}