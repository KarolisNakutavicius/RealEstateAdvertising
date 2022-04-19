using Domain.Enums;

namespace Application.DTOs.InputModels
{
    public class CreateAdvertisementRequest
    {
        public string Name { get; set; }

        public bool IsRent { get; set; }

        public BuildingType Type {get;set;}

        public string Description { get; set; }

        public string Number { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public int Size { get; set; }

        public decimal Price { get; set; }
    }
}
