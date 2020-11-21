using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using VirtualMind.Exam.Domain.Interface;
using VirtualMind.Exam.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualMind.Exam.Domain
{
    public class CurrencyFactoryDomain : InterfaceCurrencyFactoryDomain
    {
        private readonly ILogger<InterfaceCurrencyFactoryDomain> _logger;
        private readonly IEnumerable<InterfaceCurrencyDomain> _currencyDomains;
        public CurrencyFactoryDomain(ILogger<InterfaceCurrencyFactoryDomain> logger
            , IEnumerable<InterfaceCurrencyDomain> currencyDomains)
        {
            _logger = logger;
            _currencyDomains = currencyDomains;
        }
        public InterfaceCurrencyDomain Build(string currencyCode)
        {
            try
            {
            return _currencyDomains.SingleOrDefault(currency => currency.CurrencyCode == currencyCode);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
