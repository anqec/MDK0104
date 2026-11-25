using Microsoft.EntityFrameworkCore;
using RestApiDemo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

string connectionString = builder.Configuration.GetConnectionString("SchoolContext")
    ?? throw new InvalidOperationException("Не задана строка подключения к PostgreSQL.");

builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

app.MapOpenApi();
app.MapControllers();

app.Run();
