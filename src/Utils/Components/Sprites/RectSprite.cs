
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Input;

namespace project_republics.Utils.Components.Sprites;

public class RectSprite : RotatedSprite
{
    private Rectangle _rectangle;
    public RectSprite(Textures textures, Rectangle rectangle, float ax, float ay, float startingRotation) : base(textures, Vector2.Zero, ax, ay, startingRotation)
    {
        _rectangle = rectangle;
    }

    public override void Draw()
    {
        MainGame.Batch.Draw(MainGame.CL.Textures[_texture], _rectangle, null, Color, _rotation, new Vector2(MainGame.CL.Textures[_texture].Width * _ax, MainGame.CL.Textures[_texture].Height * _ay), SpriteEffects.None, LayerDepth);
    }

    public override Vector2 Position {
        get
        {
            return new Vector2(_rectangle.X, _rectangle.Y);
        }
        set
        {
            _rectangle.X = (int)value.X;
            _rectangle.Y = (int)value.Y;
        }
    }
}