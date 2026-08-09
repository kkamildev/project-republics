
using System;
using Microsoft.Xna.Framework;

namespace project_republics.Utils.Components.UI;

public abstract class UIBase
{
    protected Color _mainColor;
    protected Vector2 _mainPosition;
    public UIBase()
    {
        _mainColor = Color.White;
        _mainPosition = Vector2.Zero;
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