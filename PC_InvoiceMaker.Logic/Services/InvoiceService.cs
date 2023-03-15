using PC_InvoiceMaker.LIB.Dtos;
using PC_InvoiceMaker.LIB.Enums.Business;
using PC_InvoiceMaker.LIB.Interfaces.Services;
using PC_InvoiceMaker.LIB.Interfaces.Services.Validation;
using PC_InvoiceMaker.LIB.Interfaces.Services.Web;
using PC_InvoiceMaker.LIB.Models.Business;
using PC_InvoiceMaker.LIB.Models.Country;

namespace PC_InvoiceMaker.Logic.Services;

public class InvoiceService : IInvoiceService
{
    private readonly ICountryValidationService countryValidationService;
    private readonly ICountriesWebClient countriesWebClient;

    public InvoiceService(ICountryValidationService countryValidationService, ICountriesWebClient countriesWebClient)
    {
        this.countryValidationService = countryValidationService;
        this.countriesWebClient = countriesWebClient;
    }
    public async Invoice GenerateInvoice(InvoiceDto invoiceDto)
    {
        var officialWorldCountries = await this.countriesWebClient.GetCountriesWorld();
        var officialEuCountries = await this.countriesWebClient.GetCountriesEuropeanUnion();
        var isCountryExisting = this.countryValidationService.checkCountryExists(invoiceDto, officialWorldCountries);
        
        if (!isCountryExisting)
            throw new ArgumentException("The country provided is not recognised. Please provide a different country.");

        var isCustomerFromEuCountry = CheckCountryInTheEu(invoiceDto, officialEuCountries);

        var invoice = MapInvoiceHeaderDetails(invoiceDto, isCustomerFromEuCountry);
        invoice = CalculateInvoiceTotals(invoiceDto, invoice);
        
    }

    private static bool CheckCountryInTheEu(InvoiceDto invoiceDto, List<Country> euCountries)
    {
        var countryMatch = euCountries.Where(country => country.Name == invoiceDto.CustomerDetails.Address.Country.Name).FirstOrDefault();

        return countryMatch is not null ? true : false;
    }

    private static Invoice MapInvoiceHeaderDetails(InvoiceDto invoiceRequest, bool isCustomerFromEuCountry)
    {
        var invoice = new Invoice
        {
            CustomerDetails = new(),
            SupplierDetails = new(),
        };

        invoice.Date = DateOnly.FromDateTime(DateTime.UtcNow);
        invoice.CustomerDetails.Name = invoiceRequest.CustomerDetails.Name;
        invoice.CustomerDetails.Address.AddressLine1 = invoiceRequest.CustomerDetails.Address.AddressLine1;
        invoice.CustomerDetails.Address.Country = invoiceRequest.CustomerDetails.Address.Country;
        invoice.CustomerDetails.Address.TownCity = invoiceRequest.CustomerDetails.Address.TownCity;
        invoice.CustomerDetails.Address.Postcode = invoiceRequest.CustomerDetails.Address.Postcode;
        invoice.CustomerDetails.CustomerOrigin = isCustomerFromEuCountry ? CustomerOriginBloc.EU : CustomerOriginBloc.NonEU;
        invoice.CustomerDetails.CustomerType = invoiceRequest.CustomerDetails.CustomerType;
        invoice.SupplierDetails.Name = invoiceRequest.SupplierDetails.Name;
        invoice.SupplierDetails.Address.AddressLine1 = invoiceRequest.SupplierDetails.Address.AddressLine1;
        invoice.SupplierDetails.Address.Country = invoiceRequest.SupplierDetails.Address.Country;
        invoice.SupplierDetails.Address.TownCity = invoiceRequest.SupplierDetails.Address.TownCity;
        invoice.SupplierDetails.Address.Postcode = invoiceRequest.SupplierDetails.Address.Postcode;
        invoice.SupplierDetails.Email = invoiceRequest.SupplierDetails.Email;
        invoice.SupplierDetails.PhoneNumber = invoiceRequest.SupplierDetails.PhoneNumber;
        invoice.PONumber = invoiceRequest.PONumber;

        return invoice;
    }

    public Invoice CalculateInvoiceTotals(InvoiceDto invoiceDto, Invoice invoice)
    {
        invoice.Description = new List<InvoiceBody>();

        
    }
}