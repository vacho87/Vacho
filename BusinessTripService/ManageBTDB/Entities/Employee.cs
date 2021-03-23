using System;
using System.Collections.Generic;

#nullable disable

namespace ManageBTDB
{
    public partial class Employee
    {
        public Employee()
        {
            BusinessTrips = new HashSet<BusinessTrip>();
        }

        public short Id { get; set; }
        public byte? RankId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public byte UserId { get; set; }

        public virtual Rank Rank { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BusinessTrip> BusinessTrips { get; set; }


        public bool IsTheSameAs(Employee anotherEmployee)
        {
            if (RankId != anotherEmployee.RankId) return false;
            if (FirstName != anotherEmployee.FirstName) return false;
            if (LastName != anotherEmployee.LastName) return false;
            if (Patronymic != anotherEmployee.Patronymic) return false;
            if (UserId != anotherEmployee.UserId) return false;
            return true;
        }
    }
}
