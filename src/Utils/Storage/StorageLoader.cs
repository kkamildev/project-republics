

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using project_republics.Components.UI.Models;
using project_republics.Utils.Helpers;

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
        Directory.CreateDirectory(Path.Join(_appPath, "worlds"));
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
            if(!Settings.ValidateModel(new SettingsModel()))
            {
                throw new Exception();
            }
        } catch (Exception)
        {
            // loading default settings
            Settings = new();
        }
    }

    public List<WorldModel.WorldData> SearchForWorlds()
    {
        string dir = Path.Join(_appPath, "worlds");
        if(!Directory.Exists(dir))
        {
            return [];
        }
        List<WorldModel.WorldData> data = [];
        string[] directories = Directory.GetDirectories(dir);
        foreach (string directory in directories)
        {
            try
            {
                if(File.Exists(Path.Join(directory, "metadata.json")))
                {
                    string fileContent = File.ReadAllText(Path.Join(directory, "metadata.json"));
                    WorldModel.WorldData record = JsonSerializer.Deserialize<WorldModel.WorldData>(fileContent);
                    record.DirectoryPath = $"/{Path.GetFileName(directory)}".Truncate(20);
                    data.Add(record);
                }
            }catch (Exception)
            {
                continue;
            }
        }
        return data;
        
    }

    public void SaveErrorLog(string errorContent)
    {
        Directory.CreateDirectory(Path.Join(_appPath, "crashLogs"));
        string logPath = Path.Join(_appPath, "crashLogs", $"{DateTime.Now:yyyy-MM-dd}.txt");
        if(File.Exists(logPath))
        {
            File.AppendAllText(logPath, errorContent);
        } else
        {
            File.WriteAllText(logPath, errorContent);
        }
    }
}
