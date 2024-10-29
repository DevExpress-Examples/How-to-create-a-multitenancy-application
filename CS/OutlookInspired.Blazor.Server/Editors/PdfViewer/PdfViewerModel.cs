﻿using DevExpress.Blazor.PdfViewer;
using DevExpress.Blazor.Reporting.Models;
using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components;

namespace OutlookInspired.Blazor.Server.Editors.PdfViewer {
    public class PdfViewerModel : ComponentModelBase {
        public byte[] DocumentContent {
            get => GetPropertyValue<byte[]>();
            set => SetPropertyValue(value);
        }
        public string CssClass {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public bool IsSinglePagePreview {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
        public EventCallback<ToolbarModel> CustomizeToolbar {
            get => GetPropertyValue<EventCallback<ToolbarModel>>();
            set => SetPropertyValue(value);
        }
        public override Type ComponentType => typeof(DxPdfViewer);
    }
}