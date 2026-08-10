
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.UI;
using project_republics.Utils.Helpers;

namespace project_republics.Components.UI.Models;

public class FlagModel : UIBase, IDisposable
{
    private Color[] _colors;
    private readonly Texture2D _rawTexture;
    private RawTextureSprite _sprite;

    public Color DefaultColor{get;set;}
    public FlagModel(string[] colorRows, Vector2 position)
    {   
        try
        {
            int sideLength = 33;
            _rawTexture = new(MainGame.Graph.GraphicsDevice, sideLength, sideLength);

            _colors = new Color[sideLength * sideLength];
            string[] row;
            for(int i = 0;i<sideLength;i++)
            {
                row = colorRows[i].Split(",");
                for(int j = 0;j<sideLength;j++)
                {
                    try
                    {
                        _colors[sideLength * i + j] = ColorHelper.FromHex(row[j]);
                    } catch (Exception)
                    {
                        _colors[sideLength * i + j] = DefaultColor;
                    }
                }
            }
        } catch(Exception)
        {
            _rawTexture = new(MainGame.Graph.GraphicsDevice, 33, 33);
            _colors = new Color[33 * 33];
            for(int j = 0;j<33*33;j++)
            {
                _colors[j] = Color.Black;
            }
        }
        _rawTexture.SetData(_colors);
        _sprite = new(_rawTexture, Vector2.Zero);
        MainPosition = position;
    }

    public void SetColor(Color color, int x, int y)
    {
        _colors[y * _rawTexture.Height + x] = color;
        _rawTexture.SetData(_colors);
    }

    public string[] ToHexRows()
    {
        string[] row = new string[_rawTexture.Width];
        string[] rows = new string[_rawTexture.Height];
        for(int i = 0;i<_rawTexture.Height;i++)
        {
            for(int j = 0;j<_rawTexture.Width;j++)
            {
                row[j] = ColorHelper.ToHex(_colors[i * _rawTexture.Width + j]);
            }
            rows[i] = string.Join(",", row);
        }
        return rows;
    }

    public override void Draw()
    {
        _sprite.Draw();
    }

    public override Vector2 MainPosition {
        get => base.MainPosition;
        set
        {
            base.MainPosition = value;
            _sprite.Position = base.MainPosition;
        }
    }

    public RawTextureSprite Sprite
    {
        get
        {
            return _sprite;
        }
    }

    public void Dispose()
    {
        _rawTexture.Dispose();
    }
}