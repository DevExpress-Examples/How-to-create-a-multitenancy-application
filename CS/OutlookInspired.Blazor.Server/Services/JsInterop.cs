using System.Text.Json;
using Microsoft.JSInterop;

namespace OutlookInspired.Blazor.Server.Services{
    [Obsolete]
    public class JsInterop {
        private readonly Action<JsonElement> _action;
        public JsInterop(Action<JsonElement> action) => _action = action;

        [JSInvokable]
        public void Invoke(JsonElement param) => _action.Invoke(param);
    }
}