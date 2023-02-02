using HR_Plus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Models.DTOs
{
    public class UpdateExpenseDTO
    {
        public int AppUserId { get; set; }
        public int ExpenseId { get; set; }

        [Required(ErrorMessage = "Must to type Description of Expense")]
        [MinLength(1, ErrorMessage = "Minimum lenght is 1")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50")]
        [Display(Name = "Description of Expense")]
        public string ExpenseDescription { get; set; }

        [Required(ErrorMessage = "Must to type Amount")]
        [Range(1, 10000, ErrorMessage = "Undefined Prepayment Limit. Max: 10.000")]
        public double Amount { get; set; }

        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Must to type Expense Date")]
        [Display(Name = "Expense Date")]
        [DataType(DataType.Date, ErrorMessage = "Wrong format !!!!")]
        public DateTime ExpenseDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
