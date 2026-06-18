using TravelGuide.Data;
using TravelGuide.Models.Entities;

namespace TravelGuide.Data;

/// <summary>
/// Начальные данные для заполнения базы данных
/// </summary>
public static class SeedData
{
    public static void Initialize(TravelGuideContext context)
    {
        // Если уже есть данные — не заполняем
        if (context.Tours.Any()) return;

        Console.WriteLine("Заполнение базы данных начальными данными...");

        // =====================
        // СТРАНЫ
        // =====================
        var egypt = new Country { Name = "Египет", Description = "Страна пирамид и Красного моря. Идеальное место для пляжного отдыха и дайвинга." };
        var turkey = new Country { Name = "Турция", Description = "Курорты Средиземноморья и Эгейского моря. Всё включено, шопинг и культура." };
        var thailand = new Country { Name = "Таиланд", Description = "Тропический рай с уникальной культурой, храмами и прекрасными пляжами." };
        var italy = new Country { Name = "Италия", Description = "Колыбель цивилизации — Рим, Венеция, Флоренция. Культура, еда и история." };
        var spain = new Country { Name = "Испания", Description = "Солнечная страна с богатой культурой, архитектурой и пляжами Коста Брава." };
        var greece = new Country { Name = "Греция", Description = "Родина олимпийских игр. Острова, море и древние руины." };
        var uae = new Country { Name = "ОАЭ", Description = "Дубай и Абу-Даби — роскошь, небоскрёбы и пустыня." };
        var maldives = new Country { Name = "Мальдивы", Description = "Рай для дайверов. Бунгало на воде и бирюзовое море." };

        context.Countries.AddRange(egypt, turkey, thailand, italy, spain, greece, uae, maldives);
        context.SaveChanges();

        // =====================
        // ГОРОДА
        // =====================
        var hurghada = new City { Name = "Хургада", CountryId = egypt.Id };
        var sharm = new City { Name = "Шарм-эль-Шейх", CountryId = egypt.Id };
        var cairo = new City { Name = "Каир", CountryId = egypt.Id };
        var antalya = new City { Name = "Анталья", CountryId = turkey.Id };
        var istanbul = new City { Name = "Стамбул", CountryId = turkey.Id };
        var bodrum = new City { Name = "Бодрум", CountryId = turkey.Id };
        var phuket = new City { Name = "Пхукет", CountryId = thailand.Id };
        var bangkok = new City { Name = "Бангкок", CountryId = thailand.Id };
        var pattaya = new City { Name = "Паттайя", CountryId = thailand.Id };
        var rome = new City { Name = "Рим", CountryId = italy.Id };
        var venice = new City { Name = "Венеция", CountryId = italy.Id };
        var barcelona = new City { Name = "Барселона", CountryId = spain.Id };
        var madrid = new City { Name = "Мадрид", CountryId = spain.Id };
        var athens = new City { Name = "Афины", CountryId = greece.Id };
        var santorini = new City { Name = "Санторини", CountryId = greece.Id };
        var dubai = new City { Name = "Дубай", CountryId = uae.Id };
        var male = new City { Name = "Мале", CountryId = maldives.Id };

        context.Cities.AddRange(hurghada, sharm, cairo, antalya, istanbul, bodrum, phuket, bangkok, pattaya, rome, venice, barcelona, madrid, athens, santorini, dubai, male);
        context.SaveChanges();

        // =====================
        // АГЕНТСТВА
        // =====================
        var travelPro = new Agency { Name = "TravelPro", ContactInfo = "info@travelpro.ru, +7 (495) 123-45-67", Description = "Туристическое агентство №1 в России. Работаем с 2015 года.", Rating = 4.7 };
        var wanderlust = new Agency { Name = "Wanderlust Travel", ContactInfo = "hello@wanderlust.ru, +7 (495) 987-65-43", Description = "Специализация — экзотические направления и индивидуальные туры.", Rating = 4.5 };
        var bestTours = new Agency { Name = "Лучшие Туры", ContactInfo = "info@besttours.ru, +7 (812) 555-33-22", Description = "Бюджетные и премиум туры по всему миру.", Rating = 4.3 };

        context.Agencies.AddRange(travelPro, wanderlust, bestTours);
        context.SaveChanges();

        // =====================
        // ОТЕЛИ
        // =====================
        var hotels = new List<Hotel>
        {
            // Египет
            new Hotel { Name = "Steigenberger Al Dau Beach", CityId = hurghada.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = 50, DistanceToCenter = 8, PricePerNight = 12000, Description = "Роскошный пляжный курорт с собственным коралловым рифом." },
            new Hotel { Name = "Jaz Aquaviva", CityId = hurghada.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = 100, DistanceToCenter = 5, PricePerNight = 9500, Description = "Современный отель с аквапарком и анимацией." },
            new Hotel { Name = "Baron Palace Sahl Hasheesh", CityId = sharm.Id, Stars = 5, MealType = MealType.UAI, DistanceToBeach = 30, DistanceToCenter = 12, PricePerNight = 15000, Description = "Премиальный отель с безупречным сервисом." },
            new Hotel { Name = "Radisson Blu Resort", CityId = sharm.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = 200, DistanceToCenter = 7, PricePerNight = 11000, Description = "Отель у подножия гор Синай." },

            // Турция
            new Hotel { Name = "Rixos Premium Belek", CityId = antalya.Id, Stars = 5, MealType = MealType.UAI, DistanceToBeach = 30, DistanceToCenter = 15, PricePerNight = 18000, Description = "Один из лучших отелей мира по версии World Travel Awards." },
            new Hotel { Name = "Conrad Istanbul Bosphorus", CityId = istanbul.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 2, PricePerNight = 22000, Description = "Отель с видом на Босфор и Айя-Софию." },
            new Hotel { Name = "Hilton Dalaman Sarigerme", CityId = bodrum.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = 50, DistanceToCenter = 10, PricePerNight = 14000, Description = "Семейный курорт с пляжем и спа." },

            // Таиланд
            new Hotel { Name = "Banyan Tree Phuket", CityId = phuket.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = 100, DistanceToCenter = 20, PricePerNight = 25000, Description = "Роскошные виллы с приватными бассейнами." },
            new Hotel { Name = "Anantara Riverside", CityId = bangkok.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 5, PricePerNight = 16000, Description = "Отель на берегу реки Чао Пхрая." },
            new Hotel { Name = "Hilton Pattaya", CityId = pattaya.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = 50, DistanceToCenter = 3, PricePerNight = 10000, Description = "Панорамный отель на пляже Паттайя." },

            // Италия
            new Hotel { Name = "Hotel Eden Roma", CityId = rome.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 35000, Description = "Бутик-отель рядом с Ватиканом." },
            new Hotel { Name = "The Gritti Palace", CityId = venice.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 0, PricePerNight = 55000, Description = "Легендарный отель на Гранд-канале." },

            // Испания
            new Hotel { Name = "W Barcelona", CityId = barcelona.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = 10, DistanceToCenter = 3, PricePerNight = 28000, Description = "Иконический отель в виде паруса на пляже Барселонета." },
            new Hotel { Name = "Hotel Ritz Madrid", CityId = madrid.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 32000, Description = "Классический отель в стиле ар-деко." },

            // Греция
            new Hotel { Name = "Grace Hotel Santorini", CityId = santorini.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = 200, DistanceToCenter = 2, PricePerNight = 40000, Description = "Бутик-отель с видом на кальдеру." },
            new Hotel { Name = "Athens Marriott Hotel", CityId = athens.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 3, PricePerNight = 18000, Description = "Современный отель в центре Афин." },

            // ОАЭ
            new Hotel { Name = "Burj Al Arab Jumeirah", CityId = dubai.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = 50, DistanceToCenter = 15, PricePerNight = 85000, Description = "Самый роскошный отель мира — парус на искусственном острове." },
            new Hotel { Name = "Atlantis The Palm", CityId = dubai.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = 30, DistanceToCenter = 20, PricePerNight = 45000, Description = "Отель на Пальмовом острове с аквапарком Aquaventure." },

            // Мальдивы
            new Hotel { Name = "Soneva Fushi", CityId = male.Id, Stars = 5, MealType = MealType.FB, DistanceToBeach = 0, DistanceToCenter = null, PricePerNight = 70000, Description = "Эко-люкс на частном острове с бунгало." },
            new Hotel { Name = "Conrad Maldives", CityId = male.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = 0, DistanceToCenter = null, PricePerNight = 55000, Description = "Два острова: один — пляжный, другой — водные виллы." },
        };

        context.Hotels.AddRange(hotels);
        context.SaveChanges();

        // =====================
        // ДОСТОПРИМЕЧАТЕЛЬНОСТИ
        // =====================
        var sights = new List<Sight>
        {
            // Египет
            new Sight { Name = "Великие Пирамиды Гизы", CityId = cairo.Id, Type = SightType.Historical, Description = "Единственное из Семи чудес древнего мира, дошедшее до наших дней. Возраст более 4500 лет.", Address = "Гиза, Египет", Latitude = 29.9792, Longitude = 31.1342 },
            new Sight { Name = "Валли Царей", CityId = cairo.Id, Type = SightType.Historical, Description = "Долина, где были обнаружены гробницы фараонов, включая гробницу Тутанхамона.", Address = "Луксор, Египет", Latitude = 25.7402, Longitude = 32.6014 },

            // Турция
            new Sight { Name = "Айя-София", CityId = istanbul.Id, Type = SightType.Cathedral, Description = "Величественный собор-музей, символ Стамбула. Возраст более 1500 лет.", Address = "Стамбул, Турция", Latitude = 41.0086, Longitude = 28.9802 },
            new Sight { Name = "Голубая Мечеть", CityId = istanbul.Id, Type = SightType.Cathedral, Description = "Султан-Ахмет — единственная мечеть с шестью минаретами в Турции.", Address = "Стамбул, Турция", Latitude = 41.0054, Longitude = 28.9768 },

            // Таиланд
            new Sight { Name = "Храм Изумрудного Будды", CityId = bangkok.Id, Type = SightType.Historical, Description = "Королевский дворец и храм, где хранится статуя Изумрудного Будды.", Address = "Бангкок, Таиланд", Latitude = 13.7510, Longitude = 100.4913 },
            new Sight { Name = "Пляж Патонг", CityId = phuket.Id, Type = SightType.Nature, Description = "Самый популярный пляж Пхукета с nightlife и водными видами спорта.", Address = "Пхукет, Таиланд", Latitude = 7.8883, Longitude = 98.2917 },

            // Италия
            new Sight { Name = "Колизей", CityId = rome.Id, Type = SightType.Historical, Description = "Древнеримский амфитеатр — символ Вечного города. Вмещал до 50 000 зрителей.", Address = "Рим, Италия", Latitude = 41.8902, Longitude = 12.4922 },
            new Sight { Name = "Площадь Сан-Марко", CityId = venice.Id, Type = SightType.Historical, Description = "Главная площадь Венеции с собором и колокольней.", Address = "Венеция, Италия", Latitude = 45.4343, Longitude = 12.3386 },

            // Испания
            new Sight { Name = "Саграда Фамилия", CityId = barcelona.Id, Type = SightType.Cathedral, Description = "Незаконченный шедевр Гауди — самая посещаемая церковь Испании.", Address = "Барселона, Испания", Latitude = 41.4036, Longitude = 2.1744 },
            new Sight { Name = "Парк Гуэль", CityId = barcelona.Id, Type = SightType.Park, Description = "Парк в стиле модерн от Антони Гауди с мозаикой и видом на город.", Address = "Барселона, Испания", Latitude = 41.4145, Longitude = 2.1527 },

            // Греция
            new Sight { Name = "Акрополь", CityId = athens.Id, Type = SightType.Historical, Description = "Древнегреческий город-крепость с храмом Парфенон.", Address = "Афины, Греция", Latitude = 37.9715, Longitude = 23.7267 },
            new Sight { Name = "Остров Санторини", CityId = santorini.Id, Type = SightType.Nature, Description = "Вулканический остров с белыми домиками и голубыми куполами.", Address = "Санторини, Греция", Latitude = 36.3932, Longitude = 25.4615 },

            // ОАЭ
            new Sight { Name = "Бурдж-Халифа", CityId = dubai.Id, Type = SightType.Historical, Description = "Самое высокое здание мира — 828 метров. Обзорная площадка на 124 этаже.", Address = "Дубай, ОАЭ", Latitude = 25.1972, Longitude = 55.2744 },

            // Мальдивы
            new Sight { Name = "Рифа Мальдивских островов", CityId = male.Id, Type = SightType.Nature, Description = "Уникальная подводная экосистема с сотнями видов кораллов и рыб.", Address = "Мальдивы", Latitude = 4.1755, Longitude = 73.5093 },
        };

        context.Sights.AddRange(sights);
        context.SaveChanges();

        // =====================
        // ПОЛЬЗОВАТЕЛИ
        // =====================
        var tourist1 = new User { FullName = "Иван Иванов", Email = "ivan@test.com", Phone = "+79001112233", Role = UserRole.Tourist, RegistrationDate = DateTime.Now.AddDays(-60) };
        tourist1.PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123");

        var tourist2 = new User { FullName = "Мария Петрова", Email = "maria@test.com", Phone = "+79003334455", Role = UserRole.Tourist, RegistrationDate = DateTime.Now.AddDays(-30) };
        tourist2.PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123");

        var tourist3 = new User { FullName = "Алексей Сидоров", Email = "alex@test.com", Phone = "+79005556677", Role = UserRole.Tourist, RegistrationDate = DateTime.Now.AddDays(-15) };
        tourist3.PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123");

        context.Users.AddRange(tourist1, tourist2, tourist3);
        context.SaveChanges();

        // =====================
        // ТУРЫ
        // =====================
        var tours = new List<Tour>
        {
            new Tour { Name = "Красное море — дайвинг и пляжи", TourType = TourType.Beach, Description = "7 дней на берегу Красного моря. Дайвинг, снорклинг, пляжный отдых. Экскурсия в Луксор.", Duration = 7, Price = 45000, CountryId = egypt.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт, заселение\nДень 2: Пляж, снорклинг\nДень 3: Экскурсия в Луксор\nДень 4: Пляж\nДень 5: Дайвинг\nДень 6: Свободный день\nДень 7: Вылет", CreatedDate = DateTime.Now.AddDays(-30) },
            new Tour { Name = "Великий Каир и пирамиды", TourType = TourType.Excursion, Description = "Экскурсионный тур в Каир. Пирамиды, Египетский музей, базар Хан-эль-Халили.", Duration = 4, Price = 28000, CountryId = egypt.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт в Каир\nДень 2: Пирамиды и Сфинкс\nДень 3: Египетский музей\nДень 4: Базар, вылет", CreatedDate = DateTime.Now.AddDays(-25) },

            new Tour { Name = "All Inclusive Анталья", TourType = TourType.Beach, Description = "10 дней всё включено на средиземноморском побережье Турции. Аквапарк, спа, вечерние шоу.", Duration = 10, Price = 65000, CountryId = turkey.Id, AgencyId = bestTours.Id, Route = "День 1: Прилёт, заселение\nДни 2-9: Пляж, бассейн, анимация\nДень 10: Вылет", CreatedDate = DateTime.Now.AddDays(-20) },
            new Tour { Name = "Стамбул — два континента", TourType = TourType.Excursion, Description = "5 дней в городе на двух континентах. Айя-София, Голубая мечеть, базары, Босфор.", Duration = 5, Price = 38000, CountryId = turkey.Id, AgencyId = wanderlust.Id, Route = "День 1: Старый город\nДень 2: Айя-София, мечеть\nДень 3: Базары\nДень 4: Круиз по Босфору\nДень 5: Вылет", CreatedDate = DateTime.Now.AddDays(-18) },

            new Tour { Name = "Острова Пхукета", TourType = TourType.Beach, Description = "14 дней на Пхукете. Экскурсии на острова Джеймса Бонда, Пхи-Пхи.", Duration = 14, Price = 85000, CountryId = thailand.Id, AgencyId = wanderlust.Id, Route = "День 1: Прилёт\nДень 3: Остров Джеймса Бонда\nДень 7: Острова Пхи-Пхи\nДни 2-14: Пляж, ночные рынки", CreatedDate = DateTime.Now.AddDays(-15) },

            new Tour { Name = "Бангкок и храмы", TourType = TourType.Excursion, Description = "8 дней в Бангкоке. Королевский дворец, храмы, плавучий рынок, ночной рынок.", Duration = 8, Price = 55000, CountryId = thailand.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт\nДень 2: Дворец и храмы\nДень 3: Плавучий рынок\nДень 4: Ночные рынки\nДень 5: Аюттхая", CreatedDate = DateTime.Now.AddDays(-12) },

            new Tour { Name = "Рим — Вечный город", TourType = TourType.Excursion, Description = "6 дней в Риме. Колизей, Ватикан, Форумы, Треви. Город-государство Ватикан.", Duration = 6, Price = 72000, CountryId = italy.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт\nДень 2: Колизей, Форумы\nДень 3: Ватикан\nДень 4: Площади Рима\nДень 5: Треви, Пантеон\nДень 6: Вылет", CreatedDate = DateTime.Now.AddDays(-10) },

            new Tour { Name = "Барселона и Коста Брава", TourType = TourType.Combined, Description = "9 дней: 5 в Барселоне + 4 на пляжах Коста Брава. Гауди, Лас-Рамблас,Montserrat.", Duration = 9, Price = 68000, CountryId = spain.Id, AgencyId = bestTours.Id, Route = "День 1: Прилёт\nДень 2: Саграда Фамилия\nДень 3: Парк Гуэль\nДень 4: Лас-Рамблас\nДень 5: Монсеррат\nДни 6-9: Коста Брава", CreatedDate = DateTime.Now.AddDays(-8) },

            new Tour { Name = "Афины и Санторини", TourType = TourType.Combined, Description = "11 дней: 4 в Афинах + 7 на Санторини. Акрополь, вулкан, закаты в Ойе.", Duration = 11, Price = 78000, CountryId = greece.Id, AgencyId = wanderlust.Id, Route = "День 1: Прилёт в Афины\nДень 2: Акрополь\nДень 3: Площади\nДень 4: Перелёт на Санторини\nДни 5-11: Санторини", CreatedDate = DateTime.Now.AddDays(-5) },

            new Tour { Name = "Дубай — город будущего", TourType = TourType.Excursion, Description = "7 дней в Дубае. Бурдж-Халифа, Пальмовый остров, пустыня, шопинг.", Duration = 7, Price = 95000, CountryId = uae.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт\nДень 2: Бурдж-Халифа\nДень 3: Пальмовый остров\nДень 4: Сафари в пустыне\nДень 5: Шопинг", CreatedDate = DateTime.Now.AddDays(-3) },

            new Tour { Name = "Мальдивы — рай на Земле", TourType = TourType.Beach, Description = "10 дней в бунгало на воде. Дайвинг, рыбалка, спа, закаты.", Duration = 10, Price = 150000, CountryId = maldives.Id, AgencyId = wanderlust.Id, Route = "День 1: Прилёт, трансфер на остров\nДни 2-9: Пляж, дайвинг, спа\nДень 10: Вылет", CreatedDate = DateTime.Now.AddDays(-1) },
        };

        context.Tours.AddRange(tours);
        context.SaveChanges();

        // =====================
        // СВЯЗИ ТУР-ОТЕЛЬ и ТУР-ДОСТОПРИМЕЧАТЕЛЬНОСТЬ
        // =====================
        var tourHotels = new List<TourHotel>
        {
            new TourHotel { TourId = tours[0].Id, HotelId = hotels[0].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[0].Id, HotelId = hotels[1].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[1].Id, HotelId = hotels[2].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[2].Id, HotelId = hotels[4].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[3].Id, HotelId = hotels[5].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[4].Id, HotelId = hotels[7].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[5].Id, HotelId = hotels[8].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[6].Id, HotelId = hotels[10].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[7].Id, HotelId = hotels[12].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[8].Id, HotelId = hotels[14].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[9].Id, HotelId = hotels[17].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[10].Id, HotelId = hotels[18].Id, DayNumber = 1 },
        };

        var tourSights = new List<TourSight>
        {
            new TourSight { TourId = tours[1].Id, SightId = sights[0].Id, DayNumber = 2 },
            new TourSight { TourId = tours[1].Id, SightId = sights[1].Id, DayNumber = 3 },
            new TourSight { TourId = tours[3].Id, SightId = sights[2].Id, DayNumber = 2 },
            new TourSight { TourId = tours[3].Id, SightId = sights[3].Id, DayNumber = 2 },
            new TourSight { TourId = tours[5].Id, SightId = sights[4].Id, DayNumber = 3 },
            new TourSight { TourId = tours[6].Id, SightId = sights[6].Id, DayNumber = 2 },
            new TourSight { TourId = tours[6].Id, SightId = sights[7].Id, DayNumber = 4 },
            new TourSight { TourId = tours[7].Id, SightId = sights[8].Id, DayNumber = 2 },
            new TourSight { TourId = tours[7].Id, SightId = sights[9].Id, DayNumber = 3 },
            new TourSight { TourId = tours[8].Id, SightId = sights[10].Id, DayNumber = 2 },
            new TourSight { TourId = tours[9].Id, SightId = sights[12].Id, DayNumber = 2 },
            new TourSight { TourId = tours[10].Id, SightId = sights[13].Id, DayNumber = 1 },
        };

        context.TourHotels.AddRange(tourHotels);
        context.TourSights.AddRange(tourSights);
        context.SaveChanges();

        // =====================
        // ОТЗЫВЫ
        // =====================
        var reviews = new List<Review>
        {
            new Review { UserId = tourist1.Id, TourId = tours[0].Id, Rating = 5, Text = "Потрясающий тур! Красное море — это что-то невероятное. Дайвинг на коралловом рифе — лучший опыт в жизни. Отель отличный, питание на высоте.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-20) },
            new Review { UserId = tourist2.Id, TourId = tours[0].Id, Rating = 4, Text = "Хороший отдых, но жарко очень. Экскурсия в Луксор — must have! Пирамиды — это另个ое. Рекомендую.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-15) },
            new Review { UserId = tourist3.Id, TourId = tours[2].Id, Rating = 5, Text = "Лучший all-inclusive в моей жизни! Rixos — это另个ое. Аквапарк, шоу, еда — всё на 10+. Дети в восторге.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-10) },
            new Review { UserId = tourist1.Id, TourId = tours[4].Id, Rating = 5, Text = "Пхукет — это另个ое! Острова красивые, море бирюзовое. Экскурсия на Пхи-Пхи — must see. Ночные рынки —另个ое.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-8) },
            new Review { UserId = tourist2.Id, TourId = tours[6].Id, Rating = 4, Text = "Рим —另个ое город! Колизей впечатляет. Еда —另个ое. Но очень много туристов. Лучше ехать в межсезонье.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-5) },
            new Review { UserId = tourist3.Id, TourId = tours[7].Id, Rating = 5, Text = "Барселона + Коста Брава = идеальное сочетание! Гауди —另个ое. Пляжи —另个ое. Шопинг —另个ое.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-3) },
            new Review { UserId = tourist1.Id, TourId = tours[8].Id, Rating = 5, Text = "Санторини — это另个ое! Закаты в Ойе —另个ое. Акрополь в Афинах —另个ое. Лучший тур в жизни.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-2) },
            new Review { UserId = tourist2.Id, TourId = tours[9].Id, Rating = 4, Text = "Дубай —另个ое город! Бурдж-Халифа —另кое. Пустыня —另кое. Дорого, но另кое.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-1) },
            new Review { UserId = tourist3.Id, TourId = tours[10].Id, Rating = 5, Text = "Мальдивы —另кое! Бунгало на воде —另кое. Рыба плавает под вами —另кое. Стоит каждой копейки.", Status = ReviewStatus.Approved, Date = DateTime.Now },

            // Отзывы на отели
            new Review { UserId = tourist1.Id, HotelId = hotels[0].Id, Rating = 5, Text = "Отличный отель! Пляж чистый, кораллы прямо у берега. Питание разнообразное.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-18) },
            new Review { UserId = tourist2.Id, HotelId = hotels[4].Id, Rating = 5, Text = "Rixos —另кое! Всё включено на另кое уровне. Аквапарк, шоу, еда —另кое.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-12) },

            // Отзывы на достопримечательности
            new Review { UserId = tourist1.Id, SightId = sights[0].Id, Rating = 5, Text = "Пирамиды —另кое! Величие另кое. Обязательно к посещению.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-22) },
            new Review { UserId = tourist2.Id, SightId = sights[6].Id, Rating = 5, Text = "Колизей —另кое! Представьте, как здесь сражались гладиаторы.另кое.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-7) },
        };

        context.Reviews.AddRange(reviews);
        context.SaveChanges();

        // =====================
        // БРОНИРОВАНИЯ
        // =====================
        var bookings = new List<Booking>
        {
            new Booking { TourId = tours[0].Id, UserId = tourist1.Id, BookingDate = DateTime.Now.AddDays(14), GuestsCount = 2, TotalPrice = 90000, Status = BookingStatus.Confirmed, Phone = "+79001112233", Email = "ivan@test.com", Notes = "Хотим соседние номера", CreatedAt = DateTime.Now.AddDays(-25) },
            new Booking { TourId = tours[2].Id, UserId = tourist2.Id, BookingDate = DateTime.Now.AddDays(21), GuestsCount = 4, TotalPrice = 260000, Status = BookingStatus.New, Phone = "+79003334455", Email = "maria@test.com", Notes = "Семья с детьми 8 и 12 лет", CreatedAt = DateTime.Now.AddDays(-18) },
            new Booking { TourId = tours[6].Id, UserId = tourist3.Id, BookingDate = DateTime.Now.AddDays(30), GuestsCount = 2, TotalPrice = 144000, Status = BookingStatus.New, Phone = "+79005556677", Email = "alex@test.com", CreatedAt = DateTime.Now.AddDays(-5) },
        };

        context.Bookings.AddRange(bookings);
        context.SaveChanges();

        // =====================
        // ИЗБРАННОЕ
        // =====================
        var favorites = new List<FavoriteTour>
        {
            new FavoriteTour { UserId = tourist1.Id, TourId = tours[0].Id },
            new FavoriteTour { UserId = tourist1.Id, TourId = tours[6].Id },
            new FavoriteTour { UserId = tourist1.Id, TourId = tours[10].Id },
            new FavoriteTour { UserId = tourist2.Id, TourId = tours[2].Id },
            new FavoriteTour { UserId = tourist2.Id, TourId = tours[7].Id },
            new FavoriteTour { UserId = tourist3.Id, TourId = tours[4].Id },
            new FavoriteTour { UserId = tourist3.Id, TourId = tours[9].Id },
        };

        context.FavoriteTours.AddRange(favorites);
        context.SaveChanges();

        // =====================
        // ЧАТЫ
        // =====================
        var chat1 = new Chat { UserId = tourist1.Id, ManagerId = null, StartTime = DateTime.Now.AddDays(-5), Status = ChatStatus.Active };
        context.Chats.Add(chat1);
        context.SaveChanges();

        var chat2 = new Chat { UserId = tourist2.Id, ManagerId = null, StartTime = DateTime.Now.AddDays(-2), Status = ChatStatus.Active };
        context.Chats.Add(chat2);
        context.SaveChanges();

        var messages = new List<Message>
        {
            new Message { ChatId = chat1.Id, SenderId = tourist1.Id, Text = "Здравствуйте! Подскажите, возможен ли ранний заезд в отель?", Timestamp = DateTime.Now.AddDays(-5), IsRead = true },
            new Message { ChatId = chat1.Id, SenderId = tourist1.Id, Text = "И ещё — можно ли забронировать экскурсию заранее?", Timestamp = DateTime.Now.AddDays(-5).AddMinutes(1), IsRead = true },
            new Message { ChatId = chat2.Id, SenderId = tourist2.Id, Text = "Добрый день! Хотела бы узнать про скидки для детей.", Timestamp = DateTime.Now.AddDays(-2), IsRead = true },
        };

        context.Messages.AddRange(messages);
        context.SaveChanges();

        Console.WriteLine("База данных заполнена начальными данными!");
        Console.WriteLine("  - 8 стран, 17 городов, 3 агентства");
        Console.WriteLine("  - 20 отелей, 13 достопримечательностей");
        Console.WriteLine("  - 11 туров");
        Console.WriteLine("  - 3 пользователя (tourist/test)");
        Console.WriteLine("  - 13 отзывов, 3 бронирования, 7 избранных");
        Console.WriteLine("  - 2 чата, 3 сообщения");
    }
}
