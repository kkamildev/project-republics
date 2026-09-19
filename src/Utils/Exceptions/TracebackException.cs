

using System;
using System.Diagnostics;
using System.IO;
using project_republics.Utils.Helpers;

namespace project_republics.Utils.Exceptions;

public class TracebackException : Exception
{
    public TracebackException(string message) : base()
    {
        if(MainGame.Storage.Settings.ErrorLogging)
        {
            StackTrace trace = new(true);
            string fullErrorContent = "";
            string errorHeader = $"{DateTime.Now:[HH:mm:ss]}: {message}\n";
            fullErrorContent += errorHeader;
            string machineParams = $@"----Machine params----\n Operating System:
             {SystemHelper.GetOSName()}\n CPU: {SystemHelper.GetCPUName()}\n RAM: {Math.Round(SystemHelper.GetTotalRAM() / Math.Pow(1024, 3), 2)}GB\n GPU: {SystemHelper.GetGPUName()}\n";
            fullErrorContent += machineParams;
            fullErrorContent+= "\nStack Trace:\n";
            foreach (StackFrame frame in trace.GetFrames())
            {
                fullErrorContent+= $"Method: {frame.GetMethod().Name} in {frame.GetFileName()}:{frame.GetFileLineNumber()}\n";
            }

            fullErrorContent+="\n";
            MainGame.Storage.SaveErrorLog(fullErrorContent);
        }
    }

    public TracebackException() : this("Unknown Error appeared, check Error Stack to find out more")
    {
        
    }
}