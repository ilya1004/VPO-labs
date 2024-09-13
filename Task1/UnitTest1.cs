namespace Task1;

public class UnitTest1
{
    public string Foo()
    {
        var rand = new Random();
        var text = $"Hello, world!\nAndhiagain!\n{new string('!', rand.Next(5, 51))}";

        return text;
    }

    [Fact]
    public void Test1()
    {
        var text = Foo();

        Assert.NotEmpty(text);
    }

    [Fact]
    public void Test2()
    {
        var text = Foo();

        Assert.StartsWith("Hello, world!\nAndhiagain!\n", text);
    }

    [Fact]
    public void Test3()
    {
        for (int i = 0; i < 100; i++)
        {
            var text = Foo();
            Assert.InRange(text[26..].Length, 5, 50);
        }
    }
}