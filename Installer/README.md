# Установщик TravelGuide

## Как создать installer.exe

### Шаг 1: Установите Inno Setup

Скачайте и установите **Inno Setup** (бесплатный):
https://jrsoftware.org/isdl.php

### Шаг 2: Опубликуйте проект

```bash
cd C:\Curs\TravelGuide
dotnet publish -c Release
```

### Шаг 3: Скомпилируйте установщик

1. Откройте `TravelGuideSetup.iss` в Inno Setup
2. Нажмите **Build → Compile** (или **F9**)
3. Дождитесь завершения компиляции

### Шаг 4: Найдите installer.exe

Готовый файл будет в папке:
```
C:\Curs\Installer\Output\TravelGuideSetup.exe
```

## Что делает установщик

- Устанавливает TravelGuide в `C:\Program Files\TravelGuide`
- Создаёт ярлык на рабочем столе
- Проверяет наличие .NET 9.0 SDK
- Открывает приложение после установки
- Деинсталлятор в `Панель управления → Удаление программ`

## Тестовые учётные данные

| Роль | Email | Пароль |
|------|-------|--------|
| Администратор | admin@travelguide.com | Admin123! |
| Менеджер | manager@travelguide.com | Manager123! |
| Турист | tourist@travelguide.com | Tourist123! |
