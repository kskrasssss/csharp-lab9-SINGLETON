namespace Lab_9;

public class DispatcherModule
{
    public string MapId => FleetMap.Instance.Id;

    public void Reserve(string plate, string client)
    {
        var map = FleetMap.Instance;
        Log.Write("Диспетчер", $"Отримав карту парку (id {map.Id})");
        var result = map.Reserve(plate, client);
        Log.Write("Диспетчер", result.Message);
    }
}

public class MobileAppModule
{
    public string MapId => FleetMap.Instance.Id;

    public void Issue(string plate, string client)
    {
        var map = FleetMap.Instance;
        Log.Write("Застосунок", $"Отримав карту парку (id {map.Id})");
        var result = map.Issue(plate, client);
        Log.Write("Застосунок", result.Success ? result.Message : $"Відмова: {result.Message}");
    }
}