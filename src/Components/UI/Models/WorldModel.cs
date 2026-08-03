
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Components.UI;
using project_republics.Utils.Exceptions;
using project_republics.Utils.Helpers;

namespace project_republics.Components.UI.Models;

public class WorldModel : UIBase, IDisposable
{
    public class WorldData
    {
        public string Name{set;get;}
        public DateTime CreatedAt{set;get;}
        public DateTime LastPlayed{set;get;}
        public string[] FlagPixelRows{get;set;}
        
    }
    private Texture2D _flagTexture = new(MainGame.Graph.GraphicsDevice, 33, 33);
    private RawTextureSprite _flagSprite;
    private Sprite _worldBackground;
    private Text _titleText, _datesText;
    public WorldModel(WorldData data)
    {
        try
        {
            // creating flag texture
            Color[] colors = new Color[33 * 33];
            string[] row;
            for(int i = 0;i<33;i++)
            {
                row = data.FlagPixelRows[i].Split(",");
                for(int j = 0;j<33;j++)
                {
                    colors[33 * i + j] = ColorHelper.FromHex(row[j]);
                }
            }
            _flagTexture.SetData(colors);
            _worldBackground = new(Utils.Input.Textures.WORLD_LABEL, Vector2.Zero){Scale = 3f};
            _flagSprite = new RawTextureSprite(_flagTexture, new Vector2(7 * 3)){Scale = 3f};
            _titleText = new(Utils.Input.Fonts.SMALLER, data.Name, new Vector2(160, 15));
            _datesText = new (Utils.Input.Fonts.SMALLER, "WORLD_PLAYED_DATE", new Vector2(160, 40))
            {
                Color = Color.DimGray,
                StringParams = [data.LastPlayed.ToString("HH:mm yyyy-MM-dd")]
            };
        } catch(Exception)
        {
            throw new WorldFormatException(data);
        }
    }

    public override void Draw()
    {
        _worldBackground.Draw();
        _flagSprite.Draw();
        _titleText.Draw();
        _datesText.Draw();
    }

    public override Vector2 MainPosition {
        get => base.MainPosition;
        set
        {
            _flagSprite.Position-= base.MainPosition;
            _titleText.Position -= base.MainPosition;
            _datesText.Position -= base.MainPosition;
            base.MainPosition = value;
            _flagSprite.Position+= base.MainPosition;
            _datesText.Position += base.MainPosition;
            _titleText.Position += base.MainPosition;
            _worldBackground.Position = base.MainPosition;
        }
    }

    public void Dispose()
    {
        _flagTexture.Dispose();
        _datesText.Dispose();
    }
}