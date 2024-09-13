using Xunit;

namespace Task6;

public class UnitTests
{
    async Task<string> FooAsync(string[] args)
    {
        if (args.Length < 2 || args.Length > 2)
        {
            return "Использование: <URL> <путь_к_папке>";
        }

        string url = args[0];
        string folderPath = args[1];

        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            return "Некорректный URL.";
        }

        var uri = new Uri(url);
        string path = uri.AbsolutePath;
        if (string.IsNullOrEmpty(Path.GetExtension(path)))
        {
            return "Данный URL ссылается не на файл.";
        }

        if (!Directory.Exists(folderPath))
        {
            return "Указанная папка не существует.";
        }

        string fileName = Path.GetFileName(new Uri(url).AbsolutePath);
        string filePath = Path.Combine(folderPath, fileName);

        try
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await response.Content.CopyToAsync(fs);
            }

            if (File.Exists(filePath))
            {
                return $"Файл успешно сохранен: {filePath}";
            }
            else
            {
                return $"Документ не был сохранен";
            }

        }
        catch (HttpRequestException e)
        {
            return $"Ошибка при получении документа: {e.Message}";
        }
        catch (IOException e)
        {
            return $"Ошибка при записи файла: {e.Message}";
        }
        catch (Exception e)
        {
            return $"Произошла ошибка: {e.Message}";
        }
    }

    [Fact]
    public async Task Test1()
    {
        string[] args = ["https://avivir.ru/upload/iblock/4a7/78hrbgo9csveoq1b6mc8gq668vx1cnip.pdf", "."];

        var res = await FooAsync(args);

        Assert.Equal("Файл успешно сохранен: .\\78hrbgo9csveoq1b6mc8gq668vx1cnip.pdf", res);
    }

    [Fact]
    public async Task Test2()
    {
        string[] args = ["https://fossies.org/linux/tin/doc/article.txt", "."];

        var res = await FooAsync(args);
        var path = res[res.IndexOf('.')..];

        Assert.True(File.Exists(path));
    }

    [Fact]
    public async Task Test3()
    {
        string[] args = ["https://fossies.org/linux/tin/doc/article.txt", "."];

        var res = await FooAsync(args);
        var path = res[res.IndexOf('.')..];
        var txt = File.ReadAllText(path);

        Assert.StartsWith("<!DOCTYPE html>\n<HTML LANG=\"en\">\n<HEAD>", txt);
    }

    [Fact]
    public async Task Test4()
    {    
        string[] args = ["https://fossies.org/linux/tin/doc/article.txt", "."];

        var res = await FooAsync(args);

        Assert.Equal("Ошибка при получении документа: Этот хост неизвестен. (fossies.org:443)", res);
    }

    [Fact]
    public async Task Test5()
    {
        string[] args = ["https://google.com", "."];

        var res = await FooAsync(args);

        Assert.Equal("Данный URL ссылается не на файл.", res);
    }

    [Fact]
    public async Task Test6()
    {
        string[] args = ["https://fossies.org/linux/tin/doc/article.txt", ".", "qwe"];

        var res = await FooAsync(args);

        Assert.Equal("Использование: <URL> <путь_к_папке>", res);
    }

    [Fact]
    public async Task Test7()
    {

        string[] args = ["https://google.com"];

        var res = await FooAsync(args);

        Assert.Equal("Использование: <URL> <путь_к_папке>", res);
    }
}
