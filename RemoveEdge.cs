using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        string folderPath = @"C:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe";
        Directory.CreateDirectory(folderPath);
        using (File.Create(Path.Combine(folderPath, "MicrosoftEdge.exe"))) { }

        foreach (string dir in Directory.GetDirectories(@"C:\Program Files (x86)\Microsoft\Edge\Application")) 
        {
            Process.Start(Path.Combine(dir, "Installer", "setup.exe"), "--uninstall --system-level --force-uninstall --delete-profile");
            break;
        }
    }
}
