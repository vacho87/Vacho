using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace ManageBTDB
{
    /// <summary>
    /// В свойствах этого класса организована проверка на идентичность (не) идентичность. 
    /// В случае идентичности присваиваемого свойству значения значению аналогичного свойства исходоного приказа значение не устанавливается.
    /// Приказ об изменении должен содеражать только информацию о действительно измененных параметрах командировки.
    /// </summary>
    public partial class ChangedOrderInfo
    {
        public short Id { get; set; }
        private short orderInfoId;
        public short OrderInfoId
        { 
            get
            {
                return orderInfoId;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                if (!db.OrderInfos.Any(oi => oi.Id == value))
                {
                    throw new Service.ErrorReporter("Попытка внесения изменений в несуществующий приказ");
                }
                else
                {
                    orderInfoId = value;
                }
            }
        }
        public short OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        private DateTime? startDate;
        public DateTime? StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                OrderInfo orderInfoToChange = db.OrderInfos.Single(oi => oi.Id == OrderInfoId);
                startDate = orderInfoToChange.StartDate == value ? null : value;  
            }
        }
        private DateTime? endDate;
        public DateTime? EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                OrderInfo orderInfoToChange = db.OrderInfos.Single(oi => oi.Id == OrderInfoId);
                endDate = orderInfoToChange.EndDate == value ? null : value;
            }
        }
        private byte? localityId;
        public byte? LocalityId
        {
            get
            {
                return localityId;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                OrderInfo orderInfoToChange = db.OrderInfos.Single(oi => oi.Id == OrderInfoId);
                localityId = orderInfoToChange.LocalityId == value ? null : value;
            }
        }
        private bool? permanent;
        public bool? Permanent
        {
            get
            {
                return permanent;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                OrderInfo orderInfoToChange = db.OrderInfos.Single(oi => oi.Id == OrderInfoId);
                permanent = orderInfoToChange.Permanent == value ? null : value;
            }
        }
        private bool? trafficForward;
        public bool? TrafficForward
        {
            get
            {
                return trafficForward;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                OrderInfo orderInfoToChange = db.OrderInfos.Single(oi => oi.Id == OrderInfoId);
                trafficForward = orderInfoToChange.TrafficForward == value ? null : value;
            }
        }
        private bool? trafficBack;
        public bool? TrafficBack
        {
            get
            {
                return trafficBack;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                OrderInfo orderInfoToChange = db.OrderInfos.Single(oi => oi.Id == OrderInfoId);
                trafficBack = orderInfoToChange.TrafficBack == value ? null : value;
            }
        }
        private bool? feeding;
        public bool? Feeding
        {
            get
            {
                return feeding;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                OrderInfo orderInfoToChange = db.OrderInfos.Single(oi => oi.Id == OrderInfoId);
                feeding = orderInfoToChange.Feeding == value ? null : value;
            }
        }
        private bool? transport;
        public bool? Transport
        {
            get
            {
                return transport;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                OrderInfo orderInfoToChange = db.OrderInfos.Single(oi => oi.Id == OrderInfoId);
                transport = orderInfoToChange.Transport == value ? null : value;
            }
        }
        private bool? lodging;
        public bool? Lodging
        {
            get
            {
                return lodging;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                OrderInfo orderInfoToChange = db.OrderInfos.Single(oi => oi.Id == OrderInfoId);
                lodging = orderInfoToChange.Lodging == value ? null : value;
            }
        }
        private byte? rationPack;
        public byte? RationPack
        {
            get
            {
                return rationPack;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                OrderInfo orderInfoToChange = db.OrderInfos.Single(oi => oi.Id == OrderInfoId);
                rationPack = orderInfoToChange.RationPack == value ? null : value;
            }
        }
        private byte? purposeId;
        public byte? PurposeId
        {
            get
            {
                return purposeId;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                OrderInfo orderInfoToChange = db.OrderInfos.Single(oi => oi.Id == OrderInfoId);
                purposeId = orderInfoToChange.PurposeId == value ? null : value;
            }
        }

        public virtual Locality Locality { get; set; }
        public virtual OrderInfo OrderInfo { get; set; }
        public virtual BusinessTripPurpose Purpose { get; set; }

        public bool IsDummy()
        {
            if (startDate != null) return false;
            if (endDate != null) return false;
            if (localityId != null) return false;
            if (trafficForward != null) return false;
            if (trafficBack != null) return false;
            if (permanent != null) return false;
            if (feeding != null) return false;
            if (transport != null) return false;
            if (lodging != null) return false;
            if (rationPack != null) return false;
            if (purposeId != null) return false;
            return true;
        }

    }
       
}
