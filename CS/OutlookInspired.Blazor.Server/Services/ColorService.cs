namespace OutlookInspired.Blazor.Server.Services{
    public interface IColorService{
        List<string> AppColors{ get; }
    }

    public class ColorService : IColorService{
        public List<string> AppColors { get; } =[
            "#FF5733", // Orange-red 
            "#33FF57", // Green
            "#3357FF", // Blue
            "#FF33F5", // Magenta
            "#F5FF33", // Yellow
            "#CCCCCC", // Light Gray 
            "#FFB347", // Light Orange
            "#77DD77", // Soft Green
            "#CFCFC4"
        ];
    }}