using System.Text.Json;
using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.DC;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.FileProviders;
using Microsoft.JSInterop;
using OutlookInspired.Module.Attributes;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Services.Internal{
    internal static class Extensions{
        public static void UseComponentStaticFiles(this IApplicationBuilder builder, IWebHostEnvironment environment){
            var root = $"{environment.ContentRootPath}/{Components.ComponentBase.JsPath}";
            if (!Directory.Exists(root)){
                Directory.CreateDirectory(root);
            }
            builder.UseStaticFiles(new StaticFileOptions{
                FileProvider = new PhysicalFileProvider(root),
                RequestPath =  $"/{Components.ComponentBase.JsPath}"
            });
        }

        public static async Task<string> ModalBodyHeight(this IJSRuntime js){
            await js.EvalAsync(@"window.getClientHeight = (element) => {
        const startTime = new Date().getTime();
        let elem = null;
        let clientHeight = null;

        while (new Date().getTime() - startTime < 1000) {
            elem = document.querySelector(element);
            if (elem) {
                clientHeight = elem.closest('.dxbl-modal-body')?.clientHeight;
                if (clientHeight) break;
            }
        }

        return clientHeight;
    };");

            var result = await js.InvokeAsync<object>("getClientHeight", ".dxbl-modal-body");
            return result?.ToString();
        }
        
        public static RenderFragment BootFragment(this Evaluation evaluation,Func<Evaluation,Enum> boost ) 
            => builder =>{
                builder.OpenComponent(2, typeof(XafImage));
                builder.AddAttribute(3, "ImageName", boost(evaluation).ImageInfo().ImageName);
                builder.AddAttribute(4, "Size", 16);
                builder.AddAttribute(5,"Color", Convert.ToInt32(boost(evaluation))==0?"red":"green");
                builder.CloseComponent();
            };

        public static async ValueTask AddGridColumnTextOverflow(this IJSRuntime runtime, bool firstRender, string classname) 
            => await runtime.EvalAsync(firstRender, $@"
                    let attempts = 0;
                    function findCells() {{
                        attempts++;
                        let cells = document.querySelectorAll('.{classname}');
                        if (cells.length > 0 || attempts >= 10) {{
                            clearInterval(intervalId);
                        }}
                        cells.forEach((cell, index) => {{
                            let parentTd = cell.closest('td');  
                            let parentTdWidth = parentTd ? parentTd.offsetWidth : '100%';  
                            cell.style.maxWidth = parentTdWidth + 'px';  
                            cell.style.whiteSpace = 'nowrap';
                            cell.style.overflow = 'hidden';
                            cell.style.textOverflow = 'ellipsis';
                            cell.style.visibility = 'visible';
                        }});
                    }}
                    let intervalId = setInterval(findCells, 100);");

        public static string FontSize(this GridDataColumnCellDisplayTemplateContext context) 
            => ((IObjectSpaceLink)context.DataItem).ObjectSpace.TypesInfo.FindTypeInfo(typeof(Evaluation))
            .FindMember(context.DataColumn.FieldName)
            .FontSize();

        public static string FontSize(this IMemberInfo info){
            var fontSizeDeltaAttribute = info.FindAttribute<FontSizeDeltaAttribute>();
            if (fontSizeDeltaAttribute == null) return null;
            var size = (fontSizeDeltaAttribute.Delta == 8 ? "1.8" : "1.2");
            return $"line-height: {size}rem;font-size: {size}rem";
        }
        public static async ValueTask EvalAsync(this IJSRuntime runtime,bool firstRender,params object[] args){
            if (firstRender){
                await runtime.InvokeVoidAsync("eval", args);
            }
        }

        public static async ValueTask Console(this IJSRuntime runtime, Action<JsonElement> onError){
            await runtime.EvalAsync(@"
window.registerErrorBroker = function(errorBroker) {
    window.errorBroker = errorBroker;
};");
            await runtime.InvokeVoidAsync("registerErrorBroker", new JsInterop(onError).DotNetReference());
            await runtime.EvalAsync(@"
const originalConsoleWarn = console.warn;
console.warn = function(...args) {
  window.errorBroker.invokeMethodAsync('Invoke', args);
  originalConsoleWarn.apply(console, args);
};");
        }
        
        public static async ValueTask EvalAsync(this IJSRuntime runtime,params object[] args) 
            => await runtime.EvalAsync(true, args);
        public static DotNetObjectReference<T> DotNetReference<T>(this T value) where T:class 
            => DotNetObjectReference.Create(value);
        
        public static RenderFragment Create<T>(this IComponentModel model)  
            => builder => {
                builder.OpenComponent(0, typeof(T));
                builder.AddAttribute(1, "ComponentModel", model);
                builder.CloseComponent();
            };
        
        [Obsolete]
        public static RenderFragment Create<T>(this T componentModel,Func<T,RenderFragment> fragmentSelector) where T:IComponentModel 
            => ComponentModelObserver.Create(componentModel, fragmentSelector(componentModel));
        [Obsolete]
        public static RenderFragment Create<T>(this T componentModel)  where T:IComponentModelRenderable
            => componentModel.Create(arg => arg.GetComponentContent());
        
    }
}