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
        var italy = new Country { Name = "Италия", Description = "Колыбель цивилизации: Рим, Венеция, Флоренция. Культура, еда и история." };
        var france = new Country { Name = "Франция", Description = "Страна любви, вина и гастрономии. Париж, Лазурный берег, Бордо." };
        var japan = new Country { Name = "Япония", Description = "Страна восходящего солнца. Традиции, технологии, сакура и суши." };
        var iceland = new Country { Name = "Исландия", Description = "Страна огня и льда. Гейзеры, фьорды, Северное сияние." };
        var egypt = new Country { Name = "Египет", Description = "Страна пирамид и Красного моря. Древняя история и пляжи." };
        var turkey = new Country { Name = "Турция", Description = "Курорты Средиземноморья. Каппадокия, Стамбул, пляжи." };
        var spain = new Country { Name = "Испания", Description = "Солнечная страна. Барселона, фламенко, тапас." };
        var thailand = new Country { Name = "Таиланд", Description = "Тропический рай. Храмы, пляжи, стритфуд." };
        var usa = new Country { Name = "США", Description = "Страна возможностей. Нью-Йорк, Grand Canyon, Голливуд." };
        var georgia = new Country { Name = "Грузия", Description = "Гостеприимный Кавказ. Вино, кухня, горы и старый Тбилиси." };
        var uae = new Country { Name = "ОАЭ", Description = "Роскошь пустыни. Дубай, Бурдж-Халифа, шопинг." };
        var norway = new Country { Name = "Норвегия", Description = "Фьорды и Северное сияние. Природа в первозданном виде." };
        var peru = new Country { Name = "Перу", Description = "Земля инков. Мачу-Пикчу, Анды, амазонские джунгли." };
        var czech = new Country { Name = "Чехия", Description = "Мистическая Прага. Пиво, архитектура, легенды." };
        var greece = new Country { Name = "Греция", Description = "Родина олимпийских игр. Острова, море и древние руины." };
        var morocco = new Country { Name = "Марокко", Description = "Восточная сказка. Базары, пустыня, чай с мятой." };
        var southAfrica = new Country { Name = "ЮАР", Description = "Разнообразная природа. Сафари, виноградники, океан." };
        var uk = new Country { Name = "Великобритания", Description = "Туманный Альбион. Лондон, Шотландия, Премьер-лига." };
        var indonesia = new Country { Name = "Индонезия", Description = "Тропический архипелаг. Бали, вулканы, серфинг." };
        var switzerland = new Country { Name = "Швейцария", Description = "Альпы, шоколад, часы. Чистейшая природа и порядок." };

        context.Countries.AddRange(italy, france, japan, iceland, egypt, turkey, spain, thailand, usa, georgia, uae, norway, peru, czech, greece, morocco, southAfrica, uk, indonesia, switzerland);
        context.SaveChanges();

        // =====================
        // ГОРОДА
        // =====================
        var rome = new City { Name = "Рим", CountryId = italy.Id };
        var paris = new City { Name = "Париж", CountryId = france.Id };
        var kyoto = new City { Name = "Киото", CountryId = japan.Id };
        var vik = new City { Name = "Вик", CountryId = iceland.Id };
        var luxor = new City { Name = "Луксор", CountryId = egypt.Id };
        var cappadocia = new City { Name = "Каппадокия", CountryId = turkey.Id };
        var barcelona = new City { Name = "Барселона", CountryId = spain.Id };
        var phuket = new City { Name = "Пхукет", CountryId = thailand.Id };
        var grandCanyon = new City { Name = "Гранд-Каньон", CountryId = usa.Id };
        var tbilisi = new City { Name = "Тбилиси", CountryId = georgia.Id };
        var dubai = new City { Name = "Дубай", CountryId = uae.Id };
        var geiranger = new City { Name = "Гейрангер", CountryId = norway.Id };
        var machuPicchu = new City { Name = "Мачу-Пикчу", CountryId = peru.Id };
        var prague = new City { Name = "Прага", CountryId = czech.Id };
        var athens = new City { Name = "Афины", CountryId = greece.Id };
        var santorini = new City { Name = "Санторини", CountryId = greece.Id };
        var marrakech = new City { Name = "Марракеш", CountryId = morocco.Id };
        var kruger = new City { Name = "Национальный парк Крюгер", CountryId = southAfrica.Id };
        var london = new City { Name = "Лондон", CountryId = uk.Id };
        var bali = new City { Name = "Бали", CountryId = indonesia.Id };
        var zermatt = new City { Name = "Церматт", CountryId = switzerland.Id };

        context.Cities.AddRange(rome, paris, kyoto, vik, luxor, cappadocia, barcelona, phuket, grandCanyon, tbilisi, dubai, geiranger, machuPicchu, prague, athens, santorini, marrakech, kruger, london, bali, zermatt);
        context.SaveChanges();

        // =====================
        // АГЕНТСТВА
        // =====================
        var travelPro = new Agency { Name = "TravelPro", ContactInfo = "info@travelpro.ru, +375 (29) 123-45-67", Description = "Туристическое агентство №1 в Беларуси. Работаем с 2015 года.", Rating = 4.7 };
        var wanderlust = new Agency { Name = "Wanderlust Travel", ContactInfo = "hello@wanderlust.ru, +375 (33) 987-65-43", Description = "Специализация: экзотические направления и индивидуальные туры.", Rating = 4.5 };
        var bestTours = new Agency { Name = "Лучшие Туры", ContactInfo = "info@besttours.ru, +375 (29) 555-33-22", Description = "Бюджетные и премиум туры по всему миру.", Rating = 4.3 };

        context.Agencies.AddRange(travelPro, wanderlust, bestTours);
        context.SaveChanges();

        // =====================
        // ОТЕЛИ
        // =====================
        var hotels = new List<Hotel>
        {
            // Италия (Рим)
            new Hotel { Name = "Hotel Artemide", CityId = rome.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 35000, Description = "Бутик-отель на Via Nazionale, спа и ресторан на крыше.", Latitude = 41.9058, Longitude = 12.4877 },
            new Hotel { Name = "Elizabeth Unique Hotel", CityId = rome.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 2, PricePerNight = 28000, Description = "Изысканный отель рядом с Пьяцца Навона.", Latitude = 41.8992, Longitude = 12.4731 },
            new Hotel { Name = "IQ Hotel Roma", CityId = rome.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 3, PricePerNight = 18000, Description = "Современный отель с панорамной крышей.", Latitude = 41.8856, Longitude = 12.5012 },

            // Франция (Париж)
            new Hotel { Name = "Hotel Terrass Montmartre", CityId = paris.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 5, PricePerNight = 22000, Description = "Отель с террасой и видом на Сакре-Кёр.", Latitude = 48.8865, Longitude = 2.3332 },
            new Hotel { Name = "Le Relais Montmartre", CityId = paris.Id, Stars = 3, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 4, PricePerNight = 16000, Description = "Уютный отель в сердце Монмартра.", Latitude = 48.8845, Longitude = 2.3322 },
            new Hotel { Name = "Hotel Maison Souquet", CityId = paris.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 4, PricePerNight = 45000, Description = "Роскошный бутик-отель в стиле бель эпок.", Latitude = 48.8842, Longitude = 2.3320 },

            // Япония (Киото)
            new Hotel { Name = "The Thousand Kyoto", CityId = kyoto.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 40000, Description = "Современный отель у вокзала Киото.", Latitude = 35.0036, Longitude = 135.7594 },
            new Hotel { Name = "Kyoto Granbell Hotel", CityId = kyoto.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 2, PricePerNight = 20000, Description = "Стильный отель в районе Гион.", Latitude = 35.0045, Longitude = 135.7690 },
            new Hotel { Name = "Suiran Luxury Collection", CityId = kyoto.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 5, PricePerNight = 65000, Description = "Роскошный ryokan на берегу реки Ои.", Latitude = 35.0116, Longitude = 135.6733 },

            // Исландия (Вик)
            new Hotel { Name = "Hotel Kria", CityId = vik.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = 200, DistanceToCenter = 1, PricePerNight = 25000, Description = "Современный отель с видом на черный пляж.", Latitude = 63.4186, Longitude = -19.0060 },
            new Hotel { Name = "Black Beach Suites", CityId = vik.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = 500, DistanceToCenter = 2, PricePerNight = 22000, Description = "Просторные номера с видом на Атлантику.", Latitude = 63.4190, Longitude = -19.0070 },
            new Hotel { Name = "Hotel Vik i Myrdal", CityId = vik.Id, Stars = 3, MealType = MealType.BB, DistanceToBeach = 1000, DistanceToCenter = 1, PricePerNight = 18000, Description = "Уютный отель у подножия вулкана Мирдальсйёкюдль.", Latitude = 63.4150, Longitude = -19.0100 },

            // Египет (Луксор)
            new Hotel { Name = "Hilton Luxor Resort", CityId = luxor.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = null, DistanceToCenter = 3, PricePerNight = 18000, Description = "Роскошный курорт на берегу Нила.", Latitude = 25.6872, Longitude = 32.6396 },
            new Hotel { Name = "Sofitel Winter Palace", CityId = luxor.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 25000, Description = "Легендарный отель, где жил Говард Картер.", Latitude = 25.6950, Longitude = 32.6430 },
            new Hotel { Name = "Steigenberger Nile Palace", CityId = luxor.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = null, DistanceToCenter = 2, PricePerNight = 15000, Description = "Современный отель с бассейном у Нила.", Latitude = 25.6880, Longitude = 32.6400 },

            // Турция (Каппадокия)
            new Hotel { Name = "Sultan Cave Suites", CityId = cappadocia.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 30000, Description = "Пещерные номера с видом на шахтёры.", Latitude = 38.6430, Longitude = 34.8289 },
            new Hotel { Name = "Museum Hotel", CityId = cappadocia.Id, Stars = 5, MealType = MealType.FB, DistanceToBeach = null, DistanceToCenter = 2, PricePerNight = 45000, Description = "Бутик-отель с антикварной коллекцией.", Latitude = 38.6425, Longitude = 34.8295 },
            new Hotel { Name = "Mithra Cave Hotel", CityId = cappadocia.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 18000, Description = "Пещерный отель с террасой.", Latitude = 38.6435, Longitude = 34.8280 },

            // Испания (Барселона)
            new Hotel { Name = "H10 Cubik", CityId = barcelona.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = 500, DistanceToCenter = 2, PricePerNight = 20000, Description = "Стильный отель в районе Побленоу.", Latitude = 41.3990, Longitude = 2.1930 },
            new Hotel { Name = "Hotel Colon Barcelona", CityId = barcelona.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = 800, DistanceToCenter = 1, PricePerNight = 22000, Description = "Классический отель напротив Саграды Фамилии.", Latitude = 41.4030, Longitude = 2.1740 },
            new Hotel { Name = "Mandarin Oriental Barcelona", CityId = barcelona.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = 600, DistanceToCenter = 2, PricePerNight = 55000, Description = "Роскошный отель на Пасео де Грасия.", Latitude = 41.3925, Longitude = 2.1650 },

            // Таиланд (Пхукет)
            new Hotel { Name = "SAii Phi Phi Island Village", CityId = phuket.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = 0, DistanceToCenter = null, PricePerNight = 35000, Description = "Роскошный курорт на собственном острове.", Latitude = 7.7400, Longitude = 98.7700 },
            new Hotel { Name = "Zeavola Resort", CityId = phuket.Id, Stars = 5, MealType = MealType.FB, DistanceToBeach = 0, DistanceToCenter = null, PricePerNight = 40000, Description = "Эко-люкс на севере Пхи-Пхи.", Latitude = 7.6800, Longitude = 98.7650 },
            new Hotel { Name = "Phi Phi CoCo Beach Resort", CityId = phuket.Id, Stars = 3, MealType = MealType.BB, DistanceToBeach = 0, DistanceToCenter = 1, PricePerNight = 8000, Description = "Бюджетный курорт на пляже.", Latitude = 7.7350, Longitude = 98.7720 },

            // США (Гранд-Каньон)
            new Hotel { Name = "The Grand Hotel at the Grand Canyon", CityId = grandCanyon.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 25000, Description = "Отель с видом на каньон.", Latitude = 35.8800, Longitude = -112.1300 },
            new Hotel { Name = "El Tovar Hotel", CityId = grandCanyon.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 0, PricePerNight = 35000, Description = "Легендарный отель 1905 года на краю каньона.", Latitude = 36.0580, Longitude = -112.1430 },
            new Hotel { Name = "Bright Angel Lodge", CityId = grandCanyon.Id, Stars = 3, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 0, PricePerNight = 15000, Description = "Историческая лоджа с видом на каньон.", Latitude = 36.0575, Longitude = -112.1435 },

            // Грузия (Тбилиси)
            new Hotel { Name = "Stamba Hotel", CityId = tbilisi.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 25000, Description = "Индустриальный отель в бывшей типографии.", Latitude = 41.7150, Longitude = 44.7860 },
            new Hotel { Name = "Rooms Hotel Tbilisi", CityId = tbilisi.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 2, PricePerNight = 22000, Description = "Стильный отель с террасой.", Latitude = 41.7100, Longitude = 44.7900 },
            new Hotel { Name = "Citadel Narikala Hotel", CityId = tbilisi.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 15000, Description = "Отель у крепости Нарикала.", Latitude = 41.6850, Longitude = 44.7950 },

            // ОАЭ (Дубай)
            new Hotel { Name = "Armani Hotel Dubai", CityId = dubai.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 0, PricePerNight = 65000, Description = "Отель от Джорджо Армани в Бурдж-Халифа.", Latitude = 25.1972, Longitude = 55.2744 },
            new Hotel { Name = "The Address Downtown", CityId = dubai.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 45000, Description = "Современный отель у Дубай Молл.", Latitude = 25.1960, Longitude = 55.2700 },
            new Hotel { Name = "Rove Downtown", CityId = dubai.Id, Stars = 3, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 2, PricePerNight = 12000, Description = "Бюджетный отель в центре.", Latitude = 25.1950, Longitude = 55.2680 },

            // Норвегия (Гейрангер)
            new Hotel { Name = "Hotel Union Geiranger", CityId = geiranger.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 0, PricePerNight = 28000, Description = "Исторический отель с видом на фьорд.", Latitude = 62.1040, Longitude = 7.0940 },
            new Hotel { Name = "Grande Fjord Hotel", CityId = geiranger.Id, Stars = 3, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 18000, Description = "Семейный отель на берегу фьорда.", Latitude = 62.1035, Longitude = 7.0950 },
            new Hotel { Name = "Hotel Geiranger", CityId = geiranger.Id, Stars = 3, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 0, PricePerNight = 15000, Description = "Уютный отель с панорамным видом.", Latitude = 62.1045, Longitude = 7.0935 },

            // Перу (Мачу-Пикчу)
            new Hotel { Name = "Sanctuary Lodge Belmond", CityId = machuPicchu.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = null, DistanceToCenter = 0, PricePerNight = 80000, Description = "Единственный отель у входа в Мачу-Пикчу.", Latitude = -13.1630, Longitude = -72.5450 },
            new Hotel { Name = "Inkaterra Machu Picchu Pueblo", CityId = machuPicchu.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 45000, Description = "Эко-отель в облаках.", Latitude = -13.1580, Longitude = -72.5260 },
            new Hotel { Name = "Sumaq Machu Picchu Hotel", CityId = machuPicchu.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 35000, Description = "Роскошный отель с видом на реку.", Latitude = -13.1575, Longitude = -72.5255 },

            // Чехия (Прага)
            new Hotel { Name = "Hotel Kings Court", CityId = prague.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 25000, Description = "Пятизвёздочный отель на Вацлавской площади.", Latitude = 50.0810, Longitude = 14.4230 },
            new Hotel { Name = "BoHo Prague Hotel", CityId = prague.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 2, PricePerNight = 30000, Description = "Стильный отель в центре.", Latitude = 50.0850, Longitude = 14.4250 },
            new Hotel { Name = "Grand Mark Prague", CityId = prague.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 28000, Description = "Исторический отель в стиле барокко.", Latitude = 50.0830, Longitude = 14.4210 },

            // Греция (Афины/Санторини)
            new Hotel { Name = "Electra Metropolis Athens", CityId = athens.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 28000, Description = "Современный отель с видом на Акрополь.", Latitude = 37.9750, Longitude = 23.7350 },
            new Hotel { Name = "Andronis Luxury Suites", CityId = santorini.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 60000, Description = "Роскошные апартаменты в Ойе.", Latitude = 36.4618, Longitude = 25.3753 },
            new Hotel { Name = "Katikies Santorini", CityId = santorini.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 2, PricePerNight = 55000, Description = "Бутик-отель с бесконечным бассейном.", Latitude = 36.4610, Longitude = 25.3740 },

            // Марокко (Марракеш)
            new Hotel { Name = "La Mamounia", CityId = marrakech.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 2, PricePerNight = 45000, Description = "Легендарный дворец-отель с садами.", Latitude = 31.6250, Longitude = -7.9890 },
            new Hotel { Name = "Royal Mansour Marrakech", CityId = marrakech.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 80000, Description = "Роскошные риады от короля.", Latitude = 31.6260, Longitude = -7.9880 },
            new Hotel { Name = "Riad El Fenn", CityId = marrakech.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 35000, Description = "Бутик-риад с террасой.", Latitude = 31.6255, Longitude = -7.9885 },

            // ЮАР (НП Крюгер)
            new Hotel { Name = "Kapama River Lodge", CityId = kruger.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = null, DistanceToCenter = null, PricePerNight = 55000, Description = "Роскошный лодж в заповеднике.", Latitude = -24.1500, Longitude = 31.0500 },
            new Hotel { Name = "Sabi Sabi Earth Lodge", CityId = kruger.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = null, DistanceToCenter = null, PricePerNight = 70000, Description = "Эко-люкс, вписанный в ландшафт.", Latitude = -24.8000, Longitude = 31.4000 },
            new Hotel { Name = "Singita Ebony Lodge", CityId = kruger.Id, Stars = 5, MealType = MealType.AI, DistanceToBeach = null, DistanceToCenter = null, PricePerNight = 90000, Description = "Эксклюзивный лодж с сафари.", Latitude = -24.7500, Longitude = 31.3500 },

            // Великобритания (Лондон)
            new Hotel { Name = "The Savoy", CityId = london.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 1, PricePerNight = 60000, Description = "Легендарный отель на Темзе.", Latitude = 51.5100, Longitude = -0.1200 },
            new Hotel { Name = "CitizenM Tower of London", CityId = london.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 2, PricePerNight = 20000, Description = "Современный отель у Тауэра.", Latitude = 51.5090, Longitude = -0.0760 },
            new Hotel { Name = "The Standard London", CityId = london.Id, Stars = 4, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 3, PricePerNight = 25000, Description = "Стильный отель в Кингс-Кросс.", Latitude = 51.5350, Longitude = -0.1300 },

            // Индонезия (Бали)
            new Hotel { Name = "Maya Ubud Resort", CityId = bali.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 5, PricePerNight = 30000, Description = "Роскошный курорт среди рисовых террас.", Latitude = -8.5069, Longitude = 115.2624 },
            new Hotel { Name = "The Edge Bali", CityId = bali.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = 100, DistanceToCenter = 8, PricePerNight = 45000, Description = "Виллы на краю обрыва с видом на океан.", Latitude = -8.8300, Longitude = 115.0800 },
            new Hotel { Name = "Potato Head Suites", CityId = bali.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = 0, DistanceToCenter = 6, PricePerNight = 35000, Description = "Креативный отель на пляже Семьяк.", Latitude = -8.6850, Longitude = 115.1570 },

            // Швейцария (Церматт)
            new Hotel { Name = "The Omnia Zermatt", CityId = zermatt.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 0, PricePerNight = 55000, Description = "Бутик-отель с видом на Маттерхорн.", Latitude = 46.0207, Longitude = 7.7491 },
            new Hotel { Name = "Grand Hotel Zermatterhof", CityId = zermatt.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = 0, PricePerNight = 65000, Description = "Классический отель с СПА.", Latitude = 46.0200, Longitude = 7.7485 },
            new Hotel { Name = "Riffelalp Resort", CityId = zermatt.Id, Stars = 5, MealType = MealType.BB, DistanceToBeach = null, DistanceToCenter = null, PricePerNight = 70000, Description = "Горный отель на высоте 2222 м.", Latitude = 46.0050, Longitude = 7.7550 },
        };

        context.Hotels.AddRange(hotels);
        context.SaveChanges();

        // =====================
        // ДОСТОПРИМЕЧАТЕЛЬНОСТИ
        // =====================
        var sights = new List<Sight>
        {
            // Италия (Рим)
            new Sight { Name = "Колизей", CityId = rome.Id, Type = SightType.Historical, Description = "Древнеримский амфитеатр, символ Вечного города. Вмещал до 50 000 зрителей.", Address = "Рим, Италия", Latitude = 41.8902, Longitude = 12.4922 },
            new Sight { Name = "Римский форум", CityId = rome.Id, Type = SightType.Historical, Description = "Древний центр политической жизни Рима.", Address = "Рим, Италия", Latitude = 41.8925, Longitude = 12.4853 },
            new Sight { Name = "Фонтан Треви", CityId = rome.Id, Type = SightType.Monument, Description = "Самый большой барочный фонтан в Риме.", Address = "Рим, Италия", Latitude = 41.9009, Longitude = 12.4833 },
            new Sight { Name = "Пантеон", CityId = rome.Id, Type = SightType.Historical, Description = "Древнеримский храм с самым большим куполом без арматуры.", Address = "Рим, Италия", Latitude = 41.8986, Longitude = 12.4768 },

            // Франция (Париж)
            new Sight { Name = "Базилика Сакре-Кёр", CityId = paris.Id, Type = SightType.Cathedral, Description = "Белоснежная базилика на холме Монмартр.", Address = "Париж, Франция", Latitude = 48.8867, Longitude = 2.3431 },
            new Sight { Name = "Мулен Руж", CityId = paris.Id, Type = SightType.Entertainment, Description = "Легендарный ночной клуб с канканом.", Address = "Париж, Франция", Latitude = 48.8841, Longitude = 2.3323 },
            new Sight { Name = "Стена любви", CityId = paris.Id, Type = SightType.Monument, Description = "Стена с надписью «Я тебя люблю» на 250 языках.", Address = "Париж, Франция", Latitude = 48.8855, Longitude = 2.3325 },
            new Sight { Name = "Эйфелева башня", CityId = paris.Id, Type = SightType.Monument, Description = "Символ Парижа, 324 метра.", Address = "Париж, Франция", Latitude = 48.8584, Longitude = 2.2945 },

            // Япония (Киото)
            new Sight { Name = "Бамбуковый лес Арасияма", CityId = kyoto.Id, Type = SightType.Nature, Description = "Магический лес из высоченных бамбуков.", Address = "Киото, Япония", Latitude = 35.0170, Longitude = 135.6713 },
            new Sight { Name = "Золотой павильон Кинкаку-дзи", CityId = kyoto.Id, Type = SightType.Historical, Description = "Храм, покрытый сусальным золотом.", Address = "Киото, Япония", Latitude = 35.0394, Longitude = 135.7292 },
            new Sight { Name = "Святилище Фусими Инари", CityId = kyoto.Id, Type = SightType.Historical, Description = "Тысячи ворот тории на горе.", Address = "Киото, Япония", Latitude = 34.9671, Longitude = 135.7727 },

            // Исландия (Вик)
            new Sight { Name = "Водопад Скоугафосс", CityId = vik.Id, Type = SightType.Nature, Description = "60-метровый водопад с радугой.", Address = "Исландия", Latitude = 63.5321, Longitude = -19.5113 },
            new Sight { Name = "Черный пляж Рейнисфияра", CityId = vik.Id, Type = SightType.Nature, Description = "Драматичный черный пляж с базальтовыми колоннами.", Address = "Вик, Исландия", Latitude = 63.4048, Longitude = -19.0710 },
            new Sight { Name = "Национальный парк Тингведлир", CityId = vik.Id, Type = SightType.Nature, Description = "Место встречи тектонических плит.", Address = "Исландия", Latitude = 64.2558, Longitude = -21.1294 },

            // Египет (Луксор)
            new Sight { Name = "Карнакский храм", CityId = luxor.Id, Type = SightType.Historical, Description = "Крупнейший храмовый комплекс Египта.", Address = "Луксор, Египет", Latitude = 25.7188, Longitude = 32.6573 },
            new Sight { Name = "Долина Царей", CityId = luxor.Id, Type = SightType.Historical, Description = "Некрополь с гробницами фараонов.", Address = "Луксор, Египет", Latitude = 25.7402, Longitude = 32.6014 },
            new Sight { Name = "Колоссы Мемнона", CityId = luxor.Id, Type = SightType.Historical, Description = "Два каменных колосса высотой 18 метров.", Address = "Луксор, Египет", Latitude = 25.7190, Longitude = 32.6570 },
            new Sight { Name = "Храм Хатшепсут", CityId = luxor.Id, Type = SightType.Historical, Description = "Дворец-храм царицы Хатшепсут в отвесном утёсе.", Address = "Луксор, Египет", Latitude = 25.7380, Longitude = 32.6010 },

            // Турция (Каппадокия)
            new Sight { Name = "Национальный парк Гёреме", CityId = cappadocia.Id, Type = SightType.Nature, Description = "Уникальные каменные образования — «трубы фей».", Address = "Каппадокия, Турция", Latitude = 38.6430, Longitude = 34.8289 },
            new Sight { Name = "Долина любви", CityId = cappadocia.Id, Type = SightType.Nature, Description = "Романтические скальные образования.", Address = "Каппадокия, Турция", Latitude = 38.6200, Longitude = 34.8300 },
            new Sight { Name = "Подземный город Деринкую", CityId = cappadocia.Id, Type = SightType.Historical, Description = "Подземный город на 20 000 человек.", Address = "Каппадокия, Турция", Latitude = 38.3730, Longitude = 34.7330 },

            // Испания (Барселона)
            new Sight { Name = "Саграда Фамилия", CityId = barcelona.Id, Type = SightType.Cathedral, Description = "Незаконченный шедевр Гауди.", Address = "Барселона, Испания", Latitude = 41.4036, Longitude = 2.1744 },
            new Sight { Name = "Парк Гуэль", CityId = barcelona.Id, Type = SightType.Park, Description = "Парк в стиле модерн от Гауди.", Address = "Барселона, Испания", Latitude = 41.4145, Longitude = 2.1527 },
            new Sight { Name = "Рынок Бокерия", CityId = barcelona.Id, Type = SightType.Entertainment, Description = "Крупнейший рынок Барселоны.", Address = "Барселона, Испания", Latitude = 41.3818, Longitude = 2.1725 },
            new Sight { Name = "Дом Бальо", CityId = barcelona.Id, Type = SightType.Historical, Description = "Жилой дом от Гауди в стиле модерн.", Address = "Барселона, Испания", Latitude = 41.3914, Longitude = 2.1650 },

            // Таиланд (Пхи-Пхи)
            new Sight { Name = "Остров Пхи-Пхи-Ле", CityId = phuket.Id, Type = SightType.Nature, Description = "Необитаемый остров с лагунами.", Address = "Пхи-Пхи, Таиланд", Latitude = 7.6800, Longitude = 98.7700 },
            new Sight { Name = "Бухта Майя Бэй", CityId = phuket.Id, Type = SightType.Nature, Description = "Легендарная бухта из фильма «Пляж».", Address = "Пхи-Пхи, Таиланд", Latitude = 7.6750, Longitude = 98.7680 },
            new Sight { Name = "Phi Phi Viewpoint", CityId = phuket.Id, Type = SightType.Nature, Description = "Смотровая площадка на весь архипелаг.", Address = "Пхи-Пхи, Таиланд", Latitude = 7.7400, Longitude = 98.7750 },

            // США (Гранд-Каньон)
            new Sight { Name = "Mather Point", CityId = grandCanyon.Id, Type = SightType.Nature, Description = "Главная смотровая площадка каньона.", Address = "Гранд-Каньон, США", Latitude = 36.0612, Longitude = -112.1070 },
            new Sight { Name = "Река Колорадо", CityId = grandCanyon.Id, Type = SightType.Nature, Description = "Река, прорезавшая каньон 6 миллионов лет.", Address = "Гранд-Каньон, США", Latitude = 36.1000, Longitude = -112.1000 },
            new Sight { Name = "Стеклянный мост Skywalk", CityId = grandCanyon.Id, Type = SightType.Entertainment, Description = "Стеклянная смотровая площадка над каньоном.", Address = "Гранд-Каньон, США", Latitude = 36.0120, Longitude = -113.8110 },

            // Грузия (Тбилиси)
            new Sight { Name = "Крепость Нарикала", CityId = tbilisi.Id, Type = SightType.Historical, Description = "Древняя крепость над городом.", Address = "Тбилиси, Грузия", Latitude = 41.6850, Longitude = 44.7950 },
            new Sight { Name = "Серные бани Абанотубани", CityId = tbilisi.Id, Type = SightType.Historical, Description = "Исторический район с термальными источниками.", Address = "Тбилиси, Грузия", Latitude = 41.6820, Longitude = 44.8080 },
            new Sight { Name = "Мост Мира", CityId = tbilisi.Id, Type = SightType.Monument, Description = "Пешеходный мост через Куру.", Address = "Тбилиси, Грузия", Latitude = 41.6950, Longitude = 44.8080 },
            new Sight { Name = "Улица Шардени", CityId = tbilisi.Id, Type = SightType.Entertainment, Description = "Оживлённая улица с ресторанами.", Address = "Тбилиси, Грузия", Latitude = 41.6930, Longitude = 44.8060 },

            // ОАЭ (Дубай)
            new Sight { Name = "Бурдж-Халифа", CityId = dubai.Id, Type = SightType.Historical, Description = "Самое высокое здание мира, 828 метров.", Address = "Дубай, ОАЭ", Latitude = 25.1972, Longitude = 55.2744 },
            new Sight { Name = "Дубай Молл", CityId = dubai.Id, Type = SightType.Entertainment, Description = "Крупнейший торговый центр мира.", Address = "Дубай, ОАЭ", Latitude = 25.1980, Longitude = 55.2795 },
            new Sight { Name = "Поющие фонтаны", CityId = dubai.Id, Type = SightType.Entertainment, Description = "Музыкальные фонтаны у Бурдж-Халифа.", Address = "Дубай, ОАЭ", Latitude = 25.1960, Longitude = 55.2750 },
            new Sight { Name = "Дубай Марина", CityId = dubai.Id, Type = SightType.Nature, Description = "Искусственный канал с яхтами.", Address = "Дубай, ОАЭ", Latitude = 25.0800, Longitude = 55.1400 },

            // Норвегия (Гейрангер-фьорд)
            new Sight { Name = "Гейрангер-фьорд", CityId = geiranger.Id, Type = SightType.Nature, Description = "UNESCO: один из самых красивых фьордов.", Address = "Норвегия", Latitude = 62.1000, Longitude = 7.0900 },
            new Sight { Name = "Водопад Семь сестёр", CityId = geiranger.Id, Type = SightType.Nature, Description = "Семь тонких струй водопада.", Address = "Норвегия", Latitude = 62.1050, Longitude = 7.0950 },
            new Sight { Name = "Флюдалсьювет", CityId = geiranger.Id, Type = SightType.Nature, Description = "Знаменитая смотровая площадка.", Address = "Норвегия", Latitude = 62.1040, Longitude = 7.0940 },

            // Перу (Мачу-Пикчу)
            new Sight { Name = "Цитадель Мачу-Пикчу", CityId = machuPicchu.Id, Type = SightType.Historical, Description = "Затерянный город инков в Андах.", Address = "Мачу-Пикчу, Перу", Latitude = -13.1630, Longitude = -72.5450 },
            new Sight { Name = "Гора Уайна-Пикчу", CityId = machuPicchu.Id, Type = SightType.Nature, Description = "Остроконечная гора над цитаделью.", Address = "Мачу-Пикчу, Перу", Latitude = -13.1570, Longitude = -72.5480 },
            new Sight { Name = "Ворота Солнца Интипунку", CityId = machuPicchu.Id, Type = SightType.Historical, Description = "Каменная арка с видом на долину.", Address = "Мачу-Пикчу, Перу", Latitude = -13.1620, Longitude = -72.5440 },

            // Чехия (Прага)
            new Sight { Name = "Карлов мост", CityId = prague.Id, Type = SightType.Historical, Description = "Средневековый мост со скульптурами.", Address = "Прага, Чехия", Latitude = 50.0865, Longitude = 14.4114 },
            new Sight { Name = "Староместская площадь", CityId = prague.Id, Type = SightType.Historical, Description = "Главная площадь Старого города.", Address = "Прага, Чехия", Latitude = 50.0875, Longitude = 14.4213 },
            new Sight { Name = "Пражские куранты", CityId = prague.Id, Type = SightType.Historical, Description = "Астрономические часы XIV века.", Address = "Прага, Чехия", Latitude = 50.0870, Longitude = 14.4210 },
            new Sight { Name = "Пражский Град", CityId = prague.Id, Type = SightType.Historical, Description = "Крупнейший замковый комплекс мира.", Address = "Прага, Чехия", Latitude = 50.0910, Longitude = 14.4015 },

            // Греция (Афины/Санторини)
            new Sight { Name = "Афинский Акрополь", CityId = athens.Id, Type = SightType.Historical, Description = "Древнегреческий город-крепость.", Address = "Афины, Греция", Latitude = 37.9715, Longitude = 23.7267 },
            new Sight { Name = "Парфенон", CityId = athens.Id, Type = SightType.Historical, Description = "Храм Афины на Акрополе.", Address = "Афины, Греция", Latitude = 37.9715, Longitude = 23.7267 },
            new Sight { Name = "Деревня Ия на Санторини", CityId = santorini.Id, Type = SightType.Nature, Description = "Белые домики и закаты.", Address = "Санторини, Греция", Latitude = 36.4618, Longitude = 25.3753 },

            // Марокко (Марракеш)
            new Sight { Name = "Площадь Джамаа-эль-Фна", CityId = marrakech.Id, Type = SightType.Entertainment, Description = "Оживлённая площадь с фокусниками и едой.", Address = "Марракеш, Марокко", Latitude = 31.6260, Longitude = -7.9890 },
            new Sight { Name = "Дворец Бахия", CityId = marrakech.Id, Type = SightType.Historical, Description = "Роскошный дворец XIX века.", Address = "Марракеш, Марокко", Latitude = 31.6230, Longitude = -7.9870 },
            new Sight { Name = "Сад Мажорель", CityId = marrakech.Id, Type = SightType.Park, Description = "Сад в стиле ар-деко от Ив Сен-Лорана.", Address = "Марракеш, Марокко", Latitude = 31.6250, Longitude = -7.9980 },

            // ЮАР (НП Крюгер)
            new Sight { Name = "Национальный парк Крюгер", CityId = kruger.Id, Type = SightType.Nature, Description = "Крупнейший заповедник ЮАР.", Address = "ЮАР", Latitude = -24.0000, Longitude = 31.5000 },
            new Sight { Name = "Каньон реки Блайд", CityId = kruger.Id, Type = SightType.Nature, Description = "Глубокий каньон с водопадами.", Address = "ЮАР", Latitude = -24.5000, Longitude = 30.8000 },
            new Sight { Name = "Драконовы горы", CityId = kruger.Id, Type = SightType.Nature, Description = "Древние горы с пещерами.", Address = "ЮАР", Latitude = -24.2000, Longitude = 30.2000 },

            // Великобритания (Лондон)
            new Sight { Name = "Бейкер-стрит", CityId = london.Id, Type = SightType.Historical, Description = "Улица Шерлока Холмса.", Address = "Лондон, Великобритания", Latitude = 51.5220, Longitude = -0.1550 },
            new Sight { Name = "Платформа 9 3/4", CityId = london.Id, Type = SightType.Entertainment, Description = "Место из Гарри Поттера на Кингс-Кросс.", Address = "Лондон, Великобритания", Latitude = 51.5320, Longitude = -0.1260 },
            new Sight { Name = "Тауэрский мост", CityId = london.Id, Type = SightType.Historical, Description = "Подъёмный мост 1894 года.", Address = "Лондон, Великобритания", Latitude = 51.5055, Longitude = -0.0755 },
            new Sight { Name = "Лондонский Глаз", CityId = london.Id, Type = SightType.Entertainment, Description = "Огромное колесо обозрения на Темзе.", Address = "Лондон, Великобритания", Latitude = 51.5033, Longitude = -0.1195 },

            // Индонезия (Бали)
            new Sight { Name = "Вулкан Батур", CityId = bali.Id, Type = SightType.Nature, Description = "Действующий вулкан с видом на озеро.", Address = "Бали, Индонезия", Latitude = -8.2390, Longitude = 115.3750 },
            new Sight { Name = "Храм Улувату", CityId = bali.Id, Type = SightType.Historical, Description = "Морской храм на обрыве.", Address = "Бали, Индонезия", Latitude = -8.8290, Longitude = 115.0849 },
            new Sight { Name = "Рисовые террасы Тегаллаланг", CityId = bali.Id, Type = SightType.Nature, Description = "Ступенчатые поля риса.", Address = "Бали, Индонезия", Latitude = -8.4312, Longitude = 115.2790 },

            // Швейцария (Церматт)
            new Sight { Name = "Гора Маттерхорн", CityId = zermatt.Id, Type = SightType.Nature, Description = "Икона Альп, 4478 м.", Address = "Церматт, Швейцария", Latitude = 45.9763, Longitude = 7.6586 },
            new Sight { Name = "Курорт Церматт", CityId = zermatt.Id, Type = SightType.Nature, Description = "Беспешеходный курорт без машин.", Address = "Церматт, Швейцария", Latitude = 46.0207, Longitude = 7.7491 },
            new Sight { Name = "Перевал Юнгфрауйох", CityId = zermatt.Id, Type = SightType.Nature, Description = "Высочайшая точка Европы — 3454 м.", Address = "Швейцария", Latitude = 46.5472, Longitude = 7.9853 },
        };

        context.Sights.AddRange(sights);
        context.SaveChanges();

        // =====================
        // ПОЛЬЗОВАТЕЛИ
        // =====================
        var tourist1 = new User { FullName = "Иван Иванов", Email = "ivan@test.com", Phone = "+375 (29) 111-22-33", Role = UserRole.Tourist, RegistrationDate = DateTime.Now.AddDays(-60) };
        tourist1.PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123");

        var tourist2 = new User { FullName = "Мария Петрова", Email = "maria@test.com", Phone = "+375 (33) 333-44-55", Role = UserRole.Tourist, RegistrationDate = DateTime.Now.AddDays(-30) };
        tourist2.PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123");

        var tourist3 = new User { FullName = "Алексей Сидоров", Email = "alex@test.com", Phone = "+375 (29) 555-66-77", Role = UserRole.Tourist, RegistrationDate = DateTime.Now.AddDays(-15) };
        tourist3.PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123");

        var admin = new User { FullName = "Администратор", Email = "admin@travelguide.com", Phone = "+375 (33) 000-00-01", Role = UserRole.Admin, RegistrationDate = DateTime.Now.AddDays(-90) };
        admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123");

        var manager = new User { FullName = "Менеджер Ольга", Email = "manager@travelguide.com", Phone = "+375 (29) 000-00-02", Role = UserRole.Manager, RegistrationDate = DateTime.Now.AddDays(-80) };
        manager.PasswordHash = BCrypt.Net.BCrypt.HashPassword("manager123");

        context.Users.AddRange(tourist1, tourist2, tourist3, admin, manager);
        context.SaveChanges();

        // =====================
        // ТУРЫ
        // =====================
        var tours = new List<Tour>
        {
            // 1. Италия - city
            new Tour { Name = "Римские каникулы: Колизей без очередей и секретные дворики", TourType = TourType.Excursion, Description = "6 дней в Риме. Колизей, Римский форум, Фонтан Треви, Пантеон. Секретные дворики и кафе.", Duration = 6, Price = 72000, CountryId = italy.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт, заселение\nДень 2: Колизей, Форумы\nДень 3: Ватикан\nДень 4: Площади, Треви\nДень 5: Пантеон, дворики\nДень 6: Вылет", CreatedDate = DateTime.Now.AddDays(-30) },

            // 2. Франция - food
            new Tour { Name = "Гастрономический Париж: круассаны, сыры и вино на Монмартре", TourType = TourType.Excursion, Description = "5 дней в Париже. Дегустации, markets, Монмартр, Эйфелева башня.", Duration = 5, Price = 65000, CountryId = france.Id, AgencyId = wanderlust.Id, Route = "День 1: Прилёт\nДень 2: Монмартр, круассаны\nДень 3: Эйфелева башня\nДень 4: Дегустация вин\nДень 5: Вылет", CreatedDate = DateTime.Now.AddDays(-28) },

            // 3. Япония - nature
            new Tour { Name = "Цветение сакуры в Киото: прогулка по бамбуковой роще", TourType = TourType.Combined, Description = "8 дней в Киото. Бамбуковый лес, Золотой павильон, святилище Фусими Инари.", Duration = 8, Price = 120000, CountryId = japan.Id, AgencyId = wanderlust.Id, Route = "День 1: Прилёт в Киото\nДень 2: Бамбуковый лес\nДень 3: Золотой павильон\nДень 4: Фусими Инари\nДни 5-8: Свободное время", CreatedDate = DateTime.Now.AddDays(-25) },

            // 4. Исландия - extreme
            new Tour { Name = "Охота за Северным сиянием и черные пляжи Вик", TourType = TourType.Adventure, Description = "7 дней в Исландии. Северное сияние, черные пляжи, водопады.", Duration = 7, Price = 95000, CountryId = iceland.Id, AgencyId = bestTours.Id, Route = "День 1: Прилёт в Рейкьявик\nДень 2: Водопад Скоугафосс\nДень 3: Черный пляж\nДень 4: Тингведлир\nДни 5-7: Охота за сиянием", CreatedDate = DateTime.Now.AddDays(-22) },

            // 5. Египет - history
            new Tour { Name = "Тайны Луксора: гробницы фараонов и долина Царей", TourType = TourType.Excursion, Description = "5 дней в Луксоре. Карнакский храм, Долина Царей, Колоссы Мемнона.", Duration = 5, Price = 45000, CountryId = egypt.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт в Луксор\nДень 2: Карнакский храм\nДень 3: Долина Царей\nДень 4: Храм Хатшепсут\nДень 5: Вылет", CreatedDate = DateTime.Now.AddDays(-20) },

            // 6. Турция - nature
            new Tour { Name = "Каппадокия на рассвете: полет на воздушном шаре", TourType = TourType.Adventure, Description = "4 дня в Каппадокии. Полет на шаре, Долина любви, подземный город.", Duration = 4, Price = 55000, CountryId = turkey.Id, AgencyId = bestTours.Id, Route = "День 1: Прилёт\nДень 2: Полет на шаре\nДень 3: Деринкую, Долина любви\nДень 4: Вылет", CreatedDate = DateTime.Now.AddDays(-18) },

            // 7. Испания - city
            new Tour { Name = "Архитектура Гауди и стритфуд на рынке Бокерия в Барселоне", TourType = TourType.Combined, Description = "6 дней в Барселоне. Саграда Фамилия, Парк Гуэль, Бокерия, Дом Бальо.", Duration = 6, Price = 68000, CountryId = spain.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт\nДень 2: Саграда Фамилия\nДень 3: Парк Гуэль\nДень 4: Рынок Бокерия\nДень 5: Дом Бальо\nДень 6: Вылет", CreatedDate = DateTime.Now.AddDays(-15) },

            // 8. Таиланд - nature
            new Tour { Name = "Тропический рай: трекинг по джунглям и островам Пхи-Пхи", TourType = TourType.Combined, Description = "10 дней на Пхи-Пхи. Трекинг, острова, снорклинг.", Duration = 10, Price = 75000, CountryId = thailand.Id, AgencyId = wanderlust.Id, Route = "День 1: Прилёт\nДень 2: Трекинг\nДень 3-4: Острова Пхи-Пхи\nДни 5-10: Пляж, дайвинг", CreatedDate = DateTime.Now.AddDays(-12) },

            // 9. США - extreme
            new Tour { Name = "Вертолетная экскурсия над Гранд-Каньоном", TourType = TourType.Adventure, Description = "4 дня у Гранд-Каньона. Вертолет, Skywalk, река Колорадо.", Duration = 4, Price = 85000, CountryId = usa.Id, AgencyId = bestTours.Id, Route = "День 1: Прилёт\nДень 2: Вертолёт над каньоном\nДень 3: Skywalk, река\nДень 4: Вылет", CreatedDate = DateTime.Now.AddDays(-10) },

            // 10. Грузия - food
            new Tour { Name = "Душа Тбилиси: дегустация вин в старых маранах", TourType = TourType.Excursion, Description = "5 дней в Тбилиси. Нарикала, серные бани, винные погреба, Мост Мира.", Duration = 5, Price = 35000, CountryId = georgia.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт\nДень 2: Нарикала, бани\nДень 3: Дегустация вин\nДень 4: Улица Шардени\nДень 5: Вылет", CreatedDate = DateTime.Now.AddDays(-8) },

            // 11. ОАЭ - city
            new Tour { Name = "Футуристичный Дубай: подъем на Бурдж-Халифа и шоу фонтанов", TourType = TourType.Excursion, Description = "5 дней в Дубае. Бурдж-Халифа, Дубай Молл, фонтаны, Марина.", Duration = 5, Price = 95000, CountryId = uae.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт\nДень 2: Бурдж-Халифа\nДень 3: Дубай Молл, фонтаны\nДень 4: Дубай Марина\nДень 5: Вылет", CreatedDate = DateTime.Now.AddDays(-6) },

            // 12. Норвегия - nature
            new Tour { Name = "Круиз по Гейрангер-фьорду: среди гор и водопадов", TourType = TourType.Cruise, Description = "6 дней в Норвегии. Фьорды, водопады, Семь сестёр.", Duration = 6, Price = 110000, CountryId = norway.Id, AgencyId = wanderlust.Id, Route = "День 1: Прилёт\nДень 2-3: Круиз по фьорду\nДень 4: Флюдалсьювет\nДень 5-6: Свободное время", CreatedDate = DateTime.Now.AddDays(-5) },

            // 13. Перу - history
            new Tour { Name = "Затерянный город инков: восхождение на Мачу-Пикчу", TourType = TourType.Adventure, Description = "7 дней в Перу. Мачу-Пикчу, Уайна-Пикчу, Ворота Солнца.", Duration = 7, Price = 130000, CountryId = peru.Id, AgencyId = bestTours.Id, Route = "День 1: Прилёт в Куско\nДень 2: Акклиматизация\nДень 3-5: Трекинг\nДень 6: Мачу-Пикчу\nДень 7: Вылет", CreatedDate = DateTime.Now.AddDays(-4) },

            // 14. Чехия - city
            new Tour { Name = "Мистическая Прага: легенды алхимиков и Карлов мост", TourType = TourType.Excursion, Description = "4 дня в Праге. Карлов мост, Старая площадь, куранты, Град.", Duration = 4, Price = 42000, CountryId = czech.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт\nДень 2: Карлов мост, площадь\nДень 3: Пражский Град\nДень 4: Вылет", CreatedDate = DateTime.Now.AddDays(-3) },

            // 15. Греция - history
            new Tour { Name = "Акрополь и закаты Санторини: колыбель цивилизации", TourType = TourType.Combined, Description = "10 дней: 4 в Афинах + 6 на Санторини. Акрополь, Парфенон, закаты.", Duration = 10, Price = 88000, CountryId = greece.Id, AgencyId = wanderlust.Id, Route = "День 1: Прилёт в Афины\nДень 2: Акрополь\nДень 3: Парфенон\nДень 4: Перелёт\nДни 5-10: Санторини", CreatedDate = DateTime.Now.AddDays(-2) },

            // 16. Марокко - food
            new Tour { Name = "Ароматы Марракеша: восточные базары и чай в пустыне", TourType = TourType.Combined, Description = "5 дней в Марракеше. Базары, дворец Бахия, Сад Мажорель, чай.", Duration = 5, Price = 58000, CountryId = morocco.Id, AgencyId = bestTours.Id, Route = "День 1: Прилёт\nДень 2: Базары\nДень 3: Дворец Бахия\nДень 4: Сад Мажорель\nДень 5: Вылет", CreatedDate = DateTime.Now.AddDays(-1) },

            // 17. ЮАР - nature
            new Tour { Name = "Настоящее сафари: фотоохота на «большую пятерку»", TourType = TourType.Adventure, Description = "7 дней в ЮАР. НП Крюгер, каньон, Драконовы горы.", Duration = 7, Price = 145000, CountryId = southAfrica.Id, AgencyId = wanderlust.Id, Route = "День 1: Прилёт\nДень 2-4: Сафари\nДень 5: Каньон\nДень 6: Драконовы горы\nДень 7: Вылет", CreatedDate = DateTime.Now.AddDays(-1) },

            // 18. Великобритания - city
            new Tour { Name = "По следам Шерлока и Гарри Поттера: неформальный Лондон", TourType = TourType.Excursion, Description = "5 дней в Лондоне. Бейкер-стрит, Платформа 9 3/4, Тауэр, Глаз.", Duration = 5, Price = 78000, CountryId = uk.Id, AgencyId = travelPro.Id, Route = "День 1: Прилёт\nДень 2: Бейкер-стрит\nДень 3: Платформа 9 3/4\nДень 4: Тауэр, Глаз\nДень 5: Вылет", CreatedDate = DateTime.Now.AddDays(-1) },

            // 19. Индонезия - extreme
            new Tour { Name = "Серфинг на Бали и восхождение на вулкан Батур", TourType = TourType.Adventure, Description = "8 дней на Бали. Серфинг, вулкан, храм Улувату, рисовые террасы.", Duration = 8, Price = 82000, CountryId = indonesia.Id, AgencyId = bestTours.Id, Route = "День 1: Прилёт\nДень 2: Серфинг\nДень 3: Вулкан Батур\nДень 4: Храм Улувату\nДни 5-8: Террасы, отдых", CreatedDate = DateTime.Now.AddDays(-1) },

            // 20. Швейцария - nature
            new Tour { Name = "Альпийская сказка: панорамный поезд мимо ледников", TourType = TourType.Cruise, Description = "6 дней в Швейцарии. Маттерхорн, Церматт, Юнгфрауйох.", Duration = 6, Price = 135000, CountryId = switzerland.Id, AgencyId = wanderlust.Id, Route = "День 1: Прилёт\nДень 2: Церматт\nДень 3: Маттерхорн\nДень 4: Юнгфрауйох\nДень 5-6: Свободное время", CreatedDate = DateTime.Now.AddDays(-1) },
        };

        context.Tours.AddRange(tours);
        context.SaveChanges();

        // =====================
        // СВЯЗИ ТУР-ОТЕЛЬ и ТУР-ДОСТОПРИМЕЧАТЕЛЬНОСТЬ
        // =====================
        var tourHotels = new List<TourHotel>
        {
            // 1. Рим
            new TourHotel { TourId = tours[0].Id, HotelId = hotels[0].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[0].Id, HotelId = hotels[1].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[0].Id, HotelId = hotels[2].Id, DayNumber = 1 },
            // 2. Париж
            new TourHotel { TourId = tours[1].Id, HotelId = hotels[3].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[1].Id, HotelId = hotels[4].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[1].Id, HotelId = hotels[5].Id, DayNumber = 1 },
            // 3. Киото
            new TourHotel { TourId = tours[2].Id, HotelId = hotels[6].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[2].Id, HotelId = hotels[7].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[2].Id, HotelId = hotels[8].Id, DayNumber = 1 },
            // 4. Исландия
            new TourHotel { TourId = tours[3].Id, HotelId = hotels[9].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[3].Id, HotelId = hotels[10].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[3].Id, HotelId = hotels[11].Id, DayNumber = 1 },
            // 5. Луксор
            new TourHotel { TourId = tours[4].Id, HotelId = hotels[12].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[4].Id, HotelId = hotels[13].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[4].Id, HotelId = hotels[14].Id, DayNumber = 1 },
            // 6. Каппадокия
            new TourHotel { TourId = tours[5].Id, HotelId = hotels[15].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[5].Id, HotelId = hotels[16].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[5].Id, HotelId = hotels[17].Id, DayNumber = 1 },
            // 7. Барселона
            new TourHotel { TourId = tours[6].Id, HotelId = hotels[18].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[6].Id, HotelId = hotels[19].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[6].Id, HotelId = hotels[20].Id, DayNumber = 1 },
            // 8. Пхи-Пхи
            new TourHotel { TourId = tours[7].Id, HotelId = hotels[21].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[7].Id, HotelId = hotels[22].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[7].Id, HotelId = hotels[23].Id, DayNumber = 1 },
            // 9. Гранд-Каньон
            new TourHotel { TourId = tours[8].Id, HotelId = hotels[24].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[8].Id, HotelId = hotels[25].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[8].Id, HotelId = hotels[26].Id, DayNumber = 1 },
            // 10. Тбилиси
            new TourHotel { TourId = tours[9].Id, HotelId = hotels[27].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[9].Id, HotelId = hotels[28].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[9].Id, HotelId = hotels[29].Id, DayNumber = 1 },
            // 11. Дубай
            new TourHotel { TourId = tours[10].Id, HotelId = hotels[30].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[10].Id, HotelId = hotels[31].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[10].Id, HotelId = hotels[32].Id, DayNumber = 1 },
            // 12. Норвегия
            new TourHotel { TourId = tours[11].Id, HotelId = hotels[33].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[11].Id, HotelId = hotels[34].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[11].Id, HotelId = hotels[35].Id, DayNumber = 1 },
            // 13. Перу
            new TourHotel { TourId = tours[12].Id, HotelId = hotels[36].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[12].Id, HotelId = hotels[37].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[12].Id, HotelId = hotels[38].Id, DayNumber = 1 },
            // 14. Прага
            new TourHotel { TourId = tours[13].Id, HotelId = hotels[39].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[13].Id, HotelId = hotels[40].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[13].Id, HotelId = hotels[41].Id, DayNumber = 1 },
            // 15. Греция
            new TourHotel { TourId = tours[14].Id, HotelId = hotels[42].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[14].Id, HotelId = hotels[43].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[14].Id, HotelId = hotels[44].Id, DayNumber = 1 },
            // 16. Марокко
            new TourHotel { TourId = tours[15].Id, HotelId = hotels[45].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[15].Id, HotelId = hotels[46].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[15].Id, HotelId = hotels[47].Id, DayNumber = 1 },
            // 17. ЮАР
            new TourHotel { TourId = tours[16].Id, HotelId = hotels[48].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[16].Id, HotelId = hotels[49].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[16].Id, HotelId = hotels[50].Id, DayNumber = 1 },
            // 18. Лондон
            new TourHotel { TourId = tours[17].Id, HotelId = hotels[51].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[17].Id, HotelId = hotels[52].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[17].Id, HotelId = hotels[53].Id, DayNumber = 1 },
            // 19. Бали
            new TourHotel { TourId = tours[18].Id, HotelId = hotels[54].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[18].Id, HotelId = hotels[55].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[18].Id, HotelId = hotels[56].Id, DayNumber = 1 },
            // 20. Швейцария
            new TourHotel { TourId = tours[19].Id, HotelId = hotels[57].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[19].Id, HotelId = hotels[58].Id, DayNumber = 1 },
            new TourHotel { TourId = tours[19].Id, HotelId = hotels[59].Id, DayNumber = 1 },
        };

        var tourSights = new List<TourSight>
        {
            // 1. Рим
            new TourSight { TourId = tours[0].Id, SightId = sights[0].Id, DayNumber = 2 },
            new TourSight { TourId = tours[0].Id, SightId = sights[1].Id, DayNumber = 2 },
            new TourSight { TourId = tours[0].Id, SightId = sights[2].Id, DayNumber = 4 },
            new TourSight { TourId = tours[0].Id, SightId = sights[3].Id, DayNumber = 5 },
            // 2. Париж
            new TourSight { TourId = tours[1].Id, SightId = sights[4].Id, DayNumber = 2 },
            new TourSight { TourId = tours[1].Id, SightId = sights[5].Id, DayNumber = 2 },
            new TourSight { TourId = tours[1].Id, SightId = sights[6].Id, DayNumber = 2 },
            new TourSight { TourId = tours[1].Id, SightId = sights[7].Id, DayNumber = 3 },
            // 3. Киото
            new TourSight { TourId = tours[2].Id, SightId = sights[8].Id, DayNumber = 2 },
            new TourSight { TourId = tours[2].Id, SightId = sights[9].Id, DayNumber = 3 },
            new TourSight { TourId = tours[2].Id, SightId = sights[10].Id, DayNumber = 4 },
            // 4. Исландия
            new TourSight { TourId = tours[3].Id, SightId = sights[11].Id, DayNumber = 2 },
            new TourSight { TourId = tours[3].Id, SightId = sights[12].Id, DayNumber = 3 },
            new TourSight { TourId = tours[3].Id, SightId = sights[13].Id, DayNumber = 4 },
            // 5. Луксор
            new TourSight { TourId = tours[4].Id, SightId = sights[14].Id, DayNumber = 2 },
            new TourSight { TourId = tours[4].Id, SightId = sights[15].Id, DayNumber = 3 },
            new TourSight { TourId = tours[4].Id, SightId = sights[16].Id, DayNumber = 3 },
            new TourSight { TourId = tours[4].Id, SightId = sights[17].Id, DayNumber = 4 },
            // 6. Каппадокия
            new TourSight { TourId = tours[5].Id, SightId = sights[18].Id, DayNumber = 2 },
            new TourSight { TourId = tours[5].Id, SightId = sights[19].Id, DayNumber = 3 },
            new TourSight { TourId = tours[5].Id, SightId = sights[20].Id, DayNumber = 3 },
            // 7. Барселона
            new TourSight { TourId = tours[6].Id, SightId = sights[21].Id, DayNumber = 2 },
            new TourSight { TourId = tours[6].Id, SightId = sights[22].Id, DayNumber = 3 },
            new TourSight { TourId = tours[6].Id, SightId = sights[23].Id, DayNumber = 4 },
            new TourSight { TourId = tours[6].Id, SightId = sights[24].Id, DayNumber = 5 },
            // 8. Пхи-Пхи
            new TourSight { TourId = tours[7].Id, SightId = sights[25].Id, DayNumber = 3 },
            new TourSight { TourId = tours[7].Id, SightId = sights[26].Id, DayNumber = 4 },
            new TourSight { TourId = tours[7].Id, SightId = sights[27].Id, DayNumber = 4 },
            // 9. Гранд-Каньон
            new TourSight { TourId = tours[8].Id, SightId = sights[28].Id, DayNumber = 2 },
            new TourSight { TourId = tours[8].Id, SightId = sights[29].Id, DayNumber = 3 },
            new TourSight { TourId = tours[8].Id, SightId = sights[30].Id, DayNumber = 3 },
            // 10. Тбилиси
            new TourSight { TourId = tours[9].Id, SightId = sights[31].Id, DayNumber = 2 },
            new TourSight { TourId = tours[9].Id, SightId = sights[32].Id, DayNumber = 2 },
            new TourSight { TourId = tours[9].Id, SightId = sights[33].Id, DayNumber = 3 },
            new TourSight { TourId = tours[9].Id, SightId = sights[34].Id, DayNumber = 4 },
            // 11. Дубай
            new TourSight { TourId = tours[10].Id, SightId = sights[35].Id, DayNumber = 2 },
            new TourSight { TourId = tours[10].Id, SightId = sights[36].Id, DayNumber = 3 },
            new TourSight { TourId = tours[10].Id, SightId = sights[37].Id, DayNumber = 3 },
            new TourSight { TourId = tours[10].Id, SightId = sights[38].Id, DayNumber = 4 },
            // 12. Норвегия
            new TourSight { TourId = tours[11].Id, SightId = sights[39].Id, DayNumber = 2 },
            new TourSight { TourId = tours[11].Id, SightId = sights[40].Id, DayNumber = 2 },
            new TourSight { TourId = tours[11].Id, SightId = sights[41].Id, DayNumber = 4 },
            // 13. Перу
            new TourSight { TourId = tours[12].Id, SightId = sights[42].Id, DayNumber = 6 },
            new TourSight { TourId = tours[12].Id, SightId = sights[43].Id, DayNumber = 6 },
            new TourSight { TourId = tours[12].Id, SightId = sights[44].Id, DayNumber = 6 },
            // 14. Прага
            new TourSight { TourId = tours[13].Id, SightId = sights[45].Id, DayNumber = 2 },
            new TourSight { TourId = tours[13].Id, SightId = sights[46].Id, DayNumber = 2 },
            new TourSight { TourId = tours[13].Id, SightId = sights[47].Id, DayNumber = 2 },
            new TourSight { TourId = tours[13].Id, SightId = sights[48].Id, DayNumber = 3 },
            // 15. Греция
            new TourSight { TourId = tours[14].Id, SightId = sights[49].Id, DayNumber = 2 },
            new TourSight { TourId = tours[14].Id, SightId = sights[50].Id, DayNumber = 3 },
            new TourSight { TourId = tours[14].Id, SightId = sights[51].Id, DayNumber = 7 },
            // 16. Марокко
            new TourSight { TourId = tours[15].Id, SightId = sights[52].Id, DayNumber = 2 },
            new TourSight { TourId = tours[15].Id, SightId = sights[53].Id, DayNumber = 3 },
            new TourSight { TourId = tours[15].Id, SightId = sights[54].Id, DayNumber = 4 },
            // 17. ЮАР
            new TourSight { TourId = tours[16].Id, SightId = sights[55].Id, DayNumber = 2 },
            new TourSight { TourId = tours[16].Id, SightId = sights[56].Id, DayNumber = 5 },
            new TourSight { TourId = tours[16].Id, SightId = sights[57].Id, DayNumber = 6 },
            // 18. Лондон
            new TourSight { TourId = tours[17].Id, SightId = sights[58].Id, DayNumber = 2 },
            new TourSight { TourId = tours[17].Id, SightId = sights[59].Id, DayNumber = 3 },
            new TourSight { TourId = tours[17].Id, SightId = sights[60].Id, DayNumber = 4 },
            new TourSight { TourId = tours[17].Id, SightId = sights[61].Id, DayNumber = 4 },
            // 19. Бали
            new TourSight { TourId = tours[18].Id, SightId = sights[62].Id, DayNumber = 3 },
            new TourSight { TourId = tours[18].Id, SightId = sights[63].Id, DayNumber = 4 },
            new TourSight { TourId = tours[18].Id, SightId = sights[64].Id, DayNumber = 5 },
            // 20. Швейцария
            new TourSight { TourId = tours[19].Id, SightId = sights[65].Id, DayNumber = 3 },
            new TourSight { TourId = tours[19].Id, SightId = sights[66].Id, DayNumber = 2 },
            new TourSight { TourId = tours[19].Id, SightId = sights[67].Id, DayNumber = 4 },
        };

        context.TourHotels.AddRange(tourHotels);
        context.TourSights.AddRange(tourSights);
        context.SaveChanges();

        // =====================
        // ОТЗЫВЫ
        // =====================
        var reviews = new List<Review>
        {
            // === ТУРЫ (2-3 отзыва на каждый) ===
            // 0. Рим
            new Review { UserId = tourist1.Id, TourId = tours[0].Id, Rating = 5, Text = "Организация пушка! Всё четко, без лишней воды и беготни.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-25) },
            new Review { UserId = tourist2.Id, TourId = tours[0].Id, Rating = 5, Text = "Гид — ходячая энциклопедия! Слушали затаив дыхание.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-22) },
            new Review { UserId = tourist3.Id, TourId = tours[0].Id, Rating = 4, Text = "Гид классный, но на последней локации хотелось бы больше времени.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-20) },
            // 1. Париж
            new Review { UserId = tourist1.Id, TourId = tours[1].Id, Rating = 5, Text = "Просто вау! Гид рассказал кучу местных фишек, которых нет в путеводителях.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-24) },
            new Review { UserId = tourist2.Id, TourId = tours[1].Id, Rating = 5, Text = "Виды на закате просто завораживают, локация подобрана идеально.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-21) },
            // 2. Киото
            new Review { UserId = tourist3.Id, TourId = tours[2].Id, Rating = 5, Text = "Локации невероятные, фотки получились космические. Рекомендую!", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-20) },
            new Review { UserId = tourist1.Id, TourId = tours[2].Id, Rating = 5, Text = "Потрясающие виды и очень легкая подача материала.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-18) },
            new Review { UserId = tourist2.Id, TourId = tours[2].Id, Rating = 4, Text = "Интересно, но темп повествования в аудиогиде показался быстроватым.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-16) },
            // 3. Исландия
            new Review { UserId = tourist1.Id, TourId = tours[3].Id, Rating = 5, Text = "Супер! Настоящая перезагрузка за один день.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-18) },
            new Review { UserId = tourist2.Id, TourId = tours[3].Id, Rating = 5, Text = "Это была лучшая поездка за год! Всё организовано на высшем уровне.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-15) },
            // 4. Луксор
            new Review { UserId = tourist3.Id, TourId = tours[4].Id, Rating = 5, Text = "Лучший тур, в котором я был. Всё на высоте.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-16) },
            new Review { UserId = tourist1.Id, TourId = tours[4].Id, Rating = 5, Text = "Узнал больше, чем за все школьные уроки истории.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-14) },
            new Review { UserId = tourist2.Id, TourId = tours[4].Id, Rating = 4, Text = "Экскурсия отличная. Но берите удобную обувь, ходить придется много.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-12) },
            // 5. Каппадокия
            new Review { UserId = tourist1.Id, TourId = tours[5].Id, Rating = 5, Text = "Гид невероятно харизматичный. Время пролетело как одна минута.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-14) },
            new Review { UserId = tourist3.Id, TourId = tours[5].Id, Rating = 5, Text = "Очень крутая подборка локаций. Настоящая экзотика без толп туристов.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-11) },
            // 6. Барселона
            new Review { UserId = tourist2.Id, TourId = tours[6].Id, Rating = 4, Text = "Маршрут супер, но в выходные на точках было слишком много людей.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-12) },
            new Review { UserId = tourist1.Id, TourId = tours[6].Id, Rating = 5, Text = "Прекрасный баланс исторических фактов и современных городских легенд.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-10) },
            new Review { UserId = tourist3.Id, TourId = tours[6].Id, Rating = 5, Text = "Отель шикарный, вид из окна стоил каждого цента. Тур топ!", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-8) },
            // 7. Пхи-Пхи
            new Review { UserId = tourist1.Id, TourId = tours[7].Id, Rating = 5, Text = "Удобная навигация, нашли все секретные споты по карте.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-10) },
            new Review { UserId = tourist2.Id, TourId = tours[7].Id, Rating = 5, Text = "Космические виды! Флешка на телефоне забита фотографиями.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-8) },
            // 8. Гранд-Каньон
            new Review { UserId = tourist3.Id, TourId = tours[8].Id, Rating = 5, Text = "Очень атмосферный маршрут. Узнали много новых фактов.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-8) },
            new Review { UserId = tourist1.Id, TourId = tours[8].Id, Rating = 5, Text = "Потрясающий маршрут. Увидели всё, о чем мечтали, и даже больше.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-6) },
            new Review { UserId = tourist2.Id, TourId = tours[8].Id, Rating = 4, Text = "Экскурсия топ, но на смотровой площадке был сильный ветер.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-5) },
            // 9. Тбилиси
            new Review { UserId = tourist1.Id, TourId = tours[9].Id, Rating = 5, Text = "Открыл для себя страну заново благодаря этому гиду. Браво!", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-7) },
            new Review { UserId = tourist3.Id, TourId = tours[9].Id, Rating = 4, Text = "В целом круто, но местами пропадал интернет и карта тупила.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-5) },
            // 10. Дубай
            new Review { UserId = tourist2.Id, TourId = tours[10].Id, Rating = 5, Text = "Супер! Настоящая перезагрузка за один день.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-6) },
            new Review { UserId = tourist1.Id, TourId = tours[10].Id, Rating = 5, Text = "Максимальный комфорт на протяжении всего пути. Организаторы умницы.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-4) },
            new Review { UserId = tourist3.Id, TourId = tours[10].Id, Rating = 4, Text = "Всё понравилось, но хотелось бы больше инфы про архитектуру.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-3) },
            // 11. Норвегия
            new Review { UserId = tourist1.Id, TourId = tours[11].Id, Rating = 5, Text = "Отличный баланс между экскурсиями и свободным временем. Рекомендую!", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-5) },
            new Review { UserId = tourist2.Id, TourId = tours[11].Id, Rating = 5, Text = "Интересная подача, никакой занудной истории. Детям тоже понравилось.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-3) },
            // 12. Перу
            new Review { UserId = tourist3.Id, TourId = tours[12].Id, Rating = 5, Text = "Опыт незабываемый. Обязательно поеду еще раз по другой программе.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-4) },
            new Review { UserId = tourist1.Id, TourId = tours[12].Id, Rating = 4, Text = "Маршрут классный, но местами сложно пройти с детской коляской.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-2) },
            // 13. Прага
            new Review { UserId = tourist2.Id, TourId = tours[13].Id, Rating = 5, Text = "Просто вау! Гид рассказал кучу местных фишек, которых нет в путеводителях.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-3) },
            new Review { UserId = tourist1.Id, TourId = tours[13].Id, Rating = 5, Text = "Уютный бутик-отель, вкусные завтраки и идеальный маршрут.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-2) },
            // 14. Греция
            new Review { UserId = tourist3.Id, TourId = tours[14].Id, Rating = 5, Text = "Виды на закате просто завораживают, локация подобрана идеально.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-2) },
            new Review { UserId = tourist1.Id, TourId = tours[14].Id, Rating = 4, Text = "Программа насыщенная, но из-за пробок в конце немного смазалось.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-1) },
            // 15. Марокко
            new Review { UserId = tourist2.Id, TourId = tours[15].Id, Rating = 5, Text = "Максимально дружелюбная атмосфера, группа собралась отличная.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-2) },
            new Review { UserId = tourist3.Id, TourId = tours[15].Id, Rating = 5, Text = "Подача материала очень живая, юмор у гида отличный.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-1) },
            // 16. ЮАР
            new Review { UserId = tourist1.Id, TourId = tours[16].Id, Rating = 5, Text = "Очень емко, без занудства. Самое то для первого знакомства.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-2) },
            new Review { UserId = tourist2.Id, TourId = tours[16].Id, Rating = 4, Text = "Гид отличный, но группа была слишком большая, человек 25.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-1) },
            // 17. Лондон
            new Review { UserId = tourist3.Id, TourId = tours[17].Id, Rating = 5, Text = "Интересные интерактивы по пути, время пролетело незаметно.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-2) },
            new Review { UserId = tourist1.Id, TourId = tours[17].Id, Rating = 5, Text = "Замечательный формат для тех, кто не любит ходить толпой.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-1) },
            // 18. Бали
            new Review { UserId = tourist2.Id, TourId = tours[18].Id, Rating = 5, Text = "Рекомендую этот тур всем, кто устал от банальных экскурсий.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-1) },
            new Review { UserId = tourist3.Id, TourId = tours[18].Id, Rating = 4, Text = "Хорошая прогулка, правда погода немного подвела.", Status = ReviewStatus.Approved, Date = DateTime.Now },
            // 19. Швейцария
            new Review { UserId = tourist1.Id, TourId = tours[19].Id, Rating = 5, Text = "Рекомендую брать именно этот вариант, программа мега-насыщенная.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-1) },
            new Review { UserId = tourist2.Id, TourId = tours[19].Id, Rating = 5, Text = "Истории действительно увлекают, а не усыпляют. Твердая пятерка!", Status = ReviewStatus.Approved, Date = DateTime.Now },

            // === ОТЕЛИ (60 отзывов — по одному на каждый отель) ===
            new Review { UserId = tourist1.Id, HotelId = hotels[0].Id, Rating = 5, Text = "Отель просто шикарный! Номер чистый, просторный, а вид из окна — отдельный восторг.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-59) },
            new Review { UserId = tourist2.Id, HotelId = hotels[1].Id, Rating = 5, Text = "Идеальное расположение в самом центре. До всех главных локаций доходили пешком.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-58) },
            new Review { UserId = tourist3.Id, HotelId = hotels[2].Id, Rating = 5, Text = "Нереально вкусные завтраки! Персонал мега-приветливый, заселили даже чуть раньше времени.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-57) },
            new Review { UserId = tourist1.Id, HotelId = hotels[3].Id, Rating = 5, Text = "Очень уютный бутик-отель. Кровати удобные, выспались отлично после долгих экскурсий.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-56) },
            new Review { UserId = tourist2.Id, HotelId = hotels[4].Id, Rating = 5, Text = "Сервис на высоте. В номере есть всё необходимое, кондиционер работал тихо.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-55) },
            new Review { UserId = tourist3.Id, HotelId = hotels[5].Id, Rating = 5, Text = "Стильный интерьер и идеальная чистота. Обязательно вернусь сюда снова.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-54) },
            new Review { UserId = tourist1.Id, HotelId = hotels[6].Id, Rating = 5, Text = "Цена полностью оправдана качеством. Отличная звукоизоляция, соседей вообще не слышали.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-53) },
            new Review { UserId = tourist2.Id, HotelId = hotels[7].Id, Rating = 5, Text = "Прекрасный отель! На ресепшене помогли заказать такси и подсказали крутые рестораны.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-52) },
            new Review { UserId = tourist3.Id, HotelId = hotels[8].Id, Rating = 5, Text = "Шикарная терраса на крыше отеля, провожали там закаты каждый вечер.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-51) },
            new Review { UserId = tourist1.Id, HotelId = hotels[9].Id, Rating = 5, Text = "Всё супер! Чисто, уютно, полотенца меняли каждый день.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-50) },
            new Review { UserId = tourist2.Id, HotelId = hotels[10].Id, Rating = 5, Text = "Порадовал быстрый интернет, спокойно работал из номера по вечерам.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-49) },
            new Review { UserId = tourist3.Id, HotelId = hotels[11].Id, Rating = 5, Text = "Вживую всё выглядит даже лучше, чем на фото. Дизайн очень зацепил.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-48) },
            new Review { UserId = tourist1.Id, HotelId = hotels[12].Id, Rating = 5, Text = "Безупречный сервис, на любую просьбу реагировали мгновенно. Рекомендую!", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-47) },
            new Review { UserId = tourist2.Id, HotelId = hotels[13].Id, Rating = 5, Text = "В номере была идеальная тишина, хотя отель находится на оживленной улице.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-46) },
            new Review { UserId = tourist3.Id, HotelId = hotels[14].Id, Rating = 5, Text = "Свежий ремонт, белоснежное белье, сантехника блестит. 10 из 10.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-45) },
            new Review { UserId = tourist1.Id, HotelId = hotels[15].Id, Rating = 5, Text = "Очень приятное место с домашней атмосферой. Обязательно приедем еще.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-44) },
            new Review { UserId = tourist2.Id, HotelId = hotels[16].Id, Rating = 5, Text = "Локация бомба. Рядом куча классных кафешек и станция метро.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-43) },
            new Review { UserId = tourist3.Id, HotelId = hotels[17].Id, Rating = 5, Text = "Шикарные ортопедические матрасы. Спина после долгих прогулок сказала спасибо.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-42) },
            new Review { UserId = tourist1.Id, HotelId = hotels[18].Id, Rating = 5, Text = "В подарок при заселении принесли бутылку вина, очень милый жест.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-41) },
            new Review { UserId = tourist2.Id, HotelId = hotels[19].Id, Rating = 5, Text = "Всё на высшем уровне, от встречи на входе до выезда из номера.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-40) },
            new Review { UserId = tourist3.Id, HotelId = hotels[20].Id, Rating = 5, Text = "Отель просто шикарный! Номер чистый, просторный, а вид из окна — отдельный восторг.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-39) },
            new Review { UserId = tourist1.Id, HotelId = hotels[21].Id, Rating = 5, Text = "Идеальное расположение в самом центре. До всех главных локаций доходили пешком.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-38) },
            new Review { UserId = tourist2.Id, HotelId = hotels[22].Id, Rating = 5, Text = "Нереально вкусные завтраки! Персонал мега-приветливый, заселили даже чуть раньше времени.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-37) },
            new Review { UserId = tourist3.Id, HotelId = hotels[23].Id, Rating = 5, Text = "Очень уютный бутик-отель. Кровати удобные, выспались отлично после долгих экскурсий.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-36) },
            new Review { UserId = tourist1.Id, HotelId = hotels[24].Id, Rating = 5, Text = "Сервис на высоте. В номере есть всё необходимое, кондиционер работал тихо.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-35) },
            new Review { UserId = tourist2.Id, HotelId = hotels[25].Id, Rating = 5, Text = "Стильный интерьер и идеальная чистота. Обязательно вернусь сюда снова.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-34) },
            new Review { UserId = tourist3.Id, HotelId = hotels[26].Id, Rating = 5, Text = "Цена полностью оправдана качеством. Отличная звукоизоляция, соседей вообще не слышали.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-33) },
            new Review { UserId = tourist1.Id, HotelId = hotels[27].Id, Rating = 5, Text = "Прекрасный отель! На ресепшене помогли заказать такси и подсказали крутые рестораны.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-32) },
            new Review { UserId = tourist2.Id, HotelId = hotels[28].Id, Rating = 5, Text = "Шикарная терраса на крыше отеля, провожали там закаты каждый вечер.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-31) },
            new Review { UserId = tourist3.Id, HotelId = hotels[29].Id, Rating = 5, Text = "Всё супер! Чисто, уютно, полотенца меняли каждый день.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-30) },
            new Review { UserId = tourist1.Id, HotelId = hotels[30].Id, Rating = 5, Text = "Порадовал быстрый интернет, спокойно работал из номера по вечерам.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-29) },
            new Review { UserId = tourist2.Id, HotelId = hotels[31].Id, Rating = 5, Text = "Вживую всё выглядит даже лучше, чем на фото. Дизайн очень зацепил.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-28) },
            new Review { UserId = tourist3.Id, HotelId = hotels[32].Id, Rating = 5, Text = "Безупречный сервис, на любую просьбу реагировали мгновенно. Рекомендую!", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-27) },
            new Review { UserId = tourist1.Id, HotelId = hotels[33].Id, Rating = 5, Text = "В номере была идеальная тишина, хотя отель находится на оживленной улице.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-26) },
            new Review { UserId = tourist2.Id, HotelId = hotels[34].Id, Rating = 5, Text = "Свежий ремонт, белоснежное белье, сантехника блестит. 10 из 10.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-25) },
            new Review { UserId = tourist3.Id, HotelId = hotels[35].Id, Rating = 5, Text = "Очень приятное место с домашней атмосферой. Обязательно приедем еще.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-24) },
            new Review { UserId = tourist1.Id, HotelId = hotels[36].Id, Rating = 5, Text = "Локация бомба. Рядом куча классных кафешек и станция метро.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-23) },
            new Review { UserId = tourist2.Id, HotelId = hotels[37].Id, Rating = 5, Text = "Шикарные ортопедические матрасы. Спина после долгих прогулок сказала спасибо.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-22) },
            new Review { UserId = tourist3.Id, HotelId = hotels[38].Id, Rating = 5, Text = "В подарок при заселении принесли бутылку вина, очень милый жест.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-21) },
            new Review { UserId = tourist1.Id, HotelId = hotels[39].Id, Rating = 5, Text = "Всё на высшем уровне, от встречи на входе до выезда из номера.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-20) },
            new Review { UserId = tourist2.Id, HotelId = hotels[40].Id, Rating = 4, Text = "Отель хороший, но Wi-Fi в номере периодически отваливался.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-19) },
            new Review { UserId = tourist3.Id, HotelId = hotels[41].Id, Rating = 4, Text = "Всё понравилось, но завтраки могли бы быть чуть более разнообразными.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-18) },
            new Review { UserId = tourist1.Id, HotelId = hotels[42].Id, Rating = 4, Text = "Номер классный, правда напор воды в душе вечером был слабоват.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-17) },
            new Review { UserId = tourist2.Id, HotelId = hotels[43].Id, Rating = 4, Text = "Расположение супер, но из-за близости к барам по ночам бывало шумновато.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-16) },
            new Review { UserId = tourist3.Id, HotelId = hotels[44].Id, Rating = 4, Text = "Чисто и уютно, но комната оказалась чуть меньше, чем выглядала на фото.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-15) },
            new Review { UserId = tourist1.Id, HotelId = hotels[45].Id, Rating = 4, Text = "Персонал вежливый, но заселения пришлось ждать лишних полчаса.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-14) },
            new Review { UserId = tourist2.Id, HotelId = hotels[46].Id, Rating = 4, Text = "Хороший отель, но кондиционер дул прямо на кровать, пришлось настраивать.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-13) },
            new Review { UserId = tourist3.Id, HotelId = hotels[47].Id, Rating = 4, Text = "Всё ок, но розетка возле кровати была всего одна, неудобно заряжать гаджеты.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-12) },
            new Review { UserId = tourist1.Id, HotelId = hotels[48].Id, Rating = 4, Text = "Интерьер стильный, но освещение в ванной комнате показалось слишком тусклым.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-11) },
            new Review { UserId = tourist2.Id, HotelId = hotels[49].Id, Rating = 4, Text = "Нормальный вариант на пару ночей, но цена завышена для такого сервиса.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-10) },
            new Review { UserId = tourist3.Id, HotelId = hotels[50].Id, Rating = 4, Text = "Отель хороший, но Wi-Fi в номере периодически отваливался.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-9) },
            new Review { UserId = tourist1.Id, HotelId = hotels[51].Id, Rating = 4, Text = "Всё понравилось, но завтраки могли бы быть чуть более разнообразными.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-8) },
            new Review { UserId = tourist2.Id, HotelId = hotels[52].Id, Rating = 4, Text = "Номер классный, правда напор воды в душе вечером был слабоват.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-7) },
            new Review { UserId = tourist3.Id, HotelId = hotels[53].Id, Rating = 4, Text = "Расположение супер, но из-за близости к барам по ночам бывало шумновато.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-6) },
            new Review { UserId = tourist1.Id, HotelId = hotels[54].Id, Rating = 4, Text = "Чисто и уютно, но комната оказалась чуть меньше, чем выглядала на фото.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-5) },
            new Review { UserId = tourist2.Id, HotelId = hotels[55].Id, Rating = 4, Text = "Персонал вежливый, но заселения пришлось ждать лишних полчаса.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-4) },
            new Review { UserId = tourist3.Id, HotelId = hotels[56].Id, Rating = 4, Text = "Хороший отель, но кондиционер дул прямо на кровать, пришлось настраивать.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-3) },
            new Review { UserId = tourist1.Id, HotelId = hotels[57].Id, Rating = 4, Text = "Всё ок, но розетка возле кровати была всего одна, неудобно заряжать гаджеты.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-2) },
            new Review { UserId = tourist2.Id, HotelId = hotels[58].Id, Rating = 4, Text = "Интерьер стильный, но освещение в ванной комнате показалось слишком тусклым.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-1) },
            new Review { UserId = tourist3.Id, HotelId = hotels[59].Id, Rating = 4, Text = "Нормальный вариант на пару ночей, но цена завышена для такого сервиса.", Status = ReviewStatus.Approved, Date = DateTime.Now },

            // === ДОСТОПРИМЕЧАТЕЛЬНОСТИ (30 отзывов) ===
            new Review { UserId = tourist1.Id, SightId = sights[0].Id, Rating = 5, Text = "Масштабы и красота этого места просто поражают. Обязательно к посещению!", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-28) },
            new Review { UserId = tourist2.Id, SightId = sights[1].Id, Rating = 5, Text = "Потрясающая локация. Энергетика невероятная, сделали миллион красивых фотографий.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-27) },
            new Review { UserId = tourist3.Id, SightId = sights[4].Id, Rating = 5, Text = "Архитектура просто завораживает. Можно часами стоять и рассматривать детали.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-26) },
            new Review { UserId = tourist1.Id, SightId = sights[5].Id, Rating = 5, Text = "Великолепный вид со смотровой площадки! Это определенно стоило увидеть вживую.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-25) },
            new Review { UserId = tourist2.Id, SightId = sights[8].Id, Rating = 5, Text = "Уникальное историческое место. Рад, что взял аудиогид, узнал много интересного.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-24) },
            new Review { UserId = tourist3.Id, SightId = sights[9].Id, Rating = 5, Text = "Атмосфера сказочная, особенно на закате. Прогулка получилась незабываемой.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-23) },
            new Review { UserId = tourist1.Id, SightId = sights[14].Id, Rating = 5, Text = "Локация превзошла все ожидания. Очень ухоженная и красивая территория.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-22) },
            new Review { UserId = tourist2.Id, SightId = sights[15].Id, Rating = 5, Text = "Культовое место, которое должен увидеть каждый путешественник. Впечатляет!", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-21) },
            new Review { UserId = tourist3.Id, SightId = sights[18].Id, Rating = 5, Text = "Очень красивое и фотогеничное пространство, дух захватывает от такой красоты.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-20) },
            new Review { UserId = tourist1.Id, SightId = sights[21].Id, Rating = 5, Text = "Чистый восторг! Одно из самых ярких впечатлений за всю поездку.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-19) },
            new Review { UserId = tourist2.Id, SightId = sights[22].Id, Rating = 5, Text = "Поражает, как древним строителям удалось создать шедевр такого масштаба.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-18) },
            new Review { UserId = tourist3.Id, SightId = sights[25].Id, Rating = 5, Text = "Невероятное слияние природы и истории. Всем советую сюда доехать.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-17) },
            new Review { UserId = tourist1.Id, SightId = sights[28].Id, Rating = 5, Text = "Место, где буквально замирает сердце от красоты. Дух захватывает!", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-16) },
            new Review { UserId = tourist2.Id, SightId = sights[31].Id, Rating = 5, Text = "Локация ухоженная, есть удобные дорожки, скамейки и зоны отдыха.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-15) },
            new Review { UserId = tourist3.Id, SightId = sights[35].Id, Rating = 5, Text = "Впечатления на всю жизнь. Рад, что включил эту точку в свой маршрут.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-14) },
            new Review { UserId = tourist1.Id, SightId = sights[39].Id, Rating = 5, Text = "Прекрасный экскурсионный объект. Подача истории через аудиогид на пятерку.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-13) },
            new Review { UserId = tourist2.Id, SightId = sights[42].Id, Rating = 5, Text = "Абсолютный маст-хэв для посещения. Картинки в интернете не передают этого величия.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-12) },
            new Review { UserId = tourist3.Id, SightId = sights[45].Id, Rating = 5, Text = "Очень атмосферно, погружаешься в совершенно другую эпоху с первых минут.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-11) },
            new Review { UserId = tourist1.Id, SightId = sights[49].Id, Rating = 5, Text = "Потрясающая геометрия линий, фанаты архитектуры будут в полнейшем восторге.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-10) },
            new Review { UserId = tourist2.Id, SightId = sights[52].Id, Rating = 5, Text = "Волшебное место. Провели здесь полдня и не заметили, как пролетело время.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-9) },
            new Review { UserId = tourist3.Id, SightId = sights[55].Id, Rating = 4, Text = "Место невероятное, но людей просто тьма, сложно сделать фото без толпы в кадре.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-8) },
            new Review { UserId = tourist1.Id, SightId = sights[58].Id, Rating = 4, Text = "Очень красиво, но билеты на входе стоят довольно дорого.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-7) },
            new Review { UserId = tourist2.Id, SightId = sights[59].Id, Rating = 4, Text = "Впечатляющий объект, правда очереди на входе растянулись почти на час.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-6) },
            new Review { UserId = tourist3.Id, SightId = sights[60].Id, Rating = 4, Text = "Локация супер, но не хватило указателей — на территории легко заблудиться.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-5) },
            new Review { UserId = tourist1.Id, SightId = sights[62].Id, Rating = 4, Text = "Сама достопримечательность топ, но вокруг слишком много навязчивых зазывал.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-4) },
            new Review { UserId = tourist2.Id, SightId = sights[63].Id, Rating = 4, Text = "Интересный объект, но реставрационные леса немного испортили общий вид.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-3) },
            new Review { UserId = tourist3.Id, SightId = sights[65].Id, Rating = 4, Text = "Красиво, но добираться сюда самостоятельно без машины — тот еще квест.", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-2) },
            new Review { UserId = tourist1.Id, SightId = sights[66].Id, Rating = 4, Text = "Объект исторический, но инфраструктуры не хватает (мало туалетов и палаток с водой).", Status = ReviewStatus.Approved, Date = DateTime.Now.AddDays(-1) },
            new Review { UserId = tourist2.Id, SightId = sights[2].Id, Rating = 4, Text = "Локация отличная, но советую приходить рано утром, днем жара невыносимая.", Status = ReviewStatus.Approved, Date = DateTime.Now },
            new Review { UserId = tourist3.Id, SightId = sights[3].Id, Rating = 4, Text = "Впечатляет, но на осмотр хватает буквально 20 минут, ехать издалека ради этого не стоит.", Status = ReviewStatus.Approved, Date = DateTime.Now },
        };

        context.Reviews.AddRange(reviews);
        context.SaveChanges();

        // =====================
        // БРОНИРОВАНИЯ
        // =====================
        var bookings = new List<Booking>
        {
            new Booking { TourId = tours[0].Id, UserId = tourist1.Id, BookingDate = DateTime.Now.AddDays(14), GuestsCount = 2, TotalPrice = 144000, Status = BookingStatus.Confirmed, Phone = "+375 (29) 111-22-33", Email = "ivan@test.com", Notes = "Хотим соседние номера", CreatedAt = DateTime.Now.AddDays(-25) },
            new Booking { TourId = tours[2].Id, UserId = tourist2.Id, BookingDate = DateTime.Now.AddDays(21), GuestsCount = 4, TotalPrice = 480000, Status = BookingStatus.New, Phone = "+375 (33) 333-44-55", Email = "maria@test.com", Notes = "Семья с детьми 8 и 12 лет", CreatedAt = DateTime.Now.AddDays(-18) },
            new Booking { TourId = tours[6].Id, UserId = tourist3.Id, BookingDate = DateTime.Now.AddDays(30), GuestsCount = 2, TotalPrice = 136000, Status = BookingStatus.New, Phone = "+375 (29) 555-66-77", Email = "alex@test.com", CreatedAt = DateTime.Now.AddDays(-5) },
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
            new Message { ChatId = chat1.Id, SenderId = tourist1.Id, Text = "И ещё, можно ли забронировать экскурсию заранее?", Timestamp = DateTime.Now.AddDays(-5).AddMinutes(1), IsRead = true },
            new Message { ChatId = chat2.Id, SenderId = tourist2.Id, Text = "Добрый день! Хотела бы узнать про скидки для детей.", Timestamp = DateTime.Now.AddDays(-2), IsRead = true },
        };

        context.Messages.AddRange(messages);
        context.SaveChanges();

        Console.WriteLine("База данных заполнена начальными данными!");
        Console.WriteLine("  - 20 стран, 21 город, 3 агентства");
        Console.WriteLine("  - 60 отелей, 68 достопримечательностей");
        Console.WriteLine("  - 20 туров");
        Console.WriteLine("  - 3 пользователя (tourist/test)");
        Console.WriteLine("  - 136 отзывов, 3 бронирования, 7 избранных");
        Console.WriteLine("  - 2 чата, 3 сообщения");
    }
}
