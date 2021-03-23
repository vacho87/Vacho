using System;
using System.Collections.Generic;

#nullable disable

namespace ManageBTDB
{
    public partial class DailyRate
    {
        public byte Id { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Rate { get; set; }
    }
}
