namespace Lab_9;

public static class Log
{
    // Умовний годинник: час у журналі рухається разом із "дорогими" операціями
    private static DateTime _now = new(2026, 11, 10, 9, 0, 0);

    public static void Write(string source, string message) =>
        Console.WriteLine($"[{_now:HH:mm:ss}] [{source}] {message}");

    // Дорога операція: повідомлення + реальна затримка + зсув умовного часу
    public static void Expensive(string source, string message, int simulatedSeconds, int realMs = 150)
    {
        Write(source, message);
        Thread.Sleep(realMs);
        _now = _now.AddSeconds(simulatedSeconds);
    }

    public static void Section(string title) =>
        Console.WriteLine($"\n===== {title} =====");
}