

using System.Threading.Tasks;
using project_republics.Components.UI.Models;
using project_republics.Components.World;
using project_republics.Utils.Storage;

namespace project_republics.Scenes;

public class GameScene : IScene
{
    private readonly WorldModel.WorldData _worldData;
    private readonly WorldStorage _storage;
    private readonly WorldContainer _world;
    private Player _player;

    public GameScene(WorldModel.WorldData data)
    {
        _worldData = data;
        _storage = new(_worldData);
        _world = new(_storage);
    }

    public async Task PrepareGame()
    {
        // preparing Logic
        _player = await _storage.LoadPlayer(MainGame.Storage.Account);
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