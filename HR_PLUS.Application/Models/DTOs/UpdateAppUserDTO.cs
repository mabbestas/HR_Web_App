using HR_Plus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.DTOs
{
    public class UpdateAppUserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Must to type Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Must to type Address")]
        public string Address { get; set; }
        public string Email { get; set; }       
        public DateTime? UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
        //public bool EmailConfirmed => true;
        //public bool PhoneNumberConfirmed => true;
        //public bool TwoFactorEnabled => true;
        //public bool LockoutEnabled => true;
        //public int AccessFailedCount => 0;


    }
}
