
using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;

namespace project_republics.Utils.Components.TransitionScreens;

public class IntroTransitionScreen : TransitionScreen
{
    private RectSprite _background;
    private readonly TextGroup _studioLogoText, _authorText;
    private readonly AlignedSprite _studioLogo;
    private EaseInOutAnimation _introAnimation;
    private int _introStatus;
    public IntroTransitionScreen(Animation inAnimation, Animation outAnimation, Func<Task> taskFactory) : base(inAnimation, outAnimation, taskFactory)
    {
        _introStatus = 0;
        _background = new(Input.Textures.BACKGROUND, new Rectangle(0, 0, (int)MainGame.Resolution.X, (int)MainGame.Resolution.Y), 0, 0, 0){Color = Color.Black};

        _studioLogo = new(Input.Textures.AUTHOR_LOGO, MainGame.Resolution / 2 + new Vector2(-210, 0), 0.5f, 0.5f){Scale = 0.15f};
        _studioLogoText = new([
            new AlignedText(Input.Fonts.LARGER, "Pixlesofte", new Vector2(60, -30), 0.5f, 1f){Color = Color.White},
            new AlignedText(Input.Fonts.BASE, "The Software Studio", new Vector2(-60, 30), 0f, 1f){Color = Color.DimGray}
        ]){
            MainPosition = MainGame.Resolution / 2
        };
        _authorText = new([
            new AlignedText(Input.Fonts.LARGE, "Created by", new Vector2(0, -100), 0.5f, 0.5f){Color = Color.DimGray},
            new AlignedText(Input.Fonts.LARGER, "Kkamildev", new Vector2(0, 0), 0.5f, 0.5f){Color = Color.DarkRed},
            new AlignedText(Input.Fonts.LARGE, "With passion", new Vector2(0, 100), 0.5f, 0.5f){Color = Color.DimGray},
        ]){
            MainPosition = MainGame.Resolution / 2
        };
    }
    public override void Draw()
    {
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
    }
    public override void Update()
    {
        if(_currentTask != null)
        {
            _background.Color = new Color(_background.Color, _outAnimation.Progress);
        }
        switch(_introStatus)
        {
            case 0:
                _studioLogo.Color = new Color(_studioLogo.Color, _introAnimation.Progress);
                foreach (Text text in _studioLogoText.Texts)
                {
                    text.Color = new Color(text.Color, _introAnimation.Progress);
                }
            break;
            case 1:
            foreach (Text text in _authorText.Texts)
            {
                text.Color = new Color(text.Color, _introAnimation.Progress);
            }
            break;
        }
        base.Update();
    }

    public override void Dispose()
    {
        _studioLogoText.Dispose();
        _authorText.Dispose();
        base.Dispose();
    }

    public int IntroStatus
    {
        get
        {
            return _introStatus;
        }
        set
        {
            _introStatus = value;
        }
    }
    public EaseInOutAnimation IntroAnimation
    {
        get
        {
            return _introAnimation;
        }
        set
        {
            _introAnimation = value;
        }
    }
    
}