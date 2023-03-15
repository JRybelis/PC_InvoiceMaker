using PC_InvoiceMaker.LIB.Interfaces.Services.Web;
using PC_InvoiceMaker.LIB.Models.Country;
using System.Net;
using System.Net.Http.Headers;


namespace PC_InvoiceMaker.Logic.Services.Web;

public class CountriesWebClient : ICountriesWebClient
{
    private HttpClient HttpClient;
    private IResultParser ResultParser;
    private const string REQUEST_WORLD_URL_STRING = "/v3.1/all";
    private const string REQUEST_EU_URL_STRING = "v2/regionalbloc/EU";
    private Uri? RelativeUri;

    public CountriesWebClient(IResultParser parser)
    {
        HttpClient = GetClient();
        ResultParser = parser;
    }

    private HttpClient GetClient()
    {
        HttpClient = new HttpClient(
            new HttpClientHandler
            {
                AllowAutoRedirect = true,
                MaxAutomaticRedirections = 5
            });
        
        HttpClient.BaseAddress = new Uri("https://restcountries.com", UriKind.Absolute);
        HttpClient.DefaultRequestHeaders.Accept.Clear();
        HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;

        return HttpClient;
    }

    public async Task<List<Country>> GetCountriesWorld()
    {
        var requestUrl = $"{REQUEST_WORLD_URL_STRING}?fields=name";
        RelativeUri = new Uri(requestUrl, UriKind.Relative);
        var requestUri = new Uri(HttpClient.BaseAddress!, RelativeUri);

        var responseMessage = await MakeRestRequest(requestUri);
        var responseString = await responseMessage.Content.ReadAsStringAsync();

        return ResultParser.ParseJson(responseString);
    }
    public async Task<List<Country>> GetCountriesEuropeanUnion()
    {
        RelativeUri = new Uri(REQUEST_EU_URL_STRING, UriKind.Relative);
        var requestUri = new Uri(HttpClient.BaseAddress!, RelativeUri);

        var responseMessage = await MakeRestRequest(requestUri);
        var responseString = await responseMessage.Content.ReadAsStringAsync();

        return ResultParser.ParseJson(responseString);
    }

    private async Task<HttpResponseMessage> MakeRestRequest(Uri requestUri)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = requestUri,
            Method = HttpMethod.Get
        };

        var response = await HttpClient.SendAsync(request);
        var statusCode = (int)response.StatusCode;

        if (statusCode is >= 300 and <= 399)
        {
            var redirectUri = response.Headers.Location;
            if (!redirectUri.IsAbsoluteUri)
            {
                redirectUri = new Uri(HttpClient.BaseAddress!, redirectUri);
            }

            return await MakeRestRequest(redirectUri);
        }

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        return response;
    }
}