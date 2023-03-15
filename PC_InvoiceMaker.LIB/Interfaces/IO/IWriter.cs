namespace PC_InvoiceMaker.LIB.Interfaces.IO;

public interface IWriter
{
    void Clear();
    void Write(string text);
}