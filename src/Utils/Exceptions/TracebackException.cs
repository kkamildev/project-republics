

using System;
using System.Diagnostics;
using System.IO;

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
            string machineParams = $"----Machine params----\n Operating System: {GetOSName()}\n CPU: {GetCPUName()}\n RAM: {Math.Round(GetTotalRAM() / Math.Pow(1024, 3), 2)}GB\n GPU: {GetGPUName()}\n";
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

    public static string GetCPUName()
    {
        if (OperatingSystem.IsWindows())
        {
            using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0");
            return key?.GetValue("ProcessorNameString")?.ToString() ?? "Unknown CPU";
        }
        else if (OperatingSystem.IsLinux())
        {
            foreach (var line in File.ReadAllLines("/proc/cpuinfo"))
            {
                if (line.StartsWith("model name"))
                    return line.Split(':')[1].Trim();
            }
            return "Unknown CPU";
        }
        else if (OperatingSystem.IsMacOS())
        {
            return Exec("sysctl -n machdep.cpu.brand_string");
        }

        return "Unknown CPU";
    }

    public static string Exec(string cmd)
    {
        Process process = new()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{cmd}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();
        return process.StandardOutput.ReadToEnd().Trim();
    }

    public static ulong GetTotalRAM()
    {
        if (OperatingSystem.IsWindows())
        {
            var output = Exec("wmic computersystem get TotalPhysicalMemory");
            if (ulong.TryParse(output.Replace("TotalPhysicalMemory", "").Trim(), out ulong bytes))
                return bytes;
        }
        else if (OperatingSystem.IsLinux())
        {
            foreach (var line in File.ReadAllLines("/proc/meminfo"))
            {
                if (line.StartsWith("MemTotal"))
                {
                    var kb = ulong.Parse(line.Split(':')[1].Trim().Split(' ')[0]);
                    return kb * 1024;
                }
            }
        }
        else if (OperatingSystem.IsMacOS())
        {
            string output = Exec("sysctl -n hw.memsize");
            return ulong.Parse(output);
        }

        return 0;
    }
    public static string GetGPUName()
    {
        if (OperatingSystem.IsWindows())
        {
            return Exec("wmic path win32_VideoController get name");
        }
        else if (OperatingSystem.IsLinux())
        {
            return Exec("lspci | grep -i vga");
        }
        else if (OperatingSystem.IsMacOS())
        {
            return Exec("system_profiler SPDisplaysDataType | grep 'Chipset Model'");
        }

        return "Unknown GPU";
    }

    public static string GetOSName()
    {
        if (OperatingSystem.IsWindows())
        {
            string output = Exec("wmic os get Caption");
            return output.Replace("Caption", "").Trim();
        }
        else if (OperatingSystem.IsLinux())
        {
            if (File.Exists("/etc/os-release"))
            {
                foreach (var line in File.ReadAllLines("/etc/os-release"))
                {
                    if (line.StartsWith("PRETTY_NAME="))
                    {
                        return line.Split('=')[1].Trim().Trim('"');
                    }
                }
            }
            return "Linux (Unknown Distro)";
        }
        else if (OperatingSystem.IsMacOS())
        {
            return Exec("sw_vers -productName") + " " + Exec("sw_vers -productVersion");
        }

        return "Unknown OS";
    }
}