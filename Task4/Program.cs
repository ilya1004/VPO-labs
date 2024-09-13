
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
        string backgroundColor = $"rgb({255-i}, {255-i}, {255 - i})";
        writer.WriteLine($"<tr style=\"background-color: {backgroundColor}; height: 3px\"><td></td></tr>");
    }

    writer.WriteLine("</table>");
    writer.WriteLine("</body>");
    writer.WriteLine("</html>");
}

Console.WriteLine($"HTML-файл с таблицей создан: {htmlFilePath}");


