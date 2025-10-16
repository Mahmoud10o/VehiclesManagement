# Vehicle Rental Management System 

A simple console application written in **C#** that manages different types of vehicles (Car, Motorcycle, Truck) and calculates their rental prices.

---

## Features

- Add, remove, and list vehicles  
- Search by brand  
- Calculate rental price for each vehicle type  

---

## Vehicle Types

Each vehicle inherits from the abstract `Vehicle` class and has its own extra properties:

- **Car** → `NumberOfDoors`, `HasSunroof`, `SeatCount`
- **Motorcycle** → `HasSidecar`, `BikeType`
- **Truck** → `HasTrailer`, `CargoCapacityKg`

---

## How to Run

1. Open the project in **Visual Studio** or any C# IDE.
2. Run the project (`Ctrl + F5`).
3. Follow the instructions in the console:
   - Choose vehicle type
   - Enter details
   - View the result

---

## Example

```bash
Welcome to the Vehicle Management System!

Choose an option:
1. Add Vehicle
2. Remove Vehicle
3. View All
4. Search
5. Exit

> 1
Which type of vehicle do you want to add? (Car / Motorcycle / Truck)
> Car
Brand: Toyota
Model: Corolla
Year: 2020
MaxSpeed: 180
Car added successfully! (Rental Price: 68)

