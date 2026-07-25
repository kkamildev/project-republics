
using System;
using Microsoft.Xna.Framework;

namespace project_republics.Utils.Components.UI;

public abstract class UIBase
{
    private Color _mainColor;
    private Vector2 _mainPosition;
    protected Action _onClose;

    public UIBase()
    {
        MainColor = Color.White;
        MainPosition = Vector2.Zero;
    }

    public UIBase(Action action) : this()
    {
        
    }

    public virtual void Draw()
    {
        
    }
    public virtual void Update()
    {
        
    }

    public virtual Vector2 MainPosition
    {
        get
        {
            return _mainPosition;
        }
        set
        {
            _mainPosition = value;
        }
    }

    public virtual Color MainColor
    {
        get
        {
            return _mainColor;
        }
        set
        {
            _mainColor = value;
        }
    }
}