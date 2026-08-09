
using System;
using Microsoft.Xna.Framework;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;

namespace project_republics.Components.UI.Buttons;

public sealed class KeyButton : SpriteButton
{
    public KeyButton(string text, Vector2 position, Action onclick) : base(
        new AlignedText(Utils.Input.Fonts.BASE, "{0}", position, 0.5f, 0.5f){StringParams = [text], Color = Color.DimGray},
        new AlignedSprite(Utils.Input.Textures.BUTTON3, position, 0.5f, 0.5f){Scale = 3f},
        onclick
    )
    {
        
    }

    public string Content
    {
        get
        {
            return (string)_text.StringParams[0];
        }
        set
        {
            _text.StringParams = [value];
        }
    }
}