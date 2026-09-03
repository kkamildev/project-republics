
using System;
using System.Threading.Tasks;
using project_republics.Utils.Animations;
using project_republics.Utils.States;

namespace project_republics.Utils.Components.TransitionScreens;

public abstract class TransitionScreen : IDisposable
{
    protected Animation _inAnimation, _outAnimation;
    protected AsyncState _asyncState;
    public TransitionScreen(Animation inAnimation, Animation outAnimation, Func<Task> taskFactory)
    {
        _inAnimation = inAnimation;
        _outAnimation = outAnimation;
        _asyncState = new(taskFactory);
    }


    public virtual void Draw()
    {
        
    }

    public virtual void Update()
    {
        if(_inAnimation.BaseProgress >= 1)
        {
            if(_asyncState.Empty) _asyncState.Run();
            if(_asyncState.Completed)
            {
                _outAnimation.Update();
            }
        }
        _inAnimation.Update();
    }

    public bool Finished {
        get
        {
            return _outAnimation.BaseProgress >= 1;
        }
    }

    public virtual void Dispose()
    {
        _asyncState.Dispose();
    }
}