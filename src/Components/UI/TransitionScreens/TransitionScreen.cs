
using System;
using System.Threading.Tasks;
using project_republics.Utils.Animations;

namespace project_republics.Components.UI.TransitionScreens;

public abstract class TransitionScreen : IDisposable
{
    protected Animation _inAnimation, _outAnimation;
    protected Func<Task> _taskFactory;
    protected Task _currentTask;
    public TransitionScreen(Animation inAnimation, Animation outAnimation, Func<Task> taskFactory)
    {
        _inAnimation = inAnimation;
        _outAnimation = outAnimation;
        _taskFactory = taskFactory;
    }


    public virtual void Draw()
    {
        
    }

    public virtual void Update()
    {
        _inAnimation.Update();
        if(_inAnimation.BaseProgress >= 1)
        {
            if(_currentTask == null)
            {
                _currentTask = _taskFactory();
            }
            if(_currentTask.IsCompleted)
            {
                _outAnimation.Update();
            }
        }
    }

    public bool Finished {
        get
        {
            return _outAnimation.BaseProgress >= 1;
        }
    }

    public void Dispose()
    {
        _currentTask.Dispose();
    }
}