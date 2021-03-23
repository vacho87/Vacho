using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace ManageBTDB
{
    public partial class TransitRate
    {
        DateTime _BeginDate;
        public byte Id { get; set; }
        public DateTime BeginDate
        {
            get
            {
                return _BeginDate;
            }
            set
            {
                using BTdbContext db = new BTdbContext(ContextOptions.options);
                List<TransitRate> rates = db.TransitRates.ToList();
                if (rates.Any())
                {
                    TransitRate lastRate = rates.Single(r => r.Id == rates.Select(r => r.Id).Max());
                    if (lastRate.BeginDate >= value)
                    {
                        throw new Service.ErrorReporter("дата начала действия расчетной величины должна быть больше даты начала действия ee предыдущего значения");
                    }
                    else
                    {
                        _BeginDate = value;
                        lastRate.EndDate = value.Subtract(new TimeSpan(1, 0, 0, 0));
                        db.SaveChanges();
                    }
                }
                else
                {
                    _BeginDate = value;
                }
            }
        }
        public DateTime? EndDate { get; set; }
        public decimal Rate { get; set; }
    }
}
