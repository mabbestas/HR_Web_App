using HR_Plus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.DTOs
{
    public class CreateCompanyDTO
    {
        [Required(ErrorMessage = "Must to type Name")]
        public string CompanyName { get; set; }
      
        [Required(ErrorMessage = "Must to type Adress")]
        public string Adress { get; set; }
        
        [Required(ErrorMessage = "Must to type Phone Number")]
        public string PhoneNumber { get; set; }
       
        [Required(ErrorMessage = "Must to type Tax Identification Number")]
        public string TaxIdentificationNumber { get; set; }
       
        [Required(ErrorMessage = "Must to type Email")]
        public string CompanyEmail { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
