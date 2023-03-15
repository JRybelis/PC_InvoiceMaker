namespace PC_InvoiceMaker.LIB.Dtos;

public class InvoiceBodyDto
{
    public string? Product { get; set; }
    public string? Service { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}