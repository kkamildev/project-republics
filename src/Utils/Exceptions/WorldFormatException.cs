

using System;
using System.Linq;
using project_republics.Components.UI.Models;

namespace project_republics.Utils.Exceptions;

public class WorldFormatException : Exception
{
    public WorldFormatException(WorldModel.WorldData worldData) : base("Exception during parsing world metadata->" + GetInfo(worldData))
    {
        
    }

    private static string GetInfo(WorldModel.WorldData worldData)
    {
        return $@"Name: {worldData.Name}, CreatedAt: {worldData.CreatedAt:yyyy-MM-dd},
         LastPlayed:{worldData.LastPlayed:yyyy-MM-dd}, Mode:{((WorldModel.Modes.Length <= worldData.Mode || worldData.Mode < 0) ? "Unknown" : WorldModel.Modes[worldData.Mode])},
            RepublicName:{worldData.RepublicName}, GlobalID:{worldData.GlobalID}
          Flag info:\n{string.Join("\n", worldData.FlagPixelRows)}";
    }
}