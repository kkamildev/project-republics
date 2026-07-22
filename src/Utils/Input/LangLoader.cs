

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace project_republics.Utils.Input;


public class LangLoader
{

    public class LangModel
    {
        public string Author{get;set;}
        public string DisplayName{get;set;}
        public Dictionary<string, string> Translations{get;set;}
    }

    private readonly string _basePath = Path.Join("Content", "lang");
    private LangModel _langModel;

    public LangLoader()
    {
        LoadLang(MainGame.Storage.Settings.LangName, (error) => throw new Exception($"{MainGame.Storage.Settings.LangName} file not found"));
    }

    public void LoadLang(string name, Action<string> onError)
    {
        try
        {
            string filePath = Path.Join(_basePath, $"{name}.json");
            if(!File.Exists(filePath))
            {
                throw new Exception("Language file not found");
            }
            string fileContent = File.ReadAllText(filePath);
            _langModel = JsonSerializer.Deserialize<LangModel>(fileContent);
            MainGame.Storage.Settings.LangName = name;
            MainGame.Storage.SaveSettings();
        }catch(JsonException)
        {
            onError.Invoke("Lang file is corrupted");
        } catch(Exception e)
        {
            onError.Invoke(e.Message);
        }
    }

    public Dictionary<string, string> Translations
    {
        get
        {
            return _langModel.Translations;
        }
    }


}