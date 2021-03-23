using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace ManageBTDB
{
    public partial class User
    {
        public User()
        {
            Staff = new HashSet<Employee>();
        }

        public byte Id { get; set; }
        public string UserName { get; set; }
        public string Pasword { get; set; }

        public virtual ICollection<Employee> Staff { get; set; }

        public bool IsTheSameAs(User anotherUser)
        {
            if (UserName == anotherUser.UserName && Pasword == anotherUser.Pasword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
