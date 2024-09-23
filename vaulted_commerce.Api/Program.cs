using vaulted_commerce.Framework.Services;
using vaulted_commerce.DataAccessLayer.Context;
using vaulted_commerce.DataAccessLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Context
builder.Services.AddSingleton<MongoDbContext>(sp =>
    new MongoDbContext("your_mongodb_connection_string", "your_database_name"));

// Register Repositories and Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
