
using project_republics.Utils.Storage;

namespace project_republics.Components.World;

public class WorldContainer
{
    public const int MAP_WIDTH = 100;
    public const int MAP_HEIGHT = 100;
    public const int SECTOR_WIDTH = 1000;
    public const int SECTOR_HEIGHT = 1000;
    private readonly WorldStorage _storage;
    public WorldContainer(WorldStorage worldStorage)
    {
        _storage = worldStorage;
    }
}