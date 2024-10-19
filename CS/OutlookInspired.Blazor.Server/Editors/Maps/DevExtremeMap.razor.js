const dataSourceMap = {
    'usa': DevExpress.viz.map.sources.usa,
};

export function addMapToElement(element, layers,bounds,attributes, dotNetHelper) {
    layers[0].dataSource=dataSourceMap[layers[0].dataSource]
    return  $(element).dxVectorMap({
        layers:layers,
        bounds:bounds,
        tooltip : {
            enabled: true,
            zIndex:10000,
            customizeTooltip: function (arg) {
                return arg.layer.type === 'marker' ? {text: arg.attribute('tooltip')} : null;
            }
        },
        onClick: arg => {
            const clickedElement = arg.target;
            if (clickedElement != null) {
                clickedElement.selected(!clickedElement.selected());
                dotNetHelper.invokeMethodAsync('OnSelectionChanged', attributes.map(f => clickedElement.attribute(f)).filter(val => val != null));
            }
        }
    }).dxVectorMap('instance');
}

export function dispose(element) {
    debugger;
    if (!element) return;
    $(element).dxVectorMap('dispose');
}