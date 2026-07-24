
using System;

namespace project_republics.Utils.Animations;

public class Animation
{
    public bool Loop{get;set;}
    protected readonly float _seconds;
    protected float _elapsedSeconds;
    protected Action _action;

    public Animation(float seconds, Action onFinish)
    {
        Loop = false;
        _seconds = seconds;
        _action = onFinish;
    }

    public void Update()
    {
        if(_elapsedSeconds < _seconds)
        {
            _elapsedSeconds += MainGame.DeltaTime;
            if(_seconds < _elapsedSeconds)
            {
                _action?.Invoke();
            }
        } else if(Loop)
        {
            _elapsedSeconds = 0f;
        }
    }
    

    public void Reset()
    {
        _elapsedSeconds = 0f;
    }

    public virtual float Progress
    {
        get
        {
            return _elapsedSeconds / _seconds;
        }
    }

    public float BaseProgress
    {
        get
        {
            return _elapsedSeconds / _seconds;
        }
    }
}