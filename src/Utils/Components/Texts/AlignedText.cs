

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Input;

namespace project_republics.Utils.Components.Texts;

public class AlignedText : Text
{
    protected float _ax, _ay;
    protected Vector2 _textSize;

    public AlignedText(Fonts font, string content, Vector2 position, float ax, float ay) : base(font, content, position)
    {
        _ax = ax;
        _ay = ay;
    }

    public override void Draw()
    {
        MainGame.Batch.DrawString(MainGame.CL.Fonts[_font], Content, Position, Color, 0f, new Vector2(_textSize.X * _ax, _textSize.Y * _ay), Scale, SpriteEffects.None, LayerDepth);
    }

    public override string Content
    {
        get => base.Content;
        set
        {
            _content = value;
            _textSize = MainGame.CL.Fonts[_font].MeasureString(_content);
        }
    }

    public Vector2 TextSize
    {
        get
        {
            return _textSize;
        }
    }

}