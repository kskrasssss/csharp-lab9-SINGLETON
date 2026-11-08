namespace Lab_9;

public static partial class DemoRunner
{
    private static readonly (string Plate, string Model)[] Fleet =
    {
        ("CE1234AA", "Renault Zoe"),
        ("CE2345BB", "Nissan Leaf"),
        ("CE3456CC", "Hyundai Kona Electric"),
        ("CE4567DD", "Kia Niro")
    };

    public static void RunCarPoolScenario()
    {
        Log.Section("Завдання 1. Object Pool: видача автомобілів парку");

        var pool = new CarPool(Fleet);
        var dispatcher = new RentalDispatcher(pool);
        var active = new Dictionary<string, Car>();

        // 6 клієнтів одночасно бронюють авто, а їх лише 4
        foreach (var name in new[] { "Ткачук", "Мельник", "Шевченко", "Бондар", "Коваль", "Лисенко" })
        {
            var car = dispatcher.RequestCar(new Client(name));
            if (car is not null) active[name] = car;
        }

        // Ткачук користується авто й залишає свої дані
        var tkachukCar = active["Ткачук"];
        tkachukCar.Settings.MultimediaAccount = "tkachuk@mail.ua";
        tkachukCar.Settings.NavigatorAddresses.Add("Чернівці, вул. Головна, 1");
        tkachukCar.Settings.SeatPosition = 9;
        Log.Write("Ткачук", $"Налаштував авто {tkachukCar.Plate}: {tkachukCar.Settings}");

        Finish("Ткачук");
        Finish("Мельник");

        Log.Write("Статистика", "Кількість поїздок на кожному авто:");
        foreach (var car in pool.AllCars)
            Log.Write("Статистика", $"{car.Plate}: {car.TripsCount}");
        Log.Write("Статистика", $"Загальна кількість введених у роботу авто: {pool.TotalCreated}");

        // Завершення поїздки: авто повертається, його отримує наступний із черги
        void Finish(string clientName)
        {
            var car = active[clientName];
            active.Remove(clientName);

            var next = dispatcher.FinishTrip(car);
            if (next is null) return;

            var who = next.CurrentClient!.Name;
            active[who] = next;
            Log.Write(who, $"Отримав {next.Plate}. Стан при отриманні: {next.Settings}");
        }
    }
}