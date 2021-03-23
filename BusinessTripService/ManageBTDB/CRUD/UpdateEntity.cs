using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ManageBTDB.CRUD
{
    class UpdateEntity
    {
        public static void UpdateTransitRate(byte rateToUpdateId, decimal newRate)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            TransitRate rateToUpdate = db.TransitRates.Single(tr => tr.Id == rateToUpdateId);
            if (rateToUpdate == null)
            {
                Service.ConsoleDisplay.ShowError($"Обновление не выполнено. Запись с идентификатором \"{rateToUpdateId}\" отсутствует в базе");
            }
            else if (rateToUpdate.Rate == newRate)
            {
                Service.ConsoleDisplay.ShowError($"Обновление не выполнено. Размер изменяемой нормы равен устанавливаемому значению - {newRate} руб.");
            }
            else
            {
                rateToUpdate.Rate = newRate;
                db.TransitRates.Update(rateToUpdate);
                db.SaveChanges();
            }

        }

        public static void UpdateUser (byte userToUpdateId, User newUserToUpdateExistingOne)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            User userToUpdate = db.Users.Single(u => u.Id == userToUpdateId);
            if (userToUpdate == null)
            {
                Service.ConsoleDisplay.ShowError($"Обновление не выполнено. Пользователь с идентификатором \"{userToUpdateId}\" отсутствует в базе");
            }
            else if (userToUpdate.IsTheSameAs(newUserToUpdateExistingOne))
            {
                Service.ConsoleDisplay.ShowError($"Обновление не выполнено. Параметры пользователя, задаваемые при обновлении, идентичны текущим параметрам пользователя");
            }
            else
            {
                userToUpdate.UserName = newUserToUpdateExistingOne.UserName;
                userToUpdate.Pasword = newUserToUpdateExistingOne.Pasword;
                db.Users.Update(userToUpdate);
                db.SaveChanges();
            }
        }

        public static void UpdateOrderInfo(short orderInfoToUpdateId, OrderInfo newOrderInfoToUpdateExistingOne)
        {
            using BTdbContext db = new BTdbContext(ContextOptions.options);
            OrderInfo orderInfoToUpdate = db.OrderInfos.Single(oi => oi.Id == orderInfoToUpdateId);
            if (orderInfoToUpdate == null)
            {
                Service.ConsoleDisplay.ShowError($"Обновление не выполнено. Информация о командировке из приказа с идентификатором \"{orderInfoToUpdateId}\" отсутствует в базе");
            }
            else if (orderInfoToUpdate.IsTheSameAs(newOrderInfoToUpdateExistingOne))
            {
                Service.ConsoleDisplay.ShowError($"Обновление не выполнено. Параметры командировки, задаваемые при обновлении информации приказа, идентичны текущим параметрам");
            }
            else
            {
                orderInfoToUpdate.PurposeId = newOrderInfoToUpdateExistingOne.PurposeId;
                orderInfoToUpdate.LocalityId = newOrderInfoToUpdateExistingOne.LocalityId;
                orderInfoToUpdate.OrderNumber = newOrderInfoToUpdateExistingOne.OrderNumber;
                orderInfoToUpdate.OrderDate = newOrderInfoToUpdateExistingOne.OrderDate;
                orderInfoToUpdate.StartDate = newOrderInfoToUpdateExistingOne.StartDate;
                orderInfoToUpdate.EndDate = newOrderInfoToUpdateExistingOne.EndDate;
                orderInfoToUpdate.Permanent = newOrderInfoToUpdateExistingOne.Permanent;
                orderInfoToUpdate.TrafficForward = newOrderInfoToUpdateExistingOne.TrafficForward;
                orderInfoToUpdate.TrafficBack = newOrderInfoToUpdateExistingOne.TrafficBack;
                orderInfoToUpdate.Feeding = newOrderInfoToUpdateExistingOne.Feeding;
                orderInfoToUpdate.Transport = newOrderInfoToUpdateExistingOne.Transport;
                orderInfoToUpdate.Lodging = newOrderInfoToUpdateExistingOne.Lodging;
                orderInfoToUpdate.RationPack = newOrderInfoToUpdateExistingOne.RationPack;
                orderInfoToUpdate.TeamId = newOrderInfoToUpdateExistingOne.TeamId;

                db.OrderInfos.Update(orderInfoToUpdate);
                db.SaveChanges();
            }
        }
    }
}
