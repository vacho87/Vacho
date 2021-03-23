using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using ManageBTDB.CRUD;
using ManageBTDB.Service;


namespace ManageBTDB
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ НОРМ РАСХОДОВ ПО ПРОЕЗДУ:");
            ReadEntity.ReadTransitRates();

            ServiceReporter.ShowServiceMessage("Добавлениие в БД новой нормы =>> начало действия - 15.06.2021, окончание - нет, размер - 3,2 руб.");
            CreateEntity.CreateTransitRate(3.2m, 15, 6, 2021);
            ServiceReporter.ShowServiceMessage("Таблица после добавления записи:");
            ReadEntity.ReadTransitRates();

            ServiceReporter.ShowServiceMessage("!!!!!! попытка добавления некорректной записи (начало действия - 15.05.2021, окончание - нет, размер - 4,0 руб.)");
            CreateEntity.CreateTransitRate(4.0m, 15, 5, 2021);
            ServiceReporter.ShowServiceMessage("Таблица после добавления записи:");
            ReadEntity.ReadTransitRates();

            ServiceReporter.ShowServiceMessage("Изменение полседней записи. Размер нормы изменяется на 12,25 рублей");
            UpdateEntity.UpdateTransitRate(4, 12.25m);
            ServiceReporter.ShowServiceMessage("Таблица после изменения записи:");
            ReadEntity.ReadTransitRates();

            ServiceReporter.ShowServiceMessage("Удаление последней записи из таблицы");
            DeleteEntity.DeleteTransitRate();
            ServiceReporter.ShowServiceMessage("Таблица после удаления последней записи:");
            ReadEntity.ReadTransitRates();

            ServiceReporter.ShowServiceMessage("Удаление всех записей и последующая попытка удаления строк в пустой таблице");
            DeleteEntity.DeleteTransitRate();
            DeleteEntity.DeleteTransitRate();
            DeleteEntity.DeleteTransitRate();
            DeleteEntity.DeleteTransitRate();
            ServiceReporter.ShowServiceMessage("Таблица после удаления записей:");
            ReadEntity.ReadTransitRates();

            Console.WriteLine("=========================================================================");
            Console.WriteLine("=========================================================================");

            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ ПОЛЬЗОВАТЕЛЕЙ:");
            ReadEntity.ReadUsers();
            ServiceReporter.ShowServiceMessage("Добавлениие в БД нового пользователя =>> имя - Иванов, пароль - 12345.");
            CreateEntity.CreateUser("Иванов", "12345");
            ServiceReporter.ShowServiceMessage("Таблица после добавления записи:");
            ReadEntity.ReadUsers();
            ServiceReporter.ShowServiceMessage("Изменение записей в таблице полльзователей:\n" +
                "- сменить пароль для пользователя c Id = 1 на \"000000\";\n" +
                "- сменить имя и пароль для пользователя с Id = 2 на \"Пушкин\" и \"909090\" соответственно");
            UpdateEntity.UpdateUser(1, new User { UserName = "Кожихов", Pasword = "000000" });
            UpdateEntity.UpdateUser(2, new User { UserName = "Пушкин", Pasword = "909090" });
            ServiceReporter.ShowServiceMessage("Таблица после изменения записей:");
            ReadEntity.ReadUsers();
            ServiceReporter.ShowServiceMessage("Попытка установить для пользователя Иванов имя и пароль аналогичные уже установленным");
            UpdateEntity.UpdateUser(6, new User { UserName = "Иванов", Pasword = "12345" });
            ServiceReporter.ShowServiceMessage("Таблица после изменения записей:");
            ReadEntity.ReadUsers();
            ServiceReporter.ShowServiceMessage("Удаление записи о пользователе с Id = 6");
            DeleteEntity.DeleteUser(6);
            ServiceReporter.ShowServiceMessage("Таблица после удаления последней записи:");
            ReadEntity.ReadUsers();

            Console.WriteLine("=========================================================================");
            Console.WriteLine("=========================================================================");

            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ СОТРУДНИКОВ:\n");
            ReadEntity.ReadStaff();

            Console.WriteLine("=========================================================================");
            Console.WriteLine("=========================================================================");

            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ КОМАНДИРОВОК:\n");
            ReadEntity.ReadBusinessTrips();
            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ ПРИКАЗОВ:\n");
            ReadEntity.ReadOrderInfos();


            ServiceReporter.ShowServiceMessage("Добавление приказа о командировке (!!!при этом также создаются записи в таблице командировок!!!)");
            OrderInfo order1 = new OrderInfo
            {
                PurposeId = 1,
                LocalityId = 9,
                OrderNumber = 87,
                OrderDate = new DateTime(2021, 02, 12),
                StartDate = new DateTime(2021, 02, 13),
                EndDate = new DateTime(2021, 02, 25),
                Permanent = true,
                TrafficForward = false,
                TrafficBack = false,
                Feeding = false,
                Transport = false,
                Lodging = true
            };

            OrderInfo order2 = new OrderInfo
            {
                PurposeId = 5,
                LocalityId = 14,
                OrderNumber = 88,
                OrderDate = new DateTime(2021, 02, 13),
                StartDate = new DateTime(2021, 02, 13),
                EndDate = new DateTime(2021, 03, 28),
                Permanent = true,
                TrafficForward = true,
                TrafficBack = true,
                Feeding = false,
                Transport = true,
                Lodging = false,
                RationPack = 2,
                TeamId = 1
            };

            OrderInfo order3 = new OrderInfo
            {
                PurposeId = 2,
                LocalityId = 3,
                OrderNumber = 89,
                OrderDate = new DateTime(2021, 02, 14),
                StartDate = new DateTime(2021, 02, 15),
                EndDate = new DateTime(2021, 03, 01),
                Permanent = true,
                TrafficForward = false,
                TrafficBack = false,
                Feeding = false,
                Transport = false,
                Lodging = true,
                RationPack = 0,
                TeamId = 2
            };

            OrderInfo order4 = new OrderInfo
            {
                PurposeId = 7,
                LocalityId = 10,
                OrderNumber = 90,
                OrderDate = new DateTime(2021, 02, 16),
                StartDate = new DateTime(2021, 02, 16),
                EndDate = new DateTime(2021, 02, 18),
                Permanent = true,
                TrafficForward = false,
                TrafficBack = false,
                Feeding = true,
                Transport = false,
                Lodging = true
            };

            CreateEntity.CreateOrderInfo(1, order1);
            CreateEntity.CreateOrderInfo(3, order2);
            CreateEntity.CreateOrderInfo(new List<short> { 4, 5, 6, 7 }, order3);
            CreateEntity.CreateOrderInfo(8, order4);

            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ КОМАНДИРОВОК:\n");
            ReadEntity.ReadBusinessTrips();
            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ ПРИКАЗОВ:\n");
            ReadEntity.ReadOrderInfos();


            Console.WriteLine("=========================================================================");
            Console.WriteLine("=========================================================================");

            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ ПРИКАЗОВ ОБ ИЗМЕНЕНИИ УСЛОВИЙ КОМАНДИРОВАНИЯ:\n");
            ReadEntity.ReadChangedOrderInfos();
            ServiceReporter.ShowServiceMessage("Добавление двух приказов об изменении условий командиования");

            ChangedOrderInfo changedOrder1 = new ChangedOrderInfo
            {
                OrderInfoId = 1,
                PurposeId = 1,
                LocalityId = 9,
                OrderNumber = 90,
                OrderDate = new DateTime(2021, 02, 18),
                StartDate = new DateTime(2021, 02, 15),
                EndDate = new DateTime(2021, 02, 28),
                Permanent = true,
                TrafficForward = true,
                TrafficBack = true,
                Feeding = true,
                Transport = false,
                Lodging = true
            };

            ChangedOrderInfo changedOrder2 = new ChangedOrderInfo
            {
                OrderInfoId = 2,
                PurposeId = 5,
                LocalityId = 14,
                OrderNumber = 88,
                OrderDate = new DateTime(2021, 02, 13),
                StartDate = new DateTime(2021, 02, 13),
                EndDate = new DateTime(2021, 03, 28),
                Permanent = true,
                TrafficForward = true,
                TrafficBack = true,
                Feeding = false,
                Transport = true,
                Lodging = false
            };

            CreateEntity.CreateChangedOrderInfo(changedOrder1);
            CreateEntity.CreateChangedOrderInfo(changedOrder2);

            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ ПРИКАЗОВ ОБ ИЗМЕНЕНИИ УСЛОВИЙ КОМАНДИРОВАНИЯ:\n");
            ReadEntity.ReadChangedOrderInfos();

            ServiceReporter.ShowServiceMessage("Удаление сотрудников. (!!! Что влечет каскадное удаление связанных командировок, приказов, приказов об изменении:\n");
            DeleteEntity.DeleteEmployee(1);
            DeleteEntity.DeleteEmployee(3);
            DeleteEntity.DeleteEmployee(8);

            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ СОТРУДНИКОВ:");
            ReadEntity.ReadStaff();
            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ КОМАНДИРОВОК:\n");
            ReadEntity.ReadBusinessTrips();
            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ ПРИКАЗОВ:\n");
            ReadEntity.ReadOrderInfos();
            ServiceReporter.ShowServiceMessage("ЧТЕНИЕ ТАБЛИЦЫ ПРИКАЗОВ ОБ ИЗМЕНЕНИИ УСЛОВИЙ КОМАНДИРОВАНИЯ:\n");
            ReadEntity.ReadChangedOrderInfos();


        }
    }
}
