using System.Text.Json;
using System.Text.RegularExpressions;
using DevExpress.ExpressApp;
using DevExpress.Map.Native;
using DevExpress.Persistent.Base;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Services.Internal;
using Route = OutlookInspired.Blazor.Server.Components.DevExtreme.Maps.Route;

namespace OutlookInspired.Blazor.Server.Services.Internal{
    internal static class MapExtensions{
        private static readonly Regex RemoveTagRegex = new(@"<[^>]*>", RegexOptions.Compiled);
        public static MapItem[] Colorize(this MapItem[] mapItems, string[] palette,Type markerType) 
            => mapItems.GroupBy(item => item.PropertyValue(markerType))
                .SelectMany((items, i) => items.Do(item => item.Color = palette[i])).ToArray();

        public static object FeatureCollection(this IMapItem[] mapItems,Func<IGrouping<string,IMapItem>,List<decimal>> valuesSelector) 
            => new FeatureCollection{ Features = mapItems.Features(valuesSelector) };

        public static VectorMapOptions VectorMapOptions<TMapItem, TLayer>(this TMapItem[] mapItems,
            string[] palette,Func<IGrouping<string,IMapItem>,List<decimal>> valuesSelector) where TMapItem : IMapItem where TLayer : BaseLayer,IPaletteLayer,INamedLayer, new() 
            => new(){
                Layers = {new TLayer(){
                    DataSource = mapItems.Cast<IMapItem>().ToArray().FeatureCollection(valuesSelector), Palette =palette
                }},
                Bounds =mapItems.Bounds(),Tooltip = {Enabled = true,ZIndex = 10000},
                Attributes=new[]{nameof(IMapItem.City).FirstCharacterToLower()}
            };

        public static List<Feature> Features(this IMapItem[] mapItems,Func<IGrouping<string,IMapItem>,List<decimal>> valuesSelector) 
            => mapItems.GroupBy(item => item.City).Select(group => group.First().NewFeature(group,valuesSelector)).ToList();

        private static Feature NewFeature(this IMapItem mapItem, IGrouping<string, IMapItem> group,Func<IGrouping<string,IMapItem>,List<decimal>> valuesSelector) 
            => new(){
                Geometry = new Geometry{ Coordinates = new List<double>{ mapItem.Longitude, mapItem.Latitude } },
                Properties = new Properties{
                    Values = valuesSelector(group),
                    Tooltip = $"<span class='{mapItem.City}'>{mapItem.City} Total: {group.Sum(item => item.Total)}</span>",
                    City = mapItem.City
                }
            };

        public static async Task<RouteCalculatedArgs> ManeuverInstructions(this IObjectSpace objectSpace,
            IMapsMarker locationA, IMapsMarker locationB, string travelMode, string apiKey){
            throw new NotImplementedException();
        }

        public static async Task<RouteCalculatedArgs> ManeuverInstructions(this IObjectSpace objectSpace, Location locationA, Location locationB, string travelMode, string apiKey){
            var url = $"https://dev.virtualearth.net/REST/V1/Routes/{travelMode}?wp.0={locationA.Lat},{locationA.Lng}&wp.1={locationB.Lat},{locationB.Lng}&key={apiKey}";
            using var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.GetAsync(url);
            if (httpResponseMessage.IsSuccessStatusCode){
                var jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(jsonString);
                var result = jsonDoc.RootElement.GetProperty("resourceSets").EnumerateArray().First().GetProperty("resources").EnumerateArray().First();
                var routeLegs = result.GetProperty("routeLegs").EnumerateArray().SelectMany(leg => leg.GetProperty("itineraryItems").EnumerateArray());
                var routePoints = routeLegs.Select(objectSpace.RoutePoint).ToArray();
                var travelDistance = result.GetProperty("travelDistance").GetDouble();
                var travelDuration = result.GetProperty("travelDuration").GetDouble();
                return new RouteCalculatedArgs(routePoints, travelDistance, TimeSpan.FromMinutes(travelDuration), Enum.Parse<TravelMode>(travelMode, true));
            }

            return new RouteCalculatedArgs(Array.Empty<RoutePoint>(), 0, TimeSpan.Zero, Enum.Parse<TravelMode>(travelMode, true));
        }

        private static RoutePoint RoutePoint(this IObjectSpace objectSpace, JsonElement item){
            var point = objectSpace.CreateObject<RoutePoint>();
            point.ManeuverInstruction = RemoveTagRegex.Replace(item.GetProperty("instruction").GetProperty("text").GetString()!, string.Empty);
            var distance = item.GetProperty("travelDistance").GetDouble();
            point.Distance = distance > 0.9 ? $"{Math.Ceiling(distance):0} mi" : $"{Math.Ceiling(distance * 52.8) * 100:0} ft";
            point.Maneuver = Enum.Parse<BingManeuverType>(item.GetProperty("details").EnumerateArray().First().GetProperty("maneuverType").GetString()!);
            return point;
        }

        
        public static DxMapOptions DxMapOptions(this IMapsMarker mapsMarker, IMapsMarker homeOffice, string travelMode){
            var mode = travelMode.FirstCharacterToLower();
            var markers = new[]{
                new Marker{ Location = new Location{ Lat = homeOffice.Latitude, Lng = homeOffice.Longitude } },
                new Marker{ Location = new Location{ Lat = mapsMarker.Latitude, Lng = mapsMarker.Longitude } }
            }.ToList();
            return new DxMapOptions(){Markers =markers,
                Routes = new List<Route>()
                    { new(){Mode =mode,Color = mode=="driving"?"orange":"blue", Locations = markers.Select(marker => marker.Location).ToList() } },
                Controls = true,
                Center = markers.First().Location
            };
        }    
    }
}