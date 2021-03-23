using System;
using System.Collections.Generic;

#nullable disable

namespace ManageBTDB
{
    public partial class StatementInfo
    {
        public short Id { get; set; }
        public int BusinessTripId { get; set; }
        public short ChangeOrderNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte LocalityId { get; set; }
        public bool Permanent { get; set; }
        public bool TrafficForward { get; set; }
        public bool TrafficBack { get; set; }
        public bool Feeding { get; set; }
        public DateTime? FeedingBegin { get; set; }
        public DateTime? FeedingEnd { get; set; }
        public bool Transport { get; set; }
        public bool Lodging { get; set; }
        public byte RationPack { get; set; }
        public decimal PrePay { get; set; } 
       
        public virtual BusinessTrip BusinessTrip { get; set; }
        public virtual Locality Locality { get; set; }
    }
}
