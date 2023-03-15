using PC_InvoiceMaker.LIB.Models;
using PC_InvoiceMaker.LIB.Models.Country;

namespace PC_InvoiceMaker.LIB.Interfaces.Services.Validation
{
    public interface ICountryValidationService
    {
        bool isCountryExisting(InvoiceRequest invoiceRequest, Country officialCountry);
    }
}