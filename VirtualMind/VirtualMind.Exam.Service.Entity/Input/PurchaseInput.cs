using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualMind.Exam.Service.Entity.Model.Input
{
    public class PurchaseInput
    {
        public int UserId { get; set; }
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}
