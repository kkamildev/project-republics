

using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Input;
using project_republics.Utils.Input;

namespace project_republics.Utils.Storage;

public class StorageLoader
{
    private readonly string _appPath;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public SettingsModel Settings{get; private set;}
    
    public StorageLoader(string appName)
    {
        _jsonSerializerOptions = new JsonSerializerOptions(){WriteIndented = true};
        _appPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName);

        CreateFileTree();

    }

    private void CreateFileTree()
    {
        Directory.CreateDirectory(_appPath);
    }

    public void SaveSettings()
    {
        try
        {
            string jsonText = JsonSerializer.Serialize(Settings, _jsonSerializerOptions);
            File.WriteAllText(Path.Join(_appPath, "settings.json"), jsonText);
        } catch(Exception)
        {
            
        }
    }
    public void LoadSettings()
    {
        try
        {
            string fileContent = File.ReadAllText(Path.Join(_appPath, "settings.json"));
            Settings = JsonSerializer.Deserialize<SettingsModel>(fileContent);
        } catch (Exception)
        {
            // loading default settings
            Settings = new();
        }
    }
}
