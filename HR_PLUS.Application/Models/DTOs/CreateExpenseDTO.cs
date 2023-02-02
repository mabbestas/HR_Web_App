using HR_Plus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.DTOs
{
    public class CreateExpenseDTO
    {
        public int ExpenseId { get; set; }

        [Required(ErrorMessage = "Must to type Description of Expense")]
        [MinLength(1, ErrorMessage = "Minimum lenght is 1")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50")]
        [Display(Name = "Description of Expense")]
        public string ExpenseDescription { get; set; }

        [Required(ErrorMessage = "Must to type Amount")]
        [Range(1, 10000, ErrorMessage = "Undefined Prepayment Limit. Max: 10.000")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Must to type Expense Date")]
        [Display(Name = "Expense Date")]
        [DataType(DataType.Date, ErrorMessage = "Wrong format !!!!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpenseDate { get; set; }

        public string ImagePath { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;

        public int AppUserId { get; set; }
    }
}
