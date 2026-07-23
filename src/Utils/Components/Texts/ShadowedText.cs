
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Input;

namespace project_republics.Utils.Components.Texts;

public class ShadowedText : RotatedText
{
    public Color ShadowColor{get;set;}
    private Vector2 _shadowMargin;
    public ShadowedText(Fonts font, string content, Vector2 position, float ax, float ay, float startingRotation, Vector2 shadowMargin) : base(font, content, position, ax, ay, startingRotation)
    {
        ShadowColor = Color.Black;
        _shadowMargin = shadowMargin;
    }

    public ShadowedText(Fonts font, string content, Vector2 position, float ax, float ay, float startingRotation) : this(font, content, position, ax, ay, startingRotation, new Vector2(1))
    {
    }


    public override void Draw()
    {
        MainGame.Batch.DrawString(MainGame.CL.Fonts[_font], Content, Position + _shadowMargin, ShadowColor, _rotation, new Vector2(_textSize.X * _ax, _textSize.Y * _ay), Scale, SpriteEffects.None, LayerDepth);
        base.Draw();
    }
}