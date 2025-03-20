using InventorymanagementBlazor.Client.Services;
using InventorymanagementBlazor.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5005") });
//unsafe for prod
builder.Services.AddScoped(sp =>
{
    var handler = new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    return new HttpClient(handler)
    {
        BaseAddress = new Uri("http://localhost:5005/") // Your API URL
    };
});


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Register your services

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStoreService, StoreService>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(InventorymanagementBlazor.Client._Imports).Assembly);

app.Run();
