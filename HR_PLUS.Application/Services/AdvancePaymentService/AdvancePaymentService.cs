using AutoMapper;
using HR_Plus.Domain.Entities;
using HR_Plus.Domain.Enum;
using HR_Plus.Domain.Repositories;
using HR_PLUS.Application.Models.DTOs;
using HR_PLUS.Application.Models.VMs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.Services.AdvancePaymentService
{
    public class AdvancePaymentService : IAdvancePaymentService
    {
        private readonly IAdvancePaymentRepository _advancePaymentRepository;
        private readonly IMapper _mapper;

        public AdvancePaymentService(IAdvancePaymentRepository advancePaymentRepository, IMapper mapper)
        {
            _advancePaymentRepository = advancePaymentRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateAdvancePaymentDTO model)
        {
            AdvancePayment advancepayment = _mapper.Map<AdvancePayment>(model);
            await _advancePaymentRepository.Create(advancepayment);
        }
        public async Task Delete(int id)
        {
            AdvancePayment advancePayment = await _advancePaymentRepository.GetDefault(x => x.AdvancePaymentId == id);
            advancePayment.DeleteDate = DateTime.Now;
            advancePayment.Status = Status.Passive;
            await _advancePaymentRepository.Delete(advancePayment);
        }

        public async Task<List<AdvancePaymentVM>> GetAdvancePayments(int CurrentUserId)
        {
            var advancepayments = await _advancePaymentRepository.GetFilteredList(
        select: x => new AdvancePaymentVM
        {
            AdvancePaymentId = x.AdvancePaymentId,
            AdvancePaymentDesc = x.AdvancePaymentDesc,
            Amount = x.Amount,
            CreateDate = x.CreateDate
        },
        where: x => x.Status != Status.Passive && x.AppUserId == CurrentUserId,
        orderBy: x => x.OrderBy(x => x.AdvancePaymentId)
         );
            return advancepayments;
        }

        public async Task<UpdateAdvancePaymentDTO> GetById(int id)
        {
            AdvancePayment advancePayment = await _advancePaymentRepository.GetDefault(x => x.AdvancePaymentId == id);

            var model = _mapper.Map<UpdateAdvancePaymentDTO>(advancePayment);
            return model;
        }

        public async Task Update(UpdateAdvancePaymentDTO model)
        {
            var advancepayment = _mapper.Map<AdvancePayment>(model);
            await _advancePaymentRepository.Update(advancepayment);
        }

        public async Task<List<AdvancePaymentVM>> GetAdvancePaymentTake(int take, int CurrentUserId)
        {
            var advancePayments = await _advancePaymentRepository.GetFilteredList(
            select: x => new AdvancePaymentVM
            {
                CreateDate = x.CreateDate,
                AdvancePaymentDesc = x.AdvancePaymentDesc,
                Amount = x.Amount
            },
            where: x => x.Status != Status.Passive && x.AppUserId == CurrentUserId,
            orderBy: x => x.Take(take).OrderByDescending(x => x.AdvancePaymentId)
            );
            return advancePayments;
        }
    }
}

