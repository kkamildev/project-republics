

using project_republics.Utils.Components.Texts;
using Microsoft.Xna.Framework;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Animations;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace project_republics.Scenes;

public class StartIntroScene : IScene
{
    private readonly Sprite _background;
    private readonly TextGroup _studioLogoText, _authorText;
    private readonly AlignedSprite _studioLogo;
    private EaseInOutAnimation _animation;
    private int _introStatus;
    private bool _animationAccelerated;
    public StartIntroScene()
    {
        _animationAccelerated = false;
        _introStatus = 0;
        _animation = new(2f, () =>
            _animation = new(2f, () => {
                _introStatus = 1;
                _animation = new(2f, () =>
                    _animation = new(2f, () => MainGame.ChangeScene(new MainMenuScene())
                    , 1f, 0f)
                , 0f, 1f);
        }, 1f, 0f)
        , 0f, 1f);
        _background = new(Utils.Input.Textures.BACKGROUND, Vector2.Zero){Color = Color.Black, Scale = 100f};
        _studioLogo = new(Utils.Input.Textures.AUTHOR_LOGO, MainGame.Resolution / 2 + new Vector2(-210, 0), 0.5f, 0.5f){Scale = 0.15f};
        _studioLogoText = new([
            new AlignedText(Utils.Input.Fonts.LARGER, "Pixlesofte", new Vector2(60, -30), 0.5f, 1f){Color = Color.White},
            new AlignedText(Utils.Input.Fonts.BASE, "The Software Studio", new Vector2(-60, 30), 0f, 1f){Color = Color.DimGray}
        ]){
            MainPosition = MainGame.Resolution / 2
        };
        _authorText = new([
            new AlignedText(Utils.Input.Fonts.LARGE, "Created by", new Vector2(0, -100), 0.5f, 0.5f){Color = Color.DimGray},
            new AlignedText(Utils.Input.Fonts.LARGER, "Kkamildev", new Vector2(0, 0), 0.5f, 0.5f){Color = Color.DarkRed},
            new AlignedText(Utils.Input.Fonts.LARGE, "With passion", new Vector2(0, 100), 0.5f, 0.5f){Color = Color.DimGray},
        ]){
            MainPosition = MainGame.Resolution / 2
        };
        // inserting controls
        MainGame.Input.InsertAnyKeyPressedAction(() => _animationAccelerated = true);
        
    }

    public void Dispose()
    {
        _studioLogoText.Dispose();
        _authorText.Dispose();
        MainGame.Input.RemoveAnyKeyPressedAction();
    }

    public void Draw()
    {
        MainGame.Batch.Begin(samplerState:SamplerState.PointClamp, blendState:BlendState.NonPremultiplied);
        _background.Draw();
        switch(_introStatus)
        {
            case 0:
            _studioLogo.Draw();
            _studioLogoText.Draw();
            break;
            case 1:
            _authorText.Draw();
            break;
        }
        MainGame.Batch.End();
    }

    public void Update()
    {
        _animation.Update();
        if(_animationAccelerated)
        {
            for(int i = 0;i<4;i++)
            {
                _animation.Update();
            }
        }
        
        switch(_introStatus)
        {
            case 0:
                _studioLogo.Color = new Color(_studioLogo.Color, _animation.Progress);
                foreach (Text text in _studioLogoText.Texts)
                {
                    text.Color = new Color(text.Color, _animation.Progress);
                }
            break;
            case 1:
            foreach (Text text in _authorText.Texts)
            {
                text.Color = new Color(text.Color, _animation.Progress);
            }
            break;
        }
    }
}