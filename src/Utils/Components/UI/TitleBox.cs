
using System;
using Microsoft.Xna.Framework;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Input;

namespace project_republics.Utils.Components.UI;

public class TitleBox : UIBase, IDisposable
{
    private AlignedText _titleText;
    private Sprite _background;

    public TitleBox(string title, Textures texture, Rectangle rectangle)
    {
        _titleText = new(Fonts.LARGER, title, new Vector2(MainGame.Resolution.X / 2, 50), 0.5f, 0);
        _background = new RectSprite(texture, rectangle, 0.5f, 0.5f, 0){Color = new Color(Color.Black, 0.7f)};
    }

    public override void Draw()
    {
        _background.Draw();
        _titleText.Draw();
    }

    public override Vector2 MainPosition {
        get => base.MainPosition;
        set {
            _background.Position -= value;
            _titleText.Position -= base.MainPosition;
            base.MainPosition = value;
            _background.Position += value;
            _titleText.Position += base.MainPosition;
        }
    }

    public void Dispose()
    {
        _titleText.Dispose();
    }
}