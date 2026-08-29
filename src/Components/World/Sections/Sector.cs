
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace project_republics.Components.World.Sections;

public class Sector
{
    private readonly WorldContainer _worldRef;
    private ByteVector2 _position;
    private Chunk[,] _chunks;
    private Sector(WorldContainer worldRef, ByteVector2 position)
    {
        _worldRef = worldRef;
        _position = position;   
        _chunks = new Chunk[WorldContainer.SECTOR_CHUNKS_SIDE, WorldContainer.SECTOR_CHUNKS_SIDE];
    }

    public static async Task<Sector> GenSector(WorldContainer worldRef, ByteVector2 position)
    {
        // TODO: creating reading from storage actual is this sector exist
        Sector sector = new(worldRef, position);
        await sector.GenChunks([]);
        return sector;
    }

    public async Task GenChunks(string[] data)
    {
        _chunks[0, 0] = new Chunk(this, new ByteVector2(0, 0));
        _chunks[0, 1] = new Chunk(this, new ByteVector2(1, 0));
    }

    public void Draw()
    {
        for(int i = 0;i<_chunks.GetLength(0);i++)
        {
            for(int j = 0;j<_chunks.GetLength(1);j++)
            {
                _chunks[i, j]?.Draw();
            }
        }
    }

    public void Update()
    {
        
    }
}