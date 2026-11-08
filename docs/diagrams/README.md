🇬🇧 **English** | 🇺🇦 [Українська](README.uk.md)

# 🚗 Lab9: Creational Patterns in a Car-Sharing Service

A **C# / .NET** console project that models three problem situations of the car-sharing service "Miskavto" with the creational patterns **Object Pool**, **Prototype** and **Singleton**. Every event is written to a console journal in the format `[time] [source] event`.

![C#](https://img.shields.io/badge/C%23-12-239120?logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-8.0%2B-512BD4?logo=dotnet&logoColor=white)

## 📖 About

| Task | Problem | Pattern | Solution |
|------|---------|---------|----------|
| 1 | Only 4 cars, expensive commissioning, previous client's data leaks | **Object Pool** | `CarPool` reuses cars, `Car.ResetForNextClient` wipes personal data, FIFO queue when no car is free |
| 2 | Corporate tariffs are expensive to configure and changes leak into the standard tariff | **Prototype** | `TariffPlan.Clone` makes a deep copy, including insurance and end zones |
| 3 | Dispatchers and the app used different fleet maps | **Singleton** | `FleetMap.Instance` is the only map, the constructor is private |

**Queue strategy (task 1):** FIFO. It is fair, predictable and simple. Predicting the "nearest car to be released" needs data we do not have (KISS).

## 👥 Authors

- **_Katya_**: [@kskrasssss](https://github.com/kskrasssss): tasks 1, 2
- **_Stas_**: [@Getis8](https://github.com/Getis8): task 3
- Cross-review and README: both

## ⬇️ Download

```bash
git clone https://github.com/kskrasssss/csharp-lab9-SINGLETON
cd your-repo
```

## ▶️ Run

1. Install the [.NET SDK](https://dotnet.microsoft.com/download).
2. From the project root:
   ```bash
   dotnet build
   dotnet run
   ```
3. Choose `1`, `2` or `3` in the menu. Console text is in Ukrainian.

## 📁 Structure

```text
├── Program.cs         # menu
├── Common/            # journal and OperationResult
├── Task1_Pool/        # Object Pool
├── Task2_Prototype/   # Prototype
├── Task3_Singleton/   # Singleton
├── Runner/            # one scenario method per situation
└── docs/
    ├── diagrams/      # UML class diagrams (.mmd + .svg/.png)
    └── CROSS-REVIEW.md
```

## 📝 License

Created for educational purposes. Feel free to use it.