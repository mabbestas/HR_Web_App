using HR_Plus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Plus.Domain.Entities
{
    public class Permission : IBaseEntity
    {
        public int PermissionId { get; set; }
        public string PermissionDescription { get; set; }        
        public DateTime PermissionStartDate { get; set; }      
        public DateTime PermissionExpiryDate { get; set; }

        // From IBase Entity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        //Navigation Prop
        public int PermissionTypeId { get; set; }
        public PermissionType PermissionType { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
    }
}
