using Domain.Enums;

namespace Application.DTOs.ViewModels;

public class AdvertisementResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsRent { get; set; }

    public BuildingType Type { get; set; }

    public string Description { get; set; } = string.Empty;

    public int Number { get; set; }

    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string Zip { get; set; } = string.Empty;

    public int BuildingSize { get; set; }
    
    public int PlotSize { get; set; }

    public decimal Price { get; set; }

    public byte[]? Image { get; set; } = Array.Empty<byte>();

    public string OwnerEmail { get; set; } = string.Empty;
}