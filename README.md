# TravelGuide

A travel booking web app: tours, hotels and sights with reviews and maps, three
user roles, a live support chat and Excel reports.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4)
![SQLite](https://img.shields.io/badge/SQLite-EF%20Core-003B57)

ASP.NET Core 9 MVC on Entity Framework Core with SQLite, Bootstrap 5 for the UI,
Leaflet for maps, SignalR for real-time chat, BCrypt for passwords and ClosedXML
for reports. Ships with 36 unit tests and a Windows installer.

## What it does

- **Catalogues** of tours, hotels and sights with filters — type, price, country,
  star rating, meal plan — and pagination.
- **Detail pages** with an interactive map, reviews and a booking action.
- **Three roles**, each with its own panel: tourist, manager, administrator.
- **Bookings** with status tracking, favourites, and reviews for every entity.
- **Live support chat** over SignalR: a tourist writes, a manager answers in real
  time, history is kept per user.
- **Excel reports** for administration: popular tours, user activity, review
  statistics, manager workload, agency ratings.

## Roles

| Role | What it can do |
|---|---|
| **Tourist** | Browse catalogues, favourite tours, write reviews, book tours, chat with support, edit the profile |
| **Manager** | Manage tours, confirm or cancel bookings, answer support chat, view statistics |
| **Admin** | Manage all content and reference data (countries, cities, agencies), manage users and roles, moderate reviews, export reports |

## Tech stack

| Layer | Choice |
|---|---|
| Framework | ASP.NET Core MVC 9.0 |
| Data | Entity Framework Core 9.0 + SQLite |
| Real-time | SignalR |
| UI | Razor views, Bootstrap 5 |
| Maps | Leaflet.js |
| Passwords | BCrypt.Net |
| Reports | ClosedXML (XLSX) |
| Tests | xUnit + Moq |

## Run it

Requirements: [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0).

```bash
cd TravelGuide
dotnet run --urls="http://localhost:5107"
```

Open <http://localhost:5107>. The SQLite database is created and seeded on the
first run — no setup step.

### Demo accounts

The seed data is for local development only.

| Role | Email | Password |
|---|---|---|
| Administrator | `admin@travelguide.com` | `Admin123!` |
| Manager | `manager@travelguide.com` | `Manager123!` |
| Tourist | `tourist@travelguide.com` | `Tourist123!` |

### Seed data

20 countries, 21 cities, 3 agencies, 60 hotels, 68 sights, 20 tours, 136 reviews,
several bookings and favourites, plus two support chats with messages.

To rebuild the database from scratch, delete `TravelGuide/TravelGuide.db` and run
the app again.

## Tests

```bash
dotnet test
```

36 unit tests cover `AccountService` (registration, login, profile),
`ChatService` (chat lifecycle, messages, read state) and `ReportService`
(report generation).

## Project layout

```
TravelGuide/
├── Controllers/     MVC controllers, incl. Admin, Manager, Reports
├── Models/          entities: Tour, Hotel, Sight, Booking, Review, Chat, User
├── Data/            DbContext, migrations, SeedData
├── Services/        business logic: accounts, chat, reports
├── Repositories/    generic repository over EF Core
├── Hubs/            SignalR chat hub
├── ViewModels/      view models
└── Views/           Razor pages
Installer/           Inno Setup script for a Windows build
Tests/               xUnit project
```

## Installer

`Installer/` contains an Inno Setup script that packages a published build into a
Windows installer, so the app can be installed and started without the SDK.

## Background

This started as a coursework project for the "Software Development and
Maintenance" course at the College of Business and Law (2026). It grew past the
assignment: three roles, a real-time chat, reports and a test suite are all
beyond what was required.
