
using System;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.UI;
using Microsoft.Xna.Framework;
using project_republics.Utils.Animations;

namespace project_republics.Components.UI.Sections;

public class PlayGameSide : UIBase, IDisposable
{
    private EaseInOutAnimation _showUIAnimation;
    private Action<bool> _backAction;
    private Sprite _background;
    private bool _active;

    public PlayGameSide(Action backAction)
    {
        _active = false;
        _background = new RectSprite(Utils.Input.Textures.BACKGROUND, new Rectangle((int)MainGame.Resolution.X / 2, (int)MainGame.Resolution.Y / 2 - 900, 1600 / 5 * 4, 900 / 5 * 4), 0.5f, 0.5f, 0){Color = new Color(Color.Black, 0.7f)};
        _backAction = (hold) =>
        {
            backAction.Invoke();
            Active = false;
        };
    }

    public override void Draw()
    {
        _background.Draw();
    }
    public override void Update()
    {
        if(_showUIAnimation != null)
        {
            _showUIAnimation.Update();
            _background.Position = MainGame.Resolution / 2 + new Vector2(0, -900 * _showUIAnimation.Progress);
        }
    }

    public bool Active
    {
        get
        {
            return _active;
        }
        set
        {
            _active = value;
            if(_active)
            {
                _showUIAnimation = new(1f, () => {}, _showUIAnimation?.Progress ?? 1f, 0f);
                MainGame.Input.SubscribeAction(Utils.Input.Controls.EXIT, _backAction);
            } else
            {
                _showUIAnimation = new(1f, () => {}, _showUIAnimation?.Progress ?? 0, 1f);
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, _backAction);
            }
        }
    }

    public void Dispose()
    {
        MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, _backAction);
    }
}