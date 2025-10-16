namespace VehiclesManagement.Models;

public class Truck : Vehicle
{
    public bool HasTrailer { get; set; }
    public double CargoCapacityKg { get; set; }
    public override decimal RentalPrice()
    {
        decimal basePrice = 500m;
        basePrice += HasTrailer ? 200m : 0;
        if (CargoCapacityKg > 500) basePrice += 300m;
        return basePrice;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Trailer:{HasTrailer}, CargoCapacity:{CargoCapacityKg}kg";
    }
}
