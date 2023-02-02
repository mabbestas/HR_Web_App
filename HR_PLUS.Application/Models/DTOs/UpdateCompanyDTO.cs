using HR_Plus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.DTOs
{
    public class UpdateCompanyDTO
    {
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "Must to type Company Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Must to type Address")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Must to type Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Must to type Email")]
        public string CompanyEmail { get; set; }
        [Required(ErrorMessage = "Must to type Tax Identification Number")]
        public string TaxIdentificationNumber { get; set; }
        public DateTime? UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
