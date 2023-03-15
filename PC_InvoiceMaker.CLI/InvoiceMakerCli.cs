using PC_InvoiceMaker.CLI.Interfaces;
using PC_InvoiceMaker.LIB.Interfaces.Services;
using PC_InvoiceMaker.LIB.Interfaces.IO;
using PC_InvoiceMaker.LIB.Models;
using PC_InvoiceMaker.LIB.Models.Business;
using PC_InvoiceMaker.LIB.Models.Country;
using PC_InvoiceMaker.LIB.Enums.Business;

namespace PC_InvoiceMaker.CLI;

public class InvoiceMakerCli : IInvoiceViewService, IInvoiceMakerCli
{
    private readonly IWriter Writer;
    private readonly IInvoiceService InvoiceService;
    
    public InvoiceMakerCli(IWriter writer, IInvoiceService invoiceService)
    {
        Writer = writer;
        InvoiceService = invoiceService;
    }

    public void Run()
    {
        bool isRunning = true;

        RegionalBloc EU = new()
        {
            Acronym = "EU",
            Name = "European Union"
        };

        Country Lithuania = new()
        {
            Name = "Lithuania",
            RegionalBlocs = new List<RegionalBloc>{ EU }
        };

        Country Croatia = new()
        {
            Name = "Croatia",
            RegionalBlocs = new List<RegionalBloc> { EU }
        };

        Address LithuanianCompany1Address = new()
        {
            AddressLine1 = "Some str. 5",
            TownCity = "Rural Town",
            Country = Lithuania,
            Postcode = "00001"
        };

        Supplier LithuanianCompany1 = new()
        {
            Name = "LithuanianCompany1",
            PhoneNumber = "+37065764321",
            Email = "accounts@LithuanianCompany1"
        };

        Address CroatianCustomerAddress = new()
        {
            AddressLine1 = "Some Promenade 21",
            TownCity = "Seaside Resort",
            Country = Croatia,
            Postcode = "ABT-gx-9"
        };

        Customer CroatianCustomer = new()
        {
            Name = "CafeByTheShore",
            Address = CroatianCustomerAddress,
            CustomerType = CustomerType.Business,
            CustomerOrigin = CustomerOriginBloc.EU
        };

        InvoiceRequest invoiceRequestLtuHrv = new()
        {
            SupplierDetails = LithuanianCompany1,
            CustomerDetails = CroatianCustomer,
            PONumber = "xhr997",
            Product = "Snails, frozen, 5kg",
            Quantity = 10,
            UnitPrice = 40
        };

        do
        {
            DisplayMainMenu();

            DisplayGeneratedInvoice(invoiceRequestLtuHrv);

            Console.ReadLine();
            isRunning = false;

        } while (isRunning);
    }

    public void DisplayMainMenu()
    {
        Writer.Write("VAT Invoice generator.");
        Writer.Write("Takes supplier, customer and product/service details to generate an invoice.");
        Writer.Write(Environment.NewLine);
    }

    public void DisplayGeneratedInvoice(InvoiceRequest invoiceRequest)
    {
        Invoice invoice = InvoiceService.GenerateInvoice(invoiceRequest);

        Writer.Write($"The invoice generated successfully.");
        Writer.Write(Environment.NewLine);
        Writer.Write(Environment.NewLine);
        
        DisplayInvoiceHeader(invoice);
        DisplayInvoiceBody(invoice);
    }

    private void DisplayInvoiceHeader(Invoice invoice)
    {
        Writer.Write($"{invoice.SupplierDetails.Name}\t \t \t Invoice {invoice.InvoiceNumber}");
        Writer.Write($"{invoice.SupplierDetails.Address.AddressLine1}");
        Writer.Write($"{invoice.SupplierDetails.Address.TownCity}");
        Writer.Write($"{invoice.SupplierDetails.Address.Country.Name}");
        Writer.Write($"{invoice.SupplierDetails.Address.Postcode}");
        Writer.Write(Environment.NewLine);
        Writer.Write($"{invoice.SupplierDetails.PhoneNumber}");
        Writer.Write($"{invoice.SupplierDetails.Email}");
        Writer.Write("------------------------------------------------------------------------------");
        Writer.Write($"Bill to:  {invoice.CustomerDetails.Name} \t \t \t \t Invoice Number: {invoice.InvoiceNumber}");
        Writer.Write($"\t \t {invoice.CustomerDetails.Address.AddressLine1} \t \t \t \t Date: {DateTime.Today.ToShortDateString}");
        Writer.Write($"\t \t {invoice.CustomerDetails.Address.TownCity} \t \t \t \t PO Number: {invoice.PONumber}");
        Writer.Write($"\t \t {invoice.CustomerDetails.Address.Country.Name}");
        Writer.Write($"\t \t {invoice.CustomerDetails.Address.Postcode}");
        Writer.Write(Environment.NewLine);
    }
    
    private void DisplayInvoiceBody(Invoice invoice)
    {
        Writer.Write("Description \t \t \t \t Quantity \t Unit price, EUR  Tax \t Amount");
        
        foreach (var item in invoice.Description)
        {
            if (item.Product is not null && item.Product != string.Empty)
            {
                Writer.Write($"{item.Product} \t \t {item.Quantity} \t {item.UnitPrice} \t {item.Tax} \t {item.Amount}");
            } else
            {
                Writer.Write($"{item.Service} \t \t {item.Quantity} \t {item.UnitPrice} \t {item.Tax} \t {item.Amount}");
            }
        }
        Writer.Write($"\t \t \t \t \t \t \t \t Net total:  \t {invoice.NetAmount}");
        Writer.Write($"\t \t \t \t \t \t \t \t VAT total:  \t {invoice.VAT}");
        Writer.Write($"\t \t \t \t \t \t \t \t Grand total:\t {invoice.Total}");
        Writer.Write(Environment.NewLine);
    }
}