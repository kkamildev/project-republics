
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Input;

namespace project_republics.Utils.Components.Sprites;

public class RectSprite : RotatedSprite
{
    private Rectangle _rectangle;
    public RectSprite(Textures textures, Rectangle rectangle, float ax, float ay, float startingRotation) : base(textures, new Vector2(rectangle.X, rectangle.Y), ax, ay, startingRotation)
    {
        _rectangle = rectangle;
    }

    public override void Draw()
    {
        MainGame.Batch.Draw(MainGame.CL.Textures[_texture], new Rectangle((int)_position.X, (int)_position.Y, _rectangle.Width, _rectangle.Height), null, Color, _rotation, new Vector2(MainGame.CL.Textures[_texture].Width * _ax, MainGame.CL.Textures[_texture].Height * _ay), SpriteEffects.None, LayerDepth);
    }

    public override Vector2 Position {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
            _rectangle.X = (int)value.X;
            _rectangle.Y = (int)value.Y;
        }
    }
}