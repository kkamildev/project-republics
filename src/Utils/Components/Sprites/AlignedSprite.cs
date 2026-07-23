

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Input;

namespace project_republics.Utils.Components.Sprites;

public class AlignedSprite : Sprite
{
    protected float _ax, _ay;
    public AlignedSprite(Textures textures, Vector2 position, float ax, float ay) : base(textures, position)
    {
        _ax = ax;
        _ay = ay;
    }

    public override void Draw()
    {
        MainGame.Batch.Draw(MainGame.CL.Textures[_texture], _position, null, Color, 0f, new Vector2(MainGame.CL.Textures[_texture].Width * _ax, MainGame.CL.Textures[_texture].Height * _ay), Scale, SpriteEffects.None, LayerDepth);
    }


}