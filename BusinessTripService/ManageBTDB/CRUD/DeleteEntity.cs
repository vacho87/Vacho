using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ManageBTDB.CRUD
{
    class DeleteEntity
    {
        /// <summary>
        /// Удаляет послебнюю запись в справочнике норм возмещения расходов по проезду и "открывает" период окончания действия предпоследней записи
        /// </summary>
        public static void DeleteTransitRate()
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            var rates = db.TransitRates.ToList().OrderByDescending(r => r.Id);
            var lastRate = rates.FirstOrDefault();
            var preLastRate = rates.Skip(1).FirstOrDefault();
            if (lastRate == null)
            {
                Service.ConsoleDisplay.ShowError("Удаление записи не выполнено. В таблице отсутствуют записи");
            }
            else
            {
                db.TransitRates.Remove(lastRate);
                if (preLastRate != null)
                {
                    preLastRate.EndDate = null;
                    db.TransitRates.Update(preLastRate);
                }
                db.SaveChanges();
            }

        }

        public static void DeleteBusinessTripPurpose(byte id)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            BusinessTripPurpose purposeToDelete = db.BusinessTripPurposes.SingleOrDefault(p => p.Id == id);
            if (purposeToDelete != null)
            {
                db.BusinessTripPurposes.Remove(purposeToDelete);
                db.SaveChanges();
            }
            else
            {
                Service.ConsoleDisplay.ShowError($"Удаление не выполнено. Запись с идентификатором \"{id}\" отсутствует в базе");
            }
        }

        public static void DeleteUser(byte id)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            User userToDelete = db.Users.SingleOrDefault(u => u.Id == id);
            if (userToDelete != null)
            {
                db.Users.Remove(userToDelete);
                db.SaveChanges();
            }
            else
            {
                Service.ConsoleDisplay.ShowError($"Удаление не выполнено. Пользователь с идентификатором \"{id}\" отсутствует в базе");
            }

        }
        public static void DeleteUser(string username)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            User userToDelete = db.Users.SingleOrDefault(u => u.UserName == username);
            if (userToDelete != null)
            {
                db.Users.Remove(userToDelete);
                db.SaveChanges();
            }
            else
            {
                Service.ConsoleDisplay.ShowError($"Удаление не выполнено. Пользователь с именем \"{username}\" отсутствует в базе");
            }

        }

        public static void DeleteEmployee(int id)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            Employee employeeToDelete = db.Staff.SingleOrDefault(e => e.Id == id);
            if (employeeToDelete != null)
            {
                db.Staff.Remove(employeeToDelete);
                db.SaveChanges();
                var businessTripsToDelete = db.BusinessTrips.Where(bt => bt.EmployeeId == id);
                // Вот эта часть метода никогда не сработает так как каскадное удаление установлено на уровне БД и 
                // и последовательность businessTripsToDelete при проверке всегда будет null.
                // Изначально в этом методе вызов db.SaveChanges() стоял после блока if, что вызывало выброс исключения
                // так как сначала отрабатывала внутрення логика блока if => удалялись присущие сотруднику командировки
                // а после при сохранении изменений контекста СУБД пыталась каскадно удалить уже несуществующие к тому моменту
                // записи из зависимых таблиц. Первая мысль была оставить в коде как есть, а убрать каскадное удаление из свойств 
                // таблиц.
                // Но у меня возник вопрос, а правильно ли вообще так поступать (описывать операции какскадного удаления сущностей в коде)?
                // Ведь это, как мне кажется, дополнительные издержки в производителности (многократные вызовы методов с обращением через EF к БД,
                // вытягивание данных в контекст только лишь для того, чтобы посмотреть, есть ли связанная сущность, и, если есть, то выполнить с ней 
                // необходимые действия по удалению). Полагаю, что БД делает это на низком уровне намного быстрее, а главное, на уровне БД это нативная 
                // (правильная, от производителя) реализация.
                // Просто я помню, что ты говорил, что не принято пользоваться этим функциоаналом на уровне БД, а принято в коде это реализовывать.
                // Какие есть доводы в пользу того, чтобы делать это в коде?
                if (businessTripsToDelete != null)
                {
                    foreach (BusinessTrip trip in businessTripsToDelete)
                    {
                        DeleteBusinessTrip(trip.Id);
                    }
                }                
            }
            else
            {
                Service.ConsoleDisplay.ShowError($"Удаление не выполнено. Сотрудник с идентификатором \"{id}\" отсутствует в базе");
            }

        }

        /// <summary>
        /// Удаляет в БД запись о командировке с указанным Id, также выполняет удаление записей об указанной командировке из всех зависимых таблиц
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteBusinessTrip(int id)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            BusinessTrip tripToDelete = db.BusinessTrips.SingleOrDefault(bt => bt.Id == id);
            if (tripToDelete != null)
            {
                db.BusinessTrips.Remove(tripToDelete);
                OrderInfo orderInfoToDelete = db.OrderInfos.Where(oi => oi.BusinessTripId == id).SingleOrDefault();
                if (orderInfoToDelete != null)
                {
                    DeleteOrderInfo(orderInfoToDelete.Id);
                }
                StatementInfo statementInfoToDelete = db.StatementInfos.Where(si => si.BusinessTripId == id).SingleOrDefault();
                if (statementInfoToDelete != null)
                {
                    DeleteStatementInfo(statementInfoToDelete.Id);
                }
                Calculation calculationToDelete = db.Calculations.Where(c => c.BusinessTripId == id).SingleOrDefault();
                if (calculationToDelete != null)
                {
                    DeleteCalculation(calculationToDelete.Id);
                }
                db.SaveChanges();
            }
            else
            {
                Service.ConsoleDisplay.ShowError($"Удаление не выполнено. Командировка с идентификатором \"{id}\" отсутствует в базе");
            }
        }
          
        public static void DeleteOrderInfo(int id)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            OrderInfo orderInfoToDelete = db.OrderInfos.Single(oi => oi.Id == id);
            if (orderInfoToDelete != null)
            {
                db.OrderInfos.Remove(orderInfoToDelete);
                if (db.ChangedOrderInfos.Any(coi => coi.OrderInfoId == id))
                {
                    DeleteChangedOrderInfo(db.ChangedOrderInfos.Where(coi => coi.OrderInfoId == id).Single().Id);
                }               
                db.SaveChanges();
            }
            else
            {
                Service.ConsoleDisplay.ShowError($"Удаление не выполнено. Командировка с идентификатором \"{id}\" отсутствует в базе");
            }
        }

        public static void DeleteCalculation(int id)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            Calculation calculationToDelete = db.Calculations.Single(c => c.Id == id);
            if (calculationToDelete != null)
            {
                db.Calculations.Remove(calculationToDelete);
                db.SaveChanges();
            }
            else
            {
                Service.ConsoleDisplay.ShowError($"Удаление не выполнено. Расчет с идентификатором \"{id}\" отсутствует в базе");
            }
        }

        public static void DeleteStatementInfo(int id)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            StatementInfo statementToDelete = db.StatementInfos.Single(si => si.Id == id);
            if (statementToDelete != null)
            {
                db.StatementInfos.Remove(statementToDelete);
                db.SaveChanges();
            }
            else
            {
                Service.ConsoleDisplay.ShowError($"Удаление не выполнено. Аавансовый отчет с идентификатором \"{id}\" отсутствует в базе");
            }
        }

        public static void DeleteChangedOrderInfo(int id)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            ChangedOrderInfo changedOrderInfoToDelete = db.ChangedOrderInfos.Single(coi => coi.Id == id);
            if (changedOrderInfoToDelete != null)
            {
                db.ChangedOrderInfos.Remove(changedOrderInfoToDelete);
                db.SaveChanges();
            }
            else
            {
                Service.ConsoleDisplay.ShowError($"Удаление не выполнено. Измененные условия командировки с идентификатором \"{id}\" отсутствуют в базе");
            }
        }
    }
}
