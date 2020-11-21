using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exam.Domain.Entity;
using VirtualMind.Exam.Transversal;
using VirtualMind.Exam.Infraestructure;
using VirtualMind.Exam.Infraestructure.Entity;

namespace VirtualMind.Exam.Infraestructure.Interface
{
    public interface InterfacePurchaseInfraestructure
    {
        Task<Response<Boolean>> SaveTransaction(Purchase purchase);
    }
}
