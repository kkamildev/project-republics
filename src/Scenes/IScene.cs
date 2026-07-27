
using System;

namespace project_republics.Scenes;

public interface IScene : IDisposable
{
    public void Draw();

    public void Update();
}