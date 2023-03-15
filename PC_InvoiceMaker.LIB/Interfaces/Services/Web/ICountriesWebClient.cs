using PC_InvoiceMaker.LIB.Models.Country;

namespace PC_InvoiceMaker.LIB.Interfaces.Services.Web;

public interface ICountriesWebClient
{
    Task<List<Country>> GetCountriesWorld();
    Task<List<Country>> GetCountriesEuropeanUnion();
}