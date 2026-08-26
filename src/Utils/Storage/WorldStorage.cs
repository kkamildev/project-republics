

using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using project_republics.Components.UI.Models;
using project_republics.Components.World;
using project_republics.Utils.Components.Network;

namespace project_republics.Utils.Storage;

public class WorldStorage
{
    private readonly string _worldPath;
    public WorldStorage(WorldModel.WorldData data)
    {
        _worldPath = Path.Join(MainGame.Storage.AppPath, "worlds", data.DirectoryPath);
    }

    public async Task<Player> LoadPlayer(Account account)
    {
        bool loadDefault = true;
        if(account.Data == null || !account.Data.Success)
        {
            await MainGame.Storage.Account.ConnectToAccount();
            if(account.Data.Success)
            {
                loadDefault = false;
            }

        }
        Player.PlayerData playerData = await GetPlayerInfo("MASTER");
        if(!loadDefault)
        {
            playerData.Username = account.Data.AccountData["username"];
        }

        return new Player(playerData);
    }

    private async Task<Player.PlayerData> GetPlayerInfo(string id)
    {
        string fileToFind = Path.Join(_worldPath, "players", $"{id}.json");
        if(!Directory.Exists(Path.Join(_worldPath, "players"))) Directory.CreateDirectory(Path.Join(_worldPath, "players"));
        try
        {
            if(!File.Exists(fileToFind))
            {
                throw new Exception("File not found");
            }
            string rawData = File.ReadAllText(fileToFind);
            return JsonSerializer.Deserialize<Player.PlayerData>(rawData);
        } catch(Exception)
        {
            Player.PlayerData defaultPlayerData = new()
            {
                Username = "Guest",
                SectorX = MainGame.Random.Next(0, WorldContainer.MAP_WIDTH),
                SectorY = MainGame.Random.Next(0, WorldContainer.MAP_HEIGHT),
                X = MainGame.Random.Next(0, WorldContainer.SECTOR_WIDTH),
                Y = MainGame.Random.Next(0, WorldContainer.SECTOR_HEIGHT)
            };

            File.WriteAllText(fileToFind, JsonSerializer.Serialize(defaultPlayerData, MainGame.Storage.JsonSerializerOptions));
            return defaultPlayerData;
        }
    }
}