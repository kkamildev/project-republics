
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Components.UI.Buttons;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;

namespace project_republics.Scenes;


public class MainMenuScene : IScene
{
    private EaseInOutAnimation _showingScreenAnimation;
    private Sprite _blackBackground, _mainMenuSide;
    private AlignedSprite _gameLogo;
    private TextGroup _gameInfoTextGroup;
    private ButtonGroup _mainButtonGroup;
    public MainMenuScene()
    {
        _gameLogo = new(Utils.Input.Textures.GAME_LOGO, new Vector2(300, 30), 0.5f, 0f){Scale=3f};
        _blackBackground = new(Utils.Input.Textures.BACKGROUND, Vector2.Zero){Color = Color.Black, Scale = 100f};
        _mainMenuSide = new(Utils.Input.Textures.MAIN_MENU_SIDE, Vector2.Zero){Color = new Color(Color.Black, 0.7f), Scale = 100f};
        _gameInfoTextGroup = new([
            new ShadowedText(Utils.Input.Fonts.BASE, "Copyright Kkamildev", MainGame.Resolution, 1f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black},
            new ShadowedText(Utils.Input.Fonts.BASE, "Project Republics", MainGame.Resolution - new Vector2(0, 80), 1f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black},
            new ShadowedText(Utils.Input.Fonts.BASE, "In development", MainGame.Resolution - new Vector2(0, 40), 1f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black}
        ]);

        _showingScreenAnimation = new(1f, () => {}, 1f, 0f);
        _mainButtonGroup = new([
            new SpriteButton(new AlignedText(Utils.Input.Fonts.LARGE, "PLAY_BUTTON", new Vector2(300, 400), 0.5f, 0.5f){Color = Color.DimGray},
             new AlignedSprite(Utils.Input.Textures.BUTTON1, new Vector2(300, 400), 0.5f, 0.5f){Scale = 3f},
              () => {}){ChangeColor = Color.White},
            new SpriteButton(new AlignedText(Utils.Input.Fonts.LARGE, "SETTINGS_BUTTON", new Vector2(300, 525), 0.5f, 0.5f){Color = Color.DimGray},
             new AlignedSprite(Utils.Input.Textures.BUTTON1, new Vector2(300, 525), 0.5f, 0.5f){Scale = 3f},
              () => {}){ChangeColor = Color.White},
            new SpriteButton(new AlignedText(Utils.Input.Fonts.LARGE, "CREDITS_BUTTON", new Vector2(300, 650), 0.5f, 0.5f){Color = Color.DimGray},
             new AlignedSprite(Utils.Input.Textures.BUTTON1, new Vector2(300, 650), 0.5f, 0.5f){Scale = 3f},
              () => {}){ChangeColor = Color.White},
            new SpriteButton(new AlignedText(Utils.Input.Fonts.LARGE, "EXIT_BUTTON", new Vector2(300, 775), 0.5f, 0.5f){Color = Color.DimGray},
             new AlignedSprite(Utils.Input.Textures.BUTTON1, new Vector2(300, 775), 0.5f, 0.5f){Scale = 3f},
              MainGame.Shutdown){ChangeColor = Color.White},
        ]){Active = true};
    }


    public void Draw()
    {
        MainGame.Batch.Begin(samplerState:SamplerState.PointClamp, blendState:BlendState.NonPremultiplied);
        _mainMenuSide.Draw();
        _gameLogo.Draw();
        _gameInfoTextGroup.Draw();
        _mainButtonGroup.Draw();
        _blackBackground.Draw();
        MainGame.Batch.End();
    }

    public void Update()
    {
        _showingScreenAnimation.Update();
        _blackBackground.Color = new Color(_blackBackground.Color, _showingScreenAnimation.Progress);
        _mainButtonGroup.Update();
    }
    
    public void Dispose()
    {
        _gameInfoTextGroup.Dispose();
        _mainButtonGroup.Dispose();
    }
}