using System;
using System.Collections.Generic;

#nullable disable

namespace ManageBTDB
{
    public partial class OrderInfo
    {
        public short Id { get; set; }
        public int BusinessTripId { get; set; }
        public byte PurposeId { get; set; }
        public byte LocalityId { get; set; }
        public short OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        private DateTime startDate;
        public DateTime StartDate
        { 
            get
            {
                return startDate;
            }
            set
            {
                if (value < OrderDate)
                {
                    throw new Service.ErrorReporter("Дата начала командиорвки не может быть меньше даты издания приказа");
                }
                else
                {
                    startDate = value;
                }
            }
        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                if (value < startDate)
                {
                    throw new Service.ErrorReporter("Дата окончания не может быть меньше даы начала командировки");
                }
                else
                {
                    endDate = value;
                }
            }
        }
        public bool Permanent { get; set; } = true;
        public bool TrafficForward { get; set; } = true;
        public bool TrafficBack { get; set; } = true;
        public bool Feeding { get; set; } = false;
        public bool Transport { get; set; } = false;
        public bool Lodging { get; set; } = true;
        public byte RationPack { get; set; } = 0;
        public short? TeamId { get; set; }
        public bool Changed { get; set; } = false;


        public virtual BusinessTrip BusinessTrip { get; set; }
        public virtual Locality Locality { get; set; }
        public virtual BusinessTripPurpose Purpose { get; set; }
        public virtual ChangedOrderInfo ChangedOrderInfo { get; set; }

        public bool IsTheSameAs(OrderInfo anotherOrderInfo)
        {
            if (BusinessTripId != anotherOrderInfo.BusinessTripId) return false;
            if (PurposeId != anotherOrderInfo.PurposeId) return false;
            if (LocalityId != anotherOrderInfo.LocalityId) return false;
            if (OrderNumber != anotherOrderInfo.OrderNumber) return false;
            if (OrderDate != anotherOrderInfo.OrderDate) return false;
            if (StartDate != anotherOrderInfo.StartDate) return false;
            if (EndDate != anotherOrderInfo.EndDate) return false;
            if (Permanent != anotherOrderInfo.Permanent) return false;
            if (TrafficForward != anotherOrderInfo.TrafficForward) return false;
            if (TrafficBack != anotherOrderInfo.TrafficBack) return false;
            if (Feeding != anotherOrderInfo.Feeding) return false;
            if (Transport != anotherOrderInfo.Transport) return false;
            if (Lodging != anotherOrderInfo.Lodging) return false;
            if (RationPack != anotherOrderInfo.RationPack) return false;
            if (TeamId != anotherOrderInfo.TeamId) return false;
            return true;
        }
    }
}
