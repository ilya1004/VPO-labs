


using System;

async Task FooAsync()
{
    if (args.Length < 2 || args.Length > 2)
    {
        Console.WriteLine("Использование: <URL> <путь_к_папке>");
        return;
    }

    string url = args[0];
    string folderPath = args[1];

    if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
    {
        Console.WriteLine("Некорректный URL.");
        return;
    }

    var uri = new Uri(url);
    string path = uri.AbsolutePath;
    if (string.IsNullOrEmpty(Path.GetExtension(path)))
    {
        Console.WriteLine("Данный URL ссылается не на файл.");
        return;
    }

    if (!Directory.Exists(folderPath))
    {
        Console.WriteLine("Указанная папка не существует.");
        return;
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
            Console.WriteLine($"Файл успешно сохранен: {filePath}");
        }
        else
        {
            Console.WriteLine($"Документ не был сохранен");
        }
        
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine($"Ошибка при получении документа: {e.Message}");
    }
    catch (UnauthorizedAccessException)
    {
        Console.WriteLine($"Ошибка при получении документа: {url}");
    }
    catch (IOException e)
    {
        Console.WriteLine($"Ошибка при записи файла: {e.Message}");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Произошла ошибка: {e.Message}");
    }
}

await FooAsync();

//var str = File.ReadAllText("C:\\Users\\user\\source\\repos\\VPO-lab1\\Task6\\78hrbgo9csveoq1b6mc8gq668vx1cnip.pdf");
//Console.WriteLine(str.StartsWith("%PDF-1.4"));

//var url = "https://google.com";
//var url = "https://fossies.org/linux/tin/doc/article.txt";

//var uri = new Uri(url);
//string path = uri.AbsolutePath;

// Проверяем наличие расширения
//if (string.IsNullOrEmpty(Path.GetExtension(path)))
//{
//    Console.WriteLine("Данный URL ссылается не на файл.");
//}