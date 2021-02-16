using System;
using System.Collections.Generic;

#nullable disable

namespace BTdbManagement
{
    public partial class Calculation
    {
        public short Id { get; set; }
        public int? BusinessTripId { get; set; }
        public decimal? TransitCosts { get; set; }
        public decimal? DailyCosts { get; set; }
        public decimal? DwellingCosts { get; set; }
        public byte? PaySheetNumber { get; set; }
        public DateTime? PaySheetDate { get; set; }

        public virtual BusinessTrip BusinessTrip { get; set; }
    }
}
