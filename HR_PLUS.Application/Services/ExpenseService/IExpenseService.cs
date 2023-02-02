using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.ExpenseService
{
    public interface IExpenseService
    {
        Task Create(CreateExpenseDTO model);
        Task<List<ExpenseVM>> GetExpenses(int CurrentUserId);
        Task Delete(int id);
        Task Update(UpdateExpenseDTO model);
        Task<UpdateExpenseDTO> GetById(int id);
        Task<List<ExpenseVM>> GetExpenseTake(int take, int CurrentUserId);
    }
}
