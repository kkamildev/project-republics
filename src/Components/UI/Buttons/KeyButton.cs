
using System;
using Microsoft.Xna.Framework;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Input;

namespace project_republics.Components.UI.Buttons;

public sealed class KeyButton : SpriteButton
{
    public KeyButton(string text, Vector2 position, Action onclick) : base(
        new AlignedText(Fonts.BASE, "{0}", position, 0.5f, 0.5f){StringParams = [text], Color = Color.DimGray},
        new AlignedSprite(Textures.BUTTON3, position, 0.5f, 0.5f){Scale = 3f},
        onclick
    )
    {
        
    }
    public KeyButton(string text, Textures customTexture, Vector2 position, Action onclick) : base(
        new AlignedText(Fonts.BASE, "{0}", position, 0.5f, 0.5f){StringParams = [text], Color = Color.DimGray},
        new AlignedSprite(customTexture, position, 0.5f, 0.5f){Scale = 3f},
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