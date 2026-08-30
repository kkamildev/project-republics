

using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Components.UI.Models;
using project_republics.Components.UI.Sections;
using project_republics.Components.World;
using project_republics.Utils.Storage;

namespace project_republics.Scenes;

public class GameScene : IScene
{
    private readonly WorldStorage _storage;
    private WorldContainer _world;
    private Player _player;
    private DebugMenu _debugMenu;

    public GameScene(WorldModel.WorldData data)
    {
        _storage = new(data);
    }

    public async Task PrepareGame()
    {
        // preparing Logic
        _player = await _storage.LoadPlayer(MainGame.Storage.Account);
        _world = new(_storage, _player);
        await _world.PrepareWorld();
        _debugMenu = new(_world);
    }

    public void Draw()
    {
        MainGame.Batch.Begin(samplerState:SamplerState.PointClamp, blendState:BlendState.NonPremultiplied);
        _world.Draw();
        _debugMenu.Draw();
        MainGame.Batch.End();
    }

    public void Update()
    {
        _world.Update();
        _debugMenu.Update();
    }
    public void Dispose()
    {
        _debugMenu.Dispose();
        _player.Dispose();
    }
}