
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
    public const int PLAYER_GRAPH_RENDER_RANGE = 5;
    public const int TILE_SIZE = 16;
    private int _visibleChunks;
    private Sector _activeSector;
    private BaseTile _selectedTile;
    private readonly WorldStorage _storage;
    private readonly WorldGen _worldGenerator;
    private readonly Player _mainPlayerRef;
    public WorldContainer(WorldStorage worldStorage, Player mainPlayer)
    {
        _visibleChunks = 0;
        _mainPlayerRef = mainPlayer;
        _storage = worldStorage;
        _worldGenerator = new(_storage.Metadata.Seed);
        _mainPlayerRef.OnChangePosition = OnChangePlayerPosition;
    }

    public async Task PrepareWorld()
    {
        await SetSector();
    }

    public async Task SetSector()
    {
        ByteVector2 sectorPos = new(_mainPlayerRef.Data.SectorX, _mainPlayerRef.Data.SectorY);
        if(_activeSector != null)
        {
            // TODO: saving sector data
        }
        SwitchToActiveSector(await Sector.GenSector(this, sectorPos), _mainPlayerRef.Position);
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

        // prev tile returning to normal state
        if(_selectedTile != null)
        {
            _selectedTile.Color = Color.White;
        }
        _selectedTile = _activeSector.GetTile();

        // new tile, selected tile state like highlight
        _selectedTile.Color = Color.Gray;
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
        _activeSector?.Draw();
    }

    public void Update()
    {
        _activeSector?.Update();
    }

    public int VisibleChunks
    {
        get
        {
            return _visibleChunks;
        }
    }

    public BaseTile SelectedTile
    {
        get
        {
            return _selectedTile;
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