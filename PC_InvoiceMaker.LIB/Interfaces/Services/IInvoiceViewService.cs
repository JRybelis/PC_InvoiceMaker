using PC_InvoiceMaker.LIB.Models;

namespace PC_InvoiceMaker.LIB.Interfaces.Services;

public interface IInvoiceViewService
{
    void DisplayGeneratedInvoice(InvoiceRequest invoiceRequest);
}