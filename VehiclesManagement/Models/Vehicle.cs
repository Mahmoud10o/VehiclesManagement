namespace VehiclesManagement.Models;
public abstract class Vehicle
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int MaxSpeed { get; set; }
    public int Year { get; set; }

    public override string ToString()
    {
        return $"Id:{Id}, Brand:{Brand}, Model:{Model}, Year:{Year}, MaxSpeed:{MaxSpeed} km/h";
    }

    public abstract decimal RentalPrice();
}
