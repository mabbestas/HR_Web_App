using HR_Plus.Domain.Entities;
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
    public class CreatePermissionDTO
    {
        public int PermissionId { get; set; }

        [Required(ErrorMessage = "Must to type Permission Description")]
        [MinLength(1, ErrorMessage = "Minimum lenght is 1")]
        [MaxLength(250, ErrorMessage = "Maximum lenght is 250")]
        [Display(Name = "Permission Description")]
        public string PermissionDescription { get; set; }

        [Required(ErrorMessage = "Must to type Permission Start Date")]
        [Display(Name = "Permission Start Date")]
        [DataType(DataType.Date, ErrorMessage = "Wrong format !!!!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PermissionStartDate { get; set; }

        [Required(ErrorMessage = "Must to type Permission Expiry Date")]
        [Display(Name = "Permission Expiry Date")]
        [DataType(DataType.Date, ErrorMessage = "Wrong format !!!!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PermissionExpiryDate { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status => Status.Active;
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select Permission Type")]
        public int PermissionTypeId { get; set; }

        public List<PermissionTypeVM> PermissionTypes { get; set; }
    }
}
