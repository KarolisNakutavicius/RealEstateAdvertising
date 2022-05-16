using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.InputModels;

public class FilterRequest
{
    public int? MinPrice { get; set; }

    public int? MaxPrice { get; set; }

    public int? CityId { get; set; }

    public BuildingType? Type { get; set; }
    
    public bool? IsRent { get; set; }
}