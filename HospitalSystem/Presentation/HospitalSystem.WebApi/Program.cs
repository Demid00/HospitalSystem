using HospitalSystem.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Строка подключения
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
}

// Регистрация Infrastructure слоя (DbContext + репозитории)
builder.Services.AddInfrastructure(builder.Configuration);

// Добавляем контроллеры (минимально)
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

// Применяем миграции при старте
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HospitalSystem.Infrastructure.EntityFramework.Data.AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.Run();