using System;
using System.Collections.Generic;

#nullable disable

namespace BTdbManagement
{
    public partial class BusinessTrip
    {
        public BusinessTrip()
        {
            OrderInfos = new HashSet<OrderInfo>();
        }

        public int Id { get; set; }
        public short EmployeeId { get; set; }
        public string Info { get; set; }
        public byte BusinessTripState { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Calculation Calculation { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }        
    }
}
