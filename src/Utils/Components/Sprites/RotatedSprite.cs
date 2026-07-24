
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Input;

namespace project_republics.Utils.Components.Sprites;

public class RotatedSprite : AlignedSprite
{
    protected float _rotation;
    public RotatedSprite(Textures textures, Vector2 position, float ax, float ay, float startingRotation) : base(textures, position, ax, ay)
    {
        _rotation = startingRotation;
    }

    public override void Draw()
    {
        MainGame.Batch.Draw(MainGame.CL.Textures[_texture], _position, null, Color, _rotation, new Vector2(MainGame.CL.Textures[_texture].Width * _ax, MainGame.CL.Textures[_texture].Height * _ay), Scale, SpriteEffects.None, LayerDepth);
    }

    public float RadRotation
    {
        get
        {
            return _rotation;
        }
        set
        {
            _rotation = value;
        }
    }

    public float DegRotation
    {
        get
        {
            return MathHelper.ToDegrees(_rotation);
        }
        set
        {
            _rotation = MathHelper.ToRadians(value);
        }
    }
}