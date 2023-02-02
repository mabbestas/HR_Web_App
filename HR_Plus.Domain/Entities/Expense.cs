using HR_Plus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Plus.Domain.Entities
{
    public class Expense : IBaseEntity
    {
        public int ExpenseId { get; set; }
        public string ExpenseDescription { get; set; }
        public DateTime ExpenseDate { get; set; }
        public double Amount { get; set; }
        public string ImagePath { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        //Nav Prop.
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
