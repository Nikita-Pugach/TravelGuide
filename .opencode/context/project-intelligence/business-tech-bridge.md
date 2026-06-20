<!-- Context: project-intelligence/bridge | Priority: high | Version 1.0 | Updated: 2026-06-20 -->

# Business ↔ Tech Bridge — TravelGuide

> Как бизнес-потребности реализуются технически.

## Core Mapping

| Business Need | Technical Solution | Business Value |
|---------------|-------------------|----------------|
| Поиск туров с фильтрами | EF Core LINQ + QueryString параметры | Пользователь быстро находит нужное |
| Просмотр на карте | Leaflet.js + координаты в seed data | Визуальное понимание расположения |
| Отзывы с модерацией | Review.Status enum + Approved фильтр | Качество контента |
| Бронирование туров | Booking entity + статусы | Онлайн-заявки |
| Чат поддержки | SignalR Hub + Session auth | Мгновенная связь |
| Отчёты для админа | ClosedXML + ReportService | Аналитика бизнеса |

## Feature Mapping

### Каталог с фильтрами
- **Business**: Пользователь хочет найти тур по цене, стране, типу
- **Technical**: EF Core Where() цепочки + PaginatedList<T>
- **Value**: Конверсия поиска в бронирования

### Отзывы
- **Business**: Пользователи доверяют отзывам других
- **Technical**: Review entity + модерация через Status + seed data
- **Value**: Доверие к платформе

### Excel-отчёты
- **Business**: Админу нужна аналитика
- **Technical**: ClosedXML + 5 типов отчётов
- **Value**: Принятие решений на основе данных

## Related Files

- `business-domain.md` — бизнес-потребности
- `technical-domain.md` — техническая реализация
