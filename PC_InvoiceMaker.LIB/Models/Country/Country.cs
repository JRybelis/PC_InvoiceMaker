namespace PC_InvoiceMaker.LIB.Models.Country;
public class Country
{
    public string Name { get; set; } = default!;
    public List<RegionalBloc>? RegionalBlocs { get; set; }
}