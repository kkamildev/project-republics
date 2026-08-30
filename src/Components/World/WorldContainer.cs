
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using project_republics.Components.World.Sections;
using project_republics.Utils.Storage;

namespace project_republics.Components.World;

public class WorldContainer
{
    public const byte CHUNK_SIDE = 16;
    public const byte SECTOR_CHUNKS_SIDE = 64;
    public const byte MAP_SIDE = 25;
    public const int PLAYER_MOVEMENT_SPEED = 700;
    private readonly List<Sector> _sectors;
    private Sector _activeSector;
    private readonly WorldStorage _storage;
    private readonly Player _mainPlayerRef;
    public WorldContainer(WorldStorage worldStorage, Player mainPlayer)
    {
        _mainPlayerRef = mainPlayer;
        _storage = worldStorage;
        _sectors = [];
        _mainPlayerRef.OnChangePosition = OnChangePlayerPosition;
    }

    public async Task PrepareWorld()
    {
        _sectors.Add(await Sector.GenSector(this, new ByteVector2(0, 0)));
        SwitchToActiveSector(_sectors[0], _mainPlayerRef.Position);
    }

    private void SwitchToActiveSector(Sector sector, Vector2 position)
    {
        _activeSector = sector;
        _activeSector.SetViewPosition(position);
    }

    private void OnChangePlayerPosition(Vector2 newPosition)
    {
        _activeSector.SetViewPosition(newPosition);
    }

    public void Draw()
    {
        _activeSector.Draw();
    }

    public void Update()
    {
        _activeSector.Update();
    }
}