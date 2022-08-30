using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;
using System.Drawing;
using WebApplication6.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;



        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db= context;
            _userManager= userManager;
            _signInManager= signInManager;

            if (db.Subcategories.Count() == 0)
            {
                List<Category> categories = new List<Category>()
                {
                    new Category { Name = "Комплектующие" },
                    new Category { Name = "Компьютерная периферия" },
                    new Category { Name = "Смартфоны / гаджеты" },
                    new Category { Name = "Компьютеры и ноутбуки" },


                };
                foreach (var category in categories)
                {
                    db.Categories.Add(category);
                }

                List<Subcategory> subcategories = new List<Subcategory>()
                {
                    //Комплектующие
                    new Subcategory { Name = "Процессоры", CategoryId = 1 },
                    new Subcategory { Name = "Материнские платы", CategoryId = 1 },
                    new Subcategory { Name = "Оперативная память", CategoryId = 1 },
                    new Subcategory { Name = "Видеокарты", CategoryId = 1 },
                    new Subcategory { Name = "Жесткие диски", CategoryId = 1 },
                    new Subcategory { Name = "SSD диски", CategoryId = 1 },
                    new Subcategory { Name = "Блоки питания", CategoryId = 1 },
                    new Subcategory { Name = "Корпуса", CategoryId = 1 },

                    //Компьютерная периферия
                    new Subcategory { Name = "Мониторы", CategoryId = 2 },                   
                    new Subcategory { Name = "Клавиатуры", CategoryId = 2 },
                    new Subcategory { Name = "Мыши", CategoryId = 2 },
                    new Subcategory { Name = "Коврики для мышей", CategoryId = 2 },
                    new Subcategory { Name = "Наушники и гарнитуры", CategoryId = 2 },
                    new Subcategory { Name = "Микрофоны", CategoryId = 2 },
                    new Subcategory { Name = "Геймпады, джойстики", CategoryId = 2 },
                    new Subcategory { Name = "Веб-камеры", CategoryId = 2 },

                    //Смартфоны / гаджеты
                    new Subcategory { Name = "Смартфоны", CategoryId = 3 },
                    new Subcategory { Name = "Гаджеты", CategoryId = 3 },
                    new Subcategory { Name = "Аксессуары", CategoryId = 3 },

                    //Компьютеры и ноутбуки
                    new Subcategory { Name = "Настольные компьютеры", CategoryId = 4 },
                    new Subcategory { Name = "Ноутбуки", CategoryId = 4 },
                    new Subcategory { Name = "Неттопы", CategoryId = 4 },
                    new Subcategory { Name = "Моноблоки", CategoryId = 4 },
                    

                };
                
                foreach (var subcategory in subcategories)
                {
                    
                    db.Subcategories.Add(subcategory);
                    db.SaveChanges();

                }

                List<Product> products = new List<Product>()
                {
                    new Product { Name = "RTX 2060", Description="Производитель - Palit", Amount=11, Price=260000, DiscountPercentage=0, SubcategoryId=4, TotalPrice=260000, Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/image.webp") },
                    new Product {Name="Видеокарта MSI RX 6500 XT Mech 2X, 4 GB, Radeon RX 6500 XT", Amount=6,Price=150000,DiscountPercentage=5,SubcategoryId=4,TotalPrice=142500 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/161714_1.jpg"),Description="Производитель  MSI /Модель  RX 6500 XT Mech 2X /Серия видеокарты  Radeon 6000 Series /Производитель GPU  AMD /Модель чипсета  Radeon RX 6500 XT /Техпроцесс  Подробнее 7 нм /Частота видеопроцессора OC Mode  Подробнее 2815 МГц /Частота видеопроцессора Gaming Mode  Подробнее 2610 МГц /Частота видеопамяти, МГц  Подробнее 18000 /Тип видеопамяти  Подробнее GDDR6 /Объем видеопамяти  Подробнее 4 ГБ /Разрядность шины видеопамяти  Подробнее 64 бит /Пропускная способность памяти, Гбайт/с  Подробнее 144 /Количество универсальных процессоров  Подробнее 1024 /Технологии  Подробнее AMD FreeSync, Radeon Anti-Lag, HDMI, VR Ready /Поддерживаемые API  Подробнее DirectX 12 Ultimate, OpenGL 4.6, Vulkan API /Поддержка Multi-GPU  Подробнее Не поддерживает /Интерфейс подключения  Подробнее PCI Express 4.0 /Количество поддерживаемых мониторов  2 /Разъемы  Подробнее HDMI, DisplayPort /Максимальное разрешение  Подробнее 7680 x 4320 (при подключении через DisplayPort) /Охлаждение  Подробнее Активное, 2 вентилятора, Тепловые трубки /Разъемы питания  Подробнее 6-pin /Минимальная мощность блока питания, не менее  Подробнее 400 Вт /Количество занимаемых слотов расширения  Подробнее 2 /Дополнительно  Микро-архитектура RDNA 2 /Вентиляторы TORX 3.0 c дисперсионными лопастями /Прочная усилительная пластина /Разгонная утилита Afterburner /Длина видеокарты  Подробнее 17.2 см /Размер упаковки (Ш х В х Г)  30.8 х 22.4 х 7.1 см /Вес с упаковкой  0.81 кг /", },
                     new Product {Name="Видеокарта PowerColor RX 6600 Fighter, 8 GB, Radeon RX 6600", Amount=3,Price=320000,DiscountPercentage=6,SubcategoryId=4,TotalPrice=299990 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/45645.png"),Description="Производитель  PowerColor /Модель  RX 6600 Fighter /Серия видеокарты  Radeon 6000 Series /Производитель GPU  AMD /Модель чипсета  Radeon RX 6600 /Техпроцесс  Подробнее 7 нм /Частота видеопроцессора OC Mode  Подробнее 2491 МГц /Частота видеопроцессора Gaming Mode  Подробнее 2044 МГц /Частота видеопамяти, МГц  Подробнее 14000 /Тип видеопамяти  Подробнее GDDR6 /Объем видеопамяти  Подробнее 8 ГБ /Разрядность шины видеопамяти  Подробнее 128 бит /Пропускная способность памяти, Гбайт/с  Подробнее 224 /Количество универсальных процессоров  Подробнее 1792 /Технологии  Подробнее AMD Eyefinity, AMD FreeSync, AMD Fidelity FX, Radeon Anti-Lag, HDMI, Ray Tracing /Поддерживаемые API  Подробнее DirectX 1‎‎2 Ultimate, Vulkan API /Поддержка Multi-GPU  Подробнее AMD CrossFire /Интерфейс подключения  Подробнее PCI Express 4.0 /Количество поддерживаемых мониторов  4 /Разъемы  Подробнее HDMI, DisplayPort x 3 /Максимальное разрешение  Подробнее 7680 x 4320 (при подключении через DisplayPort) /Охлаждение  Подробнее Активное, Автоматический пассивный режим, 2 вентилятора, Тепловые трубки /Разъемы питания  Подробнее 8-pin /Минимальная мощность блока питания, не менее  Подробнее 500 Вт /Количество занимаемых слотов расширения  Подробнее 2 /Дополнительно  Микроархитектура RDNA 2 /Вентиляторы с двойными шарикоподшипниками /Технология пассивного охлаждения Mute Fan /Длина видеокарты  Подробне 20.5 см /", },
                     new Product {Name="Видеокарта MSI RTX 3070 Gaming Trio Plus LHR, 8 GB", Amount=12,Price=479990,DiscountPercentage=17,SubcategoryId=4,TotalPrice=399990 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/160992_1.jpg"),Description="Производитель  MSI /Модель  RTX 3070 Gaming Trio Plus LHR /Серия видеокарты  GeForce 30 Series /Производитель GPU  NVIDIA /Модель чипсета  GeForce RTX 3070 /Техпроцесс  Подробнее 8 нм /Частота видеопроцессора OC Mode  Подробнее 1770 МГц /Частота видеопамяти, МГц  Подробнее 14000 /Тип видеопамяти  Подробнее GDDR6 /Объем видеопамяти  Подробнее 8 ГБ /Разрядность шины видеопамяти  Подробнее 256 бит /Пропускная способность памяти, Гбайт/с  Подробнее 448 /Количество универсальных процессоров  Подробнее 5888 /Технологии  Подробнее NVIDIA Ansel, NVIDIA CUDA, NVIDIA G-Sync, NVIDIA PhysX, NVIDIA DLSS, HDCP, HDMI, VR Ready, Ray Tracing  /Поддерживаемые API  Подробнее DirectX 1‎‎2 Ultimate, OpenGL 4.6, Vulkan API /Поддержка Multi-GPU  Подробнее Не поддерживает /Lite Hash Rate (LHR)  Подробнее Да /Интерфейс подключения  Подробнее PCI Express 4.0 /Количество поддерживаемых мониторов  4 /Разъемы  Подробнее HDMI, DisplayPort x 3 /Максимальное разрешение  Подробнее 7680 x 4320 (при подключении через DisplayPort) /Охлаждение  Подробнее Активное, 3 вентилятора /Разъемы питания  Подробнее 2 х 8-pin /Подсветка  Есть /Минимальная мощность блока питания, не менее  Подробнее 650 Вт /Количество занимаемых слотов расширения  Подробнее 3 /Дополнительно  Микроархитектура NVIDIA Ampere /Второе поколение RT ядер /Третье поколение тензорных ядер /Система охлаждения Tri Frozr 2 /Обновленные вентиляторы TORX Fan 4.0 /Технология Airflow Control - управление воздушным потоком /Технология Zero Frozr: под низкими нагрузками вентиляторы останавливаются, делая видеокарту бесшумной /Полноцветная подсветка RGB Mystic Light /Приложение Dragon Center для мониторинга и настройки /Длина видеокарты  Подробнее 32.4 см /Размер упаковки (Ш х В х Г)  40.5 х 29.7 х 9 см /Вес с упаковкой  2.29 кг /", },
                     new Product {Name="Видеокарта Gigabyte RTX 3080 Gaming OC LHR, 12 GB", Amount=7,Price=629990,DiscountPercentage=6,SubcategoryId=4,TotalPrice=599990 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/160948x1.jpg"),Description="Производитель  Gigabyte / Модель  RTX 3080 Gaming OC LHR / Серия видеокарты  GeForce 30 Series / Производитель GPU  NVIDIA / Модель чипсета  GeForce RTX 3080 / Техпроцесс  Подробнее 8 нм / Частота видеопроцессора OC Mode  Подробнее 1755 МГц / Частота видеопамяти, МГц  Подробнее 19000 / Тип видеопамяти  Подробнее GDDR6X / Объем видеопамяти  Подробнее 12 ГБ / Разрядность шины видеопамяти  Подробнее 384 бит / Пропускная способность памяти, Гбайт/с  Подробнее 912 / Количество универсальных процессоров  Подробнее 8960 / Технологии  Подробнее NVIDIA Ansel, NVIDIA CUDA, NVIDIA G-Sync, NVIDIA DLSS, HDCP, HDMI, VR Ready / Поддерживаемые API  Подробнее DirectX 1‎‎2 Ultimate, OpenGL 4.6, Vulkan API / Поддержка Multi-GPU  Подробнее Не поддерживает / Lite Hash Rate (LHR)  Подробнее Да / Интерфейс подключения  Подробнее PCI Express 4.0 / Количество поддерживаемых мониторов  4 / Разъемы  Подробнее HDMI x 2, DisplayPort x 3 / Максимальное разрешение  Подробнее 7680 x 4320 (при подключении через DisplayPort) / Охлаждение  Подробнее Активное, Автоматический пассивный режим, 3 вентилятора, Тепловые трубки / Разъемы питания  Подробнее 2 х 8-pin / Подсветка  Есть / Минимальная мощность блока питания, не менее  Подробнее 750 Вт / ", },
                     new Product {Name="Видеокарта PCI-E 8Gb Palit RTX 3070 JetStream LHR", Amount=2,Price=439900,DiscountPercentage=9,SubcategoryId=4,TotalPrice=399900 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/159020_01.jpg"),Description="Производитель  Palit /Модель  RTX 3070 JetStream LHR /Серия видеокарты  GeForce 30 Series /Производитель GPU  NVIDIA /Модель чипсета  GeForce RTX 3070 /Техпроцесс  Подробнее 8 нм /Частота видеопроцессора OC Mode  Подробнее 1725 МГц /Частота видеопроцессора Gaming Mode  Подробнее 1500 МГц /Частота видеопамяти, МГц  Подробнее 14000 /Тип видеопамяти  Подробнее GDDR6 /Объем видеопамяти  Подробнее 8 ГБ /Разрядность шины видеопамяти  Подробнее 256 бит /Пропускная способность памяти, Гбайт/с  Подробнее 448 /Количество универсальных процессоров  Подробнее 5888 /Технологии  Подробнее NVIDIA Ansel, NVIDIA CUDA, NVIDIA G-Sync, NVIDIA PhysX, NVIDIA DLSS, HDCP, HDMI, VR Ready, Ray Tracing /Поддерживаемые API  Подробнее DirectX 1‎‎2 Ultimate, OpenGL 4.6, Vulkan API /Поддержка Multi-GPU  Подробнее Не поддерживает /Lite Hash Rate (LHR)  Подробнее Да /Интерфейс подключения  Подробнее PCI Express 4.0 /Количество поддерживаемых мониторов  4 /Разъемы  Подробнее HDMI, DisplayPort x 3 /Максимальное разрешение  Подробнее 7680 x 4320 (при подключении через DisplayPort) /Охлаждение  Подробнее Активное, Автоматический пассивный режим, 2 вентилятора, Тепловые трубки /Разъемы питания  Подробнее 2 х 8-pin /Подсветка  Есть /Минимальная мощность блока питания, не менее  Подробнее 650 Вт /Количество занимаемых слотов расширения  Подробнее 2.7 /", },
                     new Product {Name="Видеокарта PCI-E 6Gb ASUS GTX 1660 Ti TUF Gaming EVO TOP Edition", Amount=15,Price=234900,DiscountPercentage=6,SubcategoryId=4,TotalPrice=218790 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/158253_1_min.jpg"),Description="Производитель  ASUS / Модель  GTX 1660 Ti TUF Gaming EVO TOP Edition / Серия видеокарты  GeForce 16 Series / Производитель GPU  NVIDIA / Модель чипсета  GeForce GTX 1660 Ti / Техпроцесс  Подробнее 12 нм / Частота видеопроцессора OC Mode  Подробнее 1845 МГц / Частота видеопроцессора Gaming Mode  Подробнее 1815 МГц / Частота видеопамяти, МГц  Подробнее 14000 / Тип видеопамяти  Подробнее GDDR6 / Объем видеопамяти  Подробнее 6 ГБ / Разрядность шины видеопамяти  Подробнее 192 бит / Пропускная способность памяти, Гбайт/с  Подробнее 336 / Количество универсальных процессоров  Подробнее 1536 / Технологии  Подробнее NVIDIA Ansel, NVIDIA CUDA, NVIDIA G-Sync, NVIDIA PhysX, HDCP, HDMI, VR Ready / Поддерживаемые API  Подробнее DirectX 12.0, OpenGL 4.6 / Поддержка Multi-GPU  Подробнее Не поддерживает / Интерфейс подключения  Подробнее PCI Express 3.0 / Количество поддерживаемых мониторов  4 / Разъемы  Подробнее DVI-D, HDMI x 2, DisplayPort / Максимальное разрешение  Подробнее 7680 x 4320 (при подключении через DisplayPort) / Охлаждение  Подробнее Активное, Автоматический пассивный режим, 2 вентилятора, Тепловые трубки / Разъемы питания  Подробнее 8-pin / Минимальная мощность блока питания, не менее  Подробнее 450 Вт / Количество занимаемых слотов расширения  Подробнее 2.3 / ", },
                     new Product {Name="Видеокарта PCI-E 10Gb Gigabyte RTX 3080 Turbo 2.0", Amount=3,Price=729900,DiscountPercentage=4,SubcategoryId=4,TotalPrice=699990 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/158031_1.jpg"),Description="Производитель  Gigabyte / Модель  RTX 3080 Turbo 2.0 /Серия видеокарты  GeForce 30 Series /Производитель GPU  NVIDIA /Модель чипсета  GeForce RTX 3080 /Техпроцесс  Подробнее 8 нм /Частота видеопроцессора OC Mode  Подробнее 1710 МГц /Частота видеопамяти, МГц  Подробнее 19000 /Тип видеопамяти  Подробнее GDDR6X /Объем видеопамяти  Подробнее 10 ГБ /Разрядность шины видеопамяти  Подробнее 320 бит /Пропускная способность памяти, Гбайт/с  Подробнее 760 /Количество универсальных процессоров  Подробнее 8704 /Технологии  Подробнее NVIDIA Ansel, NVIDIA CUDA, NVIDIA G-Sync, NVIDIA DLSS, HDCP, HDMI, VR Ready, Ray Tracing /Поддерживаемые API  Подробнее DirectX 1‎‎2 Ultimate, OpenGL 4.6 /Поддержка Multi-GPU  Подробнее Не поддерживает /Lite Hash Rate (LHR)  Подробнее Да /Интерфейс подключения  Подробнее PCI Express 4.0 /Количество поддерживаемых мониторов  4 /Разъемы  Подробнее HDMI x 2, DisplayPort x 2 /Максимальное разрешение  Подробнее 7680 x 4320 (при подключении через DisplayPort) /Охлаждение  Подробнее Активное, Автоматический пассивный режим, 1 вентилятор /Разъемы питания  Подробнее 2 х 8-pin /Минимальная мощность блока питания, не менее  Подробнее 750 Вт /Количество занимаемых слотов расширения  Подробнее 2 /", },
                     new Product {Name="23.8 Монитор Samsung F24T350FHI серый", Amount=17,Price=79990,DiscountPercentage=0,SubcategoryId=9,TotalPrice=79990 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/Monitors/8695416d217d3caca9cdc57f2642b5d2d9954598cf9f2239265d8265d88de37c.jpg.webp"),Description="Изогнутый экран нет /Диагональ экрана (дюйм) 23.8 /Максимальное разрешение 1920x1080 /Тип подсветки матрицы LED /Технология изготовления матрицы IPS /Соотношение сторон 16:9 / Сенсорный экран нет / Покрытие экрана матовое / Поддержка HDR нет / Технология защиты зрения есть / ", },
                     new Product {Name="Монитор Acer Nitro QG271bii черный", Amount=13,Price=103990,DiscountPercentage=0,SubcategoryId=9,TotalPrice=103990 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/Monitors/eeb9ddcb5319e74b137bedcea8cdc2e55f25e5c7e5f0d10be51ed1c26a28bce7.jpg.webp"),Description="Размер видимой области экрана 597.72 x 336.22 мм /Яркость 300 Кд/м² /Контрастность 3000 : 1 /Динамическая контрастность 100М:1 /Время отклика (GtG)1 мс /Время отклика пикселя 1 мс /Угол обзора по вертикали (градус) 178° /Угол обзора по горизонтали (градус) 178° /Технология динамического обновления экрана AMD FreeSync /Плотность пикселей 81.5 ppi /Частота при максимальном разрешении 75 Гц /Максимальная частота обновления экрана 75 Гц /Глубина цвета 8bit / ", },
                     new Product {Name="23.8 Монитор Samsung F24T350FHI серый", Amount=17,Price=79990,DiscountPercentage=0,SubcategoryId=9,TotalPrice=79990 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/Monitors/8695416d217d3caca9cdc57f2642b5d2d9954598cf9f2239265d8265d88de37c.jpg.webp"),Description="Изогнутый экран нет /Диагональ экрана (дюйм) 23.8 /Максимальное разрешение 1920x1080 /Тип подсветки матрицы LED /Технология изготовления матрицы IPS /Соотношение сторон 16:9 / Сенсорный экран нет / Покрытие экрана матовое / Поддержка HDR нет / Технология защиты зрения есть / ", },
                     new Product {Name="Монитор Acer Nitro QG271bii черный", Amount=13,Price=103990,DiscountPercentage=0,SubcategoryId=9,TotalPrice=103990 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/Monitors/eeb9ddcb5319e74b137bedcea8cdc2e55f25e5c7e5f0d10be51ed1c26a28bce7.jpg.webp"),Description="Размер видимой области экрана 597.72 x 336.22 мм /Яркость 300 Кд/м² /Контрастность 3000 : 1 /Динамическая контрастность 100М:1 /Время отклика (GtG)1 мс /Время отклика пикселя 1 мс /Угол обзора по вертикали (градус) 178° /Угол обзора по горизонтали (градус) 178° /Технология динамического обновления экрана AMD FreeSync /Плотность пикселей 81.5 ppi /Частота при максимальном разрешении 75 Гц /Максимальная частота обновления экрана 75 Гц /Глубина цвета 8bit / ", },
                     new Product {Name="Видеокарта PowerColor RX 6600 Fighter, 8 GB, Radeon RX 6600", Amount=3,Price=320000,DiscountPercentage=6,SubcategoryId=4,TotalPrice=299990 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/45645.png"),Description="Производитель  PowerColor /Модель  RX 6600 Fighter /Серия видеокарты  Radeon 6000 Series /Производитель GPU  AMD /Модель чипсета  Radeon RX 6600 /Техпроцесс  Подробнее 7 нм /Частота видеопроцессора OC Mode  Подробнее 2491 МГц /Частота видеопроцессора Gaming Mode  Подробнее 2044 МГц /Частота видеопамяти, МГц  Подробнее 14000 /Тип видеопамяти  Подробнее GDDR6 /Объем видеопамяти  Подробнее 8 ГБ /Разрядность шины видеопамяти  Подробнее 128 бит /Пропускная способность памяти, Гбайт/с  Подробнее 224 /Количество универсальных процессоров  Подробнее 1792 /Технологии  Подробнее AMD Eyefinity, AMD FreeSync, AMD Fidelity FX, Radeon Anti-Lag, HDMI, Ray Tracing /Поддерживаемые API  Подробнее DirectX 1‎‎2 Ultimate, Vulkan API /Поддержка Multi-GPU  Подробнее AMD CrossFire /Интерфейс подключения  Подробнее PCI Express 4.0 /Количество поддерживаемых мониторов  4 /Разъемы  Подробнее HDMI, DisplayPort x 3 /Максимальное разрешение  Подробнее 7680 x 4320 (при подключении через DisplayPort) /Охлаждение  Подробнее Активное, Автоматический пассивный режим, 2 вентилятора, Тепловые трубки /Разъемы питания  Подробнее 8-pin /Минимальная мощность блока питания, не менее  Подробнее 500 Вт /Количество занимаемых слотов расширения  Подробнее 2 /Дополнительно  Микроархитектура RDNA 2 /Вентиляторы с двойными шарикоподшипниками /Технология пассивного охлаждения Mute Fan /Длина видеокарты  Подробне 20.5 см /", },
                     new Product {Name="Видеокарта MSI RTX 3070 Gaming Trio Plus LHR, 8 GB", Amount=12,Price=479990,DiscountPercentage=17,SubcategoryId=4,TotalPrice=399990 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/160992_1.jpg"),Description="Производитель  MSI /Модель  RTX 3070 Gaming Trio Plus LHR /Серия видеокарты  GeForce 30 Series /Производитель GPU  NVIDIA /Модель чипсета  GeForce RTX 3070 /Техпроцесс  Подробнее 8 нм /Частота видеопроцессора OC Mode  Подробнее 1770 МГц /Частота видеопамяти, МГц  Подробнее 14000 /Тип видеопамяти  Подробнее GDDR6 /Объем видеопамяти  Подробнее 8 ГБ /Разрядность шины видеопамяти  Подробнее 256 бит /Пропускная способность памяти, Гбайт/с  Подробнее 448 /Количество универсальных процессоров  Подробнее 5888 /Технологии  Подробнее NVIDIA Ansel, NVIDIA CUDA, NVIDIA G-Sync, NVIDIA PhysX, NVIDIA DLSS, HDCP, HDMI, VR Ready, Ray Tracing  /Поддерживаемые API  Подробнее DirectX 1‎‎2 Ultimate, OpenGL 4.6, Vulkan API /Поддержка Multi-GPU  Подробнее Не поддерживает /Lite Hash Rate (LHR)  Подробнее Да /Интерфейс подключения  Подробнее PCI Express 4.0 /Количество поддерживаемых мониторов  4 /Разъемы  Подробнее HDMI, DisplayPort x 3 /Максимальное разрешение  Подробнее 7680 x 4320 (при подключении через DisplayPort) /Охлаждение  Подробнее Активное, 3 вентилятора /Разъемы питания  Подробнее 2 х 8-pin /Подсветка  Есть /Минимальная мощность блока питания, не менее  Подробнее 650 Вт /Количество занимаемых слотов расширения  Подробнее 3 /Дополнительно  Микроархитектура NVIDIA Ampere /Второе поколение RT ядер /Третье поколение тензорных ядер /Система охлаждения Tri Frozr 2 /Обновленные вентиляторы TORX Fan 4.0 /Технология Airflow Control - управление воздушным потоком /Технология Zero Frozr: под низкими нагрузками вентиляторы останавливаются, делая видеокарту бесшумной /Полноцветная подсветка RGB Mystic Light /Приложение Dragon Center для мониторинга и настройки /Длина видеокарты  Подробнее 32.4 см /Размер упаковки (Ш х В х Г)  40.5 х 29.7 х 9 см /Вес с упаковкой  2.29 кг /", },
                     new Product {Name="Видеокарта Gigabyte RTX 3080 Gaming OC LHR, 12 GB", Amount=7,Price=629990,DiscountPercentage=6,SubcategoryId=4,TotalPrice=599990 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/160948x1.jpg"),Description="Производитель  Gigabyte / Модель  RTX 3080 Gaming OC LHR / Серия видеокарты  GeForce 30 Series / Производитель GPU  NVIDIA / Модель чипсета  GeForce RTX 3080 / Техпроцесс  Подробнее 8 нм / Частота видеопроцессора OC Mode  Подробнее 1755 МГц / Частота видеопамяти, МГц  Подробнее 19000 / Тип видеопамяти  Подробнее GDDR6X / Объем видеопамяти  Подробнее 12 ГБ / Разрядность шины видеопамяти  Подробнее 384 бит / Пропускная способность памяти, Гбайт/с  Подробнее 912 / Количество универсальных процессоров  Подробнее 8960 / Технологии  Подробнее NVIDIA Ansel, NVIDIA CUDA, NVIDIA G-Sync, NVIDIA DLSS, HDCP, HDMI, VR Ready / Поддерживаемые API  Подробнее DirectX 1‎‎2 Ultimate, OpenGL 4.6, Vulkan API / Поддержка Multi-GPU  Подробнее Не поддерживает / Lite Hash Rate (LHR)  Подробнее Да / Интерфейс подключения  Подробнее PCI Express 4.0 / Количество поддерживаемых мониторов  4 / Разъемы  Подробнее HDMI x 2, DisplayPort x 3 / Максимальное разрешение  Подробнее 7680 x 4320 (при подключении через DisplayPort) / Охлаждение  Подробнее Активное, Автоматический пассивный режим, 3 вентилятора, Тепловые трубки / Разъемы питания  Подробнее 2 х 8-pin / Подсветка  Есть / Минимальная мощность блока питания, не менее  Подробнее 750 Вт / ", },
                     new Product {Name="Видеокарта PCI-E 8Gb Palit RTX 3070 JetStream LHR", Amount=2,Price=439900,DiscountPercentage=9,SubcategoryId=4,TotalPrice=399900 , Image=ConvertImageToByte.ConvertImage("wwwroot/files/productImages/VideoCards/159020_01.jpg"),Description="Производитель  Palit /Модель  RTX 3070 JetStream LHR /Серия видеокарты  GeForce 30 Series /Производитель GPU  NVIDIA /Модель чипсета  GeForce RTX 3070 /Техпроцесс  Подробнее 8 нм /Частота видеопроцессора OC Mode  Подробнее 1725 МГц /Частота видеопроцессора Gaming Mode  Подробнее 1500 МГц /Частота видеопамяти, МГц  Подробнее 14000 /Тип видеопамяти  Подробнее GDDR6 /Объем видеопамяти  Подробнее 8 ГБ /Разрядность шины видеопамяти  Подробнее 256 бит /Пропускная способность памяти, Гбайт/с  Подробнее 448 /Количество универсальных процессоров  Подробнее 5888 /Технологии  Подробнее NVIDIA Ansel, NVIDIA CUDA, NVIDIA G-Sync, NVIDIA PhysX, NVIDIA DLSS, HDCP, HDMI, VR Ready, Ray Tracing /Поддерживаемые API  Подробнее DirectX 1‎‎2 Ultimate, OpenGL 4.6, Vulkan API /Поддержка Multi-GPU  Подробнее Не поддерживает /Lite Hash Rate (LHR)  Подробнее Да /Интерфейс подключения  Подробнее PCI Express 4.0 /Количество поддерживаемых мониторов  4 /Разъемы  Подробнее HDMI, DisplayPort x 3 /Максимальное разрешение  Подробнее 7680 x 4320 (при подключении через DisplayPort) /Охлаждение  Подробнее Активное, Автоматический пассивный режим, 2 вентилятора, Тепловые трубки /Разъемы питания  Подробнее 2 х 8-pin /Подсветка  Есть /Минимальная мощность блока питания, не менее  Подробнее 650 Вт /Количество занимаемых слотов расширения  Подробнее 2.7 /", },

                };
                
                
                foreach (var product in products)
                {
                    db.Products.Add(product);
                }
                List<News> newsList = new List<News>()
                {
                    new News{ Name="Распродажа видеокарт", Description="С 18 июня по 25 июня действуют скидки на линейку видеокарт Nvidia Geforce 20-й серии!", PublicationDate= DateTime.Now, Image=ConvertImageToByte.ConvertImage("wwwroot/files/newsImages/255587_O.png")},
                    new News{ Name="Распродажа продукции HyperX", Description="С 11 июня по 25 июня действуют скидки на все игровые девайсы от производителя HyperX!", PublicationDate= DateTime.Now, Image=ConvertImageToByte.ConvertImage("wwwroot/files/newsImages/hyperx-780x439.jpg")},
                    new News{ Name="Новая линейка видеокарт Nvidia 30-й серии", Description="В нашем магазине появилась новая серия видеокарт от Nvidia! ", PublicationDate= DateTime.Now, Image=ConvertImageToByte.ConvertImage("wwwroot/files/newsImages/nvidia-geforce-rtx-3080-ti-photo-001-1030x557_large.png")},
                };
                foreach (var news in newsList)
                {
                    db.NewsList.Add(news);
                }














                db.SaveChanges();
            }

        }

        /*<summary>
         * Данный экшен отвечает за наполнение класса "multiClass" списком из 6 продуктов с наибольшей скидкой,
         * так же наполняется 3-мя последними добавленными новостями с последующей передачей в представление для отображения контента
        </summary>*/
        [HttpGet]
        public IActionResult HomePage()
        {
            MultiClass multiClass = new MultiClass();
            List<Product> products= db.Products.ToList();
            multiClass.products = products.Where(p=>p.DiscountPercentage!=0).OrderByDescending(p => p.DiscountPercentage).Take(6).ToList();
            List<News> newsList = db.NewsList.ToList();

            multiClass.newsList = newsList.TakeLast(3).ToList();

            return View(multiClass);
        }
        /*<summary>
         * Контроллер принимает значение поисковой строки-"searchString" и перенаправляет в экшен-"Index" с передачей значения поисковой строки 
       </summary>*/
        [HttpPost]
        public IActionResult HomePage(string searchString)
        {
            

            return RedirectToAction("Index","Home",searchString);
        }
        /*<summary>
         * Экшен Index принимает значение поисковой строки-"searchString", Id категории-"categoryId" и подкатегории-"subcategoryId", номер страницы-"page".
         * Он подготавливает список продуктов на странице "Index" в соответствии с переданными параметрами, 
       </summary>*/
        public IActionResult Index(string searchString, int categoryId,int subcategoryId, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            
            //Заполнение класса данными из базы данных для последующих операций сортировки и выборки
            MultiClass multiClass = new MultiClass();
            multiClass.products = db.Products.ToList();
            multiClass.subcategories = db.Subcategories.ToList();
            multiClass.categories = db.Categories.ToList();
            //Переменная для отображения на странице выбранной категории\подкатегории
            string categoryName="Каталог";
            IndexViewModel viewModel = new IndexViewModel();
            //Проверка наличия выбранной категории
            if (categoryId!=0)
            {
                viewModel.categoryId = categoryId;
                //Присвоение переменной названия выбранной категории
                categoryName=multiClass.categories.Where(c=>c.Id==categoryId).First().Name;
                //Проверка наличия выбранной подкатегории
                if (subcategoryId != 0)
                {
                    viewModel.subcategoryId=subcategoryId;
                    //Присвоение переменной названия выбранной подкатегории
                    categoryName = multiClass.subcategories.Where(s => s.Id == subcategoryId).First().Name;
                    //Заполнение списка продуктов из выбранной подкатегории
                    multiClass.products = multiClass.products.Where(p => p.SubcategoryId == subcategoryId).ToList();
                    
                }
                else
                {
                    //Список подкатегорий находящихся в категории
                    List<Subcategory> subcategories = multiClass.subcategories.Where(s => s.CategoryId == categoryId).ToList();
                    //Промежуточная переменная для хранения списка продуктов
                    List<Product> products = new List<Product>();
                    //Заполнение списка продуктов находящихся в категории
                    foreach (Subcategory sub in subcategories)
                    {
                        products = products.Union(multiClass.products.Where(p => p.SubcategoryId == sub.Id).ToList()).ToList();
                    }
                    multiClass.products = products;
                }
               
            }
            //Проверка переменной "searchString" на пустую строку, если строка не пустая подготавливается список продуктов, в названии которых
            //входит значение поисковой строки
            if (!String.IsNullOrEmpty(searchString))
            {
                multiClass.products = multiClass.products.Where(p=>p.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
            }
            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    multiClass.products = multiClass.products.OrderByDescending(s => s.Name).ToList();
                    break;
                case SortState.PriceAsc:
                    multiClass.products = multiClass.products.OrderBy(s => s.TotalPrice).ToList();
                    break;
                case SortState.PriceDesc:
                    multiClass.products = multiClass.products.OrderByDescending(s => s.TotalPrice).ToList();
                    break;
                case SortState.CountAsc:
                    multiClass.products = multiClass.products.OrderBy(s => s.Amount).ToList();
                    break;
                case SortState.CountDesc:
                    multiClass.products = multiClass.products.OrderByDescending(s => s.Amount).ToList();
                    break;
                default:
                    multiClass.products = multiClass.products.OrderBy(s => s.Name).ToList();
                    break;
            }
            //Максимальное кол-во товаров на одной странице
            int pageSize = 15;
            var count=multiClass.products.Count();
            //Разбиение списка продуктов по 15 на страницу
            var items = multiClass.products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);


            viewModel.PageViewModel = pageViewModel;
            viewModel.products = items;
            viewModel.CategoryName = categoryName;
            viewModel.SortViewModel = new SortViewModel(sortOrder);
            


            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        /*<summary>
         * Экшен подготавливающий модель продукта для передачи в представление с информацией о продукте, принимающий параметр - Id выбранного продукта
         * </summary>*/
        [HttpGet]
        public async Task<IActionResult> ProductPage(int? id)
        {
            
            if (id != null)
            {
                //Получение модели продукта из базы данных по Id
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                //Проверка на наличие выбранного продукта в бд. В случае отстутвия выводится представление об отстутствии данного товара в базе данных
                if (product != null)
                {
                    ProductPageViewModel PPVM = new ProductPageViewModel();
                    PPVM.product = product;
                    PPVM.reviews = await db.Reviews.Where(r => r.ProductId == product.Id).Include(r=>r.user).ToListAsync();
                    return View(PPVM);
                }
                else
                {
                    return NotFound();
                }

            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult ProductPage(ProductPageViewModel PPVM)
        {
            if (User.Identity.IsAuthenticated)
            {

                Review review = new Review();
                review.ProductId = PPVM.product.Id;
                review.Stars = PPVM.NewReview.Stars;
                review.Description = PPVM.NewReview.Description;
                review.dateOfWriting = DateTime.Now;
                review.user = _userManager.FindByNameAsync(User.Identity.Name).Result;

                 
                

                db.Reviews.Add(review);
                db.SaveChanges();

                return RedirectToAction("ProductPage", "Home", PPVM.product.Id);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }
        /*<summary>
         * Экшен подготавливающий модель новости для передачи в представление с подробной информацией о новости, принимающий параметр - Id выбранной новости
         * </summary>*/
        public IActionResult NewsPage(int id)
        {
            News news=db.NewsList.SingleOrDefault(p => p.Id == id);
            return View(news);
        }
        
       
    }
}
