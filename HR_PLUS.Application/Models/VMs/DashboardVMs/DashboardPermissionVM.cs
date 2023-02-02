using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.VMs.DashboardVMs
{
    public class DashboardVM
    {
        public List<PermissionVM> Permissions { get; set; }
        public List<AdvancePaymentVM> AdvancePayments { get; set; }
        public List<ExpenseVM>Expenses { get; set; }
    }
}
