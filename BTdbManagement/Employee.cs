using System;
using System.Collections.Generic;

#nullable disable

namespace BTdbManagement
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
    }
}
