namespace Lab_9;

public class TariffPlan : IPrototype<TariffPlan>
{
    public string Name { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public decimal PricePerMinute { get; set; }
    public decimal PricePerKm { get; set; }
    public int SpeedLimitKmh { get; set; }
    public InsuranceTerms Insurance { get; set; } = new(string.Empty, 0m);
    public List<EndZone> EndZones { get; private set; } = new();
    public List<string> Bonuses { get; private set; } = new();

    private TariffPlan() { }

    // Конструктор копіювання: кожна вкладена структура копіюється глибоко
    private TariffPlan(TariffPlan source)
    {
        Name = source.Name;
        Owner = source.Owner;
        PricePerMinute = source.PricePerMinute;
        PricePerKm = source.PricePerKm;
        SpeedLimitKmh = source.SpeedLimitKmh;
        Insurance = source.Insurance.Clone();                            // новий об'єкт
        EndZones = source.EndZones.Select(z => z.Clone()).ToList();       // новий список нових об'єктів
        Bonuses = new List<string>(source.Bonuses);                       // рядки незмінні, достатньо нового списку
    }

    // Дороге створення: виконується один раз
    public static TariffPlan CreateStandard()
    {
        Log.Expensive("Тариф",
            "Дороге налаштування стандартного тарифу: ціни, страхування, зони завершення, обмеження, бонуси...",
            120, 300);

        var plan = new TariffPlan
        {
            Name = "Стандартний",
            Owner = "Усі клієнти",
            PricePerMinute = 3.00m,
            PricePerKm = 6.00m,
            SpeedLimitKmh = 110,
            Insurance = new InsuranceTerms("Базове", 8000m)
        };
        plan.EndZones.Add(new EndZone("Центр", 0m));
        plan.EndZones.Add(new EndZone("Аеропорт", 100m));
        plan.Bonuses.Add("Кешбек 5% за кожну 10-ту поїздку");

        Log.Write("Тариф", "Стандартний тариф готовий");
        return plan;
    }

    public TariffPlan Clone() => new(this);

    public TariffPlan CloneFor(string company)
    {
        var copy = Clone();
        copy.Name = $"Корпоративний ({company})";
        copy.Owner = company;
        Log.Write("Тариф", $"Клон для {company} створено без повторного налаштування");
        return copy;
    }

    public IEnumerable<string> Describe()
    {
        yield return $"Назва: {Name}";
        yield return $"Для: {Owner}";
        yield return $"Ціна за хвилину: {PricePerMinute:F2}";
        yield return $"Ціна за км: {PricePerKm:F2}";
        yield return $"Ліміт швидкості: {SpeedLimitKmh} км/год";
        yield return $"Страхування: {Insurance.Coverage}, франшиза {Insurance.Deductible:F0}";
        yield return "Зони завершення:";
        foreach (var z in EndZones) yield return $"  - {z.Name} (+{z.ExtraFee:F0})";
        yield return "Бонуси:";
        foreach (var b in Bonuses) yield return $"  - {b}";
    }
}