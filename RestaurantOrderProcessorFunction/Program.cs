using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

var keyVaultUrl = new Uri("https://restaurant-keyvault-joey.vault.azure.net/");

var secretClient = new SecretClient(
    keyVaultUrl,
    new DefaultAzureCredential());

var secret = secretClient.GetSecret("ServiceBusConnection");

Console.WriteLine($"Key Vault secret loaded successfully: {secret.Value.Name}");

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
