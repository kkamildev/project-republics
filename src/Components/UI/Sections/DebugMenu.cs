

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
    private readonly ValueState<string> _fpsState, _sectorPosState, _playerPosState;
    private EaseInOutAnimation _animation;
    private TextGroup _texts;
    private RectSprite _background;
    private bool _active;
    private Player _playerRef;
    public DebugMenu(Player player)
    {
        _playerRef = player;
        _fpsCounter = new();
        MainGame.Input.SubscribeAction(Utils.Input.Controls.DEBUG_OPEN, MenuAction);
        _texts = new([
            new Text(Utils.Input.Fonts.SMALLER, "{0} {1}", new Vector2(10)){StringParams=[MainGame.TITLE, MainGame.VERSION]},
            new Text(Utils.Input.Fonts.SMALLER, "{0}", new Vector2(10, 40)){StringParams=["FPS: " + _fpsCounter.Fps]},
            new Text(Utils.Input.Fonts.SMALLER, "{0}", new Vector2(10, 80)){StringParams=[$"Sector (X:{_playerRef.Data.SectorX}, Y:{_playerRef.Data.SectorY})"]},
            new Text(Utils.Input.Fonts.SMALLER, "{0}", new Vector2(20, 110)){StringParams=[$"Position (X:{_playerRef.Data.X}, Y:{_playerRef.Data.Y})"], Color = Color.DimGray},
        ]);
        _fpsState = new((newValue) => _texts.Texts[1].StringParams = [newValue]);
        _sectorPosState = new((newValue) => _texts.Texts[2].StringParams = [newValue]);
        _playerPosState = new((newValue) => _texts.Texts[3].StringParams = [newValue]);
        _background = new(Utils.Input.Textures.BACKGROUND, new Rectangle(0, 0, 400, (int)MainGame.Resolution.Y), 0, 0, 0){Color = new Color(Color.Black, 0.75f)};
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
            _sectorPosState.CurrentValue = $"Sector (X:{_playerRef.Data.SectorX}, Y:{_playerRef.Data.SectorY})";
            _playerPosState.CurrentValue = $"Position (X:{_playerRef.Data.X}, Y:{_playerRef.Data.Y})";
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