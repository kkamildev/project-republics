

namespace project_republics.Components.World.Sections;

public class Chunk
{
    private BaseTile[,] _tiles;
    public Chunk()
    {
        _tiles = new BaseTile[WorldContainer.CHUNK_SIDE, WorldContainer.CHUNK_SIDE];
    }
}