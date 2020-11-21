using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualMind.Exam.Domain.Entity
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }
        public double ExchangeRate { get; set; }
        public double ExchangeAmount { get; set; }
        public bool? IsActive { get; set; }
    }
}
