using PC_InvoiceMaker.LIB.Dtos;
using PC_InvoiceMaker.LIB.Models.Business;

namespace PC_InvoiceMaker.LIB.Interfaces.Services;

public interface IInvoiceService
{
    Invoice GenerateInvoice(InvoiceDto invoiceRequest);
    Invoice CalculateInvoiceTotals(InvoiceDto invoiceRequest, Invoice invoice);
}