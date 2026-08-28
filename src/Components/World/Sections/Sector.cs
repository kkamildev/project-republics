
using Microsoft.Xna.Framework;

namespace project_republics.Components.World.Sections;

public class Sector
{
    private Vector2 _position;
    private Chunk[,] _chunks;
    public Sector()
    {
        _chunks = new Chunk[WorldContainer.SECTOR_CHUNKS_SIDE, WorldContainer.SECTOR_CHUNKS_SIDE];
    }
}