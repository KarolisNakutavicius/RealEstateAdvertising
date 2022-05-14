using Domain.Entities;

namespace Domain.ValueObjects;

public class Address
{
    public string Street { get; private init; } = string.Empty;
    public int Number { get; private init; }
    public City City { get; init; } = new();
    public string Zip { get; private init; } = string.Empty;

    public static Address CreateNew(string street, int number, City city, string zip)
    {
        return new Address
        {
            Street = street,
            Number = number,
            City = city,
            Zip = zip
        };
    }

    public override bool Equals(object? obj)
    {
        if (this == obj) return true;

        if (GetType() != obj?.GetType()) return false;

        var other = (Address)obj;

        return string.Equals(Street, other.Street) &&
               Equals(Number, other.Number) &&
               Equals(City, other.City) &&
               string.Equals(Zip, other.Zip);
    }

    public override int GetHashCode()
    {
        const int hashIndex = 307;
        var result = Street != null ? Street.GetHashCode() : 0;
        result = (result * hashIndex) ^ Number.GetHashCode();
        result = (result * hashIndex) ^ City.GetHashCode();
        result = (result * hashIndex) ^ Zip.GetHashCode();
        return result;
    }
}