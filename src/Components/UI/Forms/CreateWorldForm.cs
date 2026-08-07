
using System;
using project_republics.Components.UI.Models;
using project_republics.Utils.Components.UI;
using Microsoft.Xna.Framework;
using project_republics.Components.UI.Buttons;
using System.Linq;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Components.Sprites;

namespace project_republics.Components.UI.Forms;

public class CreateWorldForm : BaseForm<WorldModel.WorldData>
{
    private TitleBox _titleBox;
    private ButtonGroup _mainButtonGroup;
    public CreateWorldForm(Action<WorldModel.WorldData> onSubmit, Action<bool> backAction) : base(onSubmit, backAction)
    {
        _titleBox = new("CREATE_NEW_WORLD_TITLE", Utils.Input.Textures.BACKGROUND, new Rectangle((int)MainGame.Resolution.X / 2, (int)MainGame.Resolution.Y / 2, 1600 / 5 * 4, 900 / 5 * 4));
        string[] texts = ["CREATE_WORLD_NAME", "CREATE_WORLD_MODE", "CREATE_WORLD_REPUBLIC", "CREATE_WORLD_REPUBLIC_FLAG", "CREATE_WORLD_FINISH", "BACK"];
        Action[] actions = [
            () => {
                
            },
            () => {
                
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
    }

    public override void Draw()
    {
        if(_active)
        {
            _titleBox.Draw();
            _mainButtonGroup.Draw();
        }
    }

    public override void Update()
    {
        if(_active)
        {
            _mainButtonGroup.Update();
        }
    }

    public override void Dispose()
    {
        _titleBox.Dispose();
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