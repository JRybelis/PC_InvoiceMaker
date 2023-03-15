using PC_InvoiceMaker.LIB.Interfaces.Services;
using PC_InvoiceMaker.LIB.Models;
using PC_InvoiceMaker.LIB.Models.Business;

namespace PC_InvoiceMaker.Logic.Services;

public class InvoiceService : IInvoiceService
{
    public Invoice GenerateInvoice(InvoiceRequest invoiceRequest)
    {
        return new Invoice();
    }
}