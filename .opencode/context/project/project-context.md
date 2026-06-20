<!-- Context: project/project-context | Priority: medium | Version 1.0 | Updated: 2026-06-20 -->

# Project Context — TravelGuide

## Technology Stack

**Primary Language:** C# 12+
**Framework:** ASP.NET Core MVC 9.0
**ORM:** Entity Framework Core 9.0
**Database:** SQLite
**Realtime:** SignalR
**Testing:** xUnit + Moq

## Project Structure

```
TravelGuide/
├── Controllers/         # MVC контроллеры
├── Models/Entities/     # Сущности БД
├── Views/               # Razor-шаблоны
├── Data/                # DbContext + SeedData
├── Services/            # Бизнес-логика
├── Repositories/        # Generic-репозиторий
├── ViewModels/          # Модели представлений
├── Hubs/                # SignalR хабы
├── wwwroot/             # Статика (CSS, JS, загрузки)
└── Tests/               # xUnit тесты
```

## Key Patterns

- **Controllers** — Include/ThenInclude для eager loading навигации
- **SeedData** — FK через .Id после SaveChanges()
- **Review.Status** — модерация через enum (Pending/Approved/Rejected)
- **Сессии** — HttpContext.Session для auth (не JWT)
- **Роли** — Admin=1, Manager=2, Tourist=3
