using System.Text.RegularExpressions;
using Task2;

string Foo()
{
    int num = 0;
    try
    {
        Console.WriteLine("Введите количество людей:");
        num = Convert.ToInt32(Console.ReadLine());
        if (num <= 0)
        {
            throw new Exception();
        }
    }
    catch (Exception)
    {
        Console.WriteLine("Введено некорректное значение количества записей");
        Environment.Exit(1);
    }

    var list = new List<(string, string, int)>();
    Console.WriteLine("Введите данные в формате: <Фамилия> <Имя> <Возраст>");
    for (int i = 0; i < num; i++)
    {
        var q = Console.ReadLine();
        if (string.IsNullOrEmpty(q))
        {
            Console.WriteLine("Введена пустая строка!");
            Environment.Exit(1);
        }
        var l = new List<string>(q.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        if (l.Count > 3)
        {
            Console.WriteLine("Введеное неверное количество параметров");
            Environment.Exit(1);
        }
        if (!Regex.IsMatch(l[0], @"^[a-zA-Zа-яА-ЯёЁ]+$") || !Regex.IsMatch(l[1], @"^[a-zA-Zа-яА-ЯёЁ]+$"))
        {
            Console.WriteLine("Введены некорректные значения для имени или фамилии");
            Environment.Exit(1);
        }
        try
        {
            list.Add((l[0], l[1], Convert.ToInt32(l[2])));
        }
        catch (Exception)
        {
            Console.WriteLine("Введен некорректный возраст");
            Environment.Exit(1);
        }
        var age = Convert.ToInt32(l[2]);
        if (age <= 0 || age >= 120)
        {
            Console.WriteLine("Введен некорректный возраст");
            Environment.Exit(1);
        }
    }

    var text = "";

    Console.WriteLine("Результаты:");
    foreach (var item in list)
    {
        Console.WriteLine($"{item.Item2} {item.Item1} {item.Item3}");
        text += $"{item.Item2} {item.Item1} {item.Item3}\r\n";
    }
    var str = $"{list.Min(x => x.Item3)} {list.Max(x => x.Item3)} {list.Average(x => x.Item3):F2}";
    Console.Write(str);
    text += str;
    return text;
}

Foo();