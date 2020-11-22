using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exam.Domain.Entity;
using VirtualMind.Exam.Domain.Interface;
using VirtualMind.Exam.Transversal;

namespace VirtualMind.Exam.Domain
{
    public class QuoteDomain : InterfaceQuoteDomain
    {
        private readonly InterfaceCurrencyFactoryDomain _currencyFactoryDomain;
        private readonly ILogger<QuoteDomain> _logger;
        public QuoteDomain(ILogger<QuoteDomain> logger
            , InterfaceCurrencyFactoryDomain currencyFactoryDomain)
        {
            _currencyFactoryDomain = currencyFactoryDomain;
            _logger = logger;
        }

        public async Task<Response<ExchangeRate>> GetAll()
        {
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();
            string[] currencyCodes = new string[] { CurrencyConstants.CurrencyBrasilianCode, CurrencyConstants.CurrencyDolarCode };

            foreach (var currencyCode in currencyCodes)
            {
                var _currencyDomain = _currencyFactoryDomain.Build(currencyCode);
                var exchangeRate = await _currencyDomain.GetExchangeRate();
                if (exchangeRate.Status)
                    exchangeRates.Add(exchangeRate.Data);
            }

            return new Response<ExchangeRate>()
            {
                Status = true,
                List = exchangeRates
            };
        }

        public async Task<Response<ExchangeRate>> GetExchangeRate(string currencyCode)
        {
            var _currencyDomain = _currencyFactoryDomain.Build(currencyCode);
            var exchangeRate = await _currencyDomain.GetExchangeRate();

            return new Response<ExchangeRate>()
            {
                Status = true,
                Data = exchangeRate.Data
            };
        }
    }
}
