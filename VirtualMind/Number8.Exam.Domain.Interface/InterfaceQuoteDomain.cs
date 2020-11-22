using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exam.Domain.Entity;
using VirtualMind.Exam.Transversal;

namespace VirtualMind.Exam.Domain.Interface
{
    public interface InterfaceQuoteDomain
    {
        Task<Response<ExchangeRate>> GetExchangeRate(string currencyCode);
        Task<Response<ExchangeRate>> GetAll();
    }
}
