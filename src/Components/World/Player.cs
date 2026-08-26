

namespace project_republics.Components.World;

public class Player
{
    public class PlayerData
    {
        public string Username{get;set;}
        public int SectorX{get;set;}
        public int SectorY{get;set;}
        public int X{get;set;}
        public int Y{get;set;}
    }

    private PlayerData _data;

    public Player(PlayerData data)
    {
        _data = data;
    }
}