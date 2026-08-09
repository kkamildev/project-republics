
using System;
using project_republics.Components.UI.Models;
using project_republics.Utils.Components.UI;
using Microsoft.Xna.Framework;
using project_republics.Components.UI.Buttons;
using System.Linq;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Components.Sprites;
using project_republics.Components.UI.Labels;

namespace project_republics.Components.UI.Forms;

public class CreateWorldForm : BaseForm<WorldModel.WorldData>
{
    private TitleBox _titleBox;
    private ButtonGroup _mainButtonGroup;
    private InputField _inputField;
    public CreateWorldForm(Action<WorldModel.WorldData> onSubmit, Action<bool> backAction) : base(onSubmit, backAction)
    {
        _titleBox = new("CREATE_NEW_WORLD_TITLE", Utils.Input.Textures.BACKGROUND, new Rectangle((int)MainGame.Resolution.X / 2, (int)MainGame.Resolution.Y / 2, 1600 / 5 * 4, 900 / 5 * 4));
        string[] texts = ["CREATE_WORLD_NAME", "CREATE_WORLD_MODE", "CREATE_WORLD_REPUBLIC", "CREATE_WORLD_REPUBLIC_FLAG", "CREATE_WORLD_FINISH", "BACK"];
        Action[] actions = [
            () => {
                _mainButtonGroup.Active = false;
                MainGame.Input.UnSubscribeAction(Utils.Input.Controls.EXIT, _backAction);
                MainGame.VirtualKeyboard.SetActive(true, _inputField, () => {
                    MainGame.VirtualKeyboard.SetActive(false, null, null);
                    _mainButtonGroup.Active = true;
                    MainGame.Input.SubscribeAction(Utils.Input.Controls.EXIT, _backAction);
                });
            },
            () => {
                
            },
            () => {},
            () => {},
            () => {},
            () => _backAction.Invoke(false)
        ];
        _mainButtonGroup = new([
            ..texts.Select((text, index) => new SpriteButton(new AlignedText(Utils.Input.Fonts.SMALL, text, new Vector2(300, 200 + 100 * index), 0.5f, 0.5f){Color = Color.DimGray},
             new AlignedSprite(Utils.Input.Textures.BUTTON2, new Vector2(300, 200 + 100 * index), 0.5f, 0.5f){Scale = 3f},
              actions[index]){ChangeColor = Color.White})
        ]);
        _inputField = new("WORLD_NAME_PLACEHOLDER", new Vector2(600, 200))
        {
            Active = false
        };
    }

    public override void Draw()
    {
        if(_active)
        {
            _titleBox.Draw();
            _mainButtonGroup.Draw();
            _inputField.Draw();
        }
    }

    public override void Update()
    {
        if(_active)
        {
            _inputField.Update();
            _mainButtonGroup.Update();
        }
    }

    public override void Dispose()
    {
        _titleBox.Dispose();
        _inputField.Update();
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
            }
        }
    }
}