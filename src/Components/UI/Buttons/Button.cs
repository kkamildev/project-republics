

using System;
using Microsoft.Xna.Framework;
using project_republics.Utils.Components.Texts;

namespace project_republics.Components.UI.Buttons;

public class Button : IDisposable
{
    protected Text _text;
    private Action _onclick;
    private bool _active;
    private Color _mainTextColor;
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

    public virtual void Update()
    {
        
    }

    public void Dispose()
    {
        _text.Dispose();
    }

    public virtual Vector2 Position
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

    public virtual bool Active
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
    public Action OnClick
    {
        get
        {
            return _onclick;
        }
    }
}