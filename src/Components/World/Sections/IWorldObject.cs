
using Microsoft.Xna.Framework;

namespace project_republics.Components.World.Sections;

public interface IWorldObject
{
    public string Serialize();
    public IWorldObject Parse(Chunk chunkRef, Vector2 inChunkPosition, string data);
}