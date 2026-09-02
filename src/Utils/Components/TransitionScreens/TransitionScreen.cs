
using System;
using System.Threading.Tasks;
using project_republics.Utils.Animations;

namespace project_republics.Utils.Components.TransitionScreens;

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
        if(_inAnimation.BaseProgress >= 1)
        {
            _currentTask ??= Task.Run(() => _taskFactory());
            if(_currentTask.IsCompleted)
            {
                if (_currentTask.IsFaulted)
                {
                    throw _currentTask.Exception;
                }
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
        _currentTask.Dispose();
    }
}