using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System;

class Program
{
    static void Main()
    {
        string taskbarPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            @"Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar"
        );

        dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
        dynamic folder = shell.NameSpace(taskbarPath);
        dynamic item = folder.ParseName("Microsoft Edge.lnk");

        if (item != null)
        {
            foreach (dynamic verb in item.Verbs())
                if (((string)verb.Name).Replace("&", "").ToLower().Contains("unpin from taskbar"))
                    verb.DoIt();
        }

	var uninstallKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall", true);
	if (uninstallKey != null)
    		uninstallKey.DeleteSubKey("Microsoft Edge", false);
        string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        foreach (var p in Process.GetProcessesByName("msedge")) p.Kill();
        foreach (var p in Process.GetProcessesByName("SearchHost")) p.Kill();
        foreach (var p in Process.GetProcessesByName("msedgewebview2")) p.Kill();
        foreach (var p in Process.GetProcessesByName("MicrosoftEdgeUpdate")) p.Kill();

        if (Directory.Exists(@"C:\Program Files (x86)\Microsoft"))
            Directory.Delete(@"C:\Program Files (x86)\Microsoft", true);

        if (Directory.Exists(Path.Combine(userProfile, "AppData", "Local", "Microsoft", "Edge")))
            Directory.Delete(Path.Combine(userProfile, "AppData", "Local", "Microsoft", "Edge"), true);

        if (File.Exists(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Microsoft Edge.lnk"))
            File.Delete(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Microsoft Edge.lnk");

        if (File.Exists(@"C:\Users\Public\Desktop\Microsoft Edge.lnk"))
            File.Delete(@"C:\Users\Public\Desktop\Microsoft Edge.lnk");
    }
}
