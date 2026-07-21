

using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace project_republics.Utils.Input;


public class ContentLoader
{
    private Dictionary<Textures, Texture2D> _textures;
    private Dictionary<Fonts, SpriteFont> _fonts;
    private Dictionary<Sounds, SoundEffect> _sounds;
    private Dictionary<Music, Song> _music;
    private ContentManager _content;

    private ContentData _data;

    public ContentLoader(ContentManager content)
    {
        _content = content;
        _data = new();
        _textures = [];
        _fonts = [];
        _sounds = [];
        _music = [];
    }

    public void LoadTexture(Textures texture, string path)
    {
        _textures.Add(texture, _content.Load<Texture2D>(path));
    }
    public void LoadFont(Fonts font, string path)
    {
        _fonts.Add(font, _content.Load<SpriteFont>(path));
    }
    public void LoadSound(Sounds sound, string path)
    {
        _sounds.Add(sound, _content.Load<SoundEffect>(path));
    }
    public void LoadMusic(Music music, string path)
    {
        _music.Add(music, _content.Load<Song>(path));
    }

    public void LoadAllTextures()
    {
        foreach(Textures texture in _data.TexturesData.Keys)
        {
            LoadTexture(texture, _data.TexturesData[texture]);
        }
    }
    public void LoadAllFonts()
    {
        foreach(Fonts font in _data.FontsData.Keys)
        {
            LoadFont(font, _data.FontsData[font]);
        }
    }
    public void LoadAllSounds()
    {
        foreach(Sounds sound in _data.SoundsData.Keys)
        {
            LoadSound(sound, _data.SoundsData[sound]);
        }
    }
    public void LoadAllMusic()
    {
        foreach(Music music in _data.MusicData.Keys)
        {
            LoadMusic(music, _data.MusicData[music]);
        }
    }

    public Dictionary<Textures, Texture2D> Textures
    {
        get
        {
            return _textures;
        }
    }
    public Dictionary<Fonts, SpriteFont> Fonts
    {
        get
        {
            return _fonts;
        }
    }
    public Dictionary<Sounds, SoundEffect> Sounds
    {
        get
        {
            return _sounds;
        }
    }
    public Dictionary<Music, Song> Music
    {
        get
        {
            return _music;
        }
    }
}