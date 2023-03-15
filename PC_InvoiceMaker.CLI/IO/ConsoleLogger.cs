using PC_InvoiceMaker.LIB.Interfaces.IO;

namespace PC_InvoiceMaker.CLI.IO;

public class ConsoleLogger : IWriter
{
    public void Clear()
    {
        Console.Clear();
    }

    public void Write(string text)
    {
        Console.WriteLine(text);
    }
}