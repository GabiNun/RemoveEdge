using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        string folderPath = @"C:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe";
        Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, "MicrosoftEdge.exe");
        using (File.Create(filePath)) { }

        string basePath = @"C:\Program Files (x86)\Microsoft\Edge\Application";
        foreach (string dir in Directory.GetDirectories(basePath))
        {
            string path = Path.Combine(dir, "Installer", "setup.exe");
            Process.Start(path, "--uninstall --system-level --force-uninstall --delete-profile");
            break;
        }
    }
}