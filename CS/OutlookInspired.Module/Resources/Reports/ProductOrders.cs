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

using System.Drawing;
using System.Reflection;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base.ReportsV2;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Resources.Reports {
	public class ProductOrders : XtraReport {
		
		#region Designer generated code
		private static readonly PropertyInfo ViewDataSourceObjectSpaceProperty;
		private IObjectSpace ViewDataSourceObjectSpace => ViewDataSourceObjectSpaceProperty!.GetValue(bindingSource1) as IObjectSpace;
		static ProductOrders() => ViewDataSourceObjectSpaceProperty = typeof(ViewDataSource).GetProperty("ObjectSpace",
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		private TopMarginBand topMarginBand1;
		private DetailBand detailBand1;
		private ViewDataSource bindingSource1;
		private ViewDataSource productSource;

		private XRPictureBox xrPictureBox1;
		private BottomMarginBand bottomMarginBand1;
		private XRPageInfo xrPageInfo1;
		private ReportHeaderBand ReportHeader;
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
		private XRPageInfo xrPageInfo2;
		private XRTable xrTable2;
		private XRTableRow xrTableRow2;
		private XRTableCell xrTableCell4;
		private XRTableCell xrTableCell5;
		private XRTable xrTable1;
		private XRTableRow xrTableRow1;
		private XRTableCell xrTableCell1;
		private XRTableCell xrTableCell2;
		private XRTable xrTable4;
		private XRTableRow xrTableRow6;
		private XRTableCell xrTableCell3;
		private XRTableCell xrTableCell6;
		private XRTable xrTable7;
		private XRTableRow xrTableRow11;
		private XRTableCell xrTableCell36;
		private XRTableCell xrTableCell37;
		private XRTableCell xrTableCell38;
		private XRTableCell xrTableCell39;
		private XRTableCell xrTableCell40;
		private XRTableCell xrTableCell41;
		private XRTable xrTable6;
		private XRTableRow xrTableRow10;
		private XRTableCell xrTableCell30;
		private XRTableCell xrTableCell31;
		private XRTableCell xrTableCell32;
		private XRTableCell xrTableCell33;
		private XRTableCell xrTableCell34;
		private XRTableCell xrTableCell35;
		private XRLabel xrLabel4;
		private XRLabel xrLabel5;
		private GroupHeaderBand GroupHeader1;
		private GroupFooterBand GroupFooter1;
		private XRTable xrTable5;
		private XRTableRow xrTableRow9;
		private XRTableCell xrTableCell16;
		private XRTableCell xrTableCell17;
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
		private Color backCellColor = Color.FromArgb(223, 223, 223);
		private Color foreCellColor = Color.FromArgb(221, 128, 71);
		private XRChart xrChart1;
		private XRLabel xrLabel2;
		private CalculatedField totalCost;
        private DevExpress.XtraReports.Parameters.Parameter Product;
        private DevExpress.XtraReports.Parameters.Parameter OrderDate;
        IList<State> states;
		public ProductOrders() {
			InitializeComponent();
			InitializeDateTimeParameters();
		}

		

		
        private void InitializeDateTimeParameters() {
            // ParameterHelper.InitializeDateTimeParameters(paramFromDate, paramToDate);
        }

        public void SetStates(IList<State> states) {
			this.states = states;
		}
		private void InitializeComponent() {
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty1 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty2 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductOrders));
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
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty14 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty15 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.Persistent.Base.ReportsV2.ViewProperty viewProperty16 = new DevExpress.Persistent.Base.ReportsV2.ViewProperty();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings1 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            this.productSource = new DevExpress.Persistent.Base.ReportsV2.ViewDataSource();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.bindingSource1 = new DevExpress.Persistent.Base.ReportsV2.ViewDataSource();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.totalCost = new DevExpress.XtraReports.UI.CalculatedField();
            this.Product = new DevExpress.XtraReports.Parameters.Parameter();
            this.OrderDate = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.productSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
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
            this.productSource.Properties.AddRange(new DevExpress.Persistent.Base.ReportsV2.ViewProperty[] {
            viewProperty1,
            viewProperty2});
            this.productSource.TopReturnedRecords = 0;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
            this.topMarginBand1.HeightF = 119F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("xrPictureBox1.ImageSource"));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(473.9583F, 51F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(170.8333F, 56.41184F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // detailBand1
            // 
            this.detailBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
            this.detailBand1.HeightF = 20.27876F;
            this.detailBand1.Name = "detailBand1";
            this.detailBand1.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Order.InvoiceNumber", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            // 
            // xrTable7
            // 
            this.xrTable7.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(2.05829E-05F, 0F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.xrTable7.SizeF = new System.Drawing.SizeF(650F, 20.27876F);
            this.xrTable7.StylePriority.UseFont = false;
            this.xrTable7.StylePriority.UseForeColor = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.StylePriority.UseTextAlignment = false;
            this.xrTableRow11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableRow11.Weight = 1D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.CanGrow = false;
            this.xrTableCell36.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Order.OrderDate]")});
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 0, 0, 0, 100F);
            this.xrTableCell36.StylePriority.UsePadding = false;
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell36.TextFormatString = "{0:MM/dd/yyyy}";
            this.xrTableCell36.Weight = 0.59429261207580253D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.CanGrow = false;
            this.xrTableCell37.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Order.InvoiceNumber]")});
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 0, 0, 0, 100F);
            this.xrTableCell37.StylePriority.UsePadding = false;
            this.xrTableCell37.StylePriority.UseTextAlignment = false;
            this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell37.Weight = 0.54843580062572306D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.CanGrow = false;
            this.xrTableCell38.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ProductUnits]")});
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell38.Weight = 0.399939912733946D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.CanGrow = false;
            this.xrTableCell39.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ProductPrice]")});
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseTextAlignment = false;
            this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell39.TextFormatString = "{0:$#,#}";
            this.xrTableCell39.Weight = 0.40955509665451173D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.CanGrow = false;
            this.xrTableCell40.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Discount]")});
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell40.StylePriority.UsePadding = false;
            this.xrTableCell40.StylePriority.UseTextAlignment = false;
            this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell40.TextFormatString = "{0:$#,#;$#,#; -    }";
            this.xrTableCell40.Weight = 0.35327297724209294D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.CanGrow = false;
            this.xrTableCell41.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Total]")});
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell41.StylePriority.UsePadding = false;
            this.xrTableCell41.StylePriority.UseTextAlignment = false;
            this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell41.TextFormatString = "{0:$#,#}";
            this.xrTableCell41.Weight = 0.69450360066792372D;
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(208.3125F, 49.12503F);
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
            this.xrPictureBox4.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 51.85159F);
            this.xrPictureBox4.Name = "xrPictureBox4";
            this.xrPictureBox4.SizeF = new System.Drawing.SizeF(185.1414F, 127.9859F);
            this.xrPictureBox4.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox4.StylePriority.UseBorderColor = false;
            this.xrPictureBox4.StylePriority.UseBorders = false;
            this.xrPictureBox4.StylePriority.UseBorderWidth = false;
            this.xrPictureBox4.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.xrPictureBox4_BeforePrint);
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1});
            this.bottomMarginBand1.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F);
            this.bottomMarginBand1.HeightF = 93.37114F;
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
            viewProperty3.DisplayName = "Product.CurrentInventory";
            viewProperty3.Expression = "Product.CurrentInventory";
            viewProperty4.DisplayName = "Product.Name";
            viewProperty4.Expression = "Product.Name";
            viewProperty5.DisplayName = "Product.Manufacturing";
            viewProperty5.Expression = "Product.Manufacturing";
            viewProperty6.DisplayName = "ProductUnits";
            viewProperty6.Expression = "ProductUnits";
            viewProperty7.DisplayName = "Product.Cost";
            viewProperty7.Expression = "Product.Cost";
            viewProperty8.DisplayName = "Order.OrderDate";
            viewProperty8.Expression = "Order.OrderDate";
            viewProperty9.DisplayName = "Order.InvoiceNumber";
            viewProperty9.Expression = "Order.InvoiceNumber";
            viewProperty10.DisplayName = "Discount";
            viewProperty10.Expression = "Discount";
            viewProperty11.DisplayName = "ProductPrice";
            viewProperty11.Expression = "ProductPrice";
            viewProperty12.DisplayName = "Total";
            viewProperty12.Expression = "Total";
            viewProperty13.DisplayName = "Order.Year";
            viewProperty13.Expression = "Order.Year";
            viewProperty14.DisplayName = "Order.Store.State";
            viewProperty14.Expression = "Order.Store.State";
            viewProperty15.DisplayName = "Product.ID";
            viewProperty15.Expression = "Product.ID";
            viewProperty16.DisplayName = "Order.ID";
            viewProperty16.Expression = "Order.ID";
            this.bindingSource1.Properties.AddRange(new DevExpress.Persistent.Base.ReportsV2.ViewProperty[] {
            viewProperty3,
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
            viewProperty16});
            this.bindingSource1.TopReturnedRecords = 0;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrChart1,
            this.xrTable8,
            this.xrTable1,
            this.xrTable2,
            this.xrPictureBox4,
            this.xrTable3,
            this.xrTable4});
            this.ReportHeader.HeightF = 605.3966F;
            this.ReportHeader.Name = "ReportHeader";
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
            this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 264.3151F);
            this.xrChart1.Name = "xrChart1";
            series1.ArgumentDataMember = "Order.Store.State";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            pieSeriesLabel1.TextPattern = "{V:$#,#}";
            series1.Label = pieSeriesLabel1;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.LegendTextPattern = "{A} {V:$#,#}";
            series1.Name = "Series 1";
            series1.QualitativeSummaryOptions.SummaryFunction = "SUM([Total])";
            series1.SeriesID = 0;
            pieSeriesView1.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            series1.View = pieSeriesView1;
            this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.xrChart1.SeriesTemplate.View = pieSeriesView2;
            this.xrChart1.SizeF = new System.Drawing.SizeF(390.9943F, 284.4791F);
            // 
            // xrTable8
            // 
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(407.0465F, 328.2069F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12,
            this.xrTableRow13,
            this.xrTableRow14,
            this.xrTableRow15,
            this.xrTableRow16,
            this.xrTableRow17});
            this.xrTable8.SizeF = new System.Drawing.SizeF(242.9535F, 158.0092F);
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
            this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2});
            this.xrTableCell21.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseFont = false;
            xrSummary2.FormatString = "{0:$#,#;0:$#,#; - }";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrTableCell21.Summary = xrSummary2;
            this.xrTableCell21.Weight = 3D;
            // 
            // xrLabel2
            // 
            this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[totalCost]")});
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6.103516E-05F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(242.9535F, 34.58945F);
            this.xrLabel2.Text = "xrLabel2";
            this.xrLabel2.TextFormatString = "{0:$#,#}";
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
            // xrTable1
            // 
            this.xrTable1.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 230.2735F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(650F, 29.29686F);
            this.xrTable1.StylePriority.UseFont = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1.3333334941638817D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(128)))), ((int)(((byte)(71)))));
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
            this.xrTableCell1.Weight = 0.94159332354853686D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell2.StylePriority.UseBackColor = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "July 1, 2013 to July 31, 2013";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell2.Weight = 2.0676665025954248D;
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6.445313F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(650F, 28.71094F);
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
            this.xrTableCell5.Weight = 2.0622411543511747D;
            // 
            // xrTable4
            // 
            this.xrTable4.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 563.666F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable4.SizeF = new System.Drawing.SizeF(649.9999F, 28.64948F);
            this.xrTable4.StylePriority.UseFont = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell6});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1.3333334941638817D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(128)))), ((int)(((byte)(71)))));
            this.xrTableCell3.ForeColor = System.Drawing.Color.White;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 0, 0, 0, 100F);
            this.xrTableCell3.StylePriority.UseBackColor = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseForeColor = false;
            this.xrTableCell3.StylePriority.UsePadding = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "Orders";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell3.Weight = 0.80741964592598314D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell6.StylePriority.UseBackColor = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.StylePriority.UsePadding = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "Grouped by State | Sorted by Order Date";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell6.Weight = 2.1925803540740167D;
            // 
            // xrTable6
            // 
            this.xrTable6.Font = new DevExpress.Drawing.DXFont("Segoe UI", 10F);
            this.xrTable6.ForeColor = System.Drawing.Color.Gray;
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(1.589457E-05F, 64.00003F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
            this.xrTable6.SizeF = new System.Drawing.SizeF(650F, 24.99998F);
            this.xrTable6.StylePriority.UseFont = false;
            this.xrTable6.StylePriority.UseForeColor = false;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34,
            this.xrTableCell35});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.Padding = new DevExpress.XtraPrinting.PaddingInfo(15, 0, 0, 0, 100F);
            this.xrTableCell30.StylePriority.UsePadding = false;
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            this.xrTableCell30.Text = "ORDER DATE";
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell30.Weight = 0.65655051378103091D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            this.xrTableCell31.Text = "INVOICE #";
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell31.Weight = 0.48617789892049473D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseTextAlignment = false;
            this.xrTableCell32.Text = "QTY";
            this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell32.Weight = 0.399939912733946D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            this.xrTableCell33.Text = "PRICE";
            this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell33.Weight = 0.40955509665451173D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseTextAlignment = false;
            this.xrTableCell34.Text = "DISCOUNT";
            this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell34.Weight = 0.35327269554137175D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell35.StylePriority.UsePadding = false;
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            this.xrTableCell35.Text = "ORDER AMOUNT";
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell35.Weight = 0.6945038823686448D;
            // 
            // xrLabel4
            // 
            this.xrLabel4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Order.Store.State]")});
            this.xrLabel4.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(1.598337E-05F, 7.999996F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(156.25F, 31.33335F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLabel4.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.xrLabel4_BeforePrint);
            // 
            // xrLabel5
            // 
            this.xrLabel5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumCount([Order.Store.State])")});
            this.xrLabel5.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14F);
            this.xrLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(156.25F, 7.999996F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(200.3693F, 31.33335F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseForeColor = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel5.Summary = xrSummary4;
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrLabel5.TextFormatString = "| # OF ORDERS: {0}";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrTable6,
            this.xrLabel4});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Order.Store.State", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
            this.GroupHeader1.HeightF = 89.00002F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.GroupFooter1.HeightF = 43.799F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrTable5
            // 
            this.xrTable5.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(439.8059F, 18.79899F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTable5.SizeF = new System.Drawing.SizeF(210.1941F, 25.00001F);
            this.xrTable5.StylePriority.UseFont = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.xrTableCell17});
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "TOTAL";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell16.Weight = 0.93984267887268125D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([Total])")});
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrTableCell17.StylePriority.UsePadding = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell17.Summary = xrSummary5;
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell17.TextFormatString = "{0:$#,#}";
            this.xrTableCell17.Weight = 1.0653644820811168D;
            // 
            // totalCost
            // 
            this.totalCost.Expression = "[][[Order.OrderDate] >= [Parameters.paramFromDate]  And [Order.OrderDate] <=[Para" +
    "meters.paramToDate]].Sum([ProductUnits]) * [Product.Cost]";
            this.totalCost.FieldType = DevExpress.XtraReports.UI.FieldType.Decimal;
            this.totalCost.Name = "totalCost";
            // 
            // Product
            // 
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
            new DevExpress.XtraReports.Expressions.BasicExpressionBinding("Value", "AddDays(Today(), -30)")});
            this.OrderDate.Name = "OrderDate";
            this.OrderDate.Type = typeof(global::System.DateTime);
            this.OrderDate.ValueInfo = "2024-05-23";
            // 
            // ProductOrders
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1,
            this.ReportHeader,
            this.GroupHeader1,
            this.GroupFooter1});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.totalCost});
            this.DataSource = this.bindingSource1;
            this.DesignerOptions.ShowExportWarnings = false;
            this.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(100F, 100F, 119F, 93.37114F);
            this.ParameterPanelLayoutItems.AddRange(new DevExpress.XtraReports.Parameters.ParameterPanelLayoutItem[] {
            new DevExpress.XtraReports.Parameters.ParameterLayoutItem(this.Product, DevExpress.XtraReports.Parameters.Orientation.Horizontal),
            new DevExpress.XtraReports.Parameters.ParameterLayoutItem(this.OrderDate, DevExpress.XtraReports.Parameters.Orientation.Horizontal)});
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.Product,
            this.OrderDate});
            this.Version = "24.1";
            this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.CustomerSalesSummary_DataSourceDemanded);
            ((System.ComponentModel.ISupportInitialize)(this.productSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		private void xrLabel4_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e){
			
			Order currentOrder = (Order)ViewDataSourceObjectSpace.GetObjectByKey<Order>(GetCurrentColumnValue("Order.ID"));
			if(currentOrder != null && states != null)
				(sender as XRLabel).Text = states.Last(element => element.ShortName == currentOrder.Store.State).LongName.ToUpper();
		}
		private void CustomerSalesSummary_DataSourceDemanded(object sender, EventArgs e) {
			xrTableCell6.Text = "Grouped by State | Sorted by Invoice #";
			this.detailBand1.SortFields[0].FieldName = "Order.InvoiceNumber";
		}
        #endregion

        private void xrPictureBox4_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e) {
            xrPictureBox4.ImageSource = new ImageSource(false,
	            ViewDataSourceObjectSpace.GetObjectByKey<Product>(GetCurrentColumnValue<Guid>("Product.ID"))
                    ?.PrimaryImage.Data ?? Array.Empty<byte>());

        }
    }
}
