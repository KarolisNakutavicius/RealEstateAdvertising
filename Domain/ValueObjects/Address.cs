namespace Domain.ValueObjects;

public class Address
{
    public static Address CreateNew(string street, string number, string city, string zip)
    {
        return new Address
        {
            Street = street,
            Number = number,
            City = city,
            Zip = zip,
        };

    }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string City { get; private set; }
    public string Zip { get; private set; }

    public override bool Equals(object obj)
    {
        if (this == obj)
            return true;
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (Address)obj;
        return string.Equals(Street, other.Street) &&
               string.Equals(Number, other.Number) &&
               string.Equals(City, other.City) &&
               string.Equals(Zip, other.Zip);
    }

    public override int GetHashCode()
    {
        const int hashIndex = 307;
        var result = Street != null ? Street.GetHashCode() : 0;
        result = (result * hashIndex) ^ (Number != null ? Number.GetHashCode() : 0);
        result = (result * hashIndex) ^ (City != null ? City.GetHashCode() : 0);
        result = (result * hashIndex) ^ (Zip != null ? Zip.GetHashCode() : 0);
        return result;
    }
}