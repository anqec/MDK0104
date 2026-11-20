using Microsoft.EntityFrameworkCore;
using RestApiDemo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("SchoolDatabase")
    ?? throw new InvalidOperationException("Не задана строка подключения SchoolDatabase.");
builder.Services.AddDbContext<SchoolContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // JSON-описание API: /openapi/v1.json
}

app.MapControllers();
app.Run();
