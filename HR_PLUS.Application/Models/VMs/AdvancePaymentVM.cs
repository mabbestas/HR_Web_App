using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.VMs
{
    public class AdvancePaymentVM
    {
        public int AdvancePaymentId { get; set; }
        public string AdvancePaymentDesc { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public int AppUserId { get; set; }
    }
}
