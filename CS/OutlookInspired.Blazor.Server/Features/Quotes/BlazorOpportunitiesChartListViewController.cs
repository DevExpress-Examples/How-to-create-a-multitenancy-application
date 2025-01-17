﻿using System.Drawing;
using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.Charts;
using OutlookInspired.Module.BusinessObjects;


namespace OutlookInspired.Blazor.Server.Features.Quotes {
    public class BlazorOpportunitiesChartListViewController : ObjectViewController<ListView, Opportunity> {
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if(View.Editor is DxChartPieListEditor editor) {
                var chartModel = editor.Control;
                chartModel.ArgumentField = item => ((Opportunity)item).Stage.ToString();
                chartModel.ValueField = item => ((Opportunity)item).Value;
                chartModel.SummaryMethod = items => items.Sum();
                chartModel.CustomizeSeriesPoint = e
                    => e.PointAppearance.Color = ColorTranslator.FromHtml(e.Point.DataItems.Cast<Opportunity>().First().Stage.Color());
                chartModel.Stick = true;
                chartModel.Style = "margin-top:100px";
            }
        }
    }
}