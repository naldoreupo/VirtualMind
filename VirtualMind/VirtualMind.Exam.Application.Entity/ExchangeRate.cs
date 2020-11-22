using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualMind.Exam.Application.Entity
{
    public class ExchangeRate
    {
        public string CurrencyCode { get; set; }
        public double BuyingRate { get; set; }
        public double SellingRate { get; set; }
    }
}
