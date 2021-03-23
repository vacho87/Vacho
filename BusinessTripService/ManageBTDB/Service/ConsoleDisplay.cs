using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ManageBTDB.Service
{

    public static class ConsoleDisplay
    {
        public static void ShowError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("\n!!! Ошибка: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{errorMessage}\n");
            Console.ResetColor();
        }

        public static void Show(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{user.Id,2}. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("имя пользователя: ");
                Console.ResetColor();
                Console.Write($"{user.UserName,-10}");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("пароль: ");
                Console.ResetColor();
                Console.WriteLine($"{user.Pasword}");
            }
        }

        public static void Show(Employee p)
        {
            if (p is null)
            {
                throw new ArgumentNullException(nameof(p));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{p.Id,2}. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("звание: ");
                Console.ResetColor();
                Console.Write($"{p.Rank.RankName,-13} ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("ФИО: ");
                Console.ResetColor();
                Console.Write($"{p.LastName} {p.FirstName.First()}.{p.Patronymic.First()}. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("расчеты ведет: ");
                Console.ResetColor();
                Console.WriteLine($"{p.User.UserName}");
            }
        }

        public static void Show(BusinessTripPurpose btp)
        {
            if (btp is null)
            {
                throw new ArgumentNullException(nameof(btp));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{btp.Id,2}. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("статья расходов: ");
                Console.ResetColor();
                Console.Write($"{btp.ExpenditureItem}, ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(" наименование цели ком-ки");
                Console.ResetColor();
                Console.WriteLine($" {btp.Purpose}.");
            }
        }

        public static void Show(Locality locality)
        {
            if (locality is null)
            {
                throw new ArgumentNullException(nameof(locality));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{locality.Id,2}. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("наименование населенного пункта: ");
                Console.ResetColor();
                Console.Write($"{locality.Name,-15} ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("тип: ");
                Console.ResetColor();
                Console.WriteLine(locality.LocalityType.Type);
            }
        }

        public static void Show(BusinessTrip bt)
        {
            if (bt is null)
            {
                throw new ArgumentNullException(nameof(bt));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{bt.Id,2}. ");
                Console.ResetColor();
                Console.Write($"{bt.Employee.Rank.RankName, -12} {bt.Employee.LastName} {bt.Employee.FirstName.First()}.{bt.Employee.Patronymic.First()}. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("c ");
                Console.ResetColor();
                Console.Write($"{bt.OrderInfo.StartDate:dd.MM.yyyy} ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("по ");
                Console.ResetColor();
                Console.Write($"{bt.OrderInfo.EndDate:dd.MM.yyyy} ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("пункт командирования: ");
                Console.ResetColor();
                Console.Write($"{bt.OrderInfo.Locality.Name,-14} ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("цель: ");
                Console.ResetColor();
                Console.Write($"{bt.OrderInfo.Purpose.Purpose} ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("статус: ");
                Console.ResetColor();
                Console.WriteLine(bt.State);
            }
        }

        public static void Show(TransitRate tr)
        {
            if (tr is null)
            {
                throw new ArgumentNullException(nameof(tr));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{tr.Id,2}. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("норма возмещения расходов по проезду: ");
                Console.ResetColor();
                Console.Write($"{tr.Rate, -4:##.##} руб. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("действует с: ");
                Console.ResetColor();
                Console.Write($"{tr.BeginDate:dd.MM.yyyy} ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("по: ");
                Console.ResetColor();
                Console.WriteLine(tr.EndDate == null ? "настоящий момент" : ((DateTime)tr.EndDate).ToString("dd.MM.yyyy"));
                
            }
        }

        public static void Show(OrderInfo order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{order.Id,2}. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Приказ № ");
                Console.ResetColor();
                Console.Write($"{order.OrderNumber, -2} от {order.OrderDate:dd.MM.yyyy} ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("№ командировки: ");
                Console.ResetColor();
                Console.Write($"{order.BusinessTripId, -2}");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("c ");
                Console.ResetColor();
                Console.Write($"{order.StartDate:dd.MM.yyyy} ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("по ");
                Console.ResetColor();
                Console.Write($"{order.EndDate:dd.MM.yyyy}\n\t");
                                                
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("безвыездно: ");
                Console.ResetColor();
                Console.Write(order.Permanent ? "да  " : "нет ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("транспорт туда: ");
                Console.ResetColor();
                Console.Write(order.TrafficForward ? "да  " : "нет ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("транспорт обратно: ");
                Console.ResetColor();
                Console.Write(order.TrafficBack ? "да  " : "нет ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("транспорт в пункте: ");
                Console.ResetColor();
                Console.Write((order.Transport ? "да  " : "нет ") + "\n\t");
                
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("питание: ");
                Console.ResetColor();
                Console.Write(order.Feeding ? "да  " : "нет ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("проживание: ");
                Console.ResetColor();
                Console.Write(order.Lodging ? "да  " : "нет ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("сухпаек: ");
                Console.ResetColor();
                Console.Write(order.TrafficBack ? "да  " : "нет ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("в составе команды: ");
                Console.ResetColor();
                Console.Write((order.TeamId == null ? "нет " : ($"№ {order.TeamId}")) + "\n\t");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("пункт командирования: ");
                Console.ResetColor();
                Console.WriteLine(order.Locality.Name);
                Console.WriteLine("----------------------------------------------------------------------------------------");
            }
        }


        public static void Show(ChangedOrderInfo ChangedOrder)
        {
            if (ChangedOrder is null)
            {
                throw new ArgumentNullException(nameof(ChangedOrder));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{ChangedOrder.Id,2}. ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Приказ № ");
                Console.ResetColor();
                Console.Write($"{ChangedOrder.OrderNumber,-2} от {ChangedOrder.OrderDate:dd.MM.yyyy} ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("изменения в командировку №: ");
                Console.ResetColor();
                Console.Write($"{ChangedOrder.OrderInfo.BusinessTripId,-2}");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("по приказу №: ");
                Console.ResetColor();
                Console.WriteLine($"{ChangedOrder.OrderInfo.OrderNumber,-2} от {ChangedOrder.OrderInfo.OrderDate:dd.MM.yyyy} ");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\tВнесены следующие изменения: ");

                if (ChangedOrder.StartDate != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\t\tначало командиорвки: ");
                    Console.ResetColor();
                    Console.WriteLine($"{ChangedOrder.StartDate:dd.MM.yyyy} ");
                }

                if (ChangedOrder.EndDate != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\t\tокончание командиорвки: ");
                    Console.ResetColor();
                    Console.WriteLine($"{ChangedOrder.EndDate:dd.MM.yyyy}");
                }
                
                if (ChangedOrder.Permanent != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\t\tбезвыездно: ");
                    Console.ResetColor();
                    Console.WriteLine((bool)ChangedOrder.Permanent ? "да  " : "нет ");
                }
                
                if (ChangedOrder.TrafficForward != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\t\tтранспорт туда: ");
                    Console.ResetColor();
                    Console.WriteLine((bool)ChangedOrder.TrafficForward ? "да  " : "нет ");
                }

                if (ChangedOrder.TrafficBack != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\t\tтранспорт обратно: ");
                    Console.ResetColor();
                    Console.WriteLine((bool)ChangedOrder.TrafficBack ? "да  " : "нет ");
                }

                if (ChangedOrder.Transport != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\t\tтранспорт в пункте: ");
                    Console.ResetColor();
                    Console.WriteLine((bool)ChangedOrder.Transport ? "да  " : "нет ");
                }

                if (ChangedOrder.Feeding != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\t\tпитание: ");
                    Console.ResetColor();
                    Console.WriteLine((bool)ChangedOrder.Feeding ? "да  " : "нет ");
                }

                if (ChangedOrder.Lodging != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\t\tпроживание: ");
                    Console.ResetColor();
                    Console.WriteLine((bool)ChangedOrder.Lodging ? "да  " : "нет ");
                }
                   
                if (ChangedOrder.RationPack != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\t\tсухпаек: ");
                    Console.ResetColor();
                    Console.WriteLine(ChangedOrder.RationPack);
                }                
                                
                if (ChangedOrder.Locality != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\t\tпункт командирования: ");
                    Console.ResetColor();
                    Console.WriteLine(ChangedOrder.Locality.Name);
                }
               
                Console.WriteLine("----------------------------------------------------------------------------------------");
            }
        }
    }
}