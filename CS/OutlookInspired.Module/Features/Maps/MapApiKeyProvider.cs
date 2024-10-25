namespace OutlookInspired.Module.Features.Maps{
    public class MapApiKeyProvider : IMapApiKeyProvider {
        public string Key => Environment.GetEnvironmentVariable("BingKey");
    }
    
    public interface IMapApiKeyProvider {
        public string Key{ get; }
    }

}