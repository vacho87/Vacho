using System;
using System.Collections.Generic;

#nullable disable

namespace ManageBTDB
{
    public partial class BusinessTripState
    {
        public BusinessTripState()
        {
            BusinessTrips = new HashSet<BusinessTrip>();
        }

        public byte Id { get; set; }
        public string Summary { get; set; }

        public virtual ICollection<BusinessTrip> BusinessTrips { get; set; }
    }
}
