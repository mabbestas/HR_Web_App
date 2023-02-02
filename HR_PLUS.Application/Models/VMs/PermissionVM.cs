using HR_Plus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.VMs
{
    public class PermissionVM
    {
        public int PermissionId { get; set; }        
        public string PermissionTypeName { get; set; }
        public string PermissionDescription { get; set; }
        public DateTime PermissionStartDate { get; set; }
        public DateTime PermissionExpiryDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int PermissionTypeId { get; set; }
        public string FullName { get; set; }
    }
}
