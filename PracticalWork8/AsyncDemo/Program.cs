using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Практическая работа №8. Асинхронное приготовление заказов");
Console.WriteLine();

Stopwatch timer = Stopwatch.StartNew();

Task<string>[] tasks =
{
    CookAsync("Пицца", 3000),
    CookAsync("Салат", 1000),
    CookAsync("Суп", 2000)
};

string[] results = await Task.WhenAll(tasks);

timer.Stop();

Console.WriteLine();
Console.WriteLine("Все заказы готовы:");
foreach (string result in results)
{
    Console.WriteLine(result);
}

Console.WriteLine($"Общее время: {timer.Elapsed.TotalSeconds:F1} сек.");

async Task<string> CookAsync(string name, int milliseconds)
{
    Console.WriteLine($"Начали готовить: {name}");
    await Task.Delay(milliseconds);
    Console.WriteLine($"Готово: {name}");
    return name;
}
