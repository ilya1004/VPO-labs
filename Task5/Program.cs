
string Foo(string[] args)
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

Console.WriteLine(Foo(args));