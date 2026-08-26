
using System;
using System.Threading.Tasks;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Sprites;
using Microsoft.Xna.Framework;

namespace project_republics.Utils.Components.TransitionScreens;

public class WorldLoadingTransitionScreen : TransitionScreen
{
    private RectSprite _background;
    public WorldLoadingTransitionScreen(Animation inAnimation, Animation outAnimation, Func<Task> taskFactory) : base(inAnimation, outAnimation, taskFactory)
    {
        _background = new(Input.Textures.BACKGROUND, new Rectangle(0, 0, (int)MainGame.Resolution.X, (int)MainGame.Resolution.Y), 0, 0, 0){Color = Color.Black};
    }

    public override void Draw()
    {
        _background.Draw();
    }
    public override void Update()
    {
        if(_currentTask == null)
        {
            _background.Color = new Color(_background.Color, _inAnimation.Progress);
        } else
        {
            _background.Color = new Color(_background.Color, _outAnimation.Progress);
        }
        base.Update();
    }
}