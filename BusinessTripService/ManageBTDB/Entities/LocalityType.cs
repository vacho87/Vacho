using System;
using System.Collections.Generic;

#nullable disable

namespace ManageBTDB
{
    public partial class LocalityType
    {
        public LocalityType()
        {
            Localities = new HashSet<Locality>();
        }

        public byte Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Locality> Localities { get; set; }
    }
}
