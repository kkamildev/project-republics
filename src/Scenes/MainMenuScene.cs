
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Sprites;

namespace project_republics.Scenes;


public class MainMenuScene : IScene
{
    private EaseInOutAnimation _showingScreenAnimation;
    private Sprite _blackBackground;
    public MainMenuScene()
    {
        _blackBackground = new(Utils.Input.Textures.BACKGROUND, Vector2.Zero){Color = Color.Black, Scale = 100f};
        _showingScreenAnimation = new(1f, () => {}, 1f, 0f);
    }


    public void Draw()
    {
        MainGame.Batch.Begin(samplerState:SamplerState.PointClamp, blendState:BlendState.NonPremultiplied);
        _blackBackground.Draw();
        MainGame.Batch.End();
    }

    public void Update()
    {
        _showingScreenAnimation.Update();
        _blackBackground.Color = new Color(_blackBackground.Color, _showingScreenAnimation.Progress);
    }
    
    public void Dispose()
    {
        
    }
}