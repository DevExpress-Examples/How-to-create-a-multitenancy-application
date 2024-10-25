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
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Resources.Reports {
	public class ProductTopSalesperson : XtraReport {
		#region Designer generated code
		private static readonly PropertyInfo ViewDataSourceObjectSpaceProperty;

		private IObjectSpace ViewDataSourceObjectSpace => ViewDataSourceObjectSpaceProperty!.GetValue(bindingSource1) as IObjectSpace;

		static ProductTopSalesperson() => ViewDataSourceObjectSpaceProperty = typeof(ViewDataSource).GetProperty("ObjectSpace",
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		private TopMarginBand topMarginBand1;
		private DetailBand detailBand1;
		private ViewDataSource bindingSource1;
		private ViewDataSource productSource;
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
		private XRTable xrTable8;
		private XRTableRow xrTableRow12;
		private XRTableCell xrTableCell18;
		private XRTableRow xrTableRow13;
		private XRTableCell xrTableCell19;
		private XRTableRow xrTableRow16;
		private XRTableCell xrTableCell22;
		private XRTableRow xrTableRow17;
		private XRTableCell xrTableCell23;
		private CalculatedField totals;
		private XRTable xrTable4;
		private XRTableRow xrTableRow10;
		private XRTableCell xrTableCell3;
		private XRTableCell xrTableCell17;
		private BottomMarginBand bottomMarginBand1;
		private XRTable xrTable5;
		private XRTableRow xrTableRow4;
		private XRTableCell xrTableCell6;
		private XRTableRow xrTableRow18;
		private XRTableCell xrTableCell28;
		private XRTableRow xrTableRow5;
		private XRTableCell xrTableCell10;
		private XRTableCell xrTableCell11;
		private XRTableRow xrTableRow6;
		private XRTableCell xrTableCell12;
		private XRTableCell xrTableCell13;
		private XRTableRow xrTableRow9;
		private XRTableCell xrTableCell24;
		private XRTableCell xrTableCell25;
		private XRTableRow xrTableRow11;
		private XRTableCell xrTableCell26;
		private XRTableCell xrTableCell27;
		private XRPictureBox xrPictureBox1;
		private GroupHeaderBand GroupHeader1;
		private XRTableCell xrTableCell30;
		private XRChart xrChart1;
        private Parameter Product;
        private Parameter OrderDate;
        private Parameter paramAscending;
		public ProductTopSalesperson() {
			InitializeComponent();
		}
		private void InitializeComponent() {
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty1 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty2 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty3 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductTopSalesperson));
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
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty14 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty15 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty16 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty17 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRGroupSortingSummary xrGroupSortingSummary1 = new DevExpress.XtraReports.UI.XRGroupSortingSummary();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings1 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            this.productSource = new DevExpress.Persistent.Base.ReportsV2.ViewDataSource();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.bindingSource1 = new DevExpress.Persistent.Base.ReportsV2.ViewDataSource();
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
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
            this.paramAscending = new DevExpress.XtraReports.Parameters.Parameter();
            this.totals = new DevExpress.XtraReports.UI.CalculatedField();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.Product = new DevExpress.XtraReports.Parameters.Parameter();
            this.OrderDate = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.productSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // productSource
            // 
            this.productSource.Name = "productSource";
            this.productSource.ObjectTypeName = "OutlookInspired.Module.BusinessObjects.Product";
            viewProperty1.DisplayName = "ID";
            viewProperty1.Expression = "ID";
            viewProperty2.DisplayName = "Name";
            viewProperty2.Expression = "Name";
            viewProperty3.DisplayName = "PrimaryImage.Data";
            viewProperty3.Expression = "PrimaryImage.Data";
            this.productSource.Properties.AddRange(new DevExpress.Persistent.Base.ReportsV2.ViewProperty[] {
            viewProperty1,
            viewProperty2,
            viewProperty3});
            this.productSource.TopReturnedRecords = 0;
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
            // bindingSource1
            // 
            this.bindingSource1.CriteriaString = "[Product.ID] = ?Product And [Order.OrderDate] > ?OrderDate";
            this.bindingSource1.Name = "bindingSource1";
            this.bindingSource1.ObjectTypeName = "OutlookInspired.Module.BusinessObjects.OrderItem";
            viewProperty4.DisplayName = "Product.Name";
            viewProperty4.Expression = "Product.Name";
            viewProperty5.DisplayName = "Product.CurrentInventory";
            viewProperty5.Expression = "Product.CurrentInventory";
            viewProperty6.DisplayName = "Product.Manufacturing";
            viewProperty6.Expression = "Product.Manufacturing";
            viewProperty7.DisplayName = "Total";
            viewProperty7.Expression = "Total";
            viewProperty8.DisplayName = "Order.Employee.Prefix";
            viewProperty8.Expression = "Order.Employee.Prefix";
            viewProperty9.DisplayName = "Order.Employee.FullName";
            viewProperty9.Expression = "Order.Employee.FullName";
            viewProperty10.DisplayName = "Order.Employee.Address";
            viewProperty10.Expression = "Order.Employee.Address";
            viewProperty11.DisplayName = "Order.Employee.City";
            viewProperty11.Expression = "Order.Employee.City";
            viewProperty12.DisplayName = "Order.Employee.MobilePhone";
            viewProperty12.Expression = "Order.Employee.MobilePhone";
            viewProperty13.DisplayName = "Order.Employee.HomePhone";
            viewProperty13.Expression = "Order.Employee.HomePhone";
            viewProperty14.DisplayName = "Order.Employee.Email";
            viewProperty14.Expression = "Order.Employee.Email";
            viewProperty15.DisplayName = "Order.Employee.Skype";
            viewProperty15.Expression = "Order.Employee.Skype";
            viewProperty16.DisplayName = "Product.ID";
            viewProperty16.Expression = "Product.ID";
            viewProperty17.DisplayName = "Order.Employee.ID";
            viewProperty17.Expression = "Order.Employee.ID";
            this.bindingSource1.Properties.AddRange(new DevExpress.Persistent.Base.ReportsV2.ViewProperty[] {
            viewProperty4,
            viewProperty5,
            viewProperty6,
            viewProperty7,
            viewProperty8,
            viewProperty9,
            viewProperty10,
            viewProperty11,
            viewProperty12,
            viewProperty13,
            viewProperty14,
            viewProperty15,
            viewProperty16,
            viewProperty17});
            this.bindingSource1.TopReturnedRecords = 0;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrPictureBox4,
            this.xrTable2,
            this.xrTable1,
            this.xrTable8,
            this.xrTable4,
            this.xrChart1});
            this.ReportHeader.HeightF = 505.1248F;
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
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[Product].[Image]")});
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
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(9.536743E-05F, 207.3333F);
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
            this.xrTableCell1.Text = "Analysis";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 0.9470181772948294D;
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
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell2.Weight = 2.0252044636233322D;
            // 
            // xrTable8
            // 
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(392.7134F, 274.8359F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12,
            this.xrTableRow13,
            this.xrTableRow16,
            this.xrTableRow17});
            this.xrTable8.SizeF = new System.Drawing.SizeF(238.9533F, 136.3069F);
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
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell23.Summary = xrSummary2;
            this.xrTableCell23.Weight = 3D;
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0.3332933F, 475.4284F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
            this.xrTable4.SizeF = new System.Drawing.SizeF(641.6667F, 29.69642F);
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell17});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(128)))), ((int)(((byte)(71)))));
            this.xrTableCell3.Font = new DevExpress.Drawing.DXFont("Segoe UI", 13F);
            this.xrTableCell3.ForeColor = System.Drawing.Color.White;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 0, 0, 0, 100F);
            this.xrTableCell3.StylePriority.UseBackColor = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseForeColor = false;
            this.xrTableCell3.StylePriority.UsePadding = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Employee List";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell3.Weight = 0.95480759106244184D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(218)))), ((int)(((byte)(218)))));
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseBackColor = false;
            this.xrTableCell17.Weight = 2.0451924089375586D;
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
            this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 245.8359F);
            this.xrChart1.Name = "xrChart1";
            series1.ArgumentDataMember = "Order.Employee.FullName";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            pieSeriesLabel1.TextPattern = "{V:$#,#}";
            series1.Label = pieSeriesLabel1;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.LegendTextPattern = "{A}\n{V:$#,#}\n";
            series1.Name = "Series 1";
            series1.QualitativeSummaryOptions.SummaryFunction = "SUM([Total])";
            series1.SeriesID = 0;
            pieSeriesView1.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            series1.View = pieSeriesView1;
            this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.xrChart1.SeriesTemplate.View = pieSeriesView2;
            this.xrChart1.SizeF = new System.Drawing.SizeF(360.2291F, 205.039F);
            // 
            // paramAscending
            // 
            this.paramAscending.Description = "Ascending";
            this.paramAscending.Name = "paramAscending";
            this.paramAscending.Type = typeof(bool);
            this.paramAscending.ValueInfo = "False";
            this.paramAscending.Visible = false;
            // 
            // totals
            // 
            this.totals.Name = "totals";
            // 
            // xrTable5
            // 
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(169.1667F, 25F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow18,
            this.xrTableRow5,
            this.xrTableRow6,
            this.xrTableRow9,
            this.xrTableRow11});
            this.xrTable5.SizeF = new System.Drawing.SizeF(462.5F, 195.0564F);
            this.xrTable5.StylePriority.UsePadding = false;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1.3733330546061278D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Font = new DevExpress.Drawing.DXFont("Segoe UI", 22.25F);
            this.xrTableCell6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(178)))), ((int)(((byte)(144)))));
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.StylePriority.UseForeColor = false;
            this.xrTableCell6.Text = "[Order.Employee.Prefix].[Order.Employee.FullName]";
            this.xrTableCell6.Weight = 3D;
            // 
            // xrTableRow18
            // 
            this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell30,
            this.xrTableCell28});
            this.xrTableRow18.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14F);
            this.xrTableRow18.Name = "xrTableRow18";
            this.xrTableRow18.StylePriority.UseFont = false;
            this.xrTableRow18.Weight = 0.87125743440749637D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.Text = "TOTAL SALES";
            this.xrTableCell30.Weight = 1.4122964395059121D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([Total])")});
            this.xrTableCell28.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseFont = false;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell28.Summary = xrSummary3;
            this.xrTableCell28.TextFormatString = "{0:$#,#}";
            this.xrTableCell28.Weight = 1.5877035604940879D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell10,
            this.xrTableCell11});
            this.xrTableRow5.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F);
            this.xrTableRow5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.StylePriority.UseFont = false;
            this.xrTableRow5.StylePriority.UseForeColor = false;
            this.xrTableRow5.StylePriority.UsePadding = false;
            this.xrTableRow5.StylePriority.UseTextAlignment = false;
            this.xrTableRow5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            this.xrTableRow5.Weight = 0.719922729133434D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "HOME OFFICE";
            this.xrTableCell10.Weight = 1.4122964395059121D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseForeColor = false;
            this.xrTableCell11.Text = "PHONE";
            this.xrTableCell11.Weight = 1.5877035604940879D;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell12,
            this.xrTableCell13});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1.2264689358948921D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Multiline = true;
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "[Order.Employee.Address]\r\n[Order.Employee.City]";
            this.xrTableCell12.Weight = 1.4122964395059121D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Multiline = true;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "[Order.Employee.MobilePhone] (Mobile)\r\n[Order.Employee.HomePhone] (Home)";
            this.xrTableCell13.Weight = 1.5877035604940879D;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell24,
            this.xrTableCell25});
            this.xrTableRow9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.StylePriority.UseForeColor = false;
            this.xrTableRow9.StylePriority.UsePadding = false;
            this.xrTableRow9.StylePriority.UseTextAlignment = false;
            this.xrTableRow9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            this.xrTableRow9.Weight = 0.638498971011733D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.Text = "EMAIL";
            this.xrTableCell24.Weight = 1.4122964395059121D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Text = "SKYPE";
            this.xrTableCell25.Weight = 1.5877035604940879D;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell26,
            this.xrTableCell27});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 0.70086023142775844D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Order.Employee.Email]")});
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.Weight = 1.4122964395059121D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Order.Employee.Skype]")});
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Weight = 1.5877035604940879D;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.xrPictureBox1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox1.BorderWidth = 1F;
            this.xrPictureBox1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[Order].[Employee].[Picture].[Data]")});
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(158.7394F, 190.1483F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            this.xrPictureBox1.StylePriority.UseBorderColor = false;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            this.xrPictureBox1.StylePriority.UseBorderWidth = false;
            this.xrPictureBox1.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.xrPictureBox1_BeforePrint);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrTable5});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Order.Employee.ID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 262.5F;
            this.GroupHeader1.KeepTogether = true;
            this.GroupHeader1.Name = "GroupHeader1";
            xrGroupSortingSummary1.Enabled = true;
            xrGroupSortingSummary1.FieldName = "Total";
            this.GroupHeader1.SortingSummary = xrGroupSortingSummary1;
            // 
            // Product
            // 
            this.Product.AllowNull = true;
            this.Product.Name = "Product";
            this.Product.Type = typeof(global::System.Guid);
            dynamicListLookUpSettings1.DataMember = null;
            dynamicListLookUpSettings1.DataSource = this.productSource;
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
            // ProductTopSalesperson
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1,
            this.ReportHeader,
            this.GroupHeader1});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.totals});
            this.DataSource = this.bindingSource1;
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(104F, 104F, 125F, 102F);
            this.ParameterPanelLayoutItems.AddRange(new DevExpress.XtraReports.Parameters.ParameterPanelLayoutItem[] {
            new DevExpress.XtraReports.Parameters.ParameterLayoutItem(this.paramAscending, DevExpress.XtraReports.Parameters.Orientation.Horizontal)});
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.paramAscending,
            this.Product,
            this.OrderDate});
            this.Version = "24.1";
            this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.ProductTopSalesperson_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.productSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		private void ProductTopSalesperson_BeforePrint(object sender, CancelEventArgs e) {
			if(Equals(true, paramAscending.Value)) {
				this.GroupHeader1.SortingSummary.SortOrder = XRColumnSortOrder.Ascending;
			} else {
				this.GroupHeader1.SortingSummary.SortOrder = XRColumnSortOrder.Descending;
			}
		}

        #endregion

        private void xrPictureBox4_BeforePrint(object sender, CancelEventArgs e) 
            => xrPictureBox4.ImageSource = new ImageSource(false, GetProduct()?.PrimaryImage?.Data??Array.Empty<byte>());

        private Product GetProduct() 
            => ViewDataSourceObjectSpace.GetObjectByKey<Product>(GetCurrentColumnValue("Product.ID"));

        private void xrPictureBox1_BeforePrint(object sender, CancelEventArgs e){
	        var employee = ViewDataSourceObjectSpace.GetObjectByKey<Employee>(GetCurrentColumnValue("Order.Employee.ID"));
	        var pictureData = employee?.Picture?.Data;
	        xrPictureBox1.ImageSource = new ImageSource(false, pictureData ?? Array.Empty<byte>());
        }
	}
}
