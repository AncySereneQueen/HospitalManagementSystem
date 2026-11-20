# Hospital Management System (Beginner C# Exercise)

A small console application using Models, Interfaces, and Services. No database, dependency injection framework, exception handling, or complex architecture.

## Run

```bash
dotnet run
```

## Features

- Register and list patients
- Add and list doctors
- Book and list appointments (reject invalid IDs and duplicate doctor/time slots)
- Generate and list bills (one bill per appointment)

## Learning concepts

- **Inheritance:** Patient and Doctor inherit Person.
- **Encapsulation:** Read-only properties and private name setter.
- **Interfaces:** Four small service contracts.
- **SOLID:** Each service has one job; appointment and billing services depend on interfaces, not implementations. Additional features can be added via new services without changing existing models unnecessarily.
- **Validation:** The menu loops for valid user input; services return null for invalid bookings/bills.

The menu owns simple service setup so Program.cs stays one line. Data is in memory and resets when the program exits.
