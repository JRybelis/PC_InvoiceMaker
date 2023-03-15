using PC_InvoiceMaker.LIB.Models.Business;

namespace PC_InvoiceMaker.LIB.Models;

public class InvoiceRequest
{
    public Supplier SupplierDetails { get; set; } = default!;
    public Customer CustomerDetails { get; set; } = default!;
    public string? PONumber { get; set; }
    public string? Product { get; set; }
    public string? Service { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}