using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.VMs
{
    public class GetEmployeesVM
    {
        public List<EmployeesVM> EmployeesActive { get; set; }
        public List<EmployeesVM> EmployeesInactive { get; set; }
    }
}
