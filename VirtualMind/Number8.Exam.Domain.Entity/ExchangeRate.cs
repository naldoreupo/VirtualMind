using System;

namespace VirtualMind.Exam.Domain.Entity
{
    public class ExchangeRate
    {
        public string CurrencyCode { get; set; }
        public double BuyingRate { get; set; }
        public double SellingRate { get; set; }
    }
}
