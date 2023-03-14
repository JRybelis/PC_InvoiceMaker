namespace PC_InvoiceMaker.LIB.Models.Business;
public class Supplier
{
    public string Name { get; set; } = default!;
    public Address Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
}