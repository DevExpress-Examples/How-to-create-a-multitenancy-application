using DevExpress.ExpressApp.Blazor.Services;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace OutlookInspired.Blazor.Server.Services;

internal class CircuitHandlerProxy(IScopedCircuitHandler scopedCircuitHandler) : CircuitHandler{
    public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken) 
        => scopedCircuitHandler.OnCircuitOpenedAsync(cancellationToken);

    public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken) 
        => scopedCircuitHandler.OnConnectionUpAsync(cancellationToken);

    public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken) 
        => scopedCircuitHandler.OnCircuitClosedAsync(cancellationToken);

    public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken) 
        => scopedCircuitHandler.OnConnectionDownAsync(cancellationToken);
}
