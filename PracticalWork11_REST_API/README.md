# Практическая работа №11 — Разработка REST API

**Тема:** REST API на C# с ASP.NET Core 10, Entity Framework Core и PostgreSQL.

## Цель

Научиться создавать RESTful API, обрабатывать HTTP-запросы, настраивать маршруты, контроллеры и доступ к базе данных через Entity Framework Core.

## Выполнено

- Модель `Student` (`Id`, `Name`, `Age`) и проверка вводимых данных.
- Контекст `SchoolContext` для PostgreSQL.
- Миграция `InitialCreate` для создания таблицы `Students`.
- Контроллер `StudentsController` с операциями GET, POST, PUT, DELETE.
- Файл `RestApiDemo/RestApiDemo.http` с примерами всех запросов.
- Папка `images` со схемой архитектуры и инструкцией по сохранению скриншотов Postman.

## Запуск

Понадобятся .NET SDK **10** и PostgreSQL либо Docker Desktop.

1. Откройте терминал в папке `PracticalWork11_REST_API`.
2. Если используете Docker, запустите БД:
   ```bash
   docker compose up -d
   ```
   При использовании установленного PostgreSQL создайте базу `mdk0104_ib` и измените строку подключения `RestApiDemo/appsettings.json` по настройкам кабинета.
3. Установите инструмент миграций (если ещё не установлен) и примените миграцию:
   ```bash
   dotnet tool install --global dotnet-ef --version 10.0.0
   dotnet restore RestApiDemo/RestApiDemo.csproj
   dotnet ef database update --project RestApiDemo
   ```
4. Запустите REST API:
   ```bash
   dotnet run --project RestApiDemo --launch-profile http
   ```

URL для Postman: `http://localhost:5081/api/students`. Описание OpenAPI: `http://localhost:5081/openapi/v1.json` (режим Development). Интерфейс Swagger UI в проект не добавлен.

> Пароль `postgres` в примере предназначен только для локальной учебной БД. Реальный пароль преподавателя не следует публиковать в репозитории.

## REST API — методы CRUD

| HTTP | Адрес | Назначение | Ответ |
|---|---|---|---|
| GET | `/api/students` | Список студентов | 200 OK |
| GET | `/api/students/{id}` | Студент по идентификатору | 200 OK / 404 |
| POST | `/api/students` | Создать студента | 201 Created |
| PUT | `/api/students/{id}` | Обновить студента | 204 No Content |
| DELETE | `/api/students/{id}` | Удалить студента | 204 No Content / 404 |

Пример POST (Postman: Body → raw → JSON):

```json
{
  "name": "Иван Иванов",
  "age": 19
}
```

Пример PUT `/api/students/1`:

```json
{
  "id": 1,
  "name": "Иван Петров",
  "age": 20
}
```

Проверка GET и DELETE выполняется по идентификатору, присвоенному БД. Изменённый объект можно проверить повторным GET.

## Структура проекта

```text
PracticalWork11_REST_API/
├── README.md
├── docker-compose.yml
├── .gitignore
├── images/
│   ├── README.md
│   └── architecture.svg
└── RestApiDemo/
    ├── RestApiDemo.csproj
    ├── Program.cs
    ├── appsettings.json
    ├── RestApiDemo.http
    ├── Models/Student.cs
    ├── Data/SchoolContext.cs
    ├── Controllers/StudentsController.cs
    ├── Migrations/20261120000000_InitialCreate.cs
    ├── Migrations/SchoolContextModelSnapshot.cs
    └── Properties/launchSettings.json
```

## Контрольные вопросы

**REST API** — интерфейс для работы с ресурсами по HTTP. **GET** читает данные, **POST** создаёт, **PUT** обновляет, **DELETE** удаляет. **Контроллер** обрабатывает запросы и возвращает ответы. **DbContext** организует взаимодействие с базой через EF Core. **Миграции** задают изменения структуры базы данных.

## Критерии оценки и скриншоты

Для оценки «5» по заданию преподавателя требуются работающие CRUD-маршруты, EF Core, тестирование в Postman, README и **настоящие** скриншоты. После запуска API сделайте снимки POST, GET, PUT, DELETE и сохраните их в `images/` по инструкции `images/README.md`.

**Статус проверки:** исходный код подготовлен, но сборка и запросы к действующей БД здесь не запускались. Схема `architecture.svg` не является скриншотом проверки.
