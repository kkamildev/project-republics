

namespace project_republics.Components.World.Sections;

public class SectorData
{
    public int Cash{get;set;}
    public uint Population{get;set;}
    public string Name{get;set;}
    public string[] ChunksData{get;set;}


    public static SectorData Parse(string data)
    {
        // TODO: create a parser
        return new SectorData();
    }
}