
using Microsoft.Xna.Framework;

namespace project_republics.Utils.Components.UI;

public abstract class UIBase
{
    public Color MainColor{get;set;}
    public Vector2 MainPosition{get;set;}

    public UIBase() : this(Color.White, Vector2.Zero)
    {
        
    }
    public UIBase(Color mainColor, Vector2 mainPosition)
    {
        MainColor = mainColor;
        MainPosition = mainPosition;
    }

    public virtual void Draw()
    {
        
    }
    public virtual void Update()
    {
        
    }
}