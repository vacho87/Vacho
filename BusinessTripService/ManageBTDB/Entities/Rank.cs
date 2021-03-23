using System;
using System.Collections.Generic;

#nullable disable

namespace ManageBTDB
{
    public partial class Rank
    {
        public Rank()
        {
            Staff = new HashSet<Employee>();
        }

        public byte Id { get; set; }
        public string RankName { get; set; }

        public virtual ICollection<Employee> Staff { get; set; }
    }
}
