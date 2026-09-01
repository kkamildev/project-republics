
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using project_republics.Components.World.Sections;
using project_republics.Utils.DataStructures;
using project_republics.Utils.Storage;

namespace project_republics.Components.World;

public class WorldContainer
{
    public const byte CHUNK_SIDE = 16;
    public const byte SECTOR_CHUNKS_SIDE = 64;
    public const byte MAP_SIDE = 25;
    public const int PLAYER_MOVEMENT_SPEED = 700;
    public const int PLAYER_GRAPH_RENDER_RANGE = 3;
    private int _visibleChunks;
    private readonly List<Sector> _sectors;
    private Sector _activeSector;
    private readonly WorldStorage _storage;
    private readonly WorldGen _worldGenerator;
    private readonly Player _mainPlayerRef;
    public WorldContainer(WorldStorage worldStorage, Player mainPlayer)
    {
        _visibleChunks = 0;
        _mainPlayerRef = mainPlayer;
        _storage = worldStorage;
        _sectors = [];
        _worldGenerator = new(_storage.Metadata.Seed);
        _mainPlayerRef.OnChangePosition = OnChangePlayerPosition;
    }

    public async Task PrepareWorld()
    {
        // TODO: generate all sectors in the map
        _sectors.Add(await Sector.GenSector(this, new ByteVector2(0, 0)));
        SwitchToActiveSector(_sectors[0], _mainPlayerRef.Position);
    }

    private void SwitchToActiveSector(Sector sector, Vector2 position)
    {
        _visibleChunks = 0;
        _activeSector = sector;
        OnChangePlayerPosition(position);
    }

    private void OnChangePlayerPosition(Vector2 newPosition)
    {
        _activeSector.SetViewPosition(newPosition, OnChangeChunkVisibility);
    }

    private void OnChangeChunkVisibility(bool visible)
    {
        if(visible)
        {
            _visibleChunks++;
        } else
        {
            _visibleChunks--;
        }
    }

    public void Draw()
    {
        _activeSector.Draw();
    }

    public void Update()
    {
        _activeSector.Update();
    }

    public int VisibleChunks
    {
        get
        {
            return _visibleChunks;
        }
    }

    public WorldStorage Storage
    {
        get
        {
            return _storage;
        }
    }

    public Player MasterPlayer
    {
        get
        {
            return _mainPlayerRef;
        }
    }

    public WorldGen WorldGen
    {
        get
        {
            return _worldGenerator;
        }
    }
}