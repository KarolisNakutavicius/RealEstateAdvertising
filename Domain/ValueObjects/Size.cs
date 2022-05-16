namespace Domain.ValueObjects;

public class Size
{
    public int PlotSize { get; set; }
    
    public int BuildingSize { get; set; }

    public string MeasurementUnit { get; set; } = "m²";

    public static Size CreateNew(int buildingSize, int plotSize)
    {
        return new Size
        {
            PlotSize = plotSize,
            BuildingSize = buildingSize
        };
    }
}