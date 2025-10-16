namespace VehiclesManagement.Models;
public class Car : Vehicle
{
    public int NumberOfDoors { get; set; }
    public bool HasSunroof { get; set; }
    public int SeatCount { get; set; }

    public override decimal RentalPrice()
    {
        decimal basePrice = 100m;
        if (HasSunroof)
            basePrice += 100m;
        if (SeatCount > 4) basePrice += 500m;
        return basePrice;
    }
    public override string ToString()
    {
        return $"{base.ToString()}, Doors:{NumberOfDoors}, Seats:{SeatCount}, Sunroof:{HasSunroof}";
    }
}
