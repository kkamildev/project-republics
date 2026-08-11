

using System.Threading.Tasks;
using project_republics.Components.UI.Models;

namespace project_republics.Scenes;

public class GameScene : IScene
{
    private readonly WorldModel.WorldData _worldData;

    public GameScene(WorldModel.WorldData data)
    {
        _worldData = data;
    }

    public async Task PrepareGame()
    {
        // preparing Logic
        await Task.Delay(2000);
    }

    public void Draw()
    {
        
    }

    public void Update()
    {
        
    }
    public void Dispose()
    {
        
    }
}