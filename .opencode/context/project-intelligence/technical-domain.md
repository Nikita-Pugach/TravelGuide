<!-- Context: project-intelligence/technical | Priority: critical | Version 1.0 | Updated: 2026-06-20 -->

# Technical Domain — TravelGuide

**Purpose**: Технический стек, архитектура и стандарты разработки TravelGuide.
**Last Updated**: 2026-06-20

## Quick Reference
- **Update Triggers**: Смена стека | Новые компоненты | Изменение стандартов
- **Audience**: Разработчики, AI-агенты

## Primary Stack

| Layer | Technology | Примечание |
|-------|-----------|------------|
| Backend | ASP.NET Core MVC 9.0 (C#) | Контроллеры, Razor Views |
| ORM | Entity Framework Core 9.0 | Include/ThenInclude для навигации |
| Database | SQLite | TravelGuide.db, EnsureCreated() |
| Auth | Сессии (HttpContext.Session) | Role enum: Admin=1, Manager=2, Tourist=3 |
| Realtime | SignalR | Чат поддержки |
| Maps | Leaflet.js | Интерактивные карты отелей/достопримечательностей |
| CSS | Bootstrap 5 + CSS Variables | Кастомные стили в site.css |
| Passwords | BCrypt.Net | Хеширование паролей |
| Reports | ClosedXML | Excel-экспорт отчётов |
| Testing | xUnit + Moq | 36 unit-тестов |
| ORM Migrations | EnsureCreated() | Не миграции — пересоздание БД |

## Architecture

**MVC-монолит** — стандартная архитектура ASP.NET Core MVC.

```
Controllers/        → бизнес-логика, запросы
Models/Entities/    → сущности БД (POCO)
Views/              → Razor-шаблоны (.cshtml)
Data/               → DbContext + SeedData
Services/           → бизнес-сервисы (Account, Chat, Report)
Repositories/       → generic-репозиторий
ViewModels/         → модели представлений
Hubs/               → SignalR хабы
wwwroot/            → статика (CSS, JS, загрузки)
```

## Code Principles

1. **C# 12+** — nullable references, файловые пространства
2. **Русские комментарии** — XML-doc на русском
3. **Русские строки в UI** — весь интерфейс на русском
4. **Без эмодзи** — нет эмодзи в интерфейсе
5. **Без длинных тире** — запятые или обычные тире в текстах
6. **Сессии для auth** — HttpContext.Session, не JWT
7. **Коммиты на английском** — Conventional Commits: fix:/feat:/chore:/docs:

## Quality Standards

- EF Core Include/ThenInclude — всегда eager loading навигации
- Review.Status = Approved — модерация отзывов через enum
- SeedData — все FK через .Id после SaveChanges()
- Тесты — Arrange-Act-Assert, мокаем DbContext

## Security

- Роли: Admin > Manager > Tourist
- Пароли через BCrypt
- anti-CSRF: @Html.AntiForgeryToken() в формах
- Сессия: IdleTimeout 30 мин

## DB Management

- `EnsureCreated()` — создание схемы при первом запуске
- Пересоздание: удалить TravelGuide.db → запустить заново
- SeedData.Initialize() — проверка `if (context.Tours.Any()) return;`

## Related Files

- `decisions-log.md` — архитектурные решения
- `business-domain.md` — бизнес-контекст
