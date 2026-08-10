
using System;
using project_republics.Components.UI.Models;
using project_republics.Utils.Components.UI;
using Microsoft.Xna.Framework;
using project_republics.Components.UI.Buttons;
using System.Linq;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Components.Sprites;
using project_republics.Components.UI.Labels;
using System.Collections.Generic;

namespace project_republics.Components.UI.Forms;

public class CreateWorldForm : BaseForm<WorldModel.WorldData>
{
    private TitleBox _titleBox;
    private ButtonGroup _mainButtonGroup;
    private InputField _worldNameinputField, _republicNameInputField;
    private Text _nameSimilarWarningText;
    private TextGroup _modeTextGroup;
    private FlagModel _flag;
    private bool _worldNameSimilarity;
    private int _currentModeIndex;
    public CreateWorldForm(Action<WorldModel.WorldData> onSubmit, Action<bool> backAction) : base(onSubmit, backAction)
    {
        _currentModeIndex = 0;
        _titleBox = new("CREATE_NEW_WORLD_TITLE", Utils.Input.Textures.BACKGROUND, new Rectangle((int)MainGame.Resolution.X / 2, (int)MainGame.Resolution.Y / 2, 1600 / 5 * 4, 900 / 5 * 4));
        _modeTextGroup = new([
            new Text(Utils.Input.Fonts.BASE, WorldModel.Modes[_currentModeIndex], new Vector2(600, 620)){StringParams = [""], Color = Color.DarkCyan},
            new Text(Utils.Input.Fonts.SMALL, WorldModel.Modes[_currentModeIndex] + "_DESC", new Vector2(630, 670)){}
        ]);
        string[] texts = ["CREATE_WORLD_NAME", "CREATE_WORLD_MODE", "CREATE_WORLD_REPUBLIC", "CREATE_WORLD_REPUBLIC_FLAG", "CREATE_WORLD_FINISH", "BACK"];
        Action[] actions = [
            () => {
                _mainButtonGroup.Active = false;
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, _backAction);
                MainGame.VirtualKeyboard.SetActive(true, _worldNameinputField, () => {
                    MainGame.VirtualKeyboard.SetActive(false, null, null);
                    _mainButtonGroup.Active = true;
                    SearchSimilarWorldName();
                    MainGame.Input.SubscribeAction(Utils.Input.Controls.EXIT, _backAction);
                });
            },
            () => {
                _currentModeIndex++;
                if(_currentModeIndex >= WorldModel.Modes.Length) {
                    _currentModeIndex = 0;
                }
                _modeTextGroup.Texts[0].TranslationKey = WorldModel.Modes[_currentModeIndex];
                _modeTextGroup.Texts[1].TranslationKey = WorldModel.Modes[_currentModeIndex] + "_DESC";
                _modeTextGroup.Texts[0].Color = WorldModel.ModesColors[_currentModeIndex];
            },
            () => {
                _mainButtonGroup.Active = false;
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, _backAction);
                MainGame.VirtualKeyboard.SetActive(true, _republicNameInputField, () => {
                    MainGame.VirtualKeyboard.SetActive(false, null, null);
                    _mainButtonGroup.Active = true;
                    MainGame.Input.SubscribeAction(Utils.Input.Controls.EXIT, _backAction);
                });
            },
            () => {},
            SubmitForm,
            () => _backAction.Invoke(false)
        ];
        _mainButtonGroup = new([
            ..texts.Select((text, index) => new SpriteButton(new AlignedText(Utils.Input.Fonts.SMALL, text, new Vector2(300, 200 + 100 * index), 0.5f, 0.5f){Color = Color.DimGray},
             new AlignedSprite(Utils.Input.Textures.BUTTON2, new Vector2(300, 200 + 100 * index), 0.5f, 0.5f){Scale = 3f},
              actions[index]){ChangeColor = Color.White})
        ]);
        _worldNameinputField = new("CREATE_WORLD_NAME", "WORLD_NAME_PLACEHOLDER", new Vector2(600, 200))
        {
            Active = false,
            MaxCharactersCount = 30
        };
        _republicNameInputField = new("CREATE_WORLD_REPUBLIC", "REPUBLIC_NAME_PLACEHOLDER", new Vector2(600, 470))
        {
            Active = false,
            MaxCharactersCount = 30
        };
        _nameSimilarWarningText = new(Utils.Input.Fonts.BASE, "WORLD_ALREADY_EXIST", new Vector2(630, 300))
        {
            Color = new(166, 19, 8),
        };
        _flag = new(new string[33*33], new Vector2(630, 350)){DefaultColor = Color.Black};
        _flag.Sprite.Scale = 3f;
    }

    public override void Draw()
    {
        if(_active)
        {
            _titleBox.Draw();
            _mainButtonGroup.Draw();
            _worldNameinputField.Draw();
            _republicNameInputField.Draw();
            _modeTextGroup.Draw();
            _flag.Draw();
            if(_worldNameSimilarity) _nameSimilarWarningText.Draw();
        }
    }

    public override void Update()
    {
        if(_active)
        {
            _worldNameinputField.Update();
            _republicNameInputField.Update();
            _mainButtonGroup.Update();
        }
    }

    private void SearchSimilarWorldName()
    {
        List<WorldModel.WorldData> worldsData = MainGame.Storage.SearchForWorlds();

        foreach (WorldModel.WorldData data in worldsData)
        {
            if(data.DirectoryPath == _worldNameinputField.Content)
            {
                _worldNameSimilarity = true;
                return;
            }
        }
        _worldNameSimilarity = false;
    }

    private void SubmitForm()
    {
        SearchSimilarWorldName();
        if(_worldNameSimilarity || _worldNameinputField.Content.Length == 0 || _republicNameInputField.Content.Length == 0)
        {
            return;
        }
        _onSubmit.Invoke(new WorldModel.WorldData()
        {
            Name = _worldNameinputField.Content,
            RepublicName = _republicNameInputField.Content,
            DirectoryPath = _worldNameinputField.Content,
            CreatedAt = DateTime.Now,
            LastPlayed = DateTime.Now,
            Mode = _currentModeIndex,
            FlagPixelRows = _flag.ToHexRows()
        });

    }

    public override void Dispose()
    {
        _titleBox.Dispose();
        _worldNameinputField.Dispose();
        _republicNameInputField.Dispose();
        _nameSimilarWarningText.Dispose();
        _modeTextGroup.Dispose();
        _flag.Dispose();
    }


    public override bool Active {
        get => base.Active;
        set
        {
            base.Active = value;
            if(_active)
            {
                _mainButtonGroup.Active = true;
            } else
            {
                _mainButtonGroup.Active = false;
                _mainButtonGroup.SelectedIndex = 0;
                _worldNameinputField.Clear();
            }
        }
    }
}