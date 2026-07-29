
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using project_republics.Components.UI.Buttons;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Components.UI;

namespace project_republics.Components.UI.Sections;

public class MainMenuSide : UIBase, IDisposable
{
    private Sprite _mainMenuSide;
    private RotatedText _menuTip;
    private SinAnimation _menuTipAnimation;
    private AlignedSprite _gameLogo;

    private ButtonGroup _mainButtonGroup;

    public MainMenuSide(Action[] actions)
    {
        _gameLogo = new(Utils.Input.Textures.GAME_LOGO, new Vector2(300, 30), 0.5f, 0f){Scale=3f};
        _menuTip = new(Utils.Input.Fonts.BASE, "In development", new Vector2(550, 200), 0.5f, 0.5f, 0){Color = Color.Goldenrod, DegRotation = -10};
        _menuTipAnimation = new(1.3f, 0.3f, () => {}){Loop = true};
        _mainMenuSide = new(Utils.Input.Textures.MAIN_MENU_SIDE, Vector2.Zero){Color = new Color(Color.Black, 0.7f), Scale = 100f};

        string[] texts = ["PLAY_BUTTON", "SETTINGS_BUTTON", "CREDITS_BUTTON", "EXIT_BUTTON"];
        _mainButtonGroup = new([
            ..texts.Select((text, index) => new SpriteButton(new AlignedText(Utils.Input.Fonts.LARGE, text, new Vector2(300, 400 + 125 * index), 0.5f, 0.5f){Color = Color.DimGray},
             new AlignedSprite(Utils.Input.Textures.BUTTON1, new Vector2(300, 400 + 125 * index), 0.5f, 0.5f){Scale = 3f},
              actions[index]){ChangeColor = Color.White})
        ]){Active = true};
    }

    public override void Draw()
    {
        _mainMenuSide.Draw();
        _gameLogo.Draw();
        _menuTip.Draw();
        _mainButtonGroup.Draw();
    }

    public override void Update()
    {
        _menuTipAnimation.Update();
        _menuTip.Scale = _menuTipAnimation.Progress + 1.3f;
        _mainButtonGroup.Update();
    }

    public override Vector2 MainPosition {
        get => base.MainPosition;
        set
        {
            base.MainPosition = value;
            _gameLogo.Position = new Vector2(300, 30) + _mainPosition;
            _menuTip.Position = new Vector2(550, 200) + _mainPosition;
            _mainMenuSide.Position = _mainPosition;
        }
    }

    public void Dispose()
    {
        _menuTip.Dispose();
        _mainButtonGroup.Dispose();
    }
}