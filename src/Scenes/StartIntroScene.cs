
using project_republics.Utils.Animations;
using project_republics.Components.UI.TransitionScreens;
using System.Threading.Tasks;

namespace project_republics.Scenes;

public class StartIntroScene : IScene
{
    private bool _animationAccelerated;
    private bool _introFinished;
    private IntroTransitionScreen _introTransition;
    public StartIntroScene()
    {
        _introFinished = false;
        _introTransition = new IntroTransitionScreen(new LinearAnimation(0.1f, () => {}, 0f, 1f),
        new EaseInOutAnimation(1f, () => {}, 1f, 0f), IntroTask);
        MainGame.TransitionScreen = _introTransition;
        _animationAccelerated = false;
        InitAnimation();

        // inserting controls
        MainGame.Input.SubcribeAnyKeyPressedAction(AccelerateAnimation);
        
    }

    private void InitAnimation ()
    {
        _introTransition.IntroAnimation = new(2f, () =>
            _introTransition.IntroAnimation = new(2f, () => {
                _introTransition.IntroStatus = 1;
                _introTransition.IntroAnimation = new(2f, () =>
                    _introTransition.IntroAnimation = new(2f, () => {MainGame.ChangeScene(new MainMenuScene()); _introFinished = true;}
                    , 1f, 0f)
                , 0f, 1f);
        }, 1f, 0f)
        , 0f, 1f);
    }

    private async Task IntroTask()
    {
        while(!_introFinished)
        {
            await Task.Delay(1);
        }
    }

    private void AccelerateAnimation()
    {
        _animationAccelerated = true;
    }

    public void Dispose()
    {
        MainGame.Input.UnsubcribeAnyKeyPressedAction(AccelerateAnimation);
    }

    public void Draw()
    {
        
    }

    public void Update()
    {
        _introTransition.IntroAnimation?.Update();
        if(_animationAccelerated)
        {
            for(int i = 0;i<4;i++)
            {
                _introTransition.IntroAnimation?.Update();
            }
        }
        
    }
}