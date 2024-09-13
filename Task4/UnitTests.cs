using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using Xunit;

namespace Task4;

public class UnitTests
{
    public string Foo()
    {
        string htmlFilePath = "C:\\Users\\user\\source\\repos\\VPO-lab1\\Task4\\view.html";

        File.Create(htmlFilePath).Close();

        using (StreamWriter writer = new StreamWriter(htmlFilePath))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\">");
            writer.WriteLine("<head>");
            writer.WriteLine("<meta charset=\"UTF-8\">");
            writer.WriteLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            writer.WriteLine("<title>Index</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body style=\"margin: 0px; padding: 7px 0px 0px 0px\">");
            writer.WriteLine("<table style=\"width: 100%; border-collapse: collapse;\">");

            for (int i = 0; i < 256; i++)
            {
                string backgroundColor = $"rgb({255 - i}, {255 - i}, {255 - i})";
                writer.WriteLine($"<tr style=\"background-color: {backgroundColor}; height: 3px\"><td></td></tr>");
            }

            writer.WriteLine("</table>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        return $"HTML-файл с таблицей создан: {htmlFilePath}";
    }

    [Fact]
    public void Test1()
    {
        var res = Foo();

        Assert.Equal("HTML-файл с таблицей создан: C:\\Users\\user\\source\\repos\\VPO-lab1\\Task4\\view.html", res);
    }

    [Fact]
    public void Test2()
    {
        var res = Foo();

        var path = res[res.IndexOf('C')..];

        Assert.True(File.Exists(path));
    }

    [Fact]
    public void Test3()
    {
        var res = Foo();

        var path = res[res.IndexOf('C')..];

        var txt = File.ReadAllText(path);

        Assert.StartsWith("<!DOCTYPE html>", txt);
    }

    [Fact]
    public void Test4()
    {
        var res = Foo();

        var path = res[res.IndexOf('C')..];
        var txt = File.ReadAllText(path);

        Assert.Equal(256, Regex.Matches(txt, Regex.Escape("<td></td>")).Count);
    }

}
