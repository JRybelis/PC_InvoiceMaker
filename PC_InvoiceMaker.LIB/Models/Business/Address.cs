namespace PC_InvoiceMaker.LIB.Models.Business;
public class Address
{
    public string AddressLine1 { get; set; } = default!;
    public string TownCity { get; set; } = default!;
    public Country.Country Country { get; set; } = default!;
    public string Postcode { get; set; } = default!;
}