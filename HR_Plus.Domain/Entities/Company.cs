using HR_Plus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Plus.Domain.Entities
{
    public class Company:IBaseEntity
    {
        public int CompanyId { get; set; }
        public string Adress { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyEmail { get; set; }
        public string TaxIdentificationNumber { get; set; }

        // From IBase Entity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        //Navigation Prop
        public List<AppUser> AppUsers { get; set; }
    }
}
