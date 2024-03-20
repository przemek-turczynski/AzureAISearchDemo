using Newtonsoft.Json;

namespace Domain;

public class Hotel
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    public int HotelId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string? Description_pl { get; set; }

    public string? Description_fa { get; set; }

    public string Category { get; set; } = string.Empty;

    public IEnumerable<string> Tags { get; set; } = [];

    public bool ParkingIncluded { get; set; }

    public DateTime LastRenovationDate { get; set; }

    public double Rating { get; set; }

    public Address Address { get; set; } = null!;

    public Location Location { get; set; } = null!;
}
