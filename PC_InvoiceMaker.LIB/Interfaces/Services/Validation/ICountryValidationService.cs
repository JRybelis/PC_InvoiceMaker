using PC_InvoiceMaker.LIB.Dtos;
using PC_InvoiceMaker.LIB.Models.Country;

namespace PC_InvoiceMaker.LIB.Interfaces.Services.Validation;

public interface ICountryValidationService
{
    bool checkCountryExists(InvoiceDto invoiceRequest, List<Country> officialCountries);
}