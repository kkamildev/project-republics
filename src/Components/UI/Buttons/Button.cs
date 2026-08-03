

using System;
using Microsoft.Xna.Framework;
using project_republics.Utils.Components.Texts;

namespace project_republics.Components.UI.Buttons;

public class Button : BaseButton
{
    protected Text _text;
    private Color _mainTextColor;
    public Color ChangeColor{get;set;}

    public Button(Action onClick, Text text) : base(onClick)
    {
        _text = text;
        _mainTextColor = _text.Color;
        ChangeColor = Color.Yellow;
    }

    public override void Draw()
    {
        _text.Draw();
    }


    public override void Dispose()
    {
        _text.Dispose();
    }

    public override Vector2 MainPosition
    {
        get
        {
            return _text.Position;
        }
        set
        {
            _text.Position = value;
        }
    }

    public override bool Active
    {
        get
        {
            return _active;
        }
        set
        {
            _active = value;
            if(_active)
            {
                _text.Color = ChangeColor;
            } else
            {
                _text.Color = _mainTextColor;
            }
        }
    }
}