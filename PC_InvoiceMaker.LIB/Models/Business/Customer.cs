using PC_InvoiceMaker.LIB.Enums.Business;

namespace PC_InvoiceMaker.LIB.Models.Business;
public class Customer
{
    public string Name { get; set; } = default!;
    public Address Address { get; set; } = default!;
    public CustomerType CustomerType { get; set; }
    public CustomerOriginBloc CustomerOrigin { get; set; }
    public Country.Country Country { get; set; } = default!;
}