using System;
using System.Collections.Generic;

#nullable disable

namespace ManageBTDB
{
    public partial class BusinessTrip
    {
        public int Id { get; set; }
        public short EmployeeId { get; set; }
        public string Info { get; set; }
        public byte StateId { get; set; } = 1;

        public BusinessTrip(short employeeId)
        {
            EmployeeId = employeeId;
        }

        public virtual Employee Employee { get; set; }
        public virtual BusinessTripState State { get; set; }
        public virtual Calculation Calculation { get; set; }
        public virtual OrderInfo OrderInfo { get; set; }
        public virtual StatementInfo StatementInfo { get; set; }
    }
}
