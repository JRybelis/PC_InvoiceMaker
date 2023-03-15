using PC_InvoiceMaker.LIB.Dtos;

namespace PC_InvoiceMaker.LIB.Interfaces.Services;

public interface IInvoiceViewService
{
    void DisplayGeneratedInvoice(InvoiceDto invoiceRequest);
}