namespace Application.DTOs.InputModels;

public class FilterRequest
{
    public int? MinPrice { get; set; }

    public int? MaxPrice { get; set; }

    public int? CityId { get; set; }
}