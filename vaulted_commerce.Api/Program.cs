using vaulted_commerce.Framework.Services;
using vaulted_commerce.DataAccessLayer.Context;
using vaulted_commerce.DataAccessLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Context
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<MongoDbContext>();
// Register Repositories and Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
