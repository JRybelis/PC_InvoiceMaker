using PC_InvoiceMaker.LIB.Interfaces.Services.Web;
using PC_InvoiceMaker.LIB.Models.Country;
using Newtonsoft.Json.Linq;

namespace PC_InvoiceMaker.Logic.Services.Web;
public class ResultParser : IResultParser
{
    public List<Country> ParseJson(string jsonString)
    {
        JObject countriesJson = JObject.Parse(jsonString);

        // get JSON result objects into a list
        IList<JToken> results = countriesJson["name"].Children().ToList();

        // serialize JSON results into .NET objects
        IList<Country> countries = new List<Country>();

        foreach (JToken result in results)
        {
            // JToken.ToObject is a helper method that uses JsonSerializer internally
            Country country = result.ToObject<Country>();
            countries.Add(country);
        }

        return countries.ToList();
    }
}