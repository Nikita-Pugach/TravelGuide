using Microsoft.EntityFrameworkCore;
using TravelGuide.Data;
using TravelGuide.Services;
using TravelGuide.Services.Interfaces;
using TravelGuide.Models.Entities;
using TravelGuide.Repositories;
using TravelGuide.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with SQLite
builder.Services.AddDbContext<TravelGuideContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IChatService, ChatService>();

// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Map SignalR hub
app.MapHub<ChatHub>("/chathub");

// Обновляем пароль админа при запуске
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TravelGuideContext>();
    context.Database.EnsureCreated();
    
    // Находим админа и обновляем пароль
    var admin = context.Users.FirstOrDefault(u => u.Role == UserRole.Admin);
    if (admin != null)
    {
        admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123");
        context.SaveChanges();
        Console.WriteLine($"Пароль админа обновлён: {admin.Email} / admin123");
    }
    else
    {
        // Создаём админа если его нет
        admin = new User
        {
            FullName = "Администратор",
            Email = "admin@admin.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Role = UserRole.Admin,
            RegistrationDate = DateTime.Now
        };
        context.Users.Add(admin);
        context.SaveChanges();
        Console.WriteLine("Создан администратор: admin@admin.com / admin123");
    }
}

app.Run();
