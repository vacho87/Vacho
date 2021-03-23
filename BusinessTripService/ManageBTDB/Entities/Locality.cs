using System;
using System.Collections.Generic;
using System.Linq;


#nullable disable

namespace ManageBTDB
{
    public partial class Locality
    {
        public Locality()
        {
            ChangedOrderInfos = new HashSet<ChangedOrderInfo>();
            OrderInfos = new HashSet<OrderInfo>();
            StatementInfos = new HashSet<StatementInfo>();
        }

        public byte Id { get; set; }
        private string name;
        public string Name         
        {             
            get
            {
                return name;
            }

            set 
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                var existingLocalities = db.Localities.Select(l => l.Name).ToList(); 
                if (existingLocalities.Contains(value))
                {
                    throw new Service.ErrorReporter("Указанное наименование населенного пункта уже существует");                    
                }
                else
                {
                    name = value;
                }
            } 
        }
        private byte localityTypeId;
        public byte LocalityTypeId 
        {
            get
            { 
                return localityTypeId;
            }
            set 
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                var existingLocalityTypes = db.LocalityTypes.Select(lt => lt.Id).ToList(); // Have a question
                if (existingLocalityTypes.Contains(value))
                {
                    localityTypeId = value;
                }
                else
                {
                    throw new Service.ErrorReporter("Указан неверный тип населенного пункта");
                }
            }
        }

        public virtual LocalityType LocalityType { get; set; }
        public virtual ICollection<ChangedOrderInfo> ChangedOrderInfos { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
        public virtual ICollection<StatementInfo> StatementInfos { get; set; }
    }
}
