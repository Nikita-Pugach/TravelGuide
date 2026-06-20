<!-- Context: project-intelligence/notes | Priority: high | Version 1.0 | Updated: 2026-06-20 -->

# Living Notes — TravelGuide

> Текущее состояние, известные проблемы, открытые вопросы.

## Technical Debt

| Item | Impact | Priority | Mitigation |
|------|--------|----------|------------|
| EnsureCreated() вместо миграций | Нельзя изменить схему мягко | Low | Для курсового ок |
| SQLite — нет конкурентного доступа | Ограничение на 1 пользователя | Low | Не нужно для демо |
| SeedData ~780 строк | Сложно поддерживать | Medium | Работает, не трогаем |

## Known Issues

| Issue | Severity | Workaround | Status |
|-------|----------|------------|--------|
| Эмодзи в tour listings (было) | Low | Убрано | Fixed |
| Отзывы отелей не отображались (было) | High | Пересоздание БД | Fixed |
| Средний рейтинг туров без отзывов = 0 | Medium | Исключаем из расчёта | Fixed |

## Patterns & Conventions

### Code Patterns Worth Preserving
- **Include/ThenInclude** — всегда eager loading для навигации в контроллерах
- **Review.Status = Approved** — модерация через enum в seed data
- **tour-card CSS class** — единый стиль карточек для туров/отелей/достопримечательностей
- **info-chip** — чипы с информацией на hero-секциях

### Gotchas for Maintainers
- TravelGuide.db пересоздаётся при удалении файла — seed заполняет заново
- SeedData.Initialize() проверяет `if (context.Tours.Any()) return;`
- Сессии хранятся в памяти (DistributedMemoryCache) — при перезапуске теряются
- Нет Razor Runtime Compilation — нужно перезапускать сервер для .cshtml изменений

## Active Projects

| Project | Goal | Owner | Timeline |
|---------|------|-------|----------|
| Заполнение seed data | Реалистичные данные для демо | Пугач | Завершено |
| UI fixes | Убрать эмодзи, исправить стили | Пугач | Завершено |
| Отзывы на отели | Каждый отель с отзывом | Пугач | Завершено |

## Related Files

- `decisions-log.md` — прошлые решения
- `technical-domain.md` — технический контекст
- `business-domain.md` — бизнес-контекст
