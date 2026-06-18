using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelGuide.Migrations
{
    /// <inheritdoc />
    public partial class AddCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ContactInfo = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Rating = table.Column<double>(type: "REAL", nullable: true),
                    LogoUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    PhotoUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsBlocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    AvatarUrl = table.Column<string>(type: "TEXT", nullable: true),
                    AgencyId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TourType = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    AgencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Route = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsSeasonal = table.Column<bool>(type: "INTEGER", nullable: false),
                    SeasonalMonths = table.Column<string>(type: "TEXT", nullable: true),
                    PhotoUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ViewCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<double>(type: "REAL", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tours_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ManagerId = table.Column<int>(type: "INTEGER", nullable: true),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Chats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Stars = table.Column<int>(type: "INTEGER", nullable: false),
                    MealType = table.Column<int>(type: "INTEGER", nullable: false),
                    DistanceToBeach = table.Column<int>(type: "INTEGER", nullable: true),
                    DistanceToCenter = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    PricePerNight = table.Column<decimal>(type: "TEXT", nullable: true),
                    PhotoUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Longitude = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    PhotoUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Longitude = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sights_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TourId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GuestsCount = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", precision: 12, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProcessedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProcessedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ManagerNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_ProcessedByUserId",
                        column: x => x.ProcessedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteTours",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TourId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteTours", x => new { x.UserId, x.TourId });
                    table.ForeignKey(
                        name: "FK_FavoriteTours_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteTours_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChatId = table.Column<int>(type: "INTEGER", nullable: false),
                    SenderId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourHotels",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "INTEGER", nullable: false),
                    HotelId = table.Column<int>(type: "INTEGER", nullable: false),
                    DayNumber = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourHotels", x => new { x.TourId, x.HotelId });
                    table.ForeignKey(
                        name: "FK_TourHotels_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourHotels_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TourId = table.Column<int>(type: "INTEGER", nullable: true),
                    HotelId = table.Column<int>(type: "INTEGER", nullable: true),
                    SightId = table.Column<int>(type: "INTEGER", nullable: true),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    PhotoUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Sights_SightId",
                        column: x => x.SightId,
                        principalTable: "Sights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourSights",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "INTEGER", nullable: false),
                    SightId = table.Column<int>(type: "INTEGER", nullable: false),
                    DayNumber = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourSights", x => new { x.TourId, x.SightId });
                    table.ForeignKey(
                        name: "FK_TourSights_Sights_SightId",
                        column: x => x.SightId,
                        principalTable: "Sights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourSights_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Agencies",
                columns: new[] { "Id", "ContactInfo", "Description", "LogoUrl", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "info@travelworld.com, +7 (495) 123-45-67", "Крупнейший туроператор с 15-летним опытом", null, "TravelWorld", 4.7999999999999998 },
                    { 2, "contact@suntour.ru, +7 (495) 987-65-43", "Специализация на пляжном отдыхе", null, "SunTour", 4.5 },
                    { 3, "info@adventure.club, +7 (495) 555-44-33", "Экстремальный и приключенческий туризм", null, "AdventureClub", 4.7000000000000002 }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Description", "Name", "PhotoUrl" },
                values: new object[,]
                {
                    { 1, "Безвизовая страна. Тёплый климат, средиземноморское побережье.", "Турция", null },
                    { 2, "Безвизовая страна. Песчаные пляжи, Красное море.", "Египет", null },
                    { 3, "Требуется виза Шенген. Богатая история, кухня, искусство.", "Италия", null },
                    { 4, "Требуется виза Шенген. Пляжи, архитектура, фламенко.", "Испания", null },
                    { 5, "Виза по прилёту. Тропический климат, буддийские храмы.", "Таиланд", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AgencyId", "AvatarUrl", "Email", "FullName", "IsBlocked", "PasswordHash", "Phone", "RegistrationDate", "Role" },
                values: new object[] { 1, null, null, "admin@travelguide.com", "Администратор Системы", false, "$2a$11$placeholderWillBeUpdatedOnFirstRun", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Анталья" },
                    { 2, 1, "Стамбул" },
                    { 3, 2, "Хургада" },
                    { 4, 2, "Шарм-эль-Шейх" },
                    { 5, 3, "Рим" },
                    { 6, 3, "Венеция" },
                    { 7, 4, "Барселона" },
                    { 8, 4, "Мадрид" },
                    { 9, 5, "Паттайя" },
                    { 10, 5, "Пхукет" }
                });

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "AgencyId", "CountryId", "CreatedDate", "Description", "Duration", "EndDate", "IsSeasonal", "Name", "PhotoUrl", "Price", "Rating", "Route", "SeasonalMonths", "StartDate", "TourType", "ViewCount" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Отдых в отеле Rixos Premium Belek по системе всё включено. Прекрасные пляжи, бассейны, SPA.", 7, null, false, "Турция: Все включено", null, 65000m, null, null, null, null, 2, 0 },
                    { 2, 2, 2, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дайвинг и сноркелинг на Красном море. Прекрасные коралловые рифы.", 10, null, false, "Египет: Красное море", null, 55000m, null, null, null, null, 2, 0 },
                    { 3, 1, 3, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Рим, Флоренция, Венеция за 8 дней. Обзорные экскурсии, музеи, дегустации.", 8, null, false, "Италия: Классика", null, 85000m, null, null, null, null, 1, 0 },
                    { 4, 1, 4, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Архитектура Гауди, пляжи, испанская кухня. Экскурсии с гидом.", 5, null, false, "Испания: Барселона", null, 45000m, null, null, null, null, 1, 0 },
                    { 5, 3, 5, new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Отдых на тропическом острове. Экскурсии к храмам, дайвинг, тайский массаж.", 14, null, false, "Таиланд: Пхукет", null, 95000m, null, null, null, null, 2, 0 },
                    { 6, 2, 1, new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гранд-базар, Голубая мечеть, дворец Топкапы. Шоппинг и история.", 4, null, false, "Турция: Стамбул", null, 35000m, null, null, null, null, 1, 0 },
                    { 7, 3, 2, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Пирамиды, Сфинкс, Долина царей. История Древнего Египта.", 6, null, false, "Египет: Каир и Луксор", null, 60000m, null, null, null, null, 1, 0 },
                    { 8, 1, 1, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Стамбул и Шарм-эль-Шейх. Экскурсии и пляжный отдых.", 12, null, false, "Комбинированный: Турция+Египет", null, 110000m, null, null, null, null, 3, 0 }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "CityId", "Description", "DistanceToBeach", "DistanceToCenter", "Latitude", "Longitude", "MealType", "Name", "PhotoUrl", "PricePerNight", "Stars" },
                values: new object[,]
                {
                    { 1, 1, "Роскошный отель премиум-класса", 100, null, 36.856699999999996, 31.057400000000001, 4, "Rixos Premium Belek", null, 15000m, 5 },
                    { 2, 1, "Семейный отель с аквапарком", 50, null, 36.860100000000003, 30.789300000000001, 4, "Titanic Deluxe Lara", null, 12000m, 5 },
                    { 3, 3, "Отель на берегу Красного моря", 0, null, 27.257899999999999, 33.811599999999999, 3, "Hilton Hurghada Plaza", null, 10000m, 5 },
                    { 4, 4, "Премиум отель с собственным рифом", 0, null, 27.948599999999999, 34.389899999999997, 4, "Four Seasons Sharm El Sheikh", null, 20000m, 5 },
                    { 5, 5, "Отель в историческом центре Рима", null, null, 41.902799999999999, 12.4964, 1, "Hotel Artemide Rome", null, 8000m, 4 },
                    { 6, 6, "Исторический отель у каналов", null, null, 45.4343, 12.338699999999999, 1, "Hotel Danieli Venice", null, 25000m, 5 },
                    { 7, 7, "Дизайнерский отель у пляжа", 200, null, 41.367400000000004, 2.1964999999999999, 2, "Hotel Arts Barcelona", null, 18000m, 5 },
                    { 8, 9, "Люкс-отель на реке Чаупхрая", null, null, 13.7409, 100.5423, 1, "Mandarin Oriental Bangkok", null, 15000m, 5 },
                    { 9, 10, "Отель на пляже Кататхани", 0, null, 7.8253000000000004, 98.2971, 4, "Katathani Phuket", null, 12000m, 5 }
                });

            migrationBuilder.InsertData(
                table: "Sights",
                columns: new[] { "Id", "Address", "CityId", "Description", "Latitude", "Longitude", "Name", "PhotoUrl", "Type" },
                values: new object[,]
                {
                    { 1, "Duden Park", 1, "Красивый водопад в парке", 36.857500000000002, 30.785299999999999, "Водопад Дюден", null, 6 },
                    { 2, "Giza Plateau", 3, "Древние пирамиды Египта", 29.979199999999999, 31.1342, "Пирамиды Гизы", null, 5 },
                    { 3, "Piazza del Colosseo", 5, "Древнеримский амфитеатр", 41.8902, 12.4922, "Колизей", null, 5 },
                    { 4, "Piazza San Marco", 6, "Шедевр византийской архитектуры", 45.4343, 12.338699999999999, "Собор Святого Марка", null, 4 },
                    { 5, "Carrer de Mallorca", 7, "Шедевр Гауди", 41.403599999999997, 2.1743999999999999, "Саграда Фамилия", null, 4 },
                    { 6, "Soi Naklua", 9, "Уникальный деревянный храм", 12.9857, 100.8865, "Храм Истины", null, 4 },
                    { 7, "Nakkerd Hills", 10, "45-метровая статуя Будды", 7.8293999999999997, 98.319299999999998, "Большой Будда", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "TourHotels",
                columns: new[] { "HotelId", "TourId", "DayNumber" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 3, 2, null },
                    { 5, 3, null },
                    { 6, 3, null },
                    { 7, 4, null },
                    { 9, 5, null },
                    { 2, 6, null },
                    { 4, 7, null },
                    { 2, 8, null },
                    { 4, 8, null }
                });

            migrationBuilder.InsertData(
                table: "TourSights",
                columns: new[] { "SightId", "TourId", "DayNumber" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 3, 3, null },
                    { 4, 3, null },
                    { 5, 4, null },
                    { 7, 5, null },
                    { 2, 7, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_Name",
                table: "Agencies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ProcessedByUserId",
                table: "Bookings",
                column: "ProcessedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TourId",
                table: "Bookings",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ManagerId",
                table: "Chats",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteTours_TourId",
                table: "FavoriteTours",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CityId",
                table: "Hotels",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HotelId",
                table: "Reviews",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SightId",
                table: "Reviews",
                column: "SightId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TourId",
                table: "Reviews",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sights_CityId",
                table: "Sights",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TourHotels_HotelId",
                table: "TourHotels",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_AgencyId",
                table: "Tours",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_CountryId",
                table: "Tours",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TourSights_SightId",
                table: "TourSights",
                column: "SightId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AgencyId",
                table: "Users",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "FavoriteTours");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "TourHotels");

            migrationBuilder.DropTable(
                name: "TourSights");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Sights");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
