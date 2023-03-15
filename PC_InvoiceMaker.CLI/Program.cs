using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using PC_InvoiceMaker.CLI;
using PC_InvoiceMaker.CLI.Interfaces;
using PC_InvoiceMaker.LIB.Interfaces.Services;
using PC_InvoiceMaker.LIB.Interfaces.Services.Validation;
using PC_InvoiceMaker.LIB.Interfaces.Services.Web;
using PC_InvoiceMaker.Logic.Services;
using PC_InvoiceMaker.Logic.Services.Validation;
using PC_InvoiceMaker.Logic.Services.Web;

class Program
{
    public static Task<int> Main(string[] args) =>
        CommandLineApplication.ExecuteAsync<Program>(args);
    
    public async Task<int> OnExcecuteAsync(CancellationToken cancellationToken)
    {
        var services = new ServiceCollection();

        services.AddScoped<InvoiceMakerCli>();
        services.AddScoped<IInvoiceMakerCli>(implementationFactory: service =>
            service.GetRequiredService<InvoiceMakerCli>());
        services.AddScoped<IInvoiceViewService>(implementationFactory: service =>
            service.GetRequiredService<InvoiceMakerCli>());
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<ICountryValidationService, CountryValidationService>();
        services.AddScoped<ICountriesWebClient, CountriesWebClient>();
        services.AddScoped<IResultParser, ResultParser>();

        await using ServiceProvider serviceProvider = 
            services.BuildServiceProvider(validateScopes: true);

        using (IServiceScope scope = serviceProvider.CreateScope())
        {
            var invoiceMakerConsole = scope.ServiceProvider.GetRequiredService<IInvoiceMakerCli>();
            invoiceMakerConsole.Run();
        }

        return Environment.ExitCode;
    }
}