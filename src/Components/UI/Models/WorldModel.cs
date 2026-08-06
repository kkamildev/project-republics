
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using project_republics.Components.UI.Buttons;
using project_republics.Utils.Components.Sprites;
using project_republics.Utils.Components.Texts;
using project_republics.Utils.Exceptions;
using project_republics.Utils.Helpers;

namespace project_republics.Components.UI.Models;

public class WorldModel : BaseButton
{
    public class WorldData : IComparable<WorldData>
    {
        public string Name{get;set;}
        public int Mode{get;set;}
        public string RepublicName{get;set;}
        public string DirectoryPath{get;set;}
        public DateTime CreatedAt{get;set;}
        public DateTime LastPlayed{get;set;}
        public string[] FlagPixelRows{get;set;}
        public string GlobalID{get;set;}

        public int CompareTo(WorldData other)
        {
            if (other == null) return 1;

            int lastPlayedComparison = other.LastPlayed.CompareTo(LastPlayed);
            
            if (lastPlayedComparison != 0)
            {
                return lastPlayedComparison;
            }

            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }
    }

    public static readonly string[] Modes = ["NORMAL_MODE", "SANDBOX_MODE", "PEACEFUL_MODE", "HARDCODE_MODE"];
    
    private bool _choosen;
    private ButtonGroup _worldOptions;
    private WorldData _data;
    private Texture2D _flagTexture = new(MainGame.Graph.GraphicsDevice, 33, 33);
    private RawTextureSprite _flagSprite;
    private Sprite _worldBackground;
    private TextGroup _texts;
    public WorldModel(WorldData data, Action onClick, Action onPlay) : base(onClick)
    {
        _choosen = false;
        _data = data;
        _worldOptions = new([
            new Button(onPlay, new AlignedText(Utils.Input.Fonts.BASE, "PLAY_BUTTON", new Vector2(230, 72), 0.5f, 0.5f){Color = Color.DimGray}){ChangeColor = Color.White},
            new Button(() => {}, new AlignedText(Utils.Input.Fonts.BASE, "EDIT_BUTTON", new Vector2(430, 72), 0.5f, 0.5f){Color = Color.DimGray}){ChangeColor = Color.White},
            new Button(() => {}, new AlignedText(Utils.Input.Fonts.BASE, "DELETE_BUTTON", new Vector2(630, 72), 0.5f, 0.5f){Color = Color.DimGray}){ChangeColor = Color.DarkRed}
        ], true);
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

            _texts = new([
                new(Utils.Input.Fonts.SMALLER, "{0}", new Vector2(160, 15)){ StringParams = [data.Name] },
                new(Utils.Input.Fonts.SMALLER, "WORLD_PLAYED_DATE", new Vector2(160, 40)){ Color = Color.DimGray, StringParams = [data.LastPlayed.ToString("HH:mm yyyy-MM-dd")] },
                new(Utils.Input.Fonts.SMALLER, "{0}", new Vector2(460, 40)){ Color = Color.DimGray, StringParams = [data.DirectoryPath] },
                new(Utils.Input.Fonts.SMALLER, data.GlobalID != null ? "ONLINE_YES" : "ONLINE_NO", new Vector2(160, 70)),
                new(Utils.Input.Fonts.SMALLER, Modes[data.Mode], new Vector2(160, 100)){ StringParams = [data.RepublicName] }
            ]);

            if(data.GlobalID != null)
            {
                // TODO: add texts like connecting, count of players
            }
        } catch(Exception)
        {
            throw new WorldFormatException(data);
        }
    }

    public override void Draw()
    {
        _worldBackground.Draw();
        _flagSprite.Draw();
        if(_choosen)
        {
            _worldOptions.Draw();
        } else
        {
            _texts.Draw();
        }
    }

    public override void Update()
    {
        if(_data.GlobalID != null)
        {
            // TODO: add connecting system to online mode
        }
    }

    public override Vector2 MainPosition {
        get => base.MainPosition;
        set
        {
            _flagSprite.Position-= base.MainPosition;
            _texts.MainPosition = value;
            foreach(BaseButton button in _worldOptions.Buttons)
            {
                button.MainPosition-=base.MainPosition;
            }
            base.MainPosition = value;
            foreach(BaseButton button in _worldOptions.Buttons)
            {
                button.MainPosition+=base.MainPosition;
            }
            _flagSprite.Position+= base.MainPosition;
            _worldBackground.Position = base.MainPosition;
        }
    }

    public override bool Active {
        get => base.Active;
        set {
            base.Active = value;
            if(base.Active)
            {
                _worldBackground.Color = Color.DimGray;
            } else
            {
                _worldBackground.Color = Color.White;
            }
        }
    }

    public bool Choosen
    {
        get
        {
            return _choosen;
        }
        set
        {
            _choosen = value;
            if(_choosen)
            {
                _worldOptions.Active = true;
                _worldBackground.Color = Color.DimGray;
            } else
            {
                _worldOptions.Active = false;
                _worldBackground.Color = Color.White;
            }
        }
    }

    public WorldData Data
    {
        get
        {
            return _data;
        }
    }

    public override void Dispose()
    {
        _flagTexture.Dispose();
        _texts.Dispose();
        _worldOptions.Dispose();
    }
}