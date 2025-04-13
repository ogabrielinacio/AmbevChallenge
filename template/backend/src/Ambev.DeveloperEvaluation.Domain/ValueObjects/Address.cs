namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    
    public string Country {get; private set; }
    public string ZipCode { get; private set; }

    protected Address() { }

    public Address(string street, string city, string state, string country, string zip)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zip;
    } 
}