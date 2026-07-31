

using System;
using Microsoft.Xna.Framework;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;

namespace project_republics.Components.UI.Buttons;

public class SpriteButton : Button
{
    protected AlignedSprite _sprite;
    public SpriteButton(Text text, AlignedSprite sprite, Action onclick) : base(text, onclick)
    {
        _sprite = sprite;
    }

    public override void Draw()
    {
        _sprite.Draw();
        base.Draw();
    }

    public override Vector2 Position {
        get => base.Position;
        set
        {
            base.Position = value;
            _sprite.Position = value;
        }
    }

    public float SpriteScale
    {
        get
        {
            return _sprite.Scale;
        }
        set
        {
            _sprite.Scale = value;
        }
    }
}