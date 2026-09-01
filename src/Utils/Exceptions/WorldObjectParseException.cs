
namespace project_republics.Utils.Exceptions;

public class WorldObjectParseException : TracebackException
{
    public WorldObjectParseException(string objectName, string argsData) : base($"Error during parsing: {objectName} failed with args --> {argsData}")
    {
        
    }
}