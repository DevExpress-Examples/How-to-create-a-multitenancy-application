#region Copyright (c) 2000-2023 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{                                                                   }
{                                                                   }
{       Copyright (c) 2000-2023 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
#endregion Copyright (c) 2000-2023 Developer Express Inc.

using System.ComponentModel;
using System.Reflection;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base.ReportsV2;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Resources.Reports {
	public class ProductSalesSummary : XtraReport {
		#region Designer generated code
		private static readonly PropertyInfo ViewDataSourceObjectSpaceProperty;

		static ProductSalesSummary() => ViewDataSourceObjectSpaceProperty = typeof(ViewDataSource).GetProperty("ObjectSpace",
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

		private IObjectSpace ViewDataSourceObjectSpace => ViewDataSourceObjectSpaceProperty!.GetValue(bindingSource1) as IObjectSpace;

		private TopMarginBand topMarginBand1;
		private DetailBand detailBand1;
		private ViewDataSource bindingSource1;
		private ViewDataSource productsSource1;
		private ReportHeaderBand ReportHeader;
		private XRPictureBox xrPictureBox2;
		private XRPageInfo xrPageInfo2;
		private XRPageInfo xrPageInfo1;
		private XRTable xrTable3;
		private XRTableRow xrTableRow8;
		private XRTableCell xrTableCell15;
		private XRTableRow xrTableRow7;
		private XRTableCell xrTableCell7;
		private XRTableCell xrTableCell14;
		private XRTableRow xrTableRow3;
		private XRTableCell xrTableCell8;
		private XRTableCell xrTableCell9;
		private XRPictureBox xrPictureBox4;
		private XRTable xrTable2;
		private XRTableRow xrTableRow2;
		private XRTableCell xrTableCell4;
		private XRTableCell xrTableCell5;
		private XRTable xrTable1;
		private XRTableRow xrTableRow1;
		private XRTableCell xrTableCell1;
		private XRTableCell xrTableCell2;
		private ReportFooterBand ReportFooter;
		private XRChart xrChart1;
		private XRTable xrTable8;
		private XRTableRow xrTableRow12;
		private XRTableCell xrTableCell18;
		private XRTableRow xrTableRow13;
		private XRTableCell xrTableCell19;
		private XRTableRow xrTableRow14;
		private XRTableCell xrTableCell20;
		private XRTableRow xrTableRow15;
		private XRTableCell xrTableCell21;
		private XRTableRow xrTableRow16;
		private XRTableCell xrTableCell22;
		private XRTableRow xrTableRow17;
		private XRTableCell xrTableCell23;
        private DevExpress.XtraReports.Parameters.Parameter Product;
        private DevExpress.XtraReports.Parameters.Parameter OrderDate;
        private BottomMarginBand bottomMarginBand1;
		public ProductSalesSummary() {
			InitializeComponent();
			InitializeMultiValueDateParameter();
		}
		
		private void InitializeMultiValueDateParameter() {
			// ParameterHelper.InitializeMultiValueDateParameter(paramYears);
		}

		private void InitializeComponent() {
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty1 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty2 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty3 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty4 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty5 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty6 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty7 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty8 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty9 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty10 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty11 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty12 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty13 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductSalesSummary));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings1 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            this.bindingSource1 = new DevExpress.Persistent.Base.ReportsV2.ViewDataSource();
            this.productsSource1 = new DevExpress.Persistent.Base.ReportsV2.ViewDataSource();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBox4 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
            this.Product = new DevExpress.XtraReports.Parameters.Parameter();
            this.OrderDate = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // bindingSource1
            // 
            this.bindingSource1.CriteriaString = "[Product.ID] = ?Product And [Order.OrderDate] > ?OrderDate";
            this.bindingSource1.Name = "bindingSource1";
            this.bindingSource1.ObjectTypeName = "OutlookInspired.Module.BusinessObjects.OrderItem";
            viewProperty1.DisplayName = "Product.Name";
            viewProperty1.Expression = "Product.Name";
            viewProperty2.DisplayName = "Product.CurrentInventory";
            viewProperty2.Expression = "Product.CurrentInventory";
            viewProperty3.DisplayName = "Product.Manufacturing";
            viewProperty3.Expression = "Product.Manufacturing";
            viewProperty4.DisplayName = "Total";
            viewProperty4.Expression = "Total";
            viewProperty5.DisplayName = "ProductUnits";
            viewProperty5.Expression = "ProductUnits";
            viewProperty6.DisplayName = "Product.ID";
            viewProperty6.Expression = "Product.ID";
            viewProperty7.DisplayName = "Order.Store.State";
            viewProperty7.Expression = "Order.Store.State";
            viewProperty8.DisplayName = "Order.OrderDate";
            viewProperty8.Expression = "Order.OrderDate";
            viewProperty9.DisplayName = "Order.Year";
            viewProperty9.Expression = "Order.Year";
            this.bindingSource1.Properties.AddRange(new DevExpress.Persistent.Base.ReportsV2.ViewProperty[] {
            viewProperty1,
            viewProperty2,
            viewProperty3,
            viewProperty4,
            viewProperty5,
            viewProperty6,
            viewProperty7,
            viewProperty8,
            viewProperty9});
            this.bindingSource1.TopReturnedRecords = 0;
            // 
            // productsSource1
            // 
            this.productsSource1.Name = "productsSource1";
            this.productsSource1.ObjectTypeName = "OutlookInspired.Module.BusinessObjects.Product";
            viewProperty10.DisplayName = "ID";
            viewProperty10.Expression = "ID";
            viewProperty11.DisplayName = "Name";
            viewProperty11.Expression = "Name";
            viewProperty12.DisplayName = "PrimaryImage.Data";
            viewProperty12.Expression = "PrimaryImage.Data";
            viewProperty13.DisplayName = "Cost";
            viewProperty13.Expression = "Cost";
            this.productsSource1.Properties.AddRange(new DevExpress.Persistent.Base.ReportsV2.ViewProperty[] {
            viewProperty10,
            viewProperty11,
            viewProperty12,
            viewProperty13});
            this.productsSource1.TopReturnedRecords = 0;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2});
            this.topMarginBand1.HeightF = 125F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("xrPictureBox2.ImageSource"));
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(470.8333F, 52.08333F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(170.8333F, 56.41184F);
            this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // detailBand1
            // 
            this.detailBand1.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F);
            this.detailBand1.HeightF = 0F;
            this.detailBand1.Name = "detailBand1";
            this.detailBand1.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Order.OrderDate", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.detailBand1.StylePriority.UseFont = false;
            this.detailBand1.StylePriority.UseForeColor = false;
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1});
            this.bottomMarginBand1.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F);
            this.bottomMarginBand1.HeightF = 102F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            this.bottomMarginBand1.StylePriority.UseFont = false;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(485.4167F, 0F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(156.25F, 23F);
            this.xrPageInfo2.StylePriority.UseForeColor = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrPageInfo2.TextFormatString = "{0:MMMM d, yyyy}";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(156.25F, 23F);
            this.xrPageInfo1.StylePriority.UseForeColor = false;
            this.xrPageInfo1.TextFormatString = "Page {0} of {1}";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrPictureBox4,
            this.xrTable2});
            this.ReportHeader.HeightF = 225.8333F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(203.3125F, 43.67973F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8,
            this.xrTableRow7,
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(429.6875F, 130.7124F);
            this.xrTable3.StylePriority.UsePadding = false;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1.3733330546061278D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Product.Name]")});
            this.xrTableCell15.Font = new DevExpress.Drawing.DXFont("Segoe UI", 26.25F);
            this.xrTableCell15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(178)))), ((int)(((byte)(144)))));
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UseForeColor = false;
            this.xrTableCell15.Weight = 3D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell14});
            this.xrTableRow7.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F);
            this.xrTableRow7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.StylePriority.UseFont = false;
            this.xrTableRow7.StylePriority.UseForeColor = false;
            this.xrTableRow7.StylePriority.UsePadding = false;
            this.xrTableRow7.StylePriority.UseTextAlignment = false;
            this.xrTableRow7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            this.xrTableRow7.Weight = 1.2978654898467186D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "CURRENT INVENTORY";
            this.xrTableCell7.Weight = 1.4122964395059123D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UseForeColor = false;
            this.xrTableCell14.Text = "IN MANUFACTURING";
            this.xrTableCell14.Weight = 1.5877035604940877D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell9});
            this.xrTableRow3.Font = new DevExpress.Drawing.DXFont("Segoe UI", 18F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StylePriority.UseFont = false;
            this.xrTableRow3.StylePriority.UseTextAlignment = false;
            this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableRow3.Weight = 1.0915353464368094D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Product.CurrentInventory]")});
            this.xrTableCell8.Multiline = true;
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Weight = 1.4122964395059121D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Product.Manufacturing]")});
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Weight = 1.5877035604940879D;
            // 
            // xrPictureBox4
            // 
            this.xrPictureBox4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.xrPictureBox4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox4.BorderWidth = 1F;
            this.xrPictureBox4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[Product].[PrimaryImage].[Data]")});
            this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(4.00001F, 49.40628F);
            this.xrPictureBox4.Name = "xrPictureBox4";
            this.xrPictureBox4.SizeF = new System.Drawing.SizeF(185.1414F, 127.9859F);
            this.xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox4.StylePriority.UseBorderColor = false;
            this.xrPictureBox4.StylePriority.UseBorders = false;
            this.xrPictureBox4.StylePriority.UseBorderWidth = false;
            this.xrPictureBox4.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.xrPictureBox4_BeforePrint);
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(641.9999F, 28.71094F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell5});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 0.66666681489878932D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(128)))), ((int)(((byte)(71)))));
            this.xrTableCell4.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11.25F);
            this.xrTableCell4.ForeColor = System.Drawing.Color.White;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 0, 0, 0, 100F);
            this.xrTableCell4.StylePriority.UseBackColor = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UseForeColor = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "Product Profile";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell4.Weight = 0.94701867179278676D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBackColor = false;
            this.xrTableCell5.Weight = 2.0252039691253749D;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(641.9999F, 28.71094F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 0.66666681489878932D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(128)))), ((int)(((byte)(71)))));
            this.xrTableCell1.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11.25F);
            this.xrTableCell1.ForeColor = System.Drawing.Color.White;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 0, 0, 0, 100F);
            this.xrTableCell1.StylePriority.UseBackColor = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseForeColor = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Orders";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 0.94701867179278676D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.xrTableCell2.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11.5F);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell2.StylePriority.UseBackColor = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "2011 - 2013";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell2.Weight = 2.0252039691253749D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8,
            this.xrChart1,
            this.xrTable1});
            this.ReportFooter.HeightF = 333.3333F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable8
            // 
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(403.0465F, 81.83333F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12,
            this.xrTableRow13,
            this.xrTableRow14,
            this.xrTableRow15,
            this.xrTableRow16,
            this.xrTableRow17});
            this.xrTable8.SizeF = new System.Drawing.SizeF(238.9533F, 158.0092F);
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 0.77837459842722589D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F);
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.Text = "Total orders for the [Product.Name]";
            this.xrTableCell18.Weight = 3D;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 1.042431401190909D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([Total])")});
            this.xrTableCell19.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseFont = false;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell19.Summary = xrSummary1;
            this.xrTableCell19.TextFormatString = "{0:$#,#}";
            this.xrTableCell19.Weight = 3D;
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20});
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 0.69866359482761187D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F);
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.Text = "Total cost of goods sold";
            this.xrTableCell20.Weight = 3D;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21});
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 1.1819583095512345D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[totalCost]")});
            this.xrTableCell21.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseFont = false;
            xrSummary2.FormatString = "{0:$#,#;0:$#,#; - }";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrTableCell21.Summary = xrSummary2;
            this.xrTableCell21.Weight = 3D;
            this.xrTableCell21.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.xrTableCell21_BeforePrint);
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell22});
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.Weight = 0.65786547518207683D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.CanGrow = false;
            this.xrTableCell22.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F);
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.Text = "Total units sold ";
            this.xrTableCell22.Weight = 3D;
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell23});
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.Weight = 1.0400433368797812D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([ProductUnits])")});
            this.xrTableCell23.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseFont = false;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell23.Summary = xrSummary3;
            this.xrTableCell23.Weight = 3D;
            // 
            // xrChart1
            // 
            this.xrChart1.BorderColor = System.Drawing.Color.Black;
            this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrChart1.DataSource = this.bindingSource1;
            this.xrChart1.EmptyChartText.DXFont = new DevExpress.Drawing.DXFont("Segoe UI", 12F);
            this.xrChart1.EmptyChartText.Text = "\r\n";
            this.xrChart1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.Center;
            this.xrChart1.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.xrChart1.Legend.DXFont = new DevExpress.Drawing.DXFont("Segoe UI", 11F);
            this.xrChart1.Legend.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.True;
            this.xrChart1.Legend.EquallySpacedItems = false;
            this.xrChart1.Legend.LegendID = -1;
            this.xrChart1.Legend.MarkerSize = new System.Drawing.Size(20, 20);
            this.xrChart1.Legend.Padding.Left = 30;
            this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 28.71097F);
            this.xrChart1.Name = "xrChart1";
            series1.ArgumentDataMember = "Order.Year";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            series1.DataSource = this.bindingSource1;
            pieSeriesLabel1.TextPattern = "{V:$#,#}";
            series1.Label = pieSeriesLabel1;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.LegendTextPattern = "{A}: {V:$#,#}\n";
            series1.Name = "Series 1";
            series1.QualitativeSummaryOptions.SummaryFunction = "SUM([Total])";
            series1.SeriesID = 0;
            pieSeriesView1.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            series1.View = pieSeriesView1;
            this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.xrChart1.SeriesTemplate.View = pieSeriesView2;
            this.xrChart1.SizeF = new System.Drawing.SizeF(366.6193F, 274.8307F);
            // 
            // Product
            // 
            this.Product.Name = "Product";
            this.Product.Type = typeof(global::System.Guid);
            dynamicListLookUpSettings1.DataMember = null;
            dynamicListLookUpSettings1.DataSource = this.productsSource1;
            dynamicListLookUpSettings1.DisplayMember = "Name";
            dynamicListLookUpSettings1.FilterString = null;
            dynamicListLookUpSettings1.SortMember = "Name";
            dynamicListLookUpSettings1.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            dynamicListLookUpSettings1.ValueMember = "ID";
            this.Product.ValueSourceSettings = dynamicListLookUpSettings1;
            // 
            // OrderDate
            // 
            this.OrderDate.ExpressionBindings.AddRange(new DevExpress.XtraReports.Expressions.BasicExpressionBinding[] {
            new DevExpress.XtraReports.Expressions.BasicExpressionBinding("Value", "AddDays(Today(),-30 )")});
            this.OrderDate.Name = "OrderDate";
            this.OrderDate.Type = typeof(global::System.DateTime);
            this.OrderDate.ValueInfo = "2024-05-23";
            // 
            // ProductSalesSummary
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1,
            this.ReportHeader,
            this.ReportFooter});
            this.DataSource = this.bindingSource1;
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(104F, 104F, 125F, 102F);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.Product,
            this.OrderDate});
            this.Version = "24.1";
            this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.EmployeeSummary_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productsSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

        private void XrChart1OnBeforePrint(object sender, CancelEventArgs e) {
            // var datasource = bindingSource1.Cast<EFCoreDataViewRecord>().Select(record => new{orderDate=Convert.ToDateTime(record["Order.OrderDate"]).Year})
	           //  .DistinctBy(arg => arg.orderDate);
            // xrChart1.DataSource=datasource;
            // xrChart1.Series.Cast<DevExpress.XtraCharts.Series>().First().DataSource=datasource;
        }

        private void EmployeeSummary_BeforePrint(object sender, CancelEventArgs e) {
			// string stringYears = (string)paramYears.Value;
			// bool isEmptyYears = string.IsNullOrEmpty(stringYears);
			// this.ReportFooter.Visible = !isEmptyYears;
			// if(isEmptyYears) 
			// 	return; 
			// string[] years = stringYears.Split(',').ToArray();
			// SetOrdersText(years);
			// SetFilterString(years);	 
		}
		void SetFilterString(string[] years){
			string[] filterYears = new string[years.Length];
			for(int i = 0; i < years.Length; i++) {
				filterYears[i] = "[Order.OrderDate.Year] == " + years[i];
			}
			this.FilterString = string.Join(" || ", filterYears);
		}
		void SetOrdersText(string[] years) {
			int countYears = years.Length;
			if(countYears > 1 && Convert.ToInt32(years[countYears - 1]) - Convert.ToInt32(years[0]) == countYears - 1) {
				xrTableCell2.Text = string.Join(" - ", new string[] { years[0], years[countYears - 1] });
			} else {
				// xrTableCell2.Text = (string)paramYears.Value;
			}   
		}
		private void xrTableCell21_BeforePrint(object sender, CancelEventArgs e) {
			object cost = ViewDataSourceObjectSpace.GetObjectByKey<Product>(GetCurrentColumnValue("Product.ID"))?.Cost;
			if (cost==null) return;
			decimal totalUnits = (decimal)xrTableCell23.Summary.GetResult();
			xrTableCell21.Text = ((decimal)cost * totalUnits).ToString("$#,#");
		}

        #endregion

        private void xrPictureBox4_BeforePrint(object sender, CancelEventArgs e) 
	        => xrPictureBox4.ImageSource = new ImageSource(false, ViewDataSourceObjectSpace.GetObjectByKey<Product>(GetCurrentColumnValue("Product.ID"))?.PrimaryImage.Data);

        
	}
}
