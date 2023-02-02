using AutoMapper;
using HR_Plus.Domain.Entities;
using HR_Plus.Domain.Enum;
using HR_Plus.Domain.Repositories;
using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.ExpenseService
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateExpenseDTO model)
        {
            Expense expense = _mapper.Map<Expense>(model);
            await _expenseRepository.Create(expense);
        }

        public async Task Delete(int id)
        {
            Expense expense = await _expenseRepository.GetDefault(x => x.ExpenseId == id);
            expense.DeleteDate = DateTime.Now;
            expense.Status = Status.Passive;
            await _expenseRepository.Delete(expense);
        }

        public async Task<UpdateExpenseDTO> GetById(int id)
        {
            Expense expense = await _expenseRepository.GetDefault(x => x.ExpenseId == id);
            var model = _mapper.Map<UpdateExpenseDTO>(expense);
            return model;
        }

        public async Task<List<ExpenseVM>> GetExpenses(int CurrentUserId)
        {
            var expenses = await _expenseRepository.GetFilteredList(
                select: x => new ExpenseVM
                {
                    ExpenseId = x.ExpenseId,
                    ExpenseDescription = x.ExpenseDescription,
                    ExpenseDate = x.ExpenseDate,
                    Amount = x.Amount,
                    ImagePath = x.ImagePath
                },
                where: x => x.Status != Status.Passive && x.AppUserId == CurrentUserId,
                orderBy: x => x.OrderBy(x => x.ExpenseId)
                );
            return expenses;
        }

        public async Task<List<ExpenseVM>> GetExpenseTake(int take, int CurrentUserId)
        {
            var expenses = await _expenseRepository.GetFilteredList(
                select: x => new ExpenseVM
                {
                    ExpenseId = x.ExpenseId,
                    ExpenseDescription = x.ExpenseDescription,
                    ExpenseDate = x.ExpenseDate,
                    Amount = x.Amount,
                    ImagePath = x.ImagePath
                },
                where: x => x.Status != Status.Passive && x.AppUserId == CurrentUserId,
                orderBy: x => x.Take(take).OrderByDescending(x => x.ExpenseId)
                );
            return expenses;
        }

        public async Task Update(UpdateExpenseDTO model)
        {
            var expense = _mapper.Map<Expense>(model);
            await _expenseRepository.Update(expense);
        }
    }
}
