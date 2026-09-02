
namespace project_republics.Utils.Exceptions;

public class NotSupportedWorldObjectException : TracebackException
{
    public NotSupportedWorldObjectException(string objectData) : base($"Not supported world object, data-> {objectData}")
    {
        
    }
}