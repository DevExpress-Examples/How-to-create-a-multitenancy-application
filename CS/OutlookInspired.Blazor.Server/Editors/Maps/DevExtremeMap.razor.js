const dataSourceMap = {
    'usa': DevExpress.viz.map.sources.usa,
};

export function addMapToElement(element, layers, bounds, attributes, annotationData, dotNetHelper) {
    layers[0].dataSource = dataSourceMap[layers[0].dataSource];

    return  $(element).dxVectorMap({
        layers: layers,
        bounds: bounds,
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
        annotations: annotationData,
        commonAnnotationSettings : {
            type: 'custom',
            template: function (annotation, container) {
                const svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
                svg.setAttribute("class", "annotation");

                const foreignObject = document.createElementNS("http://www.w3.org/2000/svg", "foreignObject");
                foreignObject.setAttribute("width", annotation.width);
                foreignObject.setAttribute("height", annotation.height);

                const div = document.createElement("div");
                div.innerHTML = annotation.data;
                div.setAttribute("xmlns", "http://www.w3.org/1999/xhtml");

                foreignObject.appendChild(div);
                svg.appendChild(foreignObject);
                container.appendChild(svg);
            }
        }
    }).dxVectorMap('instance');
    
}

export function dispose(element) {
    if (!element) return;
    $(element).dxVectorMap('dispose');
}