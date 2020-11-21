using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exam.Infraestructure.Entity;
using VirtualMind.Exam.Infraestructure.Interface;
using VirtualMind.Exam.Transversal;

namespace VirtualMind.Exam.Infraestructure
{
    public class PurchaseInfraestructure : BaseRepository<Purchase>, InterfacePurchaseInfraestructure
    {
        public async Task<Response<Boolean>> SaveTransaction(Purchase purchase)
        {
            return await Save(purchase); 
        }

    }
}
