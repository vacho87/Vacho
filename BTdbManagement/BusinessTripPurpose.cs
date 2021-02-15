using System;
using System.Collections.Generic;

#nullable disable

namespace BTdbManagement
{
    public partial class BusinessTripPurpose
    {
        public BusinessTripPurpose()
        {
            OrderInfos = new HashSet<OrderInfo>();
        }

        public byte Id { get; set; }
        public string Purpose { get; set; }
        public short ExpenditureItem { get; set; }

        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
    }
}
