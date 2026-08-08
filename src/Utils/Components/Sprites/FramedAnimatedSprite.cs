

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Animations;
using project_republics.Utils.Input;

namespace project_republics.Utils.Components.Sprites;

public class FramedAnimatedSprite : RotatedSprite
{
    private readonly FrameAnimation _animation;
    public FramedAnimatedSprite(Textures textures, Vector2 position, float ax, float ay, float startingRotation, FrameAnimation animation) : base(textures, position, ax, ay, startingRotation)
    {
        _animation = animation;
    }

    public override void Draw()
    {
        MainGame.Batch.Draw(MainGame.CL.Textures[_texture], _position, new Rectangle(MainGame.CL.Textures[_texture].Width / _animation.Frames * (int)_animation.Progress, 0, MainGame.CL.Textures[_texture].Width / _animation.Frames, MainGame.CL.Textures[_texture].Height), Color, _rotation, new Vector2(MainGame.CL.Textures[_texture].Width * _ax, MainGame.CL.Textures[_texture].Height * _ay), Scale, SpriteEffects.None, LayerDepth);
    }

    public void Update()
    {
        _animation.Update();
    }
    
}