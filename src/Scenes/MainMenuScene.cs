
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Components.UI.Buttons;
using project_republics.Components.UI.Sections;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;

namespace project_republics.Scenes;


public class MainMenuScene : IScene
{
    private EaseInOutAnimation _showingScreenAnimation;
    private Sprite _blackBackground;
    private TextGroup _gameInfoTextGroup;
    private MainMenuSide _leftSide;
    public MainMenuScene()
    {
        _leftSide = new([
            () => {},
            () => {},
            () => {},
            MainGame.Shutdown
        ]);
        _showingScreenAnimation = new(1f, () => {}, 1f, 0f);
        _blackBackground = new(Utils.Input.Textures.BACKGROUND, Vector2.Zero){Color = Color.Black, Scale = 100f};

        _gameInfoTextGroup = new([
            new ShadowedText(Utils.Input.Fonts.BASE, "Copyright Kkamildev", MainGame.Resolution, 1f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black},
            new ShadowedText(Utils.Input.Fonts.BASE, "Project Republics", MainGame.Resolution - new Vector2(0, 80), 1f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black},
            new ShadowedText(Utils.Input.Fonts.BASE, "In development", MainGame.Resolution - new Vector2(0, 40), 1f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black}
        ]);

    }


    public void Draw()
    {
        MainGame.Batch.Begin(samplerState:SamplerState.PointClamp, blendState:BlendState.NonPremultiplied);
        _leftSide.Draw();
        _gameInfoTextGroup.Draw();
        _blackBackground.Draw();
        MainGame.Batch.End();
    }

    public void Update()
    {
        _leftSide.Update();
        _showingScreenAnimation.Update();
        _blackBackground.Color = new Color(_blackBackground.Color, _showingScreenAnimation.Progress);
    }
    
    public void Dispose()
    {
        _gameInfoTextGroup.Dispose();
        _leftSide.Dispose();
    }
}