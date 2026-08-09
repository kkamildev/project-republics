
using Microsoft.Xna.Framework;
using project_republics.Utils.Input;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace project_republics.Utils.Components.Texts;

public class Text : IDisposable
{
    protected Fonts _font;
    protected string _cache;
    protected string _translationKey;
    protected Vector2 _position;
    private object[] _stringParams;
    public Color Color{get;set;}
    public float LayerDepth{get;set;}
    public float Scale{get;set;}

    public Text(Fonts font, string translationKey, Vector2 position)
    {
        _font = font;
        _stringParams = [];
        TranslationKey = translationKey;
        Position = position;
        LayerDepth = 1f;
        Scale = 1f;
        Color = Color.White;
        MainGame.LL.OnChangeLanguage+=UpdateText;
    }

    private void UpdateText()
    {
        if(MainGame.LL.Translations.TryGetValue(_translationKey, out string value))
        {
            if(_stringParams.Length == 0)
            {
                _cache = value;
            } else
            {
                _cache = string.Format(value, StringParams);
            }
        } else
        {
            if(_stringParams.Length == 0)
            {
                _cache = _translationKey;
            } else
            {
                _cache = string.Format(_translationKey, StringParams);
            }
        }
    }

    public virtual void Draw()
    {
        MainGame.Batch.DrawString(MainGame.CL.Fonts[_font], Content, Position, Color, 0f, Vector2.Zero, Scale, SpriteEffects.None, LayerDepth);
    }

    public void Dispose()
    {
        MainGame.LL.OnChangeLanguage-=UpdateText;
    }

    public virtual object[] StringParams
    {
        get
        {
            return _stringParams;
        }
        set
        {
            _stringParams = value;
            UpdateText();
        }
    }

    public string Content
    {
        get
        {
            return _cache;
        }
    }
    public virtual string TranslationKey
    {
        get
        {
            return _translationKey;
        }
        set
        {
            _translationKey = value;
            UpdateText();

        }
    }

    public Vector2 Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
        }
    }
}