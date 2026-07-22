

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
            {Fonts.BASE, Path.Join(_baseFontsPath, "base")}
        };
        // TEXTURES
        TexturesData = new()
        {
            
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