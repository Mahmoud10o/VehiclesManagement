
namespace VehiclesManagement.App;
using System;
using VehiclesManagement.Models;
using VehiclesManagement.Repository;

public static class VehicleUserInteraction
{
    public static void Run(VehicleRepository repo)
    {
        if (repo == null) throw new ArgumentNullException(nameof(repo));
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        ShowWelcome();

        while (true)
        {
            ShowMenu();
            var choice = (Console.ReadLine() ?? "").Trim();

            switch (choice)
            {
                case "1": DoAdd(repo); break;
                case "2": DoRemove(repo); break;
                case "3": DoSearch(repo); break;
                case "4": repo.PrintVehicles(); break;
                case "5": Console.WriteLine("Goodbye"); return;
                default: Console.WriteLine("Unknown option. Try again."); break;
            }

            Console.WriteLine();
        }
    }

    static void ShowWelcome()
    {
        Console.WriteLine("===================================");
        Console.WriteLine("  Welcome to the Vehicle Manager  ");
        Console.WriteLine("===================================");
        Console.WriteLine();
    }

    static void ShowMenu()
    {
        Console.WriteLine("Choose an action:");
        Console.WriteLine("1) Add vehicle");
        Console.WriteLine("2) Remove vehicle (by Id)");
        Console.WriteLine("3) Search vehicles");
        Console.WriteLine("4) Print all vehicles");
        Console.WriteLine("5) Exit");
        Console.Write("> ");
    }

    #region Add and input validation 
    static void DoAdd(VehicleRepository repo)
    {
        try
        {
            var vehicleType = ReadVehicleType();
            if (vehicleType == null)
            {
                Console.WriteLine("Add cancelled please try again with valid vehicle types.");
                return;
            }

            string brand = ReadNonEmptyString("Brand");
            string model = ReadNonEmptyString("Model");
            int year = ReadIntInRange("Year", 1900, DateTime.Now.Year + 1);
            int maxSpeed = ReadIntInRange("MaxSpeed (km/h)", 0, 1000);

            Vehicle created = vehicleType switch
            {
                "car" => CreateCarValidated(brand, model, year, maxSpeed),
                "motorcycle" => CreateMotorcycleValidated(brand, model, year, maxSpeed),
                "truck" => CreateTruckValidated(brand, model, year, maxSpeed),
                _ => throw new InvalidOperationException("Unexpected type")
            };

            repo.Add(created);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Add cancelled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while adding vehicle: {ex.Message}");
        }
    }

    static string? ReadVehicleType()
    {
        Console.WriteLine("Which type of vehicle do you want to add? (type 'cancel' to abort)");
        Console.WriteLine("  1) Car        (c / car)");
        Console.WriteLine("  2) Motorcycle (m / motorcycle / bike)");
        Console.WriteLine("  3) Truck      (t / truck)");
        Console.Write("> ");

        var input = (Console.ReadLine() ?? "").Trim();
        if (string.Equals(input, "cancel", StringComparison.OrdinalIgnoreCase)) return null;

        return input.ToLowerInvariant() switch
        {
            "1" or "c" or "car" => "car",
            "2" or "m" or "motorcycle" or "bike" => "motorcycle",
            "3" or "t" or "truck" => "truck",
            _ => null
        };
    }

