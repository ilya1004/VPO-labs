using Xunit;

namespace Task3;

public class UnitTests
{
    public string Foo(string[] args)
    {
        if (args.Length < 2)
        {
            return "Недостаточно аргументов";
        }
        else if (args.Length > 2)
        {
            return "Слишком большое количество аргументов";
        }

        try
        {
            double length = double.Parse(args[0]);
            double width = double.Parse(args[1]);

            if (length <= 0 || width <= 0)
            {
                return "Введите корректные числовые значения";
            }

            double area = length * width;
            return $"Площадь прямоугольника: {area:F2}";
        }
        catch
        {
            return "Введите корректные числовые значения";
        }
    }

    [Fact]
    public void Test1()
    {
        var res = Foo(["2", "3"]);

        Assert.Equal("Площадь прямоугольника: 6,00", res);
    }

    [Fact]
    public void Test2()
    {
        var res = Foo(["-12", "23"]);

        Assert.Equal("Введите корректные числовые значения", res);
    }

    [Fact]
    public void Test3()
    {
        var res = Foo(["qwe", "12"]);

        Assert.Equal("Введите корректные числовые значения", res);
    }

    [Fact]
    public void Test4()
    {
        var res = Foo([]);

        Assert.Equal("Недостаточно аргументов", res);
    }

    [Fact]
    public void Test5()
    {
        var res = Foo(["23", "34", "32", "12"]);

        Assert.Equal("Слишком большое количество аргументов", res);
    }
}
