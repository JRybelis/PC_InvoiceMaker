using PC_InvoiceMaker.LIB.Interfaces.Services.Validation;
using PC_InvoiceMaker.LIB.Dtos;
using PC_InvoiceMaker.LIB.Models.Country;

namespace PC_InvoiceMaker.Logic.Services.Validation;

public class CountryValidationService : ICountryValidationService
{
    public bool checkCountryExists(InvoiceDto invoiceDto, List<Country> officialCountries)
    {
        var countryMatch = officialCountries.Where(country => country.Name == invoiceDto.CustomerDetails.Address.Country.Name).FirstOrDefault();

        return countryMatch is not null ? true : false;
    }
}