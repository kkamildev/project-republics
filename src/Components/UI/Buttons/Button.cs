

using System;
using Microsoft.Xna.Framework;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Input;

namespace project_republics.Components.UI.Buttons;

public class Button : IDisposable
{
    protected Text _text;
    protected Action _onclick;
    protected Rectangle _bounds;
    protected bool _active;
    protected Color _mainTextColor;

    public Color ChangeColor{get;set;}
    

    public Button(Text text, Action onclick)
    {
        _onclick = onclick;
        _active = false;
        _text = text;
        _mainTextColor = _text.Color;
        ChangeColor = Color.Yellow;
    }

    public virtual void Draw()
    {
        _text.Draw();
    }

    public void Dispose()
    {
        _text.Dispose();
    }

    public bool Active
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
                _text.Scale = 1.2f;
            } else
            {
                _text.Color = _mainTextColor;
                _text.Scale = 1f;
            }
        }
    }
    public Action OnClick
    {
        get
        {
            return _onclick;
        }
    }


}