    static string ReadNonEmptyString(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (type 'cancel' to abort): ");
            var s = Console.ReadLine()?.Trim() ?? "";
            if (string.Equals(s, "cancel", StringComparison.OrdinalIgnoreCase))
                throw new OperationCanceledException();
            if (!string.IsNullOrEmpty(s)) return s;
            Console.WriteLine("Value cannot be empty.");
        }
    }

    static int ReadIntInRange(string prompt, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write($"{prompt} (type 'cancel' to abort): ");
            var s = Console.ReadLine()?.Trim() ?? "";
            if (string.Equals(s, "cancel", StringComparison.OrdinalIgnoreCase))
                throw new OperationCanceledException();
            if (int.TryParse(s, out int val) && val >= min && val <= max) return val;
            Console.WriteLine($"Please enter a valid integer{(min != int.MinValue ? $" >= {min}" : "")}{(max != int.MaxValue ? $" <= {max}" : "")}.");
        }
    }

    static double ReadDoublePositive(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (type 'cancel' to abort): ");
            var s = Console.ReadLine()?.Trim() ?? "";
            if (string.Equals(s, "cancel", StringComparison.OrdinalIgnoreCase))
                throw new OperationCanceledException();
            if (double.TryParse(s, out double val) && val >= 0) return val;
            Console.WriteLine("Please enter a valid non-negative number.");
        }
    }

    static bool ReadBoolYesNo(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (y/n or yes/no, 'cancel' to abort): ");
            var s = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
            if (s == "cancel") throw new OperationCanceledException();
            if (s == "y" || s == "yes") return true;
            if (s == "n" || s == "no") return false;
            Console.WriteLine("Please type 'y or 'yes' or 'n' or 'no'.");
        }
    }

    static T ReadEnumChoice<T>(string prompt) where T : struct, Enum
    {
        var names = Enum.GetNames(typeof(T));
        Console.WriteLine($"{prompt} - options: {string.Join(", ", names)}");
        while (true)
        {
            Console.Write($"Enter choice (or 'cancel' to abort): ");
            var s = (Console.ReadLine() ?? "").Trim();
            if (string.Equals(s, "cancel", StringComparison.OrdinalIgnoreCase))
                throw new OperationCanceledException();
            if (Enum.TryParse<T>(s, true, out var val)) return val;
            Console.WriteLine($"Invalid. Allowed: {string.Join(", ", names)}");
        }
    }

    static Car CreateCarValidated(string brand, string model, int year, int maxSpeed)
    {
        int doors = ReadIntInRange("Number of doors", 1, 6);
        bool sunroof = ReadBoolYesNo("Has sunroof?");
        int seats = ReadIntInRange("Seat count", 1, 9);

        return new Car
        {
            Brand = brand,
            Model = model,
            Year = year,
            MaxSpeed = maxSpeed,
            NumberOfDoors = doors,
            HasSunroof = sunroof,
            SeatCount = seats
        };
    }

    static Motorcycle CreateMotorcycleValidated(string brand, string model, int year, int maxSpeed)
    {
        bool sidecar = ReadBoolYesNo("Has sidecar?");
        var bikeType = ReadEnumChoice<BikeType>("Bike type");

        return new Motorcycle
        {
            Brand = brand,
            Model = model,
            Year = year,
            MaxSpeed = maxSpeed,
            HasSidecar = sidecar,
            BikeType = bikeType
        };
    }

    static Truck CreateTruckValidated(string brand, string model, int year, int maxSpeed)
    {
        bool hasTrailer = ReadBoolYesNo("Has trailer?");
        double cargo = ReadDoublePositive("Cargo capacity (kg)");

        return new Truck
        {
            Brand = brand,
            Model = model,
            Year = year,
            MaxSpeed = maxSpeed,
            HasTrailer = hasTrailer,
            CargoCapacityKg = cargo
        };
    }
    #endregion

    #region Remove & Search
    static void DoRemove(VehicleRepository repo)
    {
        try
        {
            int id = ReadIntInRange("Enter vehicle Id to remove", 1, int.MaxValue);
            repo.Remove(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Remove cancelled. " + ex.Message);
        }
    }

    static void DoSearch(VehicleRepository repo)
    {
        Console.Write("Enter brand to search (or leave empty to ignore): ");
        var brand = Console.ReadLine() ?? "";

        Console.Write("Enter type to search (Car / Motorcycle / Truck) or leave empty: ");
        var type = Console.ReadLine() ?? "";

        var results = repo.Search(string.IsNullOrWhiteSpace(brand) ? null : brand,
                                  string.IsNullOrWhiteSpace(type) ? null : type);

        Console.WriteLine($"Found {results?.Count ?? 0} result(s):");
        if (results != null)
        {
            foreach (var v in results)
            {
                Console.WriteLine(v);
                Console.WriteLine($"  RentalPrice: ${v.RentalPrice()}");
            }
        }
    }
    #endregion
}

