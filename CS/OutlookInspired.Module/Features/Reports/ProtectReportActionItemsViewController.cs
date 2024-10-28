using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.EF;

namespace OutlookInspired.Module.Features.Reports{
    public interface IReportController{
        SingleChoiceAction ReportAction{ get; }
    }

    [Obsolete]
    public class ProtectReportActionItemsViewController:ViewController{
        protected override void OnViewControllersActivated(){
            base.OnViewControllersActivated();
            foreach (var singleChoiceAction in Frame.Controllers.Values.OfType<IReportController>().Select(controller => controller.ReportAction)
                         .Where(action => action.Active)){
                var items = SelectManyRecursive(singleChoiceAction.Items,item => item.Items);
                foreach (var item in items.Where(item => item.Data!=null)){
                    var reportDataV2 = ObjectSpace.GetObjectsQuery<ReportDataV2>().FirstOrDefault(v2 => v2.DisplayName==(string)item.Data);
                    if (reportDataV2==null) continue;
                    var isGranted = ((IRequestSecurity)Application.Security).IsGranted(new PermissionRequest(ObjectSpace,
                        reportDataV2.GetType(), SecurityOperations.Read, reportDataV2));
                    item.Active["ReportProtection"] = isGranted;
                }
            }
        }
        
        IEnumerable<T> SelectManyRecursive<T>( IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector){
            foreach (var i in source){
                yield return i;
                var children = childrenSelector(i);
                if (children == null) continue;
                foreach (var child in SelectManyRecursive(children, childrenSelector))
                    yield return child;
            }
        }
    }
}