using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using OutlookInspired.Module;

namespace OutlookInspired.Win;

[ToolboxItemFilter("Xaf.Platform.Win")]
public sealed class OutlookInspiredWinModule : ModuleBase {
    private void Application_CreateCustomUserModelDifferenceStore(object sender, CreateCustomModelDifferenceStoreEventArgs e) {
        e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), false, "Win");
        e.Handled = true;
    }
    public OutlookInspiredWinModule() {
        FormattingProvider.UseMaskSettings = true;
        RequiredModuleTypes.Add(typeof(OutlookInspiredModule));
    }
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) 
        =>[new(objectSpace, versionFromDB)];


    public override void Setup(XafApplication application) {
        base.Setup(application);
        application.CreateCustomUserModelDifferenceStore += Application_CreateCustomUserModelDifferenceStore;
    }
}
