using PC_InvoiceMaker.LIB.Interfaces.Services.Validation;
using PC_InvoiceMaker.LIB.Models;
using PC_InvoiceMaker.LIB.Models.Country;

namespace PC_InvoiceMaker.Logic.Services.Validation;

public class CountryValidationService : ICountryValidationService
{
    public bool isCountryExisting(InvoiceRequest invoiceRequest, Country officialCountry)
    {
        throw new NotImplementedException();
    }
}