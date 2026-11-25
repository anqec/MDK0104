# Практическая работа №11

**Тема:** Разработка REST API

## Задание
1. Создать модель Student (Id, Name, Age).
2. Создать DbContext SchoolContext и настроить подключение к PostgreSQL.
3. Создать и применить миграцию InitialCreate.
4. Создать контроллер StudentsController с CRUD методами.
5. Настроить маршрутизацию REST API.
6. Проверить работу API через Postman и сохранить скриншоты.

## Код

### Основной код программы (Program.cs)
```csharp
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
```

### Модель студента (Models/Student.cs)
```csharp
using System.ComponentModel.DataAnnotations;

namespace RestApiDemo.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Range(1, 120)]
    public int Age { get; set; }
}
```

### Контекст базы данных (Data/SchoolContext.cs)
```csharp
using Microsoft.EntityFrameworkCore;
using RestApiDemo.Models;

namespace RestApiDemo.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; } = null!;
}
```

### Контроллер студентов (Controllers/StudentsController.cs)
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiDemo.Data;
using RestApiDemo.Models;

namespace RestApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly SchoolContext _context;

    public StudentsController(SchoolContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        Student? student = await _context.Students.FindAsync(id);
        if (student is null)
        {
            return NotFound();
        }

        return student;
    }

    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        student.Id = 0; // Идентификатор назначает база данных.
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, Student student)
    {
        Student? existing = await _context.Students.FindAsync(id);
        if (existing is null)
        {
            return NotFound();
        }

        existing.Name = student.Name;
        existing.Age = student.Age;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        Student? student = await _context.Students.FindAsync(id);
        if (student is null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
```

### Миграция базы данных (Migrations/20261125000000_InitialCreate.cs)
```csharp
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RestApiDemo.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Students",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                Age = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Students", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Students");
    }
}
```

### Настройка подключения (appsettings.json)
```json
{
  "ConnectionStrings": {
    "SchoolContext": "Host=localhost;Port=5432;Database=mdk0104_ib;Username=postgres;Password=postgres"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## Запуск программы

Необходимы .NET 10 SDK, PostgreSQL и Postman. Используйте строку подключения, выданную преподавателем. В `appsettings.json` указаны примерные параметры из задания. Если пароль на компьютере отличается, измените его локально; не публикуйте настоящий пароль. Можно задать строку через переменную окружения PowerShell:

```powershell
$env:ConnectionStrings__SchoolContext = "Host=localhost;Port=5432;Database=mdk0104_ib;Username=postgres;Password=ВАШ_ПАРОЛЬ"
```

Откройте PowerShell в папке `pr11KEM` и выполните:

```powershell
cd RestApiDemo
dotnet restore
dotnet tool restore
dotnet ef database update
dotnet run
```

Миграция InitialCreate уже включена в проект, повторно создавать её не нужно. Если таблица Students уже существует от предыдущей работы, не удаляйте её или базу: согласуйте с преподавателем использование отдельной базы либо миграцию существующей схемы. Для новой БД у пользователя должны быть права на её создание или база должна быть создана заранее.

Адрес API: `http://localhost:5111/api/students`.
Документ OpenAPI: `http://localhost:5111/openapi/v1.json`.

## Проверка API через Postman

Импортируйте файл `RestApiDemo.postman_collection.json` через кнопку Import в Postman. Запустите запросы по порядку: POST создаёт студента и запоминает ID, далее используются этот ID и заданный адрес сервера.

| Метод | Маршрут | Ожидаемый результат |
| --- | --- | --- |
| POST | /api/students | 201 Created |
| GET | /api/students | 200 OK |
| GET | /api/students/{id} | 200 OK |
| PUT | /api/students/{id} | 204 No Content |
| DELETE | /api/students/{id} | 204 No Content |
| GET (после удаления) | /api/students/{id} | 404 Not Found |
| POST (некорректный возраст) | /api/students | 400 Bad Request |

Пример тела POST:
```json
{
  "name": "Иван Иванов",
  "age": 19
}
```

Пример тела PUT (ID берётся из адреса запроса):
```json
{
  "name": "Иван Петров",
  "age": 20
}
```

## Скриншоты

Сохраните реальные скриншоты после проверки в папке `images` (создайте её при необходимости). Ниже подготовлены ссылки для отчёта; изображения появятся после сохранения файлов.

### Создание студента
![POST запрос](images/screenshot_post.png)

### Получение списка студентов
![GET запрос](images/screenshot_get.png)

### Получение студента по ID
![GET по ID](images/screenshot_get_id.png)

### Обновление студента
![PUT запрос](images/screenshot_put.png)

### Проверка обновления
![GET после обновления](images/screenshot_get_updated.png)

### Удаление студента
![DELETE запрос](images/screenshot_delete.png)

### Проверка удаления
![404 после удаления](images/screenshot_not_found.png)
