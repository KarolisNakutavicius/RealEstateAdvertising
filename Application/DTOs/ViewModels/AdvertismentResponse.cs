using Domain.Enums;

namespace Application.DTOs.ViewModels
{
    public class AdvertismentResponse
    {
        public string Name { get; set; }

        public bool IsRent { get; set; }

        public BuildingType Type { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public int Size { get; set; }
    }
}
