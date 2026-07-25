

using Microsoft.Xna.Framework;
using project_republics.Utils.Components.UI;

namespace project_republics.Utils.Components.Texts;

public class TextGroup : UIBase
{
    private Text[] _texts;
    private bool _applyParentColor = false;

    public TextGroup(Text[] texts, bool applyParentColor)
    {
        _applyParentColor = applyParentColor;
        _texts = texts;
    }

    public override Color MainColor {
        get => base.MainColor;
        set
        {
            base.MainColor = value;
            if(_applyParentColor)
            {
                foreach (Text text in _texts)
                {
                    text.Color = value;
                }
            }
        }
    }
}