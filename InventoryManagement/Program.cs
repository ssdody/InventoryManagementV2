using InventoryManagement.Data;
using InventoryManagement.Repositories;
using InventoryManagement.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5005);
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5001/") }); // Changed to HTTP


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5000")  // Ensure matches API URL
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowLocalhost", policy =>
//    {
//        policy.WithOrigins("http://localhost:5000")  // Allow your Blazor client URL
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
