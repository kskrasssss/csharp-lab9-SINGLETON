namespace Lab_9;

public class Car
{
    public string Plate { get; }
    public string Model { get; }
    public int TripsCount { get; private set; }
    public Client? CurrentClient { get; private set; }
    public PersonalSettings Settings { get; } = new();

    public Car(string plate, string model)
    {
        Plate = plate;
        Model = model;

        // Дорога операція: виконується один раз за життя авто
        Log.Expensive($"Авто {plate}",
            $"Введення в роботу ({model}): реєстрація в системі, телематика, страхування...", 30, 200);
        Log.Write($"Авто {plate}", "Готове до роботи");
    }

    public void StartTrip(Client client)
    {
        CurrentClient = client;
        TripsCount++;
    }

    // Скидання стану перед наступною орендою
    public void ResetForNextClient()
    {
        CurrentClient = null;
        Settings.Reset();
    }
}
