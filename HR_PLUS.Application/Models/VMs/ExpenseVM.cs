using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.VMs
{
    public class ExpenseVM
    {
        public int ExpenseId { get; set; }
        public string ExpenseDescription { get; set; }
        public DateTime ExpenseDate { get; set; }
        public double Amount { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateDate { get; set; }
        public int AppUserId { get; set; }
    }
}
