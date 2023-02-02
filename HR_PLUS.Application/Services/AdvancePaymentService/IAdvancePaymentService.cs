using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.AdvancePaymentService
{
    public interface IAdvancePaymentService
    {
        Task Create(CreateAdvancePaymentDTO model);
        Task<List<AdvancePaymentVM>> GetAdvancePayments(int CurrentUserId);
        Task Delete(int id);
        Task Update(UpdateAdvancePaymentDTO model);
        Task<UpdateAdvancePaymentDTO> GetById(int id);
        Task<List<AdvancePaymentVM>> GetAdvancePaymentTake(int take, int CurrentUserId);
    }
}
