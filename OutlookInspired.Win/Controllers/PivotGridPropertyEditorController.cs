﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.PivotGrid.Win;
using DevExpress.Utils;
using OutlookInspired.Win.Extensions;

namespace OutlookInspired.Win.Controllers{
    public class PivotGridPropertyEditorController:ViewController<ListView>{
        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            if (View.Editor is PivotGridListEditor pivotGridListEditor){
                var pivotGridControl = pivotGridListEditor.PivotGridControl;
                var repositoryItems = pivotGridControl.AddRepositoryItems(View);
                pivotGridControl.CustomCellEdit += (_, e) => {
                    if (repositoryItems.TryGetValue(e.DataField, out var item)){
                        e.RepositoryItem = item;
                    }
                };
                pivotGridControl.CustomCellValue += (_, e) => {
                    if (repositoryItems.TryGetValue(e.DataField, out var _)){
                        e.Value = Convert.ToDecimal(e.Value) * 100;
                    }
                };
                pivotGridControl.CustomDrawCell += (_, e) => {
                    if (repositoryItems.TryGetValue(e.DataField, out var item)){
                        e.Appearance = item.Appearance;
                    }
                };    
            }
        }
    }
}