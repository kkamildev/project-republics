

using System;
using Microsoft.Xna.Framework;
using project_republics.Components.World;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Components.UI;
using project_republics.Utils.Diagnostics;
using project_republics.Utils.States;

namespace project_republics.Components.UI.Sections;

public class DebugMenu : UIBase, IDisposable
{
    private readonly FpsCounter _fpsCounter;
    private readonly ValueState<string> _fpsState, _sectorPosState, _playerPosState, _visibleChunksState, _biomeState;
    private EaseInOutAnimation _animation;
    private TextGroup _texts;
    private RectSprite _background;
    private bool _active;
    private WorldContainer _worldRef;
    public DebugMenu(WorldContainer worldRef)
    {
        _worldRef = worldRef;
        _fpsCounter = new();
        MainGame.Input.SubscribeAction(Utils.Input.Controls.DEBUG_OPEN, MenuAction);
        _texts = new([
            new Text(Utils.Input.Fonts.SMALLER, "{0} {1}", new Vector2(10)){StringParams=[MainGame.TITLE, MainGame.VERSION]},
            new Text(Utils.Input.Fonts.SMALLER, "{0}", new Vector2(10, 60)){StringParams=["FPS: " + _fpsCounter.Fps]},
            new Text(Utils.Input.Fonts.SMALLER, "{0}", new Vector2(10, 85)){StringParams=["SEED: " + _worldRef.Storage.Metadata.Seed]},
            new Text(Utils.Input.Fonts.SMALLER, "{0}", new Vector2(10, 120)){StringParams=[$"Sector (X:{_worldRef.MasterPlayer.Data.SectorX}, Y:{_worldRef.MasterPlayer.Data.SectorY})"]},
            new Text(Utils.Input.Fonts.SMALLER, "{0}", new Vector2(20, 145)){StringParams=[$"Position (X:{_worldRef.MasterPlayer.Data.X}, Y:{_worldRef.MasterPlayer.Data.Y})"], Color = Color.DimGray},
            new Text(Utils.Input.Fonts.SMALLER, "{0}", new Vector2(10, 185)){StringParams = [$"VCH: {_worldRef.VisibleChunks}"]},
            new Text(Utils.Input.Fonts.SMALLER, "{0}", new Vector2(110, 185)){StringParams = [$"Biome: {_worldRef.SelectedTile.Biome}"]}
        ]);
        _fpsState = new((newValue) => _texts.Texts[1].StringParams = [newValue]);
        _sectorPosState = new((newValue) => _texts.Texts[3].StringParams = [newValue]);
        _playerPosState = new((newValue) => _texts.Texts[4].StringParams = [newValue]);
        _visibleChunksState = new((newValue) => _texts.Texts[5].StringParams = [newValue]);
        _biomeState = new((newValue) => _texts.Texts[6].StringParams = [newValue]);
        _background = new(Utils.Input.Textures.BACKGROUND, new Rectangle(0, 0, 400, (int)MainGame.Resolution.Y), 0, 0, 0){Color = new Color(Color.Black, 0.60f)};
        MainPosition = new Vector2(-400, 0);
    }
    

    public override void Draw()
    {
        _background.Draw();
        _texts.Draw();
    }

    public override void Update()
    {
        if(_active)
        {
            _fpsCounter.Update();
            _fpsState.CurrentValue = "FPS: " + _fpsCounter.Fps;
            _sectorPosState.CurrentValue = $"Sector (X:{_worldRef.MasterPlayer.Data.SectorX}, Y:{_worldRef.MasterPlayer.Data.SectorY})";
            _playerPosState.CurrentValue = $"Position (X:{_worldRef.MasterPlayer.Data.X}, Y:{_worldRef.MasterPlayer.Data.Y})";
            _visibleChunksState.CurrentValue = $"VCH: {_worldRef.VisibleChunks}";
            _biomeState.CurrentValue = $"Biome: {_worldRef.SelectedTile.Biome}";
        }
        if(_animation != null)
        {
            _animation.Update();
            MainPosition = new Vector2(_animation.Progress, 0);
        }
    }

    private void MenuAction(bool hold)
    {
        if(!hold)
        {
            _active = !_active;
            if(_active)
            {
                _animation = new(0.6f, () => {}, _animation?.Progress ?? -400 , 0);
            } else
            {
                _animation = new(0.6f, () => {},  _animation?.Progress ?? 0, -400);
            }
        }

    }
    public void Dispose()
    {
        MainGame.Input.UnSubscribeAction(Utils.Input.Controls.DEBUG_OPEN, MenuAction);
        _texts.Dispose();
    }

    public override Vector2 MainPosition {
        get => base.MainPosition;
        set
        {
            _background.Position -= base.MainPosition;
            _texts.MainPosition -= base.MainPosition;
            base.MainPosition = value;
            _texts.MainPosition += base.MainPosition;
            _background.Position += base.MainPosition;
        }
    }

}