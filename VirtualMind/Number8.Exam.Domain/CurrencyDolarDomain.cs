using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VirtualMind.Exam.Domain.Interface;
using VirtualMind.Exam.Transversal;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using VirtualMind.Exam.Domain.Entity;

namespace VirtualMind.Exam.Domain
{
    public class CurrencyDolarDomain : InterfaceCurrencyDomain
    {
        private const string ServiceExchangeConversion = "https://www.bancoprovincia.com.ar/Principal/Dolar";
        private readonly ILogger<InterfaceCurrencyDomain> _logger; 

        public CurrencyDolarDomain(ILogger<InterfaceCurrencyDomain> logger)
        {
            _logger = logger;
        }
        public string CurrencyCode => CurrencyConstants.CurrencyDolarCode;
        public async Task<Response<ExchangeRate>> GetExchangeRate()
        {
            try
            {
                ExchangeRate exchangeRate = new ExchangeRate();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(ServiceExchangeConversion))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var exchangeRateResponse = JsonConvert.DeserializeObject<string[]>(apiResponse);
                        exchangeRate.BuyingRate = Convert.ToDouble(exchangeRateResponse[0]);
                        exchangeRate.SellingRate = Convert.ToDouble(exchangeRateResponse[1]);
                    }
                }
                return new Response<ExchangeRate>()
                {
                    Status = true,
                    Message = "Operation successfully completed",
                    Data = exchangeRate
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new Response<ExchangeRate>()
                {
                    Status = false,
                    Message = ex.Message
                };

            }
        }

    }
}
