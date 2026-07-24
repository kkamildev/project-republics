


namespace project_republics.Utils.Animations;

public class FrameAnimation : Animation
{
    private readonly int _frames;
    private int _framesCount;

    public FrameAnimation(float secondsPerFrame, int frames) : base(secondsPerFrame, null)
    {
        _frames = frames;
        _framesCount = 0;
        _action = AddFrame;
    }

    public void AddFrame()
    {
        _framesCount++;
        if(_framesCount > _frames)
        {
            _framesCount = 0;
        }
    }

    public int Frames
    {
        get
        {
            return _frames;
        }
    }

    public override float Progress
    {
        get
        {
            return _framesCount;
        }
    }
}