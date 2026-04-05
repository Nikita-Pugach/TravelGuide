# TravelGuide - Справочник туриста

Веб-приложение для поиска и бронирования туров, отелей и достопримечательностей.

## Требования

- **.NET 9.0 SDK** - [Скачать](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Редактор кода** (рекомендуется Visual Studio 2022 или VS Code)

## Запуск проекта

### Вариант 1: Через командную строку

```bash
# Перейти в папку проекта
cd TravelGuide

# Запустить приложение
dotnet run
```

После запуска откройте браузер по адресу: `http://localhost:5000` или `https://localhost:5001`

### Вариант 2: Через Visual Studio

1. Откройте файл `TravelGuide.sln` или `TravelGuide.csproj`
2. Нажмите F5 или кнопку "Запуск"

### Вариант 3: С указанием порта

```bash
dotnet run --urls="http://localhost:5000"
```

## Структура проекта

```
TravelGuide/
├── Controllers/          # Контроллеры MVC
│   ├── AccountController.cs    - Регистрация, вход, профиль
│   ├── ToursController.cs      - Каталог туров
│   ├── HotelsController.cs     - Каталог отелей
│   ├── SightsController.cs     - Достопримечательности
│   ├── FavoritesController.cs  - Избранные туры
│   └── ChatController.cs       - Чат поддержки
├── Models/
│   └── Entities/         # Сущности БД
│       ├── Tour.cs, Hotel.cs, Sight.cs
│       ├── Country.cs, City.cs
│       ├── User.cs, Review.cs
│       ├── Chat.cs, Message.cs
│       └── Enums.cs      - Перечисления
├── Views/                # Представления Razor
├── ViewModels/           # Модели представлений
├── Services/             # Бизнес-логика
├── Data/                 # Контекст БД
├── Repositories/         # Репозитории
├── wwwroot/              # Статические файлы
│   ├── css/site.css      - Стили
│   └── js/site.js        - Скрипты
├── appsettings.json      - Конфигурация
└── TravelGuide.db        # База данных SQLite
```

## Функционал

### Для всех пользователей
- 🌍 Просмотр каталога туров с фильтрами и пагинацией
- 🏨 Поиск отелей по звёздности, городу, цене
- 📍 Достопримечательности с описанием
- 📝 Регистрация и авторизация

### Для авторизованных пользователей
- ❤️ Добавление туров в избранное
- ⭐ Написание отзывов
- 💬 Чат с поддержкой
- 👤 Редактирование профиля

## База данных

Проект использует **SQLite** - база данных создаётся автоматически при первом запуске в файле `TravelGuide.db`.

### Применение миграций (если есть)

```bash
cd TravelGuide
dotnet ef database update
```

## Конфигурация

Основные настройки в `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=TravelGuide.db"
  }
}
```

## Технологии

- **ASP.NET Core MVC 9.0**
- **Entity Framework Core 9.0**
- **SQLite** - база данных
- **Bootstrap 5** - стили
- **BCrypt.Net** - хеширование паролей

## Разработка

### Добавление новых сущностей

1. Создайте класс в `Models/Entities/`
2. Добавьте `DbSet` в `Data/TravelGuideContext.cs`
3. Создайте миграцию: `dotnet ef migrations add Name`
4. Примените: `dotnet ef database update`

### Сборка для продакшена

```bash
dotnet publish -c Release -o ./publish
```

## Контакты

Курсовой проект по разработке веб-приложения.