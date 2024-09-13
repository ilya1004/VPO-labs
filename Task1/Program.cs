
string Foo()
{
    var rand = new Random();
    var text = $"Hello, world!\nAndhiagain!\n{new string('!', rand.Next(5, 51))}";

    return text;
}

Console.WriteLine(Foo());