

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Animations;
using project_republics.Utils.Input;

namespace project_republics.Utils.Components.Sprites;

public class FramedSprite : RotatedSprite
{
    private readonly int _maxFrames;
    public int CurrentFrame{get;set;}
    public FramedSprite(Textures textures, Vector2 position, float ax, float ay, float startingRotation, int maxFrames) : base(textures, position, ax, ay, startingRotation)
    {
        _maxFrames = maxFrames;
        CurrentFrame = 0;
    }

    public override void Draw()
    {
        MainGame.Batch.Draw(MainGame.CL.Textures[_texture], _position, new Rectangle(MainGame.CL.Textures[_texture].Width / _maxFrames * CurrentFrame, 0, MainGame.CL.Textures[_texture].Width / _maxFrames, MainGame.CL.Textures[_texture].Height), Color, _rotation, new Vector2(MainGame.CL.Textures[_texture].Width * _ax, MainGame.CL.Textures[_texture].Height * _ay), Scale, SpriteEffects.None, LayerDepth);
    }
}