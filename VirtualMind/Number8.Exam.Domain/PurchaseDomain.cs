using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exam.Domain.Entity;
using VirtualMind.Exam.Domain.Interface;
using VirtualMind.Exam.Infraestructure.Entity;
using VirtualMind.Exam.Infraestructure.Interface;
using VirtualMind.Exam.Service.Entity.Model;
using VirtualMind.Exam.Transversal;

namespace VirtualMind.Exam.Domain
{
    public class PurchaseDomain : InterfacePurchaseDomain
    {
        private readonly InterfaceCurrencyFactoryDomain _currencyFactoryDomain;
        private readonly ILogger<InterfacePurchaseDomain> _logger;
        private readonly InterfacePurchaseInfraestructure _purchaseInfraestructure;
        private readonly IMapper _mapper;

        public PurchaseDomain(IMapper mapper, ILogger<InterfacePurchaseDomain> logger
            , InterfacePurchaseInfraestructure purchaseInfraestructure
            , InterfaceCurrencyFactoryDomain currencyFactoryDomain)
        {
            _logger = logger;
            _mapper = mapper;
            _purchaseInfraestructure = purchaseInfraestructure;
            _currencyFactoryDomain = currencyFactoryDomain;
        }
        public async Task<Response<PurchaseOutput>> SaveTransacction(PurchaseDTO purchaseDTO)
        {
            try
            {

                var _currencyDomain = _currencyFactoryDomain.Build(purchaseDTO.CurrencyCode);
                var exchangeRate = await _currencyDomain.GetExchangeRate();

                purchaseDTO.ExchangeRate = exchangeRate.Data.BuyingRate;
                purchaseDTO.ExchangeAmount = purchaseDTO.Amount / purchaseDTO.ExchangeRate;

                await _purchaseInfraestructure.SaveTransaction(_mapper.Map<Purchase>(purchaseDTO));

                return new Response<PurchaseOutput>()
                {
                    Status = true,
                    Data = new PurchaseOutput() { PurchasedAmount = purchaseDTO.ExchangeAmount }
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public Task<bool> ValidateMaxLimit(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO.CurrencyCode == CurrencyConstants.CurrencyDolarCode && purchaseDTO.Amount > 200)
                return Task.FromResult(false);

            if (purchaseDTO.CurrencyCode == CurrencyConstants.CurrencyBrasilianCode && purchaseDTO.Amount > 300)
                return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}
