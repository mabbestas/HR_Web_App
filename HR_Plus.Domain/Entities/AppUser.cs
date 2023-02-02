using HR_Plus.Domain.Enum;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Plus.Domain.Entities
{
    public class AppUser : IdentityUser<int>, IBaseEntity
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public bool FirstLogin { get; set; }
        public bool WorkingSituation{ get; set; }

        // Navigation Prop
        public List<Permission> Permissions { get; set; }
        public List<AdvancePayment> AdvancePayments { get; set; }
        public List<Expense> Expenses { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public bool IsManager { get; set; }


    }
}
