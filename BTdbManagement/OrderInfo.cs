using System;
using System.Collections.Generic;

#nullable disable

namespace BTdbManagement
{
    public partial class OrderInfo
    {
        public short Id { get; set; }
        public int? BusinessTripId { get; set; }
        public short OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte LocalityId { get; set; }
        public bool Permanent { get; set; }
        public bool TrafficForward { get; set; }
        public bool TrafficBack { get; set; }
        public bool Feeding { get; set; }
        public bool Transport { get; set; }
        public bool Lodging { get; set; }
        public byte RationPack { get; set; }
        public short TeamId { get; set; }
        public byte PurposeId { get; set; }

        public virtual BusinessTrip BusinessTrip { get; set; }
        public virtual Locality Locality { get; set; }
        public virtual BusinessTripPurpose Purpose { get; set; }
    }
}
