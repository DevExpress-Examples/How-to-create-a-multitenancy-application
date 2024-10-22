using Microsoft.AspNetCore.Components;
using OutlookInspired.Module.Services.Internal;


namespace OutlookInspired.Blazor.Server.Editors.Maps{

    public class DevExtremeVectorMapModel:DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase{
        
        public override Type ComponentType => typeof(DevExtremeMap);

        public IEnumerable<BaseLayer> Layers{
            get => GetPropertyValue<IEnumerable<BaseLayer>>();
            set => SetPropertyValue(value);
        }
        public double[] Bounds{
            get => GetPropertyValue<double[]>();
            set => SetPropertyValue(value);
        }
        public string[] CustomAttributes{
            get => GetPropertyValue<string[]>();
            set => SetPropertyValue(value);
        }
        public EventCallback<string[]> SelectionChanged{
            get => GetPropertyValue<EventCallback<string[]>>();
            set => SetPropertyValue(value);
        }

        public List<Annotation> Annotations{
            get => GetPropertyValue<List<Annotation>>()??new();
            set => SetPropertyValue(value);
        }
    }
    
    public class BaseLayer{
        public object DataSource{ get; set; }
    }
    
    public class Annotation{
        public string Width{ get; set; } = "100";
        public string Height{ get; set; } = "50";
        public double[] Coordinates{ get; set; }
        public object Data{ get; set; }
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
    
    public class PieLayer:BaseLayer{
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
    
    public class BubbleLayer:BaseLayer{
        public string SelectionMode{ get; set; } = "single";
        public string Name{ get; set; } = "bubbles";
        public string ElementType{ get; } = "bubble";
        public string DataField{ get; set; } = nameof(Properties.Values).FirstCharacterToLower();
        public string Color{ get; set; }
        public int MinSize{ get; init; } = 20;
        public int MaxSize{ get; init; } = 40;
        public double Opacity{ get; init; } = 0.8;
         
    }

    public class PredefinedLayer:BaseLayer{
        public bool HoverEnabled{ get; set; }

        
        public double[] Bounds(){
            switch (DataSource){
                case "usa":
                    return[-124.566244, 49.384358, -66.934570, 24.396308];
                
            }

            throw new InvalidOperationException(DataSource.ToString());
        }
    }


}