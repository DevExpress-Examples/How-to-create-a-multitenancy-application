using System.Text.Json;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Components.DevExtreme.Maps{
    public class DxVectorMapModel : MapModel<DxVectorMap>{
        public event EventHandler<MapItemSelectedArgs> MapItemSelected;
        public void SelectMapItem(JsonElement item) 
            => MapItemSelected?.Invoke(this, new MapItemSelectedArgs(item));
        public VectorMapOptions Options{ get; set; } = new();
        
        public BaseLayer LayerDatasource{
            get => GetPropertyValue<BaseLayer>();
            set => SetPropertyValue(value);
        }
        
        public bool Redraw{
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
    }
    
    public class MapItemSelectedArgs(JsonElement item) : EventArgs{
        public JsonElement Item{ get; } = item;
    }
    
    public class VectorMapOptions{
        public int Zoom{ get; set; } = 16;
        public string Height{ get; set; } = "100%";
        public string Width{ get; set; } = "100%";
        public string Provider{ get; set; } = "bing";
        public ApiKey ApiKey{ get; set; } = new();
        public List<object> Layers{ get;  } =[new PredefinedLayer{ DataSource = "DevExpress.viz.map.sources.usa" }];
        public Tooltip Tooltip{ get; set; } = new();
        public double[] Bounds{ get; set; }
        public string[] Attributes{ get; set; }
        
        public List<Annotation> Annotations{ get; } = new();
    }
    
    public class BaseLayer{
        public object DataSource{ get; set; }
    }
    
    public class PredefinedLayer:BaseLayer{
        public bool HoverEnabled{ get; set; }
        
    }

    public interface IPaletteLayer{
        string[] Palette{ get; set; }
    }

    public interface INamedLayer{
        string Name{ get; set; }
    }

    public class BubbleLayer:BaseLayer, IPaletteLayer, INamedLayer{
        public string SelectionMode{ get; set; } = "single";
        public string Name{ get; set; } = "bubbles";
        public string ElementType{ get; } = "bubble";
        public string DataField{ get; set; } = nameof(Properties.Values).FirstCharacterToLower();
        public string[] Palette{ get; set; }
        public int MinSize{ get; init; } = 20;
        public int MaxSize{ get; init; } = 40;
        public double Opacity{ get; init; } = 0.8;
         
    }

    public class Annotation{
        public string Width{ get; set; } = "100";
        public string Height{ get; set; } = "50";
        public double[] Coordinates{ get; set; }
        public object Data{ get; set; }
    }
    
    public class PieLayer:BaseLayer, IPaletteLayer, INamedLayer{
        public string SelectionMode{ get; set; }= "single";
        public string Name{ get; set; } = "pies";
        public string ElementType{ get; } = "pie"; 
        public string DataField{ get; set; }= nameof(Properties.Values).FirstCharacterToLower();
        public string[] Palette{ get; set; }
    }
    
    public class Tooltip{
        public bool Enabled{ get; set; }
        public int ZIndex{ get; set; }
    }
    
    public class FeatureCollection{
        public string Type{ get; set; } = "FeatureCollection";
        public List<Feature> Features{ get; set; }
        
    }

    public class Feature{
        public string Type{ get; set; } = "Feature";
        public Geometry Geometry{ get; set; }
        public Properties Properties{ get; set; }
    }

    public class Geometry{
        public string Type{ get; set; } = "Point";
        public List<double> Coordinates{ get; set; }
    }

    public class Properties{
        public string Tooltip{ get; set; }
        public List<decimal> Values{ get; set; }
        public string City{ get; set; }
    }
}