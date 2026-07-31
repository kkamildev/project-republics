
using System;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.UI;
using Microsoft.Xna.Framework;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Texts;
using project_republics.Components.UI.Buttons;
using System.Linq;

namespace project_republics.Components.UI.Sections;

public class PlayGameSide : UIBase, IDisposable
{
    private bool _active;
    private EaseInOutAnimation _showUIAnimation;
    private Action<bool> _backAction;
    private Sprite _background;
    private AlignedText _titleText;
    private ButtonGroup _mainButtonGroup;

    public PlayGameSide(Action backAction)
    {
        _active = false;
        _titleText = new(Utils.Input.Fonts.LARGER, "SELECT_WORLD_TITLE", new Vector2(MainGame.Resolution.X / 2, 50), 0.5f, 0);
        _background = new RectSprite(Utils.Input.Textures.BACKGROUND, new Rectangle((int)MainGame.Resolution.X / 2, (int)MainGame.Resolution.Y / 2, 1600 / 5 * 4, 900 / 5 * 4), 0.5f, 0.5f, 0){Color = new Color(Color.Black, 0.7f)};
        string[] texts = ["Select world", "Create new"];
        _mainButtonGroup = new([
            ..texts.Select((text, index) => new SpriteButton(new AlignedText(Utils.Input.Fonts.BASE, text, new Vector2(300, 200 + 100 * index), 0.5f, 0.5f){Color = Color.DimGray},
             new AlignedSprite(Utils.Input.Textures.BUTTON2, new Vector2(300, 200 + 100 * index), 0.5f, 0.5f){Scale = 3f},
              () => {}){ChangeColor = Color.White})
        ]);
        _backAction = (hold) =>
        {
            backAction.Invoke();
            Active = false;
        };
        MainPosition = new Vector2(0, -900);
    }

    public override void Draw()
    {
        _background.Draw();
        _titleText.Draw();
        _mainButtonGroup.Draw();
    }
    public override void Update()
    {
        _mainButtonGroup.Update();
        if(_showUIAnimation != null)
        {
            _showUIAnimation.Update();
            MainPosition = new Vector2(0, -900 * _showUIAnimation.Progress);
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
                _mainButtonGroup.Active = true;
                _showUIAnimation = new(1f, () => {}, _showUIAnimation?.Progress ?? 1f, 0f);
                MainGame.Input.SubscribeAction(Utils.Input.Controls.EXIT, _backAction);
            } else
            {
                _mainButtonGroup.Active = false;
                _showUIAnimation = new(1f, () => {}, _showUIAnimation?.Progress ?? 0, 1f);
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, _backAction);
            }
        }
    }

    public override Vector2 MainPosition {
        get => base.MainPosition;
        set
        {
            foreach (Button button in  _mainButtonGroup.Buttons)
            {
                button.Position-= base.MainPosition;
            }
            base.MainPosition = value;
            _background.Position = MainGame.Resolution / 2 + base.MainPosition;
            _titleText.Position = new Vector2(MainGame.Resolution.X / 2, 50) + base.MainPosition;
            foreach (Button button in _mainButtonGroup.Buttons)
            {
                button.Position+= base.MainPosition;
            }

        }
    }

    public void Dispose()
    {
        MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, _backAction);
        _titleText.Dispose();
    }
}