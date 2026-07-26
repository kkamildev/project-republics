

using Microsoft.Xna.Framework;
using project_republics.Utils.Components.UI;

namespace project_republics.Utils.Components.Texts;

public class TextGroup : UIBase
{
    protected readonly Text[] _texts;

    public TextGroup(Text[] texts)
    {
        _texts = texts;
    }

    public override void Draw()
    {
        foreach (Text text in _texts)
        {
            text.Draw();
        }
    }

    public override Color MainColor {
        get => base.MainColor;
        set
        {
            base.MainColor = value;
            foreach (Text text in _texts)
            {
                text.Color = value;
            }
        }
    }

    public override Vector2 MainPosition
    {
        get
        {
            return base.MainPosition;
        }
        set
        {
            foreach (Text text in _texts)
            {
                text.Position -= MainPosition;
            }
            base.MainPosition = value;
            foreach (Text text in _texts)
            {
                text.Position += MainPosition;
            }
        }
    }

    public Text[] Texts
    {
        get
        {
            return _texts;
        }
    }
}