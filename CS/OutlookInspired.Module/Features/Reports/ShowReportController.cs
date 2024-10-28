using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.Base.ReportsV2;
using DevExpress.Persistent.BaseImpl.EF;

namespace OutlookInspired.Module.Features.Reports{
    public class ShowReportController:ViewController{
        private string ReportContainerHandle(SingleChoiceAction reportAction,Type reportDataType)
            => ReportDataProvider.GetReportStorage(reportAction.Application.ServiceProvider)
                .GetReportContainerHandle(View.ObjectSpace.GetObjectsQuery<ReportDataV2>().FirstOrDefault(data => data.DataTypeName == reportDataType.FullName &&
                    data.DisplayName == (string)reportAction.SelectedItem.Data));

        public void ShowReportPreview(SingleChoiceAction reportAction,CriteriaOperator criteria=null,string parameterName=null){
            HandleReportParameter(reportAction,parameterName, criteria);
            Frame.GetController<ReportServiceController>()
                .ShowPreview(ReportContainerHandle(reportAction, View.ObjectTypeInfo.Type));
        }

        void HandleReportParameter(SingleChoiceAction reportAction, string parameterName, CriteriaOperator criteria){
            var reportsDataSourceHelper = reportAction.Application.Modules.FindModule<ReportsModuleV2>().ReportsDataSourceHelper;
            reportsDataSourceHelper.BeforeShowPreview += ReportsDataSourceHelperOnBeforeShowPreview;
            void ReportsDataSourceHelperOnBeforeShowPreview(object sender, BeforeShowPreviewEventArgs e){
                reportsDataSourceHelper.BeforeShowPreview -= ReportsDataSourceHelperOnBeforeShowPreview;
                e.Report.RequestParameters = false;
                var reportParameter = e.Report.Parameters[parameterName];
                if (reportParameter == null){
                    ((ISupportCriteria)e.Report.DataSource).Criteria = criteria;
                    return;
                }
                reportParameter.Visible = false;
                reportParameter.Value = View.ObjectSpace
                    .GetKeyValue(View.SelectedObjects.Cast<object>().First());
            }
        }

        }
}