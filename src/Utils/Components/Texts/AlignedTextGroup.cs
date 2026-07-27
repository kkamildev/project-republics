

using Microsoft.Xna.Framework;

namespace project_republics.Utils.Components.Texts;

public class AlignedTextGroup : TextGroup
{
    public enum Alignment
    {
        VERTICAL,
        HORIZONTAL
    }

    private Alignment _aligment;
    private Vector2 _margin;

    public AlignedTextGroup(Text[] texts, Vector2 margin, Alignment aligment = Alignment.VERTICAL) : base(texts)
    {
        _aligment = aligment;
        _margin = margin;
        MainPosition = new Vector2(0);
    }

    public override Vector2 MainPosition {
        get
        {
            return base.MainPosition;
        }
        set
        {
            _mainPosition = value;
            if(_aligment == Alignment.VERTICAL)
            {
                for(int i = 0;i<_texts.Length;i++)
                {
                    _texts[i].Position = new Vector2(_mainPosition.X, _mainPosition.Y + i * _margin.Y);
                }
            }
            if(_aligment == Alignment.HORIZONTAL)
            {
                for(int i = 0; i < _texts.Length; i++)
                {
                    _texts[i].Position = new Vector2(_mainPosition.X + i * _margin.X, _mainPosition.Y);
                }
            }
        }
    }
}