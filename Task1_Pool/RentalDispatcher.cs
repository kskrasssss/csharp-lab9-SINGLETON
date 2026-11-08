namespace Lab_9;

public class RentalDispatcher
{
    private readonly CarPool _pool;
    private readonly Queue<Client> _waiting = new();

    public RentalDispatcher(CarPool pool)
    {
        _pool = pool ?? throw new ArgumentNullException(nameof(pool));
    }

    public Car? RequestCar(Client client)
    {
        var car = _pool.TryAcquire(client);

        if (car is null)
        {
            _waiting.Enqueue(client);
            Log.Write("Диспетчер",
                $"Вільних авто немає: клієнт {client.Name} у черзі (позиція {_waiting.Count})");
            return null;
        }

        Log.Write("Диспетчер", $"Авто {car.Plate} → клієнт {client.Name}");
        return car;
    }

    // Повертає авто, яке отримав клієнт із черги (або null, якщо черга порожня)
    public Car? FinishTrip(Car car)
    {
        Log.Write("Диспетчер", $"Клієнт {car.CurrentClient?.Name} завершив поїздку на {car.Plate}");
        _pool.Release(car);

        return _waiting.Count == 0 ? null : RequestCar(_waiting.Dequeue());
    }
}