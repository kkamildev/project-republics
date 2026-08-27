

using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Components.UI.Models;
using project_republics.Components.UI.Sections;
using project_republics.Components.World;
using project_republics.Utils.Storage;

namespace project_republics.Scenes;

public class GameScene : IScene
{
    private readonly WorldModel.WorldData _worldData;
    private readonly WorldStorage _storage;
    private readonly WorldContainer _world;
    private Player _player;
    private DebugMenu _debugMenu;

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
        _debugMenu = new(_player);
    }

    public void Draw()
    {
        MainGame.Batch.Begin(samplerState:SamplerState.PointClamp, blendState:BlendState.NonPremultiplied);
        _debugMenu.Draw();
        MainGame.Batch.End();
    }

    public void Update()
    {
        _debugMenu.Update();
    }
    public void Dispose()
    {
        _debugMenu.Dispose();
    }
}