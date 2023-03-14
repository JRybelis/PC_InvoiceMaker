using PC_InvoiceMaker.LIB.Enums.Business;

namespace PC_InvoiceMaker.LIB.Models.Business;

public class InvoiceBody {
    public string? Product { get; set; }
    public string? Service { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public Tax Tax { get; set; }
    public decimal Amount { get; set; }
}