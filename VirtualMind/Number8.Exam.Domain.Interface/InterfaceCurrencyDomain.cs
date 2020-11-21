using VirtualMind.Exam.Transversal;
using System.Threading.Tasks;
using VirtualMind.Exam.Domain.Entity;

namespace VirtualMind.Exam.Domain.Interface
{
    public interface InterfaceCurrencyDomain
    {
        string CurrencyCode { get; }
        Task<Response<ExchangeRate>> GetExchangeRate();
    }
}
