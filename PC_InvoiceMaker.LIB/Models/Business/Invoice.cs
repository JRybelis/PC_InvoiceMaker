namespace PC_InvoiceMaker.LIB.Models.Business;
public class Invoice
{
    public Supplier SupplierDetails { get; set; } = default!;
    public Customer CustomerDetails { get; set; } = default!;
    public long InvoiceNumber { get; set; }
    public string? PONumber { get; set; }
    public DateOnly Date { get; set; }
    public List<InvoiceBody> Description { get; set; } = default!;
    public decimal NetAmount { get; set; }
    public decimal VAT { get; set; }
    public decimal Total { get; set; }
}