
using Microsoft.Xna.Framework;
using project_republics.Utils.Input;
using Microsoft.Xna.Framework.Graphics;

namespace project_republics.Utils.Components.Sprites;

public class Sprite
{
    public Color Color{get;set;}
    public float Scale{get;set;}
    public float LayerDepth{get;set;}
    protected Textures _texture;
    protected Vector2 _position;

    public Sprite(Textures textures, Vector2 position)
    {
        _texture = textures;
        _position = position;
        Color = Color.White;
        Scale = 1f;
    }

    public virtual void Draw()
    {
        MainGame.Batch.Draw(MainGame.CL.Textures[_texture], _position, null, Color, 0f, Vector2.Zero, Scale, SpriteEffects.None, LayerDepth);
    }

    public virtual Vector2 Position
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

    public Textures Texture
    {
        get
        {
            return _texture;
        }
        set
        {
            _texture = value;
        }
    }


}