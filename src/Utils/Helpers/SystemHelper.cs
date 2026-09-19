
using System;
using System.Diagnostics;
using System.IO;

namespace project_republics.Utils.Helpers;

public static class SystemHelper
{
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
    public static long GetAppRamUsage()
    {
        try
        {
            using (Process p = Process.GetCurrentProcess())
            {
                return p.WorkingSet64;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("RAM ERROR: " + ex.Message);
            return -1;
        }
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
}