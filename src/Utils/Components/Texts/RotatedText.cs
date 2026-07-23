
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Input;

namespace project_republics.Utils.Components.Texts;

public class RotatedText : AlignedText
{
    protected float _rotation;
    public RotatedText(Fonts font, string content, Vector2 position, float ax, float ay, float startingRotation) : base(font, content, position, ax, ay)
    {
        _rotation = startingRotation;
    }

    public override void Draw()
    {
        MainGame.Batch.DrawString(MainGame.CL.Fonts[_font], Content, Position, Color, _rotation, new Vector2(_textSize.X * _ax, _textSize.Y * _ay), Scale, SpriteEffects.None, LayerDepth);
    }

    public float RadRotation
    {
        get
        {
            return _rotation;
        }
        set
        {
            _rotation = value;
        }
    }

    public float DegRotation
    {
        get
        {
            return MathHelper.ToDegrees(_rotation);
        }
        set
        {
            _rotation = MathHelper.ToRadians(value);
        }
    }
}