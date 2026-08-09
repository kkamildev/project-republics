

using System.Collections.Generic;
using System.IO;

namespace project_republics.Utils.Input;

public class ContentData
{
    private readonly string _baseFontsPath = "fonts";
    private readonly string _baseTexturesPath = "textures";
    private readonly string _baseSoundsPath = "sounds";
    private readonly string _baseMusicPath = "sounds";

    public Dictionary<Fonts, string> FontsData{get;private set;}
    public Dictionary<Textures, string> TexturesData{get;private set;}
    public Dictionary<Sounds, string> SoundsData{get;private set;}
    public Dictionary<Music, string> MusicData{get;private set;}
    
    public ContentData()
    {
        // FONTS
        FontsData = new(){
            {Fonts.SMALLEST, Path.Join(_baseFontsPath, "smallest")},
            {Fonts.SMALLER, Path.Join(_baseFontsPath, "smaller")},
            {Fonts.SMALL, Path.Join(_baseFontsPath, "small")},
            {Fonts.BASE, Path.Join(_baseFontsPath, "base")},
            {Fonts.LARGE, Path.Join(_baseFontsPath, "large")},
            {Fonts.LARGER, Path.Join(_baseFontsPath, "larger")},
            {Fonts.LARGEST, Path.Join(_baseFontsPath, "largest")},
            {Fonts.HUGE, Path.Join(_baseFontsPath, "huge")}
        };
        // TEXTURES
        TexturesData = new()
        {
            {Textures.AUTHOR_LOGO, Path.Join(_baseTexturesPath, "UI", "studioLogo")},
            {Textures.GAME_LOGO, Path.Join(_baseTexturesPath, "UI", "gameLogo")},
            {Textures.BUTTON1, Path.Join(_baseTexturesPath, "UI", "button1")},
            {Textures.BUTTON2, Path.Join(_baseTexturesPath, "UI", "button2")},
            {Textures.BUTTON3, Path.Join(_baseTexturesPath, "UI", "button3")},
            {Textures.WORLD_LABEL, Path.Join(_baseTexturesPath, "UI", "worldLabel")},
            {Textures.INPUT_FIELD, Path.Join(_baseTexturesPath, "UI", "inputField")},
        };
        // SOUNDS
        SoundsData = new()
        {
            
        };
        // MUSIC
        MusicData = new()
        {
            
        };
    }

}