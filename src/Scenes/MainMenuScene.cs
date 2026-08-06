
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Components.UI.Models;
using project_republics.Components.UI.Sections;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Components.TransitionScreens;

namespace project_republics.Scenes;


public class MainMenuScene : IScene
{
    private EaseInOutAnimation _showUIAnimation;
    private TextGroup _gameInfoTextGroup;
    private MainMenuSide _leftSide;
    private PlayGameSide _playGameSide;
    private int _currentView;
    private bool _worldLoading;
    public MainMenuScene()
    {
        _worldLoading = false;
        _leftSide = new([
            () => ChangeCurrentView(1),
            () => {},
            () => {},
            MainGame.Shutdown
        ]);
        _playGameSide = new(() => ChangeCurrentView(0), OnPlayGame);
        ChangeCurrentView(0);  

        _gameInfoTextGroup = new([
            new ShadowedText(Utils.Input.Fonts.BASE, "Copyright Kkamildev", MainGame.Resolution - new Vector2(2, 0), 1f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black},
            new ShadowedText(Utils.Input.Fonts.BASE, "In development", MainGame.Resolution - new Vector2(2, 40), 1f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black},
            new ShadowedText(Utils.Input.Fonts.BASE, "Project Republics", MainGame.Resolution - new Vector2(2, 80), 1f, 1f, 0f, new Vector2(2)){Color = Color.GhostWhite, ShadowColor = Color.Black}
        ]);
        MainGame.Storage.Account.ConnectToAccount();

    }

    private void ChangeCurrentView(int newValue)
    {
        _currentView = newValue;
        switch(_currentView)
        {
            case 0:
                _leftSide.ButtonGroup.Active = true;
                _showUIAnimation = new(1f, () => {}, _showUIAnimation?.Progress ?? 1f, 0f);
            break;
            case 1:
                _leftSide.ButtonGroup.Active = false;
                _playGameSide.Active = true;
                _showUIAnimation = new(1f, () => {}, _showUIAnimation?.Progress ?? 0f, 1f);
            break; 
        }
    }

    private void OnPlayGame(WorldModel.WorldData worldData)
    {
        MainGame.TransitionScreen = new WorldLoadingTransitionScreen(new LinearAnimation(0.5f, () => {}, 0f, 1f),
         new LinearAnimation(0.5f, () => {}, 1f, 0f),
         () => LoadGameScene(worldData));
    }

    private async Task LoadGameScene(WorldModel.WorldData worldData)
    {
        MainGame.ChangeScene(new GameScene());
    }


    public void Draw()
    {
        MainGame.Batch.Begin(samplerState:SamplerState.PointClamp, blendState:BlendState.NonPremultiplied);
        // main menu draw
        _leftSide.Draw();
        _gameInfoTextGroup.Draw();

        // play menu
        _playGameSide.Draw();

        MainGame.Batch.End();
    }

    public void Update()
    {
        if(!_worldLoading)
        {
            _leftSide.Update();
            _playGameSide.Update();

            if(_showUIAnimation != null)
            {
                if(_showUIAnimation.BaseProgress < 1f)
                {
                    _leftSide.MainPosition = new Vector2(-800 * _showUIAnimation.Progress, 0);
                    _gameInfoTextGroup.MainPosition = new Vector2(300 * _showUIAnimation.Progress, 0);
                }
                _showUIAnimation.Update();
            }
        }
    }
    
    public void Dispose()
    {
        _gameInfoTextGroup.Dispose();
        _leftSide.Dispose();
        _playGameSide.Dispose();
    }
}