using Microsoft.EntityFrameworkCore;
using TravelGuide.Models.Entities;

namespace TravelGuide.Data;

/// <summary>
/// Контекст базы данных туристического каталога
/// </summary>
public class TravelGuideContext : DbContext
{
    public TravelGuideContext(DbContextOptions<TravelGuideContext> options)
        : base(options)
    {
    }

    // Справочники
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Hotel> Hotels { get; set; } = null!;
    public DbSet<Sight> Sights { get; set; } = null!;
    public DbSet<Agency> Agencies { get; set; } = null!;

    // Основные сущности
    public DbSet<Tour> Tours { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;

    // Чат
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;

    // Связующие таблицы
    public DbSet<TourHotel> TourHotels { get; set; } = null!;
    public DbSet<TourSight> TourSights { get; set; } = null!;
    public DbSet<FavoriteTour> FavoriteTours { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфигурация Country
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Конфигурация City
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            
            entity.HasOne(e => e.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация Hotel
        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            
            entity.HasOne(e => e.City)
                .WithMany(c => c.Hotels)
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация Sight
        modelBuilder.Entity<Sight>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            
            entity.HasOne(e => e.City)
                .WithMany(c => c.Sights)
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация Agency
        modelBuilder.Entity<Agency>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ContactInfo).IsRequired().HasMaxLength(500);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Конфигурация Tour
        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasPrecision(10, 2);
            
            entity.HasOne(e => e.Country)
                .WithMany(c => c.Tours)
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Agency)
                .WithMany(a => a.Tours)
                .HasForeignKey(e => e.AgencyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
            
            entity.HasOne(e => e.Agency)
                .WithMany(a => a.Managers)
                .HasForeignKey(e => e.AgencyId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Конфигурация Review
        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Text).IsRequired().HasMaxLength(5000);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Tour)
                .WithMany(t => t.Reviews)
                .HasForeignKey(e => e.TourId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Hotel)
                .WithMany(h => h.Reviews)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Sight)
                .WithMany(s => s.Reviews)
                .HasForeignKey(e => e.SightId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация Chat
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.Chats)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Manager)
                .WithMany(u => u.ManagedChats)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Конфигурация Message
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Text).IsRequired().HasMaxLength(5000);
            
            entity.HasOne(e => e.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(e => e.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(e => e.SenderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация TourHotel (many-to-many)
        modelBuilder.Entity<TourHotel>(entity =>
        {
            entity.HasKey(th => new { th.TourId, th.HotelId });
            
            entity.HasOne(th => th.Tour)
                .WithMany(t => t.TourHotels)
                .HasForeignKey(th => th.TourId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(th => th.Hotel)
                .WithMany(h => h.TourHotels)
                .HasForeignKey(th => th.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация TourSight (many-to-many)
        modelBuilder.Entity<TourSight>(entity =>
        {
            entity.HasKey(ts => new { ts.TourId, ts.SightId });
            
            entity.HasOne(ts => ts.Tour)
                .WithMany(t => t.TourSights)
                .HasForeignKey(ts => ts.TourId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(ts => ts.Sight)
                .WithMany(s => s.TourSights)
                .HasForeignKey(ts => ts.SightId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация FavoriteTour (many-to-many)
        modelBuilder.Entity<FavoriteTour>(entity =>
        {
            entity.HasKey(ft => new { ft.UserId, ft.TourId });
            
            entity.HasOne(ft => ft.User)
                .WithMany(u => u.FavoriteTours)
                .HasForeignKey(ft => ft.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(ft => ft.Tour)
                .WithMany(t => t.Favorites)
                .HasForeignKey(ft => ft.TourId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Заполнение начальными данными
        SeedData(modelBuilder);
    }

    /// <summary>
    /// Заполнение базы начальными данными
    /// </summary>
    private void SeedData(ModelBuilder modelBuilder)
    {
        // Добавление стран
        modelBuilder.Entity<Country>().HasData(
            new Country { Id = 1, Name = "Турция", Description = "Безвизовая страна. Тёплый климат, средиземноморское побережье." },
            new Country { Id = 2, Name = "Египет", Description = "Безвизовая страна. Песчаные пляжи, Красное море." },
            new Country { Id = 3, Name = "Италия", Description = "Требуется виза Шенген. Богатая история, кухня, искусство." },
            new Country { Id = 4, Name = "Испания", Description = "Требуется виза Шенген. Пляжи, архитектура, фламенко." },
            new Country { Id = 5, Name = "Таиланд", Description = "Виза по прилёту. Тропический климат, буддийские храмы." }
        );

        // Добавление агентств
        modelBuilder.Entity<Agency>().HasData(
            new Agency { Id = 1, Name = "TravelWorld", ContactInfo = "info@travelworld.com, +7 (495) 123-45-67", Description = "Крупнейший туроператор с 15-летним опытом", Rating = 4.8 },
            new Agency { Id = 2, Name = "SunTour", ContactInfo = "contact@suntour.ru, +7 (495) 987-65-43", Description = "Специализация на пляжном отдыхе", Rating = 4.5 },
            new Agency { Id = 3, Name = "AdventureClub", ContactInfo = "info@adventure.club, +7 (495) 555-44-33", Description = "Экстремальный и приключенческий туризм", Rating = 4.7 }
        );

        // Добавление городов
        modelBuilder.Entity<City>().HasData(
            new City { Id = 1, Name = "Анталья", CountryId = 1 },
            new City { Id = 2, Name = "Стамбул", CountryId = 1 },
            new City { Id = 3, Name = "Хургада", CountryId = 2 },
            new City { Id = 4, Name = "Шарм-эль-Шейх", CountryId = 2 },
            new City { Id = 5, Name = "Рим", CountryId = 3 },
            new City { Id = 6, Name = "Венеция", CountryId = 3 },
            new City { Id = 7, Name = "Барселона", CountryId = 4 },
            new City { Id = 8, Name = "Мадрид", CountryId = 4 },
            new City { Id = 9, Name = "Паттайя", CountryId = 5 },
            new City { Id = 10, Name = "Пхукет", CountryId = 5 }
        );

        // Добавление отелей
        modelBuilder.Entity<Hotel>().HasData(
            new Hotel { Id = 1, Name = "Rixos Premium Belek", CityId = 1, Stars = 5, Description = "Роскошный отель премиум-класса", PricePerNight = 15000, MealType = MealType.AI, DistanceToBeach = 100, Latitude = 36.8567, Longitude = 31.0574 },
            new Hotel { Id = 2, Name = "Titanic Deluxe Lara", CityId = 1, Stars = 5, Description = "Семейный отель с аквапарком", PricePerNight = 12000, MealType = MealType.AI, DistanceToBeach = 50, Latitude = 36.8601, Longitude = 30.7893 },
            new Hotel { Id = 3, Name = "Hilton Hurghada Plaza", CityId = 3, Stars = 5, Description = "Отель на берегу Красного моря", PricePerNight = 10000, MealType = MealType.FB, DistanceToBeach = 0, Latitude = 27.2579, Longitude = 33.8116 },
            new Hotel { Id = 4, Name = "Four Seasons Sharm El Sheikh", CityId = 4, Stars = 5, Description = "Премиум отель с собственным рифом", PricePerNight = 20000, MealType = MealType.AI, DistanceToBeach = 0, Latitude = 27.9486, Longitude = 34.3899 },
            new Hotel { Id = 5, Name = "Hotel Artemide Rome", CityId = 5, Stars = 4, Description = "Отель в историческом центре Рима", PricePerNight = 8000, MealType = MealType.BB, DistanceToBeach = null, Latitude = 41.9028, Longitude = 12.4964 },
            new Hotel { Id = 6, Name = "Hotel Danieli Venice", CityId = 6, Stars = 5, Description = "Исторический отель у каналов", PricePerNight = 25000, MealType = MealType.BB, DistanceToBeach = null, Latitude = 45.4343, Longitude = 12.3387 },
            new Hotel { Id = 7, Name = "Hotel Arts Barcelona", CityId = 7, Stars = 5, Description = "Дизайнерский отель у пляжа", PricePerNight = 18000, MealType = MealType.HB, DistanceToBeach = 200, Latitude = 41.3674, Longitude = 2.1965 },
            new Hotel { Id = 8, Name = "Mandarin Oriental Bangkok", CityId = 9, Stars = 5, Description = "Люкс-отель на реке Чаупхрая", PricePerNight = 15000, MealType = MealType.BB, DistanceToBeach = null, Latitude = 13.7409, Longitude = 100.5423 },
            new Hotel { Id = 9, Name = "Katathani Phuket", CityId = 10, Stars = 5, Description = "Отель на пляже Кататхани", PricePerNight = 12000, MealType = MealType.AI, DistanceToBeach = 0, Latitude = 7.8253, Longitude = 98.2971 }
        );

        // Добавление достопримечательностей
        modelBuilder.Entity<Sight>().HasData(
            new Sight { Id = 1, Name = "Водопад Дюден", CityId = 1, Address = "Duden Park", Description = "Красивый водопад в парке", Type = SightType.Nature, Latitude = 36.8575, Longitude = 30.7853 },
            new Sight { Id = 2, Name = "Пирамиды Гизы", CityId = 3, Address = "Giza Plateau", Description = "Древние пирамиды Египта", Type = SightType.Historical, Latitude = 29.9792, Longitude = 31.1342 },
            new Sight { Id = 3, Name = "Колизей", CityId = 5, Address = "Piazza del Colosseo", Description = "Древнеримский амфитеатр", Type = SightType.Historical, Latitude = 41.8902, Longitude = 12.4922 },
            new Sight { Id = 4, Name = "Собор Святого Марка", CityId = 6, Address = "Piazza San Marco", Description = "Шедевр византийской архитектуры", Type = SightType.Cathedral, Latitude = 45.4343, Longitude = 12.3387 },
            new Sight { Id = 5, Name = "Саграда Фамилия", CityId = 7, Address = "Carrer de Mallorca", Description = "Шедевр Гауди", Type = SightType.Cathedral, Latitude = 41.4036, Longitude = 2.1744 },
            new Sight { Id = 6, Name = "Храм Истины", CityId = 9, Address = "Soi Naklua", Description = "Уникальный деревянный храм", Type = SightType.Cathedral, Latitude = 12.9857, Longitude = 100.8865 },
            new Sight { Id = 7, Name = "Большой Будда", CityId = 10, Address = "Nakkerd Hills", Description = "45-метровая статуя Будды", Type = SightType.Monument, Latitude = 7.8294, Longitude = 98.3193 }
        );

        // Добавление туров
        modelBuilder.Entity<Tour>().HasData(
            new Tour { Id = 1, Name = "Турция: Все включено", TourType = TourType.Beach, Description = "Отдых в отеле Rixos Premium Belek по системе всё включено. Прекрасные пляжи, бассейны, SPA.", Duration = 7, Price = 65000, CountryId = 1, AgencyId = 1, CreatedDate = new DateTime(2026, 1, 15) },
            new Tour { Id = 2, Name = "Египет: Красное море", TourType = TourType.Beach, Description = "Дайвинг и сноркелинг на Красном море. Прекрасные коралловые рифы.", Duration = 10, Price = 55000, CountryId = 2, AgencyId = 2, CreatedDate = new DateTime(2026, 1, 20) },
            new Tour { Id = 3, Name = "Италия: Классика", TourType = TourType.Excursion, Description = "Рим, Флоренция, Венеция за 8 дней. Обзорные экскурсии, музеи, дегустации.", Duration = 8, Price = 85000, CountryId = 3, AgencyId = 1, CreatedDate = new DateTime(2026, 2, 1) },
            new Tour { Id = 4, Name = "Испания: Барселона", TourType = TourType.Excursion, Description = "Архитектура Гауди, пляжи, испанская кухня. Экскурсии с гидом.", Duration = 5, Price = 45000, CountryId = 4, AgencyId = 1, CreatedDate = new DateTime(2026, 2, 10) },
            new Tour { Id = 5, Name = "Таиланд: Пхукет", TourType = TourType.Beach, Description = "Отдых на тропическом острове. Экскурсии к храмам, дайвинг, тайский массаж.", Duration = 14, Price = 95000, CountryId = 5, AgencyId = 3, CreatedDate = new DateTime(2026, 2, 15) },
            new Tour { Id = 6, Name = "Турция: Стамбул", TourType = TourType.Excursion, Description = "Гранд-базар, Голубая мечеть, дворец Топкапы. Шоппинг и история.", Duration = 4, Price = 35000, CountryId = 1, AgencyId = 2, CreatedDate = new DateTime(2026, 2, 20) },
            new Tour { Id = 7, Name = "Египет: Каир и Луксор", TourType = TourType.Excursion, Description = "Пирамиды, Сфинкс, Долина царей. История Древнего Египта.", Duration = 6, Price = 60000, CountryId = 2, AgencyId = 3, CreatedDate = new DateTime(2026, 3, 1) },
            new Tour { Id = 8, Name = "Комбинированный: Турция+Египет", TourType = TourType.Combined, Description = "Стамбул и Шарм-эль-Шейх. Экскурсии и пляжный отдых.", Duration = 12, Price = 110000, CountryId = 1, AgencyId = 1, CreatedDate = new DateTime(2026, 3, 5) }
        );

        // Связь туров с отелями
        modelBuilder.Entity<TourHotel>().HasData(
            new TourHotel { TourId = 1, HotelId = 1 },
            new TourHotel { TourId = 2, HotelId = 3 },
            new TourHotel { TourId = 3, HotelId = 5 },
            new TourHotel { TourId = 3, HotelId = 6 },
            new TourHotel { TourId = 4, HotelId = 7 },
            new TourHotel { TourId = 5, HotelId = 9 },
            new TourHotel { TourId = 6, HotelId = 2 },
            new TourHotel { TourId = 7, HotelId = 4 },
            new TourHotel { TourId = 8, HotelId = 2 },
            new TourHotel { TourId = 8, HotelId = 4 }
        );

        // Связь туров с достопримечательностями
        modelBuilder.Entity<TourSight>().HasData(
            new TourSight { TourId = 1, SightId = 1 },
            new TourSight { TourId = 3, SightId = 3 },
            new TourSight { TourId = 3, SightId = 4 },
            new TourSight { TourId = 4, SightId = 5 },
            new TourSight { TourId = 5, SightId = 7 },
            new TourSight { TourId = 7, SightId = 2 }
        );

        // Добавление администратора (пароль: Admin123!)
        modelBuilder.Entity<User>().HasData(
            new User 
            { 
                Id = 1, 
                FullName = "Администратор Системы", 
                Email = "admin@travelguide.com", 
                PasswordHash = "$2a$11$placeholderWillBeUpdatedOnFirstRun",
                Role = UserRole.Admin,
                RegistrationDate = new DateTime(2026, 1, 1),
                IsBlocked = false
            }
        );
    }
}
