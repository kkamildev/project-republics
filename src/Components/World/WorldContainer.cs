
using project_republics.Utils.Storage;

namespace project_republics.Components.World;

public class WorldContainer
{
    public const int CHUNK_SIDE = 16;
    public const int SECTOR_CHUNKS_SIDE = 64;
    public const int MAP_SIDE = 25;
    private readonly WorldStorage _storage;
    public WorldContainer(WorldStorage worldStorage)
    {
        _storage = worldStorage;
    }
}