
using System;
using project_republics.Components.UI.Models;
using project_republics.Utils.Components.UI;
using Microsoft.Xna.Framework;

namespace project_republics.Components.UI.Forms;

public class CreateWorldForm : BaseForm<WorldModel.WorldData>
{
    private TitleBox _titleBox;
    public CreateWorldForm(Action<WorldModel.WorldData> onSubmit, Action<bool> backAction) : base(onSubmit, backAction)
    {
        _titleBox = new("CREATE_NEW_WORLD_TITLE", Utils.Input.Textures.BACKGROUND, new Rectangle((int)MainGame.Resolution.X / 2, (int)MainGame.Resolution.Y / 2, 1600 / 5 * 4, 900 / 5 * 4));
    }

    public override void Draw()
    {
        if(_active)
        {
            _titleBox.Draw();
        }
    }

    public override void Update()
    {
        if(_active)
        {
            
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
        }
    }
}