using System;
using System.Collections.Generic;

#nullable disable

namespace BTdbManagement
{
    public partial class Locality
    {
        public Locality()
        {
            OrderInfos = new HashSet<OrderInfo>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public byte LocalityTypeId { get; set; }

        public virtual LocalityType LocalityType { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
    }
}
