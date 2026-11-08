namespace Lab_9;

public sealed class FleetMap
{
    // Lazy<T> гарантує потокобезпечне створення рівно одного екземпляра
    private static readonly Lazy<FleetMap> _instance = new(() => new FleetMap());

    public static FleetMap Instance => _instance.Value;

    private readonly object _lock = new();
    private readonly Dictionary<string, FleetCar> _cars = new();

    public string Id { get; }

    // Приватний конструктор: створити ще одну карту ззовні неможливо
    private FleetMap()
    {
        Id = Guid.NewGuid().ToString("N")[..8];
        Log.Expensive("Карта парку", "Карту парку ініціалізовано", 5, 100);

        _cars["CE1234AA"] = new FleetCar("CE1234AA", 48.2915, 25.9403);
        _cars["CE2345BB"] = new FleetCar("CE2345BB", 48.3012, 25.9211);
        _cars["CE3456CC"] = new FleetCar("CE3456CC", 48.2784, 25.9567);
        _cars["CE4567DD"] = new FleetCar("CE4567DD", 48.2951, 25.9320);
    }

    public OperationResult Reserve(string plate, string requester)
    {
        lock (_lock)
        {
            if (!_cars.TryGetValue(plate, out var car))
                return new(false, $"Авто {plate} не знайдено на карті");
            if (car.Status != CarStatus.Free)
                return new(false, $"Авто {plate} недоступне ({car.Status})");

            car.Status = CarStatus.Reserved;
            car.ReservedFor = requester;
            return new(true, $"Авто {plate} зарезервоване для {requester}");
        }
    }

    public OperationResult Issue(string plate, string client)
    {
        lock (_lock)
        {
            if (!_cars.TryGetValue(plate, out var car))
                return new(false, $"Авто {plate} не знайдено на карті");
            if (car.Status == CarStatus.InRent)
                return new(false, $"Авто {plate} уже в оренді");
            if (car.Status == CarStatus.Reserved && car.ReservedFor != client)
                return new(false, $"Авто {plate} зарезервоване ({car.ReservedFor})");

            car.Status = CarStatus.InRent;
            car.ReservedFor = null;
            return new(true, $"Авто {plate} видано клієнту {client}");
        }
    }

    public IReadOnlyList<string> Describe()
    {
        lock (_lock)
        {
            return _cars.Values
                .Select(c => $"{c.Plate} | {c.Latitude:F4}, {c.Longitude:F4} | {c.Status}" +
                             (c.ReservedFor is null ? "" : $" ({c.ReservedFor})"))
                .ToList();
        }
    }
}