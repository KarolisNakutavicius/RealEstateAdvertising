using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.InputModels;

public class CreateAdvertisementRequest
{
    [Required(ErrorMessage = $"{nameof(Name)} is required")]
    public string Name { get; set; } = string.Empty;

    public bool IsRent { get; set; }

    public BuildingType Type { get; set; }

    public string Description { get; set; } = string.Empty;

    [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid number")]
    public int Number { get; set; }

    [Required(ErrorMessage = $"{nameof(City)} is required")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(Zip)} is required")]
    public string Zip { get; set; } = string.Empty;

    [Required(ErrorMessage = $"{nameof(Street)} is required")]
    public string Street { get; set; } = string.Empty;


    [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid size")]
    public int BuildingSize { get; set; }
    
    [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid size")]
    public int PlotSize { get; set; }

    [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid price")]
    public decimal Price { get; set; }

    public IList<IFormFile> Files { get; set; } = new List<IFormFile>();
}