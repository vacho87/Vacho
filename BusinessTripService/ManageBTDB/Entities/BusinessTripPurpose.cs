using System;
using System.Collections.Generic;

#nullable disable

namespace ManageBTDB
{
    public partial class BusinessTripPurpose
    {
        private short _ExpenditureItem;
        public BusinessTripPurpose()
        {
            ChangedOrderInfos = new HashSet<ChangedOrderInfo>();
            OrderInfos = new HashSet<OrderInfo>();
        }

        public byte Id { get; set; }
        public string Purpose { get; set; }
        public short ExpenditureItem 
        {
            get 
            { 
                return _ExpenditureItem;
            }

            set 
            {
                if (value == 2026 || value == 4026 || value == 4146)
                {
                    _ExpenditureItem = value;
                }
                else
                {
                    throw new Service.ErrorReporter("Неверно указана статья бюджетной классификации. Должна быть 2026, 4026 или 4146");
                }

            } 
        }

        public virtual ICollection<ChangedOrderInfo> ChangedOrderInfos { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
    }
}
