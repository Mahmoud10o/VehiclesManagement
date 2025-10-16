namespace VehiclesManagement.Models;

public class Motorcycle : Vehicle
{
    public bool HasSidecar { get; set; }
    public BikeType BikeType { get; set; }
    public override decimal RentalPrice()
    {
        decimal basePrice = 50m;
        basePrice += BikeType switch
        {
            BikeType.Cruiser => 40m,
            BikeType.Sport => 50m,
            BikeType.Dirt => 100m,
            _ => 30m
        };
        basePrice += HasSidecar ? 100m : 0;
        return basePrice;

    }


    public override string ToString()
    {
        return $"{base.ToString()}, Type:{BikeType}, Sidecar:{HasSidecar}";
    }
}
public enum BikeType { Cruiser, Sport, Touring, Dirt }

