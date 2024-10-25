// using DevExpress.ExpressApp;
// using DevExpress.ExpressApp.Security;
// using DevExpress.Persistent.BaseImpl.EF;
//
// namespace OutlookInspired.Module.Features.Security{
//     public interface IMailMergeController{
//         
//     }
//     public class ProtectMailMergeActionItemsViewController:ViewController{
//         IEnumerable<T> SelectManyRecursive<T>( IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector){
//             foreach (var i in source){
//                 yield return i;
//                 var children = childrenSelector(i);
//                 if (children == null) continue;
//                 foreach (var child in SelectManyRecursive(children, childrenSelector))
//                     yield return child;
//             }
//         }
//
//
//         protected override void OnViewControllersActivated(){
//             base.OnViewControllersActivated();
//             foreach (var reportController in Frame.Controllers.Values.OfType<IReportController>()){
//                 var items = SelectManyRecursive(reportController.ReportAction.Items,item => item.Items);
//                 foreach (var item in items.Where(item => item.Data!=null)){
//                     item.Enabled[nameof(ProtectMailMergeActionItemsViewController)] = reportController.ReportAction.Controller.Frame.View.ObjectSpace
//                         .GetObjectsQuery<RichTextMailMergeData>()
//                         .Any(data => data.Name == (string)item.Data);
//
//                 }
//             }
//         }
//     
//     }
// }