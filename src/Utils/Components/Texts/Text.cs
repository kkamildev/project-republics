
using Microsoft.Xna.Framework;
using project_republics.Utils.Input;
using Microsoft.Xna.Framework.Graphics;

namespace project_republics.Utils.Components.Texts;

public class Text
{
    protected Fonts _font;
    protected string _content;
    protected Vector2 _position;
    public Color Color{get;set;}
    public float LayerDepth{get;set;}
    public float Scale{get;set;}

    public Text(Fonts font, string content, Vector2 position)
    {
        _font = font;
        Content = content;
        Position = position;
        Color = Color.White;
        LayerDepth = 1f;
        Scale = 1f;
    }

    public virtual void Draw()
    {
        MainGame.Batch.DrawString(MainGame.CL.Fonts[_font], Content, Position, Color, 0f, Vector2.Zero, Scale, SpriteEffects.None, LayerDepth);
    }

    public virtual string Content
    {
        get
        {
            return _content;
        }
        set
        {
            _content = value;
        }
    }

    public Vector2 Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
        }
    }
}