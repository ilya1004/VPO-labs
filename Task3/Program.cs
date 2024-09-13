
void Foo()
{
    if (args.Length < 2)
    {
        Console.WriteLine("Недостаточно аргументов");
        return;
    }
    else if (args.Length > 2)
    {
        Console.WriteLine("Слишком большое количество аргументов");
        return;
    }

    try
    {
        double length = double.Parse(args[0]);
        double width = double.Parse(args[1]);

        if (length <= 0 || width <= 0)
        {
            Console.WriteLine("Введите корректные числовые значения");
            return;
        }

        double area = length * width;
        Console.WriteLine($"Площадь прямоугольника: {area:F2}");
    }
    catch
    {
        Console.WriteLine("Введите корректные числовые значения");
    }
}

Foo();