using PC_InvoiceMaker.LIB.Models.Country;

namespace PC_InvoiceMaker.LIB.Interfaces.Services.Web;

public interface IResultParser
{
    List<Country> ParseJson(string jsonString);
}