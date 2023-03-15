using PC_InvoiceMaker.LIB.Models;
using PC_InvoiceMaker.LIB.Models.Business;

namespace PC_InvoiceMaker.LIB.Interfaces.Services
{
    public interface IInvoiceService
    {
        Invoice GenerateInvoice(InvoiceRequest invoiceRequest);
    }
}