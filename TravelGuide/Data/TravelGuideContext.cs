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

    // Бронирования
    public DbSet<Booking> Bookings { get; set; } = null!;

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

        // Конфигурация Booking
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TotalPrice).HasPrecision(12, 2);
            
            entity.HasOne(e => e.Tour)
                .WithMany()
                .HasForeignKey(e => e.TourId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(e => e.ProcessedByUser)
                .WithMany()
                .HasForeignKey(e => e.ProcessedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Заполнение начальными данными через SeedData.cs (вызывается из Program.cs)
    }
}
