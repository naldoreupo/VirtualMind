using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exam.Domain.Entity;
using VirtualMind.Exam.Service.Entity.Model;
using VirtualMind.Exam.Transversal;

namespace VirtualMind.Exam.Domain.Interface
{
   public interface InterfacePurchaseDomain
    {
        Task<Response<PurchaseOutput>> SaveTransacction(PurchaseDTO purchaseDTO);
    }
}
