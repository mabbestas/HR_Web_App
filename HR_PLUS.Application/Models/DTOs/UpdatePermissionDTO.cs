using HR_Plus.Domain.Enum;
using HR_PLUS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.DTOs
{
    public class UpdatePermissionDTO
    {
        public int EmployeeId { get; set; }
        public int PermissionId { get; set; }

        [Required(ErrorMessage = "Must to type Permission Description")]
        [MinLength(1, ErrorMessage = "Minimum lenght is 1")]
        [MaxLength(250, ErrorMessage = "Maximum lenght is 250")]
        [Display(Name = "Permission Description")]
        public string PermissionDescription { get; set; }

        [Required(ErrorMessage = "Must to type Permission Start Date")]
        [Display(Name = "Permission Start Date")]
        [DataType(DataType.Date, ErrorMessage = "Wrong format !!!!")]        
        public DateTime PermissionStartDate { get; set; }

        [Required(ErrorMessage = "Must to type Permission Expiry Date")]
        [Display(Name = "Permission Expiry Date")]
        [DataType(DataType.Date, ErrorMessage = "Wrong format !!!!")]       
        public DateTime PermissionExpiryDate { get; set; }

        public DateTime? UpdateDate => DateTime.Now;
        public DateTime CreateDate { get; set; }
        public Status Status => Status.Modified;

        [Range(1, int.MaxValue, ErrorMessage = "Please select Permission Type")]
        public int PermissionTypeId { get; set; }

        public int AppUserId { get; set; }
        public List<PermissionTypeVM> PermissionTypes { get; set; }
    }
}
