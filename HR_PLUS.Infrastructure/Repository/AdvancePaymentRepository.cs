using HR_Plus.Domain.Entities;
using HR_Plus.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Infrastructure.Repository
{
    public class AdvancePaymentRepository : BaseRepository<AdvancePayment>, IAdvancePaymentRepository
    {
        public AdvancePaymentRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
