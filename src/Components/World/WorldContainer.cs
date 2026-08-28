
using System.Collections.Generic;
using System.Threading.Tasks;
using project_republics.Components.World.Sections;
using project_republics.Utils.Storage;

namespace project_republics.Components.World;

public class WorldContainer
{
    public const byte CHUNK_SIDE = 16;
    public const byte SECTOR_CHUNKS_SIDE = 64;
    public const byte MAP_SIDE = 25;
    private readonly List<Sector> _sectors;
    private Sector _activeSector;
    private readonly WorldStorage _storage;
    private readonly Player _mainPlayerRef;
    public WorldContainer(WorldStorage worldStorage, Player mainPlayer)
    {
        _mainPlayerRef = mainPlayer;
        _storage = worldStorage;
        _sectors = [];
    }

    public async Task PrepareWorld()
    {
        _sectors.Add(new Sector(new ByteVector2(0, 0)));
        _activeSector = _sectors[0];
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