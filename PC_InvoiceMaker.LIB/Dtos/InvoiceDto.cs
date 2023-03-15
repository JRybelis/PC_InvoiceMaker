using PC_InvoiceMaker.LIB.Models.Business;

namespace PC_InvoiceMaker.LIB.Dtos;

public class InvoiceDto
{
    public Supplier SupplierDetails { get; set; } = default!;
    public Customer CustomerDetails { get; set; } = default!;
    public List<InvoiceBodyDto> InvoiceBodyDto { get; set; } = default!;
    public string? PONumber { get; set; }
}