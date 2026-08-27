
namespace project_republics.Utils.Diagnostics;

public sealed class FpsCounter
{
    private int _frameCount = 0;
    private double _elapsedTime = 0;
    public int Fps { get; private set; }

    public void Update()
    {
        _elapsedTime += MainGame.DeltaTime;
        _frameCount++;

        if (_elapsedTime >= 1.0)
        {
            Fps = _frameCount;
            _frameCount = 0;
            _elapsedTime = 0;
        }
    }
}