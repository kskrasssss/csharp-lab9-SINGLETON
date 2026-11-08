namespace Lab_9;

public static partial class DemoRunner
{
    public static void RunFleetMapScenario()
    {
        Log.Section("Завдання 3. Singleton: карта парку автомобілів");

        var dispatcher = new DispatcherModule();
        var app = new MobileAppModule();

        dispatcher.Reserve("CE1234AA", "корпоративний клієнт «Альфа»");
        app.Issue("CE1234AA", "приватний клієнт Ткачук");

        Log.Write("Перевірка", $"id карти: диспетчер = {dispatcher.MapId}, застосунок = {app.MapId}");
        Log.Write("Перевірка", $"Це один і той самий екземпляр: {ReferenceEquals(FleetMap.Instance, FleetMap.Instance)} " +
                               $"(ідентифікатори збігаються: {dispatcher.MapId == app.MapId})");

        Log.Write("Карта парку", "Поточний стан:");
        foreach (var line in FleetMap.Instance.Describe())
            Log.Write("Карта парку", line);
    }
}