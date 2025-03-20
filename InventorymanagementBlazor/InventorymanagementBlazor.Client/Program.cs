using InventorymanagementBlazor.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5005") });

builder.Services.AddScoped(sp =>
{
    var client = new HttpClient() { BaseAddress = new Uri("http://localhost:5005") };
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    // You can set other headers here if needed
    return client;
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStoreService, StoreService>();
await builder.Build().RunAsync();
