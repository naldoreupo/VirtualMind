using System;
using System.Collections.Generic;

#nullable disable

namespace VirtualMind.Exam.Infraestructure.Entity
{
    public partial class Purchase
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
