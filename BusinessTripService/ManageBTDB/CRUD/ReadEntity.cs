using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ManageBTDB.CRUD
{
    class ReadEntity
    {
        public static void ReadUsers()
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            List<User> users = db.Users.ToList();
            foreach (User user in users)
            {
                try
                {
                    Service.ConsoleDisplay.Show(user);
                }
                catch (ArgumentNullException ex)
                {
                    Service.ConsoleDisplay.ShowError(ex.Message);
                }

            }

        }


        public static void ReadStaff()
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            List<Employee> staff = db.Staff.Include(p => p.Rank).Include(p => p.User).ToList();
            foreach (Employee person in staff)
            {
                try
                {
                    Service.ConsoleDisplay.Show(person);
                }
                catch (ArgumentNullException ex)
                {
                    Service.ConsoleDisplay.ShowError(ex.Message);
                }
            }
        }


        public static void ReadTransitRates()
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            List<TransitRate> trRates = db.TransitRates.ToList();
            foreach (TransitRate rate in trRates)
            {
                Service.ConsoleDisplay.Show(rate);
            }
        }


        public static void ReadBusinessTripPurposes()
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            var btPurposes = db.BusinessTripPurposes;
            foreach (BusinessTripPurpose purpose in btPurposes) Service.ConsoleDisplay.Show(purpose);
        }

        public static void ReadBusinessTrips()
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            var businessTrips = db.BusinessTrips.Include(bt => bt.Employee).ThenInclude(e => e.Rank).Include(bt => bt.OrderInfo.Locality).Include(bt => bt.OrderInfo.Purpose);
            if (!businessTrips.Any())
            {
                Service.ServiceReporter.ShowServiceMessage("СПИСОК КОМАНДИРОВОК ПУСТ");
            }
            else
            {
                foreach (BusinessTrip trip in businessTrips) Service.ConsoleDisplay.Show(trip);
            }
        }


        public static void ReadOrderInfos()
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            var orderInfos = db.OrderInfos.Include(oi => oi.Locality);

            if (!orderInfos.Any())
            {
                Service.ServiceReporter.ShowServiceMessage("СПИСОК ПРИКАЗОВ О НАПРАВЛЕНИИ В КОМАНДИРОВКУ ПУСТ");
            }
            else
            {
                foreach (OrderInfo order in orderInfos) Service.ConsoleDisplay.Show(order);
            }

        }

        public static void ReadChangedOrderInfos()
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            var changedOrderInfos = db.ChangedOrderInfos.Include(coi => coi.Locality).Include(coi => coi.OrderInfo);

            if (!changedOrderInfos.Any())
            {
                Service.ServiceReporter.ShowServiceMessage("СПИСОК ПРИКАЗОВ ОБ ИЗМЕНЕНИИ УСЛОВИЙ КОМАНДИОРВАНИЯ ПУСТ");
            }
            else
            {
                foreach (ChangedOrderInfo order in changedOrderInfos) Service.ConsoleDisplay.Show(order);
            }

        }

    }
}
