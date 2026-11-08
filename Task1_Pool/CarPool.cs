namespace Lab_9;

public class CarPool
{
    private readonly object _lock = new();
    private readonly Queue<(string Plate, string Model)> _notCommissioned;
    private readonly Queue<Car> _idle = new();
    private readonly List<Car> _all = new();

    public CarPool(IEnumerable<(string Plate, string Model)> fleet)
    {
        _notCommissioned = new Queue<(string, string)>(fleet);
    }

    public int TotalCreated => _all.Count;
    public IReadOnlyList<Car> AllCars => _all;

    // null означає: вільних авто немає
    public Car? TryAcquire(Client client)
    {
        lock (_lock)
        {
            Car? car = null;

            if (_idle.Count > 0)
            {
                car = _idle.Dequeue();                 // повторне використання без введення в роботу
            }
            else if (_notCommissioned.Count > 0)
            {
                var spec = _notCommissioned.Dequeue(); // лише поки не вичерпано фізичний парк
                car = new Car(spec.Plate, spec.Model);
                _all.Add(car);
            }

            if (car is null) return null;

            car.StartTrip(client);
            return car;
        }
    }

    public void Release(Car car)
    {
        ArgumentNullException.ThrowIfNull(car);

        lock (_lock)
        {
            if (!_all.Contains(car))
                throw new ArgumentException("Це авто не належить парку.", nameof(car));
            if (_idle.Contains(car))
                throw new InvalidOperationException($"Авто {car.Plate} уже повернуто.");

            car.ResetForNextClient();
            _idle.Enqueue(car);
            Log.Write("Парк", $"{car.Plate} повернено, персональні дані стерто");
        }
    }
}