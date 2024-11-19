using System;
using System.IO;
using System.Diagnostics;
using DevExpress.Persistent.Base;

namespace OutlookInspired.Win;

public class Initialization {
    public static void KillServerProcess() {
        Process existProc = Process.GetProcessesByName("OutlookInspired.MiddleTier").FirstOrDefault();
        if (existProc != null) {
            existProc.Kill();
            existProc.WaitForExit();
        }
    }
    public static Process RunSecurityServer(IEnumerable<string> args) {
        KillServerProcess();
        var proc = new Process();
        proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        if (args.Any()) {
            proc.StartInfo.Arguments = string.Concat(proc.StartInfo.Arguments, " ", string.Join(" ", args.Select(a => "\"" + a + "\"")));
        }
        string buildConfiguration = Path.GetFileName(Path.GetDirectoryName(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, @"..\"))));
        string middleTierDirectory = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, @$"..\..\..\..\OutlookInspired.MiddleTier");
        string workingDirectory = Path.Combine(middleTierDirectory, @$"Bin\{buildConfiguration}\net8.0");
        string fileName = Path.Combine(workingDirectory, "OutlookInspired.MiddleTier.exe");
        if (!File.Exists(fileName)) {
            throw new FileNotFoundException("Could not start a server process. The OutlookInspired.MiddleTier.exe file is missing.\r\nPlease ensure that you have built the OutlookInspired.MiddleTier project.");
        }
        proc.StartInfo.FileName = fileName;
        proc.StartInfo.WorkingDirectory = middleTierDirectory;
        proc.Start();
        return proc;
    }
}
