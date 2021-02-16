using System;
using System.Collections.Generic;

#nullable disable

namespace BTdbManagement
{
    public partial class User
    {
        public User()
        {
            staff = new HashSet<Employee>();
        }

        public byte Id { get; set; }
        public string UserName { get; set; }
        public string Pasword { get; set; }

        public virtual ICollection<Employee> staff { get; set; }
    }
}
