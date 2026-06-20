<!-- Context: project-intelligence/decisions | Priority: high | Version 1.0 | Updated: 2026-06-20 -->

# Decisions Log — TravelGuide

> Архитектурные и проектные решения.

## Decision: Сессии вместо JWT для авторизации

**Date**: 2026-05
**Status**: Decided
**Owner**: Пугач Никита

### Context
Нужна простая авторизация для MVC-приложения. Команда — 1 человек, деплой на локальный сервер.

### Decision
Используем HttpContext.Session для хранения UserId и Role.

### Rationale
- Проще реализовать для MVC
- Не нужен refresh-token логика
- Достаточно для single-tenant приложения

### Impact
- **Positive**: Простая реализация, нет токенов
- **Negative**: Нет масштабируемости на несколько серверов

---

## Decision: SQLite вместо PostgreSQL

**Date**: 2026-05
**Status**: Decided
**Owner**: Пугач Никита

### Context
Курсовой проект, деплой на один компьютер, не нужна высокая нагрузка.

### Decision
SQLite — файл TravelGuide.db в папке проекта.

### Rationale
- Нулевая настройка — нет сервера БД
- Переносимость — один файл
- Достаточно для демо и курсовой

### Impact
- **Positive**: Простой старт, нет зависимостей
- **Negative**: Нет конкурентного доступа

---

## Decision: EnsureCreated() вместо миграций

**Date**: 2026-05
**Status**: Decided
**Owner**: Пугач Никита

### Context
База пересоздаётся при каждом изменении схемы. Нет необходимости в миграциях.

### Decision
context.Database.EnsureCreated() + SeedData.Initialize().

### Rationale
- Упрощает разработку — нет миграционных файлов
- Seed data заполняется автоматически
- Для курсового проекта это нормально

### Impact
- **Positive**: Быстрый старт, нет орм-мусора
- **Negative**: Нельзя изменить схему без пересоздания БД

---

## Decision: Без эмодзи в UI

**Date**: 2026-06
**Status**: Decided
**Owner**: Пугач Никита

### Context
Эмодзи выглядят непрофессионально в курсовом проекте.

### Decision
Убираем все эмодзи из интерфейса. Используем SVG-иконки или текст.

### Impact
- **Positive**: Чистый, профессиональный вид
- **Negative**: Нужно искать альтернативы для иконок
