using VirtualMind.Exam.Transversal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMind.Exam.Domain.Interface
{
    public interface InterfaceCurrencyFactoryDomain
    {
        InterfaceCurrencyDomain Build(string currencyCode);
    }
}
