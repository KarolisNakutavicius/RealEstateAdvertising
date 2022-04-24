using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.InputModels;

public class CreateAdvertisementRequest
{
    public string Name { get; set; } = string.Empty;

    public bool IsRent { get; set; }

    public BuildingType Type { get; set; }

    public string Description { get; set; } = string.Empty;

    public int Number { get; set; }

    public string City { get; set; } = string.Empty;

    public string Zip { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;


    [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid size")]
    public int Size { get; set; }

    [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid price")]
    public decimal Price { get; set; }

    public IList<IFormFile> Files { get; set; }
}