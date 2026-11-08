using Lab_9;

Console.OutputEncoding = System.Text.Encoding.UTF8;

while (true)
{
    Console.WriteLine("\n=== Лабораторна робота №9 (варіант 8, породжувальні шаблони) ===");
    Console.WriteLine("1 - Object Pool: видача автомобілів парку");
    Console.WriteLine("2 - Prototype: корпоративні тарифні плани");
    Console.WriteLine("3 - Singleton: карта парку автомобілів");
    Console.WriteLine("0 - Вихід");
    Console.Write("Ваш вибір: ");

    switch (Console.ReadLine()?.Trim())
    {
        case "1": DemoRunner.RunCarPoolScenario(); break;
        case "2": DemoRunner.RunTariffPrototypeScenario(); break;
        case "3": DemoRunner.RunFleetMapScenario(); break;
        case "0": return;
        default: Console.WriteLine("Невірний вибір"); break;
    }
}