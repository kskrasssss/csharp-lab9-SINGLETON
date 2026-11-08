namespace Lab_9;

public static partial class DemoRunner
{
    public static void RunTariffPrototypeScenario()
    {
        Log.Section("Завдання 2. Prototype: корпоративні тарифні плани");

        var standard = TariffPlan.CreateStandard();

        var corporate = new List<TariffPlan>();
        foreach (var company in new[] { "ТОВ «Альфа»", "ТОВ «Бета»", "ТОВ «Гамма»" })
            corporate.Add(standard.CloneFor(company));

        var alpha = corporate[0];
        Log.Write("Сценарій", "Змінюємо тариф «Альфа»: повне страхування, нова зона, нова ціна за хвилину");
        alpha.Insurance.Coverage = "Повне";
        alpha.Insurance.Deductible = 0m;
        alpha.EndZones.Add(new EndZone("Склад «Альфа»", 0m));
        alpha.PricePerMinute = 2.40m;

        Console.WriteLine();
        PrintSideBySide("СТАНДАРТНИЙ", standard, "КОРПОРАТИВНИЙ (Альфа)", alpha);
        Console.WriteLine();

        Log.Write("Перевірка", $"Стандарт: страхування «{standard.Insurance.Coverage}», зон {standard.EndZones.Count}, ціна/хв {standard.PricePerMinute:F2}");
        Log.Write("Перевірка", $"Тариф «Бета» не змінився: страхування «{corporate[1].Insurance.Coverage}», зон {corporate[1].EndZones.Count}");
        Log.Write("Перевірка", $"Insurance спільний об'єкт зі стандартом: {ReferenceEquals(standard.Insurance, alpha.Insurance)}");
    }

    private static void PrintSideBySide(string leftTitle, TariffPlan left, string rightTitle, TariffPlan right)
    {
        const int width = 46;
        var l = left.Describe().ToList();
        var r = right.Describe().ToList();

        Console.WriteLine($"{leftTitle,-width}| {rightTitle}");
        Console.WriteLine(new string('-', width) + "+" + new string('-', width));
        for (int i = 0; i < Math.Max(l.Count, r.Count); i++)
        {
            string a = i < l.Count ? l[i] : "";
            string b = i < r.Count ? r[i] : "";
            Console.WriteLine($"{a,-width}| {b}");
        }
    }
}