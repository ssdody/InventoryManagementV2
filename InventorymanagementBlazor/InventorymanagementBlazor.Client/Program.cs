using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });
//builder.Services.AddScoped<ProductService>();
//builder.Services.AddScoped<StoreService>();
await builder.Build().RunAsync();
