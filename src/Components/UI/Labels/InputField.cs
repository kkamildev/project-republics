
using System;
using Microsoft.Xna.Framework;
using project_republics.Utils.Animations;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Components.UI;
using project_republics.Utils.Helpers;

namespace project_republics.Components.UI.Labels;

public sealed class InputField : UIBase, IDisposable
{

    private AlignedText _contentText, _placeholderText;
    private Sprite _inputFieldSprite;
    private Animation _writingAnimation;
    private bool _active;

    public int MaxCharactersCount{get;set;}

    public InputField(string placeholderText, Vector2 position)
    {
        _inputFieldSprite = new(Utils.Input.Textures.INPUT_FIELD, Vector2.Zero)
        {
            Scale = 3f
        };
        _placeholderText = new(Utils.Input.Fonts.BASE, placeholderText, new Vector2(256 * 3 / 2, 31 * 3 / 2), 0.5f, 0.5f)
        {
            Color = Color.DimGray
        };
        _contentText = new(Utils.Input.Fonts.BASE, "{0}{1}", new Vector2(20, 31 * 3 / 2), 0f, 0.5f)
        {
            StringParams = ["", ""]
        };
        _writingAnimation = new(0.5f, ChangeWritingIndicator)
        {
            Loop = true
        };
        MainPosition = position;
        MaxCharactersCount = 100;
    }

    public override void Draw()
    {
        _inputFieldSprite.Draw();
        if(!_active && _contentText.Content.Length == 0)
        {
            _placeholderText.Draw();
        } else
        {
            _contentText.Draw();
        }
    }

    public override void Update()
    {
        if(_active)
        {
            _writingAnimation.Update();
        }
    }

    public void AddText(string text)
    {
        _contentText.StringParams = [(_contentText.StringParams[0] + text).Truncate(MaxCharactersCount, ""), ""];
        _writingAnimation.Reset();
    }
    public void RemoveLast()
    {
        if(_contentText.StringParams[0].ToString().Length != 0)
        {
            _contentText.StringParams = [_contentText.StringParams[0].ToString()[..^1], ""];
        } else
        {
            _contentText.StringParams = ["", ""];
        }
        _writingAnimation.Reset();
    }
    public void Clear()
    {
        _contentText.StringParams = ["", ""];
        _writingAnimation.Reset();
    }

    private void ChangeWritingIndicator()
    {
        if((string)_contentText.StringParams[1] == "_")
        {
            _contentText.StringParams = [_contentText.StringParams[0], ""];
        } else
        {
            _contentText.StringParams = [_contentText.StringParams[0], "_"];
        }
    }

    public override Vector2 MainPosition {
        get => base.MainPosition;
        set
        {
            _placeholderText.Position-= base.MainPosition;
            _contentText.Position-=base.MainPosition;
            base.MainPosition = value;
            _inputFieldSprite.Position = value;
            _contentText.Position+=base.MainPosition;
            _placeholderText.Position+= base.MainPosition;
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
            _writingAnimation.Reset();
            _contentText.StringParams = [_contentText.StringParams[0], ""];
        }
    }

    public string Content
    {
        get
        {
            return (string)_contentText.StringParams[0];
        }
    }

    public void Dispose()
    {
        _placeholderText.Dispose();
        _contentText.Dispose();
    }
}