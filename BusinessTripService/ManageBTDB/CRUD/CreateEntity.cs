using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ManageBTDB.CRUD
{
    class CreateEntity
    {
        public static void CreateUser(User user)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            try
            {
                if (db.Users.Any(u => u.UserName == user.UserName))
                {
                    Service.ConsoleDisplay.ShowError($"Пользователь с именем \"{user.UserName}\" уже существует");
                }
                else
                {
                    db.Add(user);
                    db.SaveChanges();
                }
                
            }
            catch (Service.ErrorReporter err)
            {
                Service.ConsoleDisplay.ShowError(err.Message);
            }
        }
        public static void CreateUser(string userName, string password)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            try
            {
                db.Add(new User {UserName = userName, Pasword = password });
                db.SaveChanges();
            }
            catch (Service.ErrorReporter err)
            {
                Service.ConsoleDisplay.ShowError(err.Message);
            }
        }


        public static void CreateTransitRate(decimal rate, int day, int month, int year)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            try
            {
                TransitRate tr = new TransitRate { Rate = rate, BeginDate = new DateTime(year, month, day) };              
                db.Add(tr);
                db.SaveChanges();
            }
            catch (Service.ErrorReporter err)
            {
                Service.ConsoleDisplay.ShowError(err.Message);
            }
        }
        public static void CreateTransitRate(TransitRate rate)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            try
            {
                db.Add(rate);
                db.SaveChanges();
            }
            catch (Service.ErrorReporter err)
            {
                Service.ConsoleDisplay.ShowError(err.Message);
            }
        }


        public static void CreateBusinessTripPurpose(string purpose, short expenditureIitem)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            try
            {
                BusinessTripPurpose btp = new BusinessTripPurpose { ExpenditureItem = expenditureIitem, Purpose = purpose };
                db.Add(btp);
                db.SaveChanges();
            }
            catch (Service.ErrorReporter err)
            {
                Service.ConsoleDisplay.ShowError(err.Message);
            }
        }
        public static void CreateBusinessTripPurpose(BusinessTripPurpose btp)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            try
            {                
                db.Add(btp);
                db.SaveChanges();
            }
            catch (Service.ErrorReporter err)
            {
                Service.ConsoleDisplay.ShowError(err.Message);
            }
        }


        public static void CreateEmployee(Employee employee)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            try
            {
                db.Add(employee);
                db.SaveChanges();
            }
            catch (Service.ErrorReporter err)
            {
                Service.ConsoleDisplay.ShowError(err.Message);
            }
        }
        public static void CreateEmployee(string lName, string fName, byte userId, string patronym = null, byte? rankId = null)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            try
            {
                db.Add(new Employee { RankId = rankId, FirstName = fName, LastName = lName, Patronymic = patronym, UserId = userId});
                db.SaveChanges();
            }
            catch (Service.ErrorReporter err)
            {
                Service.ConsoleDisplay.ShowError(err.Message);
            }
        }


        public static void CreateBusinessTrip(short employeeId)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            try
            {
                db.Add(new BusinessTrip(employeeId));
                db.SaveChanges();
            }
            catch (Service.ErrorReporter err)
            {
                Service.ConsoleDisplay.ShowError(err.Message);
            }
        }

        public static void CreateOrderInfo(short employeeId, OrderInfo orderInfo)
        {
            CreateBusinessTrip(employeeId);
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            int createdTripId = db.BusinessTrips.Where(bt => bt.EmployeeId == employeeId).Max(bt => bt.Id);                      
            try
            {               
                orderInfo.BusinessTripId = createdTripId;
                db.Add(orderInfo);
                db.SaveChanges();
            }
            catch (Service.ErrorReporter err)
            {
                Service.ConsoleDisplay.ShowError(err.Message);
            }
        }

        public static void CreateOrderInfo(List<short> listOfEmployeeId, OrderInfo orderInfo)
        {
            foreach (short employeeId in listOfEmployeeId)
            {
                orderInfo.Id = 0;
                CreateOrderInfo(employeeId, orderInfo);
            }
        }

        public static void CreateChangedOrderInfo(ChangedOrderInfo changedOrderInfo)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            try
            {   
                if (changedOrderInfo.IsDummy())
                {
                    Service.ConsoleDisplay.ShowError("Приказ об изменении не был добавлен так как не указано ни одного изменившегося условия командировки");
                }
                else
                {
                    db.Add(changedOrderInfo);
                    db.SaveChanges();
                }
                
            }
            catch (Service.ErrorReporter err)
            {
                Service.ConsoleDisplay.ShowError(err.Message);
            }
        }
    }
}
