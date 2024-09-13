using Xunit;

namespace Task5;

public class UnitTests
{
    public string Foo(string[] args)
    {
        if (args.Length < 2 || args.Length > 2)
        {
            return "Используйте: <путь к папке> <расширение>";
        }

        string directoryPath = args[0];
        string fileExtension = args[1];

        if (!Directory.Exists(directoryPath))
        {
            return $"Директория '{directoryPath}' не найдена.";
        }

        try
        {
            var files = Directory.GetFiles(directoryPath, $"*{fileExtension}", SearchOption.AllDirectories);

            if (files.Length > 0)
            {
                string txt = "";
                foreach (var file in files)
                {
                    txt += file + "\n";
                }
                if (txt.EndsWith("\n"))
                {
                    txt = txt.Remove(txt.Length - 1);
                }
                return txt;
            }
            else
            {
                return "Файлов в указанной папке с данным расширением не найдено";
            }

        }
        catch (Exception ex)
        {
            return $"Произошла ошибка: {ex.Message}";
        }
    }

    [Fact]
    public void Test1()
    {
        string[] args = ["D:\\Документы\\Pictures\\qwe", "xcf"];

        var res = Foo(args);

        Assert.Equal("D:\\Документы\\Pictures\\qwe\\admin.xcf\nD:\\Документы\\Pictures\\qwe\\erfef.xcf\nD:\\Документы\\Pictures\\qwe\\moder123.xcf\nD:\\Документы\\Pictures\\qwe\\ffrrferfe\\ava.xcf", res);
    }

    [Fact]
    public void Test2()
    {
        string[] args = ["D:\\Документы\\Pictures\\qwe", "qwe"];

        var res = Foo(args);

        Assert.Equal("Файлов в указанной папке с данным расширением не найдено", res);
    }

    [Fact]
    public void Test3()
    {
        string[] args = ["D:\\Qwe", "qwe"];

        var res = Foo(args);

        Assert.Equal("Директория 'D:\\Qwe' не найдена.", res);
    }

    [Fact]
    public void Test4()
    {
        string[] args = ["D:\\Документы\\Pictures", "png", "png"];

        var res = Foo(args);

        Assert.Equal("Используйте: <путь к папке> <расширение>", res);
    }

    [Fact]
    public void Test5()
    {
        string[] args = ["D:\\Документы\\Pictures"];

        var res = Foo(args);

        Assert.Equal("Используйте: <путь к папке> <расширение>", res);
    }
}
