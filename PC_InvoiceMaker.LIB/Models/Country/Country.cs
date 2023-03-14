namespace PC_InvoiceMaker.LIB.Models.Country;
public class Country
{
    public Name Name { get; set; } = default!;
    public string Region { get; set; } = default!;
    public string Subregion { get; set; } = default!;
    public string[] Continents { get; set; } = default!;
    public RegionalBlocs[]? RegionalBlocs { get; set; }
}