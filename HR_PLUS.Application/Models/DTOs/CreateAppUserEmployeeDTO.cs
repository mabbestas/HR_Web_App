using HR_Plus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.DTOs
{
    public class CreateAppUserEmployeeDTO
    {
        [Required(ErrorMessage = "Must to type Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Must to type Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Must to type Adress")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Must to type Birth Date")]
        [DataType(DataType.Date, ErrorMessage = "Wrong format !!!!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Must to type Hire Date")]
        [DataType(DataType.Date, ErrorMessage = "Wrong format !!!!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }
        [Required(ErrorMessage = "Must to type Gender")]
        public Gender Gender { get; set; }
        public DateTime CreateDate { get; set; }
        public Status Status { get; set; }
        public bool FirstLogin { get; set; }
        [Required(ErrorMessage = "Must to type Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Must to type Email")]
        public string Email { get; set; }
        public bool IsManager { get; set; }
    }
}
