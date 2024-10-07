namespace OutlookInspired.Blazor.Server.Components.DevExtreme.Maps{
    public class DxrMapModel : MapModel<DxrMap>{

        public DxMapOptions Options{ get; set; } = new();
        public string RouteMode{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
    }
    
    public class Location{
        public double Lat{ get; init; }
        public double Lng{ get; init; }
    }
    
    public class DxMapOptions{
        public ApiKey ApiKey{ get; set; } = new();
        public string Provider{ get; set; } = "bing";
        public int Zoom{ get; set; } = 16;
        public string Height{ get; set; } = "100vh";
        public string Width{ get; set; } = "100%";
        public string Type{ get; set; } = "roadmap";
        public bool Controls{ get; set; }
        public Location Center{ get; set; }
        public List<Marker> Markers{ get; init; } = new();
        public List<Route> Routes{ get; init; } = new();
    }
    
    public class Marker{
        public Location Location{ get; init; } = new();
    }

    public class Route{
        public int Weight{ get; set; }
        public string Color{ get; set; }
        public double Opacity{ get; set; }
        public string Mode{ get; init; }
        public List<Location> Locations{ get; set; } = new();
    }
}