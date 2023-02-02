using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.DTOs
{
    public class UpdatePasswordDTO
    {        
            [Display(Name = "Yeni Şifre")]
            [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }     
    }
}
