﻿using System.Configuration;
using System.Diagnostics;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.XtraEditors;

namespace OutlookInspired.Win;

static class Program {
    [STAThread]
    public static int Main(string[] args){
        
        FrameworkSettings.DefaultSettingsCompatibilityMode = FrameworkSettingsCompatibilityMode.Latest;
#if EASYTEST
        DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
        WindowsFormsSettings.LoadApplicationSettings();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
		DevExpress.Utils.ToolTipController.DefaultController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
        if(Tracing.GetFileLocationFromSettings() == FileLocation.CurrentUserApplicationDataFolder) {
            Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
        }
        Tracing.Initialize();
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        foreach (var process in Process.GetProcessesByName("OutlookInspired.MiddleTier")){
            process.Kill();
            process.WaitForExit();    
        }
        var proc = new Process();
        var middleTierDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase!,
            @"..\..\..\..\OutlookInspired.MiddleTier\"));
        proc.StartInfo.FileName = "dotnet";
        proc.StartInfo.Arguments="run --no-build dotnet --launch-profile \"OutlookInspired.MiddleTier\"";
        proc.StartInfo.WorkingDirectory = middleTierDir;
        proc.Start();

        var winApplication = ApplicationBuilder.BuildApplication(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        try {
            
            winApplication.Setup();
            winApplication.Start();
        }
        catch(Exception e) {
            winApplication.StopSplash();
            winApplication.HandleException(e);
        }
        return 0;
    }
}
