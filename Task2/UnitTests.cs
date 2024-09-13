using System.Text.RegularExpressions;
using Xunit;

namespace Task2;


public class UnitTests
{
    public string Foo(string count, List<string> data)
    {
        int num = 0;
        try
        {
            Console.WriteLine("Введите количество людей:");
            num = Convert.ToInt32(count);
            if (num <= 0)
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {
            return "Введено некорректное значение количества записей";
        }

        var list = new List<(string, string, int)>();
        Console.WriteLine("Введите данные в формате: <Фамилия> <Имя> <Возраст>");
        for (int i = 0; i < num; i++)
        {
            var q = data[i];
            if (string.IsNullOrEmpty(q))
            {
                return "Введена пустая строка!";
            }
            var l = new List<string>(q.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            if (l.Count > 3)
            {
                return "Введеное неверное количество параметров";
            }
            if (!Regex.IsMatch(l[0], @"^[a-zA-Zа-яА-ЯёЁ]+$") || !Regex.IsMatch(l[1], @"^[a-zA-Zа-яА-ЯёЁ]+$"))
            {
                return "Введены некорректные значения для имени или фамилии";
            }
            try
            {
                list.Add((l[0], l[1], Convert.ToInt32(l[2])));
            }
            catch (Exception)
            {
                return "Введен некорректный возраст";
            }
            var age = Convert.ToInt32(l[2]);
            if (age <= 0 || age >= 120)
            {
                return "Введен некорректный возраст";
            }
        }

        var text = "";

        Console.WriteLine("Результаты:");
        foreach (var item in list)
        {
            Console.WriteLine($"{item.Item2} {item.Item1} {item.Item3}");
            text += $"{item.Item2} {item.Item1} {item.Item3}\n";
        }
        var str = $"{list.Min(x => x.Item3)} {list.Max(x => x.Item3)} {list.Average(x => x.Item3):F2}";
        text += str;
        return text;
    }

    [Fact]
    public void Test1()
    {
        var text = Foo("3", ["qwe wer 12", "wer sdf 34", "fsd wef 92"]);

        Assert.Equal("wer qwe 12\nsdf wer 34\nwef fsd 92\n12 92 46,00", text);
    }

    [Fact]
    public void Test2()
    {
        var text = Foo("-1", ["qwe wer 12", "wer sdf 34", "fsd wef 92"]);

        Assert.Equal("Введено некорректное значение количества записей", text);
    }

    [Fact]
    public void Test3()
    {
        var text = Foo("qwe", ["qwqe qwe 12", "wer sdf 34", "fsd wef 92"]);

        Assert.Equal("Введено некорректное значение количества записей", text);
    }

    [Fact]
    public void Test4()
    {
        var text = Foo("3", ["qw1 234 12", "wer sdf 34", "fsd wef 92"]);

        Assert.Equal("Введены некорректные значения для имени или фамилии", text);
    }

    [Fact]
    public void Test5()
    {
        var text = Foo("3", ["qwe erf 1234", "wer sdf 34", "fsd wef 92"]);

        Assert.Equal("Введен некорректный возраст", text);
    }

    [Fact]
    public void Test6()
    {
        var text = Foo("3", ["qwe erf -1", "wer sdf 34", "fsd wef 92"]);

        Assert.Equal("Введен некорректный возраст", text);
    }

    [Fact]
    public void Test7()
    {
        var text = Foo("2", ["qwe ewу qwe", "wer sdf 34"]);

        Assert.Equal("Введен некорректный возраст", text);
    }

    [Fact]
    public void Test8()
    {
        var text = Foo("2", ["qwe ewу 12 qw qwe", "wer sdf 34"]);

        Assert.Equal("Введеное неверное количество параметров", text);
    }

}