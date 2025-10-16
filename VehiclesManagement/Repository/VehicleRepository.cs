using VehiclesManagement.Models;

namespace VehiclesManagement.Repository;
public class VehicleRepository
{
    private List<Vehicle> vehicles = [];
    private static int nextId = 1;
    public void Add(Vehicle vehicle)
    {
        vehicle.Id = nextId++;
        vehicles.Add(vehicle);
    }

    public void Remove(int id)
    {
        var vehicle = vehicles.Find(v => v.Id == id);
        if (vehicle != null)
        {
            vehicles.Remove(vehicle);
        }
        throw new ArgumentNullException(nameof(vehicle));
    }
    public List<Vehicle>? Search(string? brand, string? type)
    {
        if (brand == null && type == null) return null;
        if (type is not null && brand is not null)
            return vehicles.FindAll(v => v.Brand == brand || v.GetType().Name.Equals(type));

        if (type is not null && brand is null)
            return vehicles.FindAll(v => v.GetType().Name.Equals(type));

        return vehicles.FindAll(v => v.Brand == brand);
    }

    public void PrintVehicles()
    {
        if (vehicles.Count == 0)
        {
            Console.WriteLine("no vehicles available");
            return;
        }

        foreach (var vehicle in vehicles)
        {
            Console.WriteLine(vehicle);
            Console.WriteLine($"  RentalPrice: ${vehicle.RentalPrice()}");
        }
    }
}
