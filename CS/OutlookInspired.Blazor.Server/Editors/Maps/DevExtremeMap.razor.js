const dataSourceMap = {
    'usa': DevExpress.viz.map.sources.usa,
};


export function addMapToElement(element,height, layers, bounds, attributes, annotationData, dotNetHelper) {
    layers[0].dataSource = dataSourceMap[layers[0].dataSource];

    return  $(element).dxVectorMap({
        layers: layers,
        bounds: bounds,
        height: height,
        tooltip: {
            enabled: true,
            zIndex: 10000,
            customizeTooltip: function (arg) {
                return arg.layer.type === 'marker' ? { text: arg.attribute('tooltip') } : null;
            }
        },
        onClick: arg => {
            const clickedElement = arg.target;
            if (clickedElement != null) {
                clickedElement.selected(!clickedElement.selected());
                dotNetHelper.invokeMethodAsync('OnSelectionChanged', attributes.map(f => clickedElement.attribute(f)).filter(val => val != null));
            }
        },
        annotations: annotationData.map(s => { return { coordinates: s.coordinates, text: s.data} }),
        commonAnnotationSettings : {
            type: 'text',
            font:{
                color:"#FFFFFF"
            }
        }
    }).dxVectorMap('instance');
    
}

export function dispose(element) {
    if (!element) return;
    $(element).dxVectorMap('dispose');
}