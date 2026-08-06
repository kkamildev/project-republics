
using System;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.UI;
using Microsoft.Xna.Framework;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Texts;
using project_republics.Components.UI.Buttons;
using System.Linq;
using project_republics.Components.UI.Models;
using System.Collections.Generic;
using project_republics.Utils.Components.Network;

namespace project_republics.Components.UI.Sections;

public class PlayGameSide : UIBase, IDisposable
{
    private bool _active;
    private EaseInOutAnimation _showUIAnimation;
    private Action<bool> _backAction;
    private Action<WorldModel.WorldData> _playAction;
    private TitleBox _titleBox;
    private ButtonGroup _mainButtonGroup;
    private ScrollButtonGroup _worlds;
    public PlayGameSide(Action backAction, Action<WorldModel.WorldData> playAction)
    {
        _active = false;
        _worlds = new([], 3, 1)
        {
            MoveVector = new Vector2(0, 200)
        };
        _titleBox = new("SELECT_WORLD_TITLE", Utils.Input.Textures.BACKGROUND, new Rectangle((int)MainGame.Resolution.X / 2, (int)MainGame.Resolution.Y / 2, 1600 / 5 * 4, 900 / 5 * 4));
        string[] texts = ["SELECT_WORLD", "CREATE_WORLD", "CONNECT_WORLD", "ACCOUNT", "BACK"];
        Action[] actions = [
            () => {
                _worlds.Active = true;
                _mainButtonGroup.Active = false;
                MainGame.Input.SubscribeAction(Utils.Input.Controls.EXIT, ExitActionFromWorldSelect);
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, _backAction);
            },
            () => {},
            () => {},
            () => {},
            () => _backAction.Invoke(false)
        ];
        _mainButtonGroup = new([
            ..texts.Select((text, index) => new SpriteButton(new AlignedText(Utils.Input.Fonts.BASE, text, new Vector2(300, 200 + 100 * index), 0.5f, 0.5f){Color = Color.DimGray},
             new AlignedSprite(Utils.Input.Textures.BUTTON2, new Vector2(300, 200 + 100 * index), 0.5f, 0.5f){Scale = 3f},
              actions[index]){ChangeColor = Color.White})
        ]);
        _playAction = playAction;
        _backAction = (hold) =>
        {
            if(!hold)
            {
                backAction.Invoke();
                Active = false;
            }
        };
        MainGame.Storage.Account.MainPosition = new Vector2(0, 400);
        MainPosition = new Vector2(0, -900);
    }

    public override void Draw()
    {
        _titleBox.Draw();
        _mainButtonGroup.Draw();
        _worlds.Draw();
        MainGame.Storage.Account.Draw();
    }
    public override void Update()
    {
        _mainButtonGroup.Update();
        if(_showUIAnimation != null)
        {
            _showUIAnimation.Update();
            MainPosition = new Vector2(0, -900 * _showUIAnimation.Progress);
            MainGame.Storage.Account.MainPosition = new Vector2(0, 400 * _showUIAnimation.Progress);
        }
        _worlds.Update();
    }

    private void ExitActionFromWorldSelect (bool hold) {
        if(!hold)
        {
            _worlds.Active = false;
            _mainButtonGroup.Active = true;
            MainGame.Input.SubscribeAction(Utils.Input.Controls.EXIT, _backAction);
            MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, ExitActionFromWorldSelect);
        }
    }
    private void ExitActionFromWorld(bool hold) {
        if(!hold)
        {
            WorldModel choosenButton = (WorldModel)_worlds.Buttons.Find((button) => (button as WorldModel).Choosen);
            choosenButton.Choosen = false;
            _worlds.Active = true;
            MainGame.Input.SubscribeAction(Utils.Input.Controls.EXIT, ExitActionFromWorldSelect);
            MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, ExitActionFromWorld);
        }
    }

    private void ChooseWorld(int index)
    {
        foreach (WorldModel model in _worlds.Buttons)
        {
            model.Choosen = false;
        }
        WorldModel choosenModel = (WorldModel)_worlds.Buttons[index];
        _worlds.Active = false;
        choosenModel.Choosen = true;

        MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, ExitActionFromWorldSelect);
        MainGame.Input.SubscribeAction(Utils.Input.Controls.EXIT, ExitActionFromWorld);
    }

    private void PlayWorld(int index)
    {
        WorldModel playedModel = (WorldModel)_worlds.Buttons[index];
        MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, ExitActionFromWorld);
        _playAction.Invoke(playedModel.Data);
    }

    private void SearchWorlds()
    {
        List<WorldModel.WorldData> data = MainGame.Storage.SearchForWorlds();
        data.Sort();
        _worlds.Dispose();
        _worlds.SelectedIndex = 0;
        _worlds.Buttons.Clear();
        for(int i = 0;i<data.Count;i++)
        {
            int index = i;
            WorldModel model = new(data[i], () => ChooseWorld(index), () => PlayWorld(index)){MainPosition = new Vector2(575, 180 + 200 * i)};
            _worlds.Buttons.Add(model);
        }
        MainPosition = new Vector2(0, -900);
        if(_worlds.Buttons.Count > 0)
        {
            _worlds.SelectedIndex = 0;
            _worlds.Active = false;
        }
    }

    public bool Active
    {
        get
        {
            return _active;
        }
        set
        {
            _active = value;
            if(_active)
            {
                _mainButtonGroup.Active = true;
                _showUIAnimation = new(1f, () => {}, _showUIAnimation?.Progress ?? 1f, 0f);
                MainGame.Input.SubscribeAction(Utils.Input.Controls.EXIT, _backAction);
                // Searching for worlds
                MainPosition = new Vector2(0, 0);
                SearchWorlds();
            } else
            {
                _mainButtonGroup.SelectedIndex = 0;
                _mainButtonGroup.Active = false;
                _showUIAnimation = new(1f, () => {}, _showUIAnimation?.Progress ?? 0, 1f);
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, _backAction);
            }
        }
    }

    public override Vector2 MainPosition {
        get => base.MainPosition;
        set
        {
            foreach (BaseButton button in  _mainButtonGroup.Buttons)
            {
                button.MainPosition-= base.MainPosition;
            }
            foreach (BaseButton button in _worlds.Buttons)
            {
                button.MainPosition-= base.MainPosition;
            }
            base.MainPosition = value;
            _titleBox.MainPosition = base.MainPosition;
            foreach (BaseButton button in _worlds.Buttons)
            {
                button.MainPosition+= base.MainPosition;
            }
            foreach (BaseButton button in _mainButtonGroup.Buttons)
            {
                button.MainPosition+= base.MainPosition;
            }

        }
    }

    public void Dispose()
    {
        MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, _backAction);
        _titleBox.Dispose();
        _mainButtonGroup.Dispose();
        _worlds.Dispose();
    }
}