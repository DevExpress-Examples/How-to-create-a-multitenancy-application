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
using System.Drawing;
using DevExpress.Drawing;
using DevExpress.Drawing.Printing;
using DevExpress.Persistent.Base.ReportsV2;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Resources.Reports {
	public  class FedExGroundLabel : XtraReport {
		#region Designer generated code

		private CollectionDataSource bindingSource1;
		private TopMarginBand topMarginBand1;
		private DetailBand detailBand1;
		private XRLabel xrLabel1;
		private XRLabel xrLabel3;
		private XRBarCode xrBarCode2;
		private XRBarCode xrBarCode1;
		private XRLabel xrLabel2;
		private SubBand SubBand1;
		private XRPanel xrPanel1;
		private XRLabel xrLabelFromState;
		private XRLabel xrLabelFromStreetLine1;
		private XRLabel xrLabel6;
		private XRPanel xrPanel2;
		private XRLabel xrLabel19;
		private XRLabel xrLabel18;
		private XRLabel xrLabel17;
		private XRLabel xrLabel16;
		private XRLabel xrLabel9;
		private XRLabel xrLabel8;
		private XRLabel xrLabel7;
		private XRLabel xrLabel4;
		private XRLabel xrLabel5;
		private SubBand SubBand2;
		private XRPanel xrPanel3;
		private XRLabel xrLabel10;
		private XRLabel xrLabel12;
		private XRLabel xrLabel11;
		private XRLabel xrLabel13;
		private SubBand SubBand3;
		private XRPanel xrPanel4;
		private XRPictureBox xrPictureBoxUPS;
		private XRLabel xrLabel20;
		private XRLabel xrLabel21;
		private SubBand SubBand4;
		private XRControlStyle style1;
		private XRControlStyle style2;
		private XRControlStyle style3;
		private XRLabel xrLabel26;
		private XRLabel xrLabel25;
		private XRLabel xrLabel24;
		private XRLabel xrLabel23;
		private XRLabel xrLabel22;
		private BottomMarginBand bottomMarginBand1;
		private CalculatedField barcodeData;
		private XRLabel xrLabel28;
		private XRLabel xrLabel27;
		private XRLabel xrLabelFromStreetLine2;
		private XRLabel xrLabel30;
		private XRPictureBox xrPictureBoxFedEx;
		private XRPictureBox xrPictureBoxDLH;

		public FedExGroundLabel() {
			InitializeComponent();
		}
		private void InitializeComponent() {
			ComponentResourceManager resources = new ComponentResourceManager(typeof(FedExGroundLabel));
			PDF417Generator pdF417Generator1 = new PDF417Generator();
			EAN128Generator eaN128Generator1 = new EAN128Generator();
			this.topMarginBand1 = new TopMarginBand();
			this.detailBand1 = new DetailBand();
			this.SubBand1 = new SubBand();
			this.xrPanel1 = new XRPanel();
			this.xrLabelFromStreetLine2 = new XRLabel();
			this.xrLabel28 = new XRLabel();
			this.xrLabelFromState = new XRLabel();
			this.xrLabelFromStreetLine1 = new XRLabel();
			this.xrLabel6 = new XRLabel();
			this.xrLabel2 = new XRLabel();
			this.xrLabel1 = new XRLabel();
			this.xrPanel2 = new XRPanel();
			this.xrLabel19 = new XRLabel();
			this.xrLabel18 = new XRLabel();
			this.xrLabel17 = new XRLabel();
			this.xrLabel16 = new XRLabel();
			this.xrLabel9 = new XRLabel();
			this.xrLabel8 = new XRLabel();
			this.xrLabel7 = new XRLabel();
			this.xrLabel4 = new XRLabel();
			this.xrLabel5 = new XRLabel();
			this.SubBand2 = new SubBand();
			this.xrPanel3 = new XRPanel();
			this.xrLabel30 = new XRLabel();
			this.xrLabel27 = new XRLabel();
			this.xrLabel22 = new XRLabel();
			this.xrLabel26 = new XRLabel();
			this.xrLabel25 = new XRLabel();
			this.xrLabel24 = new XRLabel();
			this.xrLabel23 = new XRLabel();
			this.xrLabel10 = new XRLabel();
			this.xrLabel12 = new XRLabel();
			this.xrLabel11 = new XRLabel();
			this.xrLabel3 = new XRLabel();
			this.xrLabel13 = new XRLabel();
			this.SubBand3 = new SubBand();
			this.xrPanel4 = new XRPanel();
			this.xrPictureBoxFedEx = new XRPictureBox();
			this.xrLabel21 = new XRLabel();
			this.xrBarCode2 = new XRBarCode();
			this.xrPictureBoxUPS = new XRPictureBox();
			this.xrLabel20 = new XRLabel();
			this.xrPictureBoxDLH = new XRPictureBox();
			this.SubBand4 = new SubBand();
			this.xrBarCode1 = new XRBarCode();
			this.bottomMarginBand1 = new BottomMarginBand();
			this.style1 = new XRControlStyle();
			this.style2 = new XRControlStyle();
			this.style3 = new XRControlStyle();
			this.bindingSource1 = new CollectionDataSource();
			this.barcodeData = new CalculatedField();
			((ISupportInitialize)(this.bindingSource1)).BeginInit();
			((ISupportInitialize)(this)).BeginInit();
			// 
			// topMarginBand1
			// 
			this.topMarginBand1.HeightF = 0F;
			this.topMarginBand1.Name = "topMarginBand1";
			// 
			// detailBand1
			// 
			this.detailBand1.HeightF = 0F;
			this.detailBand1.Name = "detailBand1";
			this.detailBand1.SubBands.AddRange(new SubBand[] {
				this.SubBand1,
				this.SubBand2,
				this.SubBand3,
				this.SubBand4});
			// 
			// SubBand1
			// 
			this.SubBand1.Controls.AddRange(new XRControl[] {
				this.xrPanel1,
				this.xrPanel2});
			this.SubBand1.HeightF = 85F;
			this.SubBand1.Name = "SubBand1";
			// 
			// xrPanel1
			// 
			this.xrPanel1.Borders = ((BorderSide)((BorderSide.Right | BorderSide.Bottom)));
			this.xrPanel1.CanGrow = false;
			this.xrPanel1.Controls.AddRange(new XRControl[] {
				this.xrLabelFromStreetLine2,
				this.xrLabel28,
				this.xrLabelFromState,
				this.xrLabelFromStreetLine1,
				this.xrLabel6,
				this.xrLabel2,
				this.xrLabel1});
			this.xrPanel1.LocationFloat = new PointFloat(7.999999F, 5F);
			this.xrPanel1.Name = "xrPanel1";
			this.xrPanel1.SizeF = new SizeF(239.611F, 80F);
			this.xrPanel1.StylePriority.UseBorders = false;
			// 
			// xrLabelFromStreetLine2
			// 
			this.xrLabelFromStreetLine2.AnchorHorizontal = HorizontalAnchorStyles.Left;
			this.xrLabelFromStreetLine2.Borders = BorderSide.None;
			this.xrLabelFromStreetLine2.CanGrow = false;
			this.xrLabelFromStreetLine2.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[Employee.City]")});
			this.xrLabelFromStreetLine2.LocationFloat = new PointFloat(3.999952F, 51.00001F);
			this.xrLabelFromStreetLine2.Multiline = true;
			this.xrLabelFromStreetLine2.Name = "xrLabelFromStreetLine2";
			this.xrLabelFromStreetLine2.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabelFromStreetLine2.SizeF = new SizeF(191.5179F, 12.49997F);
			this.xrLabelFromStreetLine2.StyleName = "style1";
			this.xrLabelFromStreetLine2.StylePriority.UseBorders = false;
			this.xrLabelFromStreetLine2.StylePriority.UseFont = false;
			this.xrLabelFromStreetLine2.StylePriority.UseTextAlignment = false;
			this.xrLabelFromStreetLine2.Text = "StreetLine2";
			this.xrLabelFromStreetLine2.TextAlignment = TextAlignment.TopLeft;
			this.xrLabelFromStreetLine2.BeforePrint += new BeforePrintEventHandler(this.xrLabelFromStreetLine2_BeforePrint);
			// 
			// xrLabel28
			// 
			this.xrLabel28.AnchorHorizontal = HorizontalAnchorStyles.Left;
			this.xrLabel28.Borders = BorderSide.None;
			this.xrLabel28.CanGrow = false;
			this.xrLabel28.LocationFloat = new PointFloat(3.999992F, 25F);
			this.xrLabel28.Multiline = true;
			this.xrLabel28.Name = "xrLabel28";
			this.xrLabel28.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel28.SizeF = new SizeF(191.5179F, 13F);
			this.xrLabel28.StyleName = "style1";
			this.xrLabel28.StylePriority.UseBorders = false;
			this.xrLabel28.StylePriority.UseFont = false;
			this.xrLabel28.StylePriority.UseTextAlignment = false;
			this.xrLabel28.Text = "DevAV";
			this.xrLabel28.TextAlignment = TextAlignment.TopLeft;
			// 
			// xrLabelFromState
			// 
			this.xrLabelFromState.AnchorHorizontal = HorizontalAnchorStyles.Left;
			this.xrLabelFromState.Borders = BorderSide.None;
			this.xrLabelFromState.CanGrow = false;
			this.xrLabelFromState.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[Employee.State]")});
			this.xrLabelFromState.LocationFloat = new PointFloat(4F, 63.49999F);
			this.xrLabelFromState.Name = "xrLabelFromState";
			this.xrLabelFromState.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabelFromState.SizeF = new SizeF(191.5179F, 13F);
			this.xrLabelFromState.StyleName = "style1";
			this.xrLabelFromState.StylePriority.UseBorders = false;
			this.xrLabelFromState.StylePriority.UseFont = false;
			this.xrLabelFromState.StylePriority.UseTextAlignment = false;
			this.xrLabelFromState.Text = "City, State or Province Code, Postal Code";
			this.xrLabelFromState.BeforePrint += new BeforePrintEventHandler(this.xrLabelFromState_BeforePrint);
			// 
			// xrLabelFromStreetLine1
			// 
			this.xrLabelFromStreetLine1.AnchorHorizontal = HorizontalAnchorStyles.Left;
			this.xrLabelFromStreetLine1.Borders = BorderSide.None;
			this.xrLabelFromStreetLine1.CanGrow = false;
			this.xrLabelFromStreetLine1.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[Employee.Address]")});
			this.xrLabelFromStreetLine1.LocationFloat = new PointFloat(4.000001F, 38F);
			this.xrLabelFromStreetLine1.Multiline = true;
			this.xrLabelFromStreetLine1.Name = "xrLabelFromStreetLine1";
			this.xrLabelFromStreetLine1.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabelFromStreetLine1.SizeF = new SizeF(191.5179F, 13.00001F);
			this.xrLabelFromStreetLine1.StyleName = "style1";
			this.xrLabelFromStreetLine1.StylePriority.UseBorders = false;
			this.xrLabelFromStreetLine1.StylePriority.UseFont = false;
			this.xrLabelFromStreetLine1.StylePriority.UseTextAlignment = false;
			this.xrLabelFromStreetLine1.Text = "StreetLine1";
			this.xrLabelFromStreetLine1.TextAlignment = TextAlignment.TopLeft;
			this.xrLabelFromStreetLine1.BeforePrint += new BeforePrintEventHandler(this.xrLabelFromStreetLine1_BeforePrint);
			// 
			// xrLabel6
			// 
			this.xrLabel6.AnchorHorizontal = HorizontalAnchorStyles.Left;
			this.xrLabel6.Borders = BorderSide.None;
			this.xrLabel6.CanGrow = false;
			this.xrLabel6.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[Employee.MobilePhone]")});
			this.xrLabel6.LocationFloat = new PointFloat(121.0238F, 0F);
			this.xrLabel6.Name = "xrLabel6";
			this.xrLabel6.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel6.SizeF = new SizeF(74.49411F, 12.00002F);
			this.xrLabel6.StyleName = "style1";
			this.xrLabel6.StylePriority.UseBorders = false;
			this.xrLabel6.StylePriority.UseFont = false;
			this.xrLabel6.StylePriority.UseTextAlignment = false;
			this.xrLabel6.Text = "(123) 456-7890";
			// 
			// xrLabel2
			// 
			this.xrLabel2.AnchorHorizontal = HorizontalAnchorStyles.Left;
			this.xrLabel2.Borders = BorderSide.None;
			this.xrLabel2.CanGrow = false;
			this.xrLabel2.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[Employee.FullName]")});
			this.xrLabel2.LocationFloat = new PointFloat(3.999992F, 12.50001F);
			this.xrLabel2.Multiline = true;
			this.xrLabel2.Name = "xrLabel2";
			this.xrLabel2.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel2.SizeF = new SizeF(191.5179F, 12.5F);
			this.xrLabel2.StyleName = "style1";
			this.xrLabel2.StylePriority.UseBorders = false;
			this.xrLabel2.StylePriority.UseFont = false;
			this.xrLabel2.StylePriority.UseTextAlignment = false;
			this.xrLabel2.Text = "Person Name\r\nCompany Name\r\n";
			this.xrLabel2.TextAlignment = TextAlignment.TopLeft;
			// 
			// xrLabel1
			// 
			this.xrLabel1.AnchorHorizontal = HorizontalAnchorStyles.Left;
			this.xrLabel1.Borders = BorderSide.None;
			this.xrLabel1.CanGrow = false;
			this.xrLabel1.LocationFloat = new PointFloat(3.99999F, 0F);
			this.xrLabel1.Name = "xrLabel1";
			this.xrLabel1.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel1.SizeF = new SizeF(100F, 12F);
			this.xrLabel1.StyleName = "style1";
			this.xrLabel1.StylePriority.UseBorders = false;
			this.xrLabel1.StylePriority.UseFont = false;
			this.xrLabel1.StylePriority.UseTextAlignment = false;
			this.xrLabel1.Text = "FROM";
			// 
			// xrPanel2
			// 
			this.xrPanel2.Borders = BorderSide.Bottom;
			this.xrPanel2.CanGrow = false;
			this.xrPanel2.Controls.AddRange(new XRControl[] {
				this.xrLabel19,
				this.xrLabel18,
				this.xrLabel17,
				this.xrLabel16,
				this.xrLabel9,
				this.xrLabel8,
				this.xrLabel7,
				this.xrLabel4,
				this.xrLabel5});
			this.xrPanel2.LocationFloat = new PointFloat(247.611F, 5F);
			this.xrPanel2.Name = "xrPanel2";
			this.xrPanel2.SizeF = new SizeF(146.3702F, 80F);
			this.xrPanel2.StylePriority.UseBorders = false;
			// 
			// xrLabel19
			// 
			this.xrLabel19.Borders = BorderSide.None;
			this.xrLabel19.CanGrow = false;
			this.xrLabel19.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[InvoiceNumber]")});
			this.xrLabel19.LocationFloat = new PointFloat(75.36169F, 38F);
			this.xrLabel19.Name = "xrLabel19";
			this.xrLabel19.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel19.SizeF = new SizeF(68.00862F, 13F);
			this.xrLabel19.StyleName = "style1";
			this.xrLabel19.StylePriority.UseBorders = false;
			this.xrLabel19.StylePriority.UseFont = false;
			this.xrLabel19.StylePriority.UseTextAlignment = false;
			this.xrLabel19.Text = "18 X 18 X 40 IN";
			// 
			// xrLabel18
			// 
			this.xrLabel18.Borders = BorderSide.None;
			this.xrLabel18.CanGrow = false;
			this.xrLabel18.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[ShipmentCourierId]")});
			this.xrLabel18.LocationFloat = new PointFloat(34.82996F, 25F);
			this.xrLabel18.Name = "xrLabel18";
			this.xrLabel18.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel18.SizeF = new SizeF(108.5402F, 13F);
			this.xrLabel18.StyleName = "style1";
			this.xrLabel18.StylePriority.UseBorders = false;
			this.xrLabel18.StylePriority.UseFont = false;
			this.xrLabel18.StylePriority.UseTextAlignment = false;
			this.xrLabel18.Text = "100615311/NET3850";
			// 
			// xrLabel17
			// 
			this.xrLabel17.Borders = BorderSide.None;
			this.xrLabel17.CanGrow = false;
			this.xrLabel17.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[ActualWeight]")});
			this.xrLabel17.LocationFloat = new PointFloat(55.67004F, 13.00001F);
			this.xrLabel17.Name = "xrLabel17";
			this.xrLabel17.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel17.SizeF = new SizeF(87.70013F, 12F);
			this.xrLabel17.StyleName = "style1";
			this.xrLabel17.StylePriority.UseBorders = false;
			this.xrLabel17.StylePriority.UseFont = false;
			this.xrLabel17.StylePriority.UseTextAlignment = false;
			this.xrLabel17.Text = "Actual Weight";
			this.xrLabel17.TextFormatString = "{0:f1} lb";
			// 
			// xrLabel16
			// 
			this.xrLabel16.Borders = BorderSide.None;
			this.xrLabel16.CanGrow = false;
			this.xrLabel16.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[ShipDate]")});
			this.xrLabel16.LocationFloat = new PointFloat(75.36169F, 0F);
			this.xrLabel16.Name = "xrLabel16";
			this.xrLabel16.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel16.SizeF = new SizeF(68.00851F, 13F);
			this.xrLabel16.StyleName = "style1";
			this.xrLabel16.StylePriority.UseBorders = false;
			this.xrLabel16.StylePriority.UseFont = false;
			this.xrLabel16.StylePriority.UseTextAlignment = false;
			this.xrLabel16.TextFormatString = "{0:ddMMMyy}";
			this.xrLabel16.BeforePrint += new BeforePrintEventHandler(this.xrLabel16_BeforePrint);
			// 
			// xrLabel9
			// 
			this.xrLabel9.Borders = BorderSide.None;
			this.xrLabel9.CanGrow = false;
			this.xrLabel9.LocationFloat = new PointFloat(7.00004F, 60F);
			this.xrLabel9.Name = "xrLabel9";
			this.xrLabel9.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel9.SizeF = new SizeF(67.74F, 12F);
			this.xrLabel9.StyleName = "style1";
			this.xrLabel9.StylePriority.UseBorders = false;
			this.xrLabel9.StylePriority.UseFont = false;
			this.xrLabel9.StylePriority.UseTextAlignment = false;
			this.xrLabel9.Text = "BILL SENDER";
			// 
			// xrLabel8
			// 
			this.xrLabel8.Borders = BorderSide.None;
			this.xrLabel8.CanGrow = false;
			this.xrLabel8.LocationFloat = new PointFloat(7.000065F, 38F);
			this.xrLabel8.Name = "xrLabel8";
			this.xrLabel8.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel8.SizeF = new SizeF(67.73999F, 13F);
			this.xrLabel8.StyleName = "style1";
			this.xrLabel8.StylePriority.UseBorders = false;
			this.xrLabel8.StylePriority.UseFont = false;
			this.xrLabel8.StylePriority.UseTextAlignment = false;
			this.xrLabel8.Text = "ACCOUNT:";
			// 
			// xrLabel7
			// 
			this.xrLabel7.Borders = BorderSide.None;
			this.xrLabel7.CanGrow = false;
			this.xrLabel7.LocationFloat = new PointFloat(7F, 25F);
			this.xrLabel7.Name = "xrLabel7";
			this.xrLabel7.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel7.SizeF = new SizeF(27.83F, 13F);
			this.xrLabel7.StyleName = "style1";
			this.xrLabel7.StylePriority.UseBorders = false;
			this.xrLabel7.StylePriority.UseFont = false;
			this.xrLabel7.StylePriority.UseTextAlignment = false;
			this.xrLabel7.Text = "CAD:";
			// 
			// xrLabel4
			// 
			this.xrLabel4.Borders = BorderSide.None;
			this.xrLabel4.CanGrow = false;
			this.xrLabel4.LocationFloat = new PointFloat(7F, 13F);
			this.xrLabel4.Name = "xrLabel4";
			this.xrLabel4.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel4.SizeF = new SizeF(48.67F, 12F);
			this.xrLabel4.StyleName = "style1";
			this.xrLabel4.StylePriority.UseBorders = false;
			this.xrLabel4.StylePriority.UseFont = false;
			this.xrLabel4.StylePriority.UseTextAlignment = false;
			this.xrLabel4.Text = "ACTWGT:";
			// 
			// xrLabel5
			// 
			this.xrLabel5.Borders = BorderSide.None;
			this.xrLabel5.CanGrow = false;
			this.xrLabel5.LocationFloat = new PointFloat(7.000033F, 0F);
			this.xrLabel5.Name = "xrLabel5";
			this.xrLabel5.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel5.SizeF = new SizeF(67.74004F, 13F);
			this.xrLabel5.StyleName = "style1";
			this.xrLabel5.StylePriority.UseBorders = false;
			this.xrLabel5.StylePriority.UseFont = false;
			this.xrLabel5.StylePriority.UseTextAlignment = false;
			this.xrLabel5.Text = "SHIP DATE:";
			// 
			// SubBand2
			// 
			this.SubBand2.Controls.AddRange(new XRControl[] {
				this.xrPanel3});
			this.SubBand2.HeightF = 115F;
			this.SubBand2.Name = "SubBand2";
			// 
			// xrPanel3
			// 
			this.xrPanel3.Borders = BorderSide.Bottom;
			this.xrPanel3.CanGrow = false;
			this.xrPanel3.Controls.AddRange(new XRControl[] {
				this.xrLabel30,
				this.xrLabel27,
				this.xrLabel22,
				this.xrLabel26,
				this.xrLabel25,
				this.xrLabel24,
				this.xrLabel23,
				this.xrLabel10,
				this.xrLabel12,
				this.xrLabel11,
				this.xrLabel3,
				this.xrLabel13});
			this.xrPanel3.LocationFloat = new PointFloat(7.999999F, 0F);
			this.xrPanel3.Name = "xrPanel3";
			this.xrPanel3.SizeF = new SizeF(385.9812F, 115F);
			this.xrPanel3.StylePriority.UseBorders = false;
			// 
			// xrLabel30
			// 
			this.xrLabel30.CanGrow = false;
			this.xrLabel30.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[Store.City]")});
			this.xrLabel30.LocationFloat = new PointFloat(16.76457F, 40.01671F);
			this.xrLabel30.Multiline = true;
			this.xrLabel30.Name = "xrLabel30";
			this.xrLabel30.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel30.SizeF = new SizeF(288.8608F, 13.81655F);
			this.xrLabel30.StyleName = "style2";
			this.xrLabel30.StylePriority.UseFont = false;
			this.xrLabel30.StylePriority.UseTextAlignment = false;
			this.xrLabel30.Text = "StreetLine2";
			// 
			// xrLabel27
			// 
			this.xrLabel27.CanGrow = false;
			this.xrLabel27.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[Customer.Name]")});
			this.xrLabel27.LocationFloat = new PointFloat(16.76451F, 13.25314F);
			this.xrLabel27.Multiline = true;
			this.xrLabel27.Name = "xrLabel27";
			this.xrLabel27.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel27.SizeF = new SizeF(288.8608F, 14.08012F);
			this.xrLabel27.StyleName = "style2";
			this.xrLabel27.StylePriority.UseFont = false;
			this.xrLabel27.StylePriority.UseTextAlignment = false;
			this.xrLabel27.Text = "Company Name\r\n";
			// 
			// xrLabel22
			// 
			this.xrLabel22.Angle = 90F;
			this.xrLabel22.CanGrow = false;
			this.xrLabel22.Font = new DXFont("Arial Narrow", 6F);
			this.xrLabel22.LocationFloat = new PointFloat(374.8799F, 0F);
			this.xrLabel22.Name = "xrLabel22";
			this.xrLabel22.SizeF = new SizeF(11.10129F, 115F);
			this.xrLabel22.StyleName = "style1";
			this.xrLabel22.StylePriority.UseBorders = false;
			this.xrLabel22.StylePriority.UseFont = false;
			this.xrLabel22.StylePriority.UsePadding = false;
			this.xrLabel22.StylePriority.UseTextAlignment = false;
			this.xrLabel22.Text = "545B10A53C1";
			this.xrLabel22.TextAlignment = TextAlignment.MiddleCenter;
			// 
			// xrLabel26
			// 
			this.xrLabel26.AnchorHorizontal = HorizontalAnchorStyles.Left;
			this.xrLabel26.Borders = BorderSide.None;
			this.xrLabel26.CanGrow = false;
			this.xrLabel26.LocationFloat = new PointFloat(212.611F, 101F);
			this.xrLabel26.Name = "xrLabel26";
			this.xrLabel26.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel26.SizeF = new SizeF(32F, 12F);
			this.xrLabel26.StyleName = "style1";
			this.xrLabel26.StylePriority.UseBorders = false;
			this.xrLabel26.StylePriority.UseFont = false;
			this.xrLabel26.StylePriority.UseTextAlignment = false;
			this.xrLabel26.Text = "DEPT:";
			// 
			// xrLabel25
			// 
			this.xrLabel25.AnchorHorizontal = HorizontalAnchorStyles.Left;
			this.xrLabel25.Borders = BorderSide.None;
			this.xrLabel25.CanGrow = false;
			this.xrLabel25.LocationFloat = new PointFloat(157.7526F, 76.00005F);
			this.xrLabel25.Name = "xrLabel25";
			this.xrLabel25.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel25.SizeF = new SizeF(26F, 11.58332F);
			this.xrLabel25.StyleName = "style1";
			this.xrLabel25.StylePriority.UseBorders = false;
			this.xrLabel25.StylePriority.UseFont = false;
			this.xrLabel25.StylePriority.UseTextAlignment = false;
			this.xrLabel25.Text = "REF:";
			// 
			// xrLabel24
			// 
			this.xrLabel24.AnchorHorizontal = HorizontalAnchorStyles.Left;
			this.xrLabel24.Borders = BorderSide.None;
			this.xrLabel24.CanGrow = false;
			this.xrLabel24.LocationFloat = new PointFloat(16.76448F, 88.00003F);
			this.xrLabel24.Name = "xrLabel24";
			this.xrLabel24.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel24.SizeF = new SizeF(22F, 12F);
			this.xrLabel24.StyleName = "style1";
			this.xrLabel24.StylePriority.UseBorders = false;
			this.xrLabel24.StylePriority.UseFont = false;
			this.xrLabel24.StylePriority.UseTextAlignment = false;
			this.xrLabel24.Text = "INV:";
			// 
			// xrLabel23
			// 
			this.xrLabel23.AnchorHorizontal = HorizontalAnchorStyles.Left;
			this.xrLabel23.Borders = BorderSide.None;
			this.xrLabel23.CanGrow = false;
			this.xrLabel23.LocationFloat = new PointFloat(16.76451F, 101F);
			this.xrLabel23.Name = "xrLabel23";
			this.xrLabel23.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel23.SizeF = new SizeF(20F, 12F);
			this.xrLabel23.StyleName = "style1";
			this.xrLabel23.StylePriority.UseBorders = false;
			this.xrLabel23.StylePriority.UseFont = false;
			this.xrLabel23.StylePriority.UseTextAlignment = false;
			this.xrLabel23.Text = "PO:";
			// 
			// xrLabel10
			// 
			this.xrLabel10.CanGrow = false;
			this.xrLabel10.LocationFloat = new PointFloat(16.76448F, 0.833263F);
			this.xrLabel10.Multiline = true;
			this.xrLabel10.Name = "xrLabel10";
			this.xrLabel10.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel10.SizeF = new SizeF(288.8609F, 12.41988F);
			this.xrLabel10.StyleName = "style2";
			this.xrLabel10.StylePriority.UseFont = false;
			this.xrLabel10.StylePriority.UseTextAlignment = false;
			this.xrLabel10.Text = "Person Name\r\nCompany Name\r\n";
			this.xrLabel10.BeforePrint += new BeforePrintEventHandler(this.xrLabel10_BeforePrint);
			// 
			// xrLabel12
			// 
			this.xrLabel12.CanGrow = false;
			this.xrLabel12.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[Store.State]")});
			this.xrLabel12.LocationFloat = new PointFloat(16.76451F, 53.83326F);
			this.xrLabel12.Multiline = true;
			this.xrLabel12.Name = "xrLabel12";
			this.xrLabel12.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel12.SizeF = new SizeF(288.8608F, 17F);
			this.xrLabel12.StyleName = "style3";
			this.xrLabel12.StylePriority.UseFont = false;
			this.xrLabel12.StylePriority.UseTextAlignment = false;
			this.xrLabel12.Text = "City, State or Province Code, Postal Code";
			// 
			// xrLabel11
			// 
			this.xrLabel11.CanGrow = false;
			this.xrLabel11.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[Store.Line]")});
			this.xrLabel11.LocationFloat = new PointFloat(16.76451F, 27.33326F);
			this.xrLabel11.Multiline = true;
			this.xrLabel11.Name = "xrLabel11";
			this.xrLabel11.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel11.SizeF = new SizeF(288.8608F, 12.68345F);
			this.xrLabel11.StyleName = "style2";
			this.xrLabel11.StylePriority.UseFont = false;
			this.xrLabel11.StylePriority.UseTextAlignment = false;
			this.xrLabel11.Text = "StreetLine1";
			// 
			// xrLabel3
			// 
			this.xrLabel3.CanGrow = false;
			this.xrLabel3.LocationFloat = new PointFloat(7.915497E-05F, 0F);
			this.xrLabel3.Name = "xrLabel3";
			this.xrLabel3.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel3.SizeF = new SizeF(16.76448F, 12F);
			this.xrLabel3.StyleName = "style1";
			this.xrLabel3.StylePriority.UseFont = false;
			this.xrLabel3.StylePriority.UseTextAlignment = false;
			this.xrLabel3.Text = "TO";
			// 
			// xrLabel13
			// 
			this.xrLabel13.CanGrow = false;
			this.xrLabel13.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[Store.Phone]")});
			this.xrLabel13.LocationFloat = new PointFloat(16.76451F, 76.00003F);
			this.xrLabel13.Multiline = true;
			this.xrLabel13.Name = "xrLabel13";
			this.xrLabel13.Padding = new PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel13.SizeF = new SizeF(140.9881F, 11.58334F);
			this.xrLabel13.StyleName = "style2";
			this.xrLabel13.StylePriority.UseFont = false;
			this.xrLabel13.StylePriority.UseTextAlignment = false;
			this.xrLabel13.Text = "Phone";
			// 
			// SubBand3
			// 
			this.SubBand3.Controls.AddRange(new XRControl[] {
				this.xrPanel4});
			this.SubBand3.HeightF = 180F;
			this.SubBand3.Name = "SubBand3";
			this.SubBand3.BeforePrint += new BeforePrintEventHandler(this.SubBand3_BeforePrint);
			// 
			// xrPanel4
			// 
			this.xrPanel4.Borders = BorderSide.Bottom;
			this.xrPanel4.CanGrow = false;
			this.xrPanel4.Controls.AddRange(new XRControl[] {
				this.xrPictureBoxFedEx,
				this.xrLabel21,
				this.xrBarCode2,
				this.xrPictureBoxUPS,
				this.xrLabel20,
				this.xrPictureBoxDLH});
			this.xrPanel4.LocationFloat = new PointFloat(7.999999F, 0F);
			this.xrPanel4.Name = "xrPanel4";
			this.xrPanel4.SizeF = new SizeF(385.9812F, 180F);
			this.xrPanel4.StylePriority.UseBorders = false;
			// 
			// xrPictureBoxFedEx
			// 
			this.xrPictureBoxFedEx.Borders = BorderSide.None;
			this.xrPictureBoxFedEx.ImageSource = new ImageSource("img", resources.GetString("xrPictureBoxFedEx.ImageSource"));
			this.xrPictureBoxFedEx.LocationFloat = new PointFloat(303.46F, 2.98F);
			this.xrPictureBoxFedEx.Name = "xrPictureBoxFedEx";
			this.xrPictureBoxFedEx.SizeF = new SizeF(78.53894F, 58.48613F);
			this.xrPictureBoxFedEx.Sizing = ImageSizeMode.ZoomImage;
			this.xrPictureBoxFedEx.StylePriority.UseBorders = false;
			this.xrPictureBoxFedEx.Visible = false;
			// 
			// xrLabel21
			// 
			this.xrLabel21.Angle = 90F;
			this.xrLabel21.CanGrow = false;
			this.xrLabel21.Font = new DXFont("Arial Narrow", 6F);
			this.xrLabel21.LocationFloat = new PointFloat(374.8799F, 76.0692F);
			this.xrLabel21.Name = "xrLabel21";
			this.xrLabel21.SizeF = new SizeF(11.10132F, 73.7997F);
			this.xrLabel21.StyleName = "style1";
			this.xrLabel21.StylePriority.UseBorders = false;
			this.xrLabel21.StylePriority.UseFont = false;
			this.xrLabel21.StylePriority.UsePadding = false;
			this.xrLabel21.StylePriority.UseTextAlignment = false;
			this.xrLabel21.Text = "J11201104290125";
			this.xrLabel21.TextAlignment = TextAlignment.MiddleCenter;
			// 
			// xrBarCode2
			// 
			this.xrBarCode2.AutoModule = true;
			this.xrBarCode2.Borders = BorderSide.None;
			this.xrBarCode2.ExpressionBindings.AddRange(new ExpressionBinding[] {
				new ExpressionBinding("BeforePrint", "Text", "[Id]")});
			this.xrBarCode2.LocationFloat = new PointFloat(5F, 2.976193F);
			this.xrBarCode2.Name = "xrBarCode2";
			this.xrBarCode2.Padding = new PaddingInfo(0, 0, 5, 5, 100F);
			this.xrBarCode2.ShowText = false;
			this.xrBarCode2.SizeF = new SizeF(291.3011F, 142.0816F);
			this.xrBarCode2.StylePriority.UseBorders = false;
			this.xrBarCode2.StylePriority.UsePadding = false;
			pdF417Generator1.Rows = 9;
			this.xrBarCode2.Symbology = pdF417Generator1;
			// 
			// xrPictureBoxUPS
			// 
			this.xrPictureBoxUPS.Borders = BorderSide.None;
			this.xrPictureBoxUPS.ImageSource = new ImageSource("img", resources.GetString("xrPictureBoxUPS.ImageSource"));
			this.xrPictureBoxUPS.LocationFloat = new PointFloat(303.96F, 7.89F);
			this.xrPictureBoxUPS.Name = "xrPictureBoxUPS";
			this.xrPictureBoxUPS.SizeF = new SizeF(82.01926F, 60.45258F);
			this.xrPictureBoxUPS.Sizing = ImageSizeMode.ZoomImage;
			this.xrPictureBoxUPS.StylePriority.UseBorders = false;
			this.xrPictureBoxUPS.Visible = false;
			// 
			// xrLabel20
			// 
			this.xrLabel20.Borders = ((BorderSide)((((BorderSide.Left | BorderSide.Top) 
			                                         | BorderSide.Right) 
			                                        | BorderSide.Bottom)));
			this.xrLabel20.BorderWidth = 7F;
			this.xrLabel20.CanGrow = false;
			this.xrLabel20.Font = new DXFont("Arial", 44F, DXFontStyle.Bold);
			this.xrLabel20.LocationFloat = new PointFloat(314.9727F, 84.80755F);
			this.xrLabel20.Name = "xrLabel20";
			this.xrLabel20.Padding = new PaddingInfo(0, 0, 0, 0, 100F);
			this.xrLabel20.SizeF = new SizeF(59.90723F, 57.27404F);
			this.xrLabel20.StylePriority.UseBorders = false;
			this.xrLabel20.StylePriority.UseBorderWidth = false;
			this.xrLabel20.StylePriority.UseFont = false;
			this.xrLabel20.StylePriority.UsePadding = false;
			this.xrLabel20.StylePriority.UseTextAlignment = false;
			this.xrLabel20.Text = "G";
			this.xrLabel20.TextAlignment = TextAlignment.MiddleCenter;
			// 
			// xrPictureBoxDLH
			// 
			this.xrPictureBoxDLH.Borders = BorderSide.None;
			this.xrPictureBoxDLH.ImageSource = new ImageSource("img", resources.GetString("xrPictureBoxDLH.ImageSource"));
			this.xrPictureBoxDLH.LocationFloat = new PointFloat(303.4611F, 2.976193F);
			this.xrPictureBoxDLH.Name = "xrPictureBoxDLH";
			this.xrPictureBoxDLH.SizeF = new SizeF(78.53894F, 58.48614F);
			this.xrPictureBoxDLH.Sizing = ImageSizeMode.ZoomImage;
			this.xrPictureBoxDLH.StylePriority.UseBorders = false;
			this.xrPictureBoxDLH.Visible = false;
			// 
			// SubBand4
			// 
			this.SubBand4.Controls.AddRange(new XRControl[] {
				this.xrBarCode1});
			this.SubBand4.HeightF = 225.2261F;
			this.SubBand4.Name = "SubBand4";
			// 
			// xrBarCode1
			// 
			this.xrBarCode1.AutoModule = true;
			this.xrBarCode1.LocationFloat = new PointFloat(8.000079F, 10F);
			this.xrBarCode1.Name = "xrBarCode1";
			this.xrBarCode1.Padding = new PaddingInfo(10, 10, 10, 10, 100F);
			this.xrBarCode1.SizeF = new SizeF(385.9811F, 131.8155F);
			this.xrBarCode1.StyleName = "style2";
			this.xrBarCode1.StylePriority.UseFont = false;
			this.xrBarCode1.StylePriority.UsePadding = false;
			this.xrBarCode1.StylePriority.UseTextAlignment = false;
			eaN128Generator1.HumanReadableText = false;
			this.xrBarCode1.Symbology = eaN128Generator1;
			this.xrBarCode1.Text = "9622001670008130914600794801750435";
			this.xrBarCode1.TextAlignment = TextAlignment.TopCenter;
			// 
			// bottomMarginBand1
			// 
			this.bottomMarginBand1.HeightF = 0F;
			this.bottomMarginBand1.Name = "bottomMarginBand1";
			// 
			// style1
			// 
			this.style1.Borders = BorderSide.None;
			this.style1.Font = new DXFont("Arial Narrow", 8F);
			this.style1.Name = "style1";
			this.style1.TextAlignment = TextAlignment.TopLeft;
			// 
			// style2
			// 
			this.style2.Borders = BorderSide.None;
			this.style2.Font = new DXFont("Arial", 8F, DXFontStyle.Bold);
			this.style2.Name = "style2";
			this.style2.TextAlignment = TextAlignment.TopLeft;
			// 
			// style3
			// 
			this.style3.Borders = BorderSide.None;
			this.style3.Font = new DXFont("Arial", 10F, DXFontStyle.Bold);
			this.style3.Name = "style3";
			this.style3.TextAlignment = TextAlignment.TopLeft;
			// 
			// bindingSource1
			// 
			this.bindingSource1.Name = "bindingSource1";
			this.bindingSource1.ObjectTypeName = "OutlookInspired.Module.BusinessObjects.Order";
			this.bindingSource1.TopReturnedRecords = 1;
			// 
			// barcodeData
			// 
			this.barcodeData.Expression = "\'Id \' + [Id] + \' InvoiceNumber \'  + [InvoiceNumber] + \' OrderDate \' + [OrderDate]" +
			                              " + \' OrderTerms \' + [OrderTerms] + \' StoreId \' +[StoreId] + \' Customer Name \' + " +
			                              "[Customer.Name]";
			this.barcodeData.FieldType = FieldType.String;
			this.barcodeData.Name = "barcodeData";
			// 
			// FedExGroundLabel
			// 
			this.Bands.AddRange(new Band[] {
				this.topMarginBand1,
				this.detailBand1,
				this.bottomMarginBand1});
			this.CalculatedFields.AddRange(new CalculatedField[] {
				this.barcodeData});
			this.DataSource = this.bindingSource1;
			this.Margins = new DXMargins(0F, 0F, 0F, 0F);
			this.PageHeight = 600;
			this.PageWidth = 400;
			this.PaperKind = DXPaperKind.Custom;
			this.StyleSheet.AddRange(new XRControlStyle[] {
				this.style1,
				this.style2,
				this.style3});
			this.Version = "23.1";
			((ISupportInitialize)(this.bindingSource1)).EndInit();
			((ISupportInitialize)(this)).EndInit();

		}
		private void xrLabel16_BeforePrint(object sender, CancelEventArgs e) {
			XRLabel label = (XRLabel)sender;
			label.Text = label.Text.ToUpper();
		}
		
		private void xrLabelFromStreetLine1_BeforePrint(object sender, CancelEventArgs e) {
			// ((XRLabel)sender).Text = AddressHelper.DevAVHomeOffice.Line;
		}
		
		private void xrLabelFromStreetLine2_BeforePrint(object sender, CancelEventArgs e) {
			// ((XRLabel)sender).Text = AddressHelper.DevAVHomeOffice.CityLine;
		}
		
		private void xrLabelFromState_BeforePrint(object sender, CancelEventArgs e) {
			// ((XRLabel)sender).Text = AddressHelper.DevAVHomeOffice.State.ToString();
		}

		#endregion
		
		private void SubBand3_BeforePrint(object sender, CancelEventArgs e) {
			var shipmentCourier = (ShipmentCourier)GetCurrentColumnValue(nameof(Order.ShipmentCourier));
			switch(shipmentCourier) {
				case ShipmentCourier.DHL:
					xrPictureBoxDLH.Visible = true;
					break;
				case ShipmentCourier.FedEx:
					xrPictureBoxFedEx.Visible = true;
					break;
				case ShipmentCourier.UPS:
					xrPictureBoxUPS.Visible = true;
					break;
			}
		}
		
		private void xrLabel10_BeforePrint(object sender, CancelEventArgs e) {
			var currentStore = (CustomerStore)GetCurrentColumnValue(nameof(Order.Store));
			var storeIndex = currentStore.Customer.CustomerStores.IndexOf(currentStore);
			((XRLabel)sender).Text = currentStore.Customer.Employees[storeIndex].FullName;
		}
	}
}
