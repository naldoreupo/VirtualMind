using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using VirtualMind.Exam.Domain.Entity;
using VirtualMind.Exam.Domain.Interface;
using VirtualMind.Exam.Transversal;

namespace VirtualMind.Exam.Domain
{
    public class CurrencyBrasilianDomain : InterfaceCurrencyDomain
    {
        private const string ServiceExchangeConversion = "https://www.bancoprovincia.com.ar/Principal/Dolar";
        private readonly ILogger<InterfaceCurrencyDomain> _logger;
        public CurrencyBrasilianDomain(ILogger<InterfaceCurrencyDomain> logger)
        {
            _logger = logger;
        }

        public string CurrencyCode => CurrencyConstants.CurrencyBrasilianCode;

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
                        exchangeRate.CurrencyCode = this.CurrencyCode;
                        exchangeRate.BuyingRate = Convert.ToDouble(exchangeRateResponse[0]) / 4;
                        exchangeRate.SellingRate = Convert.ToDouble(exchangeRateResponse[1]) / 4;
                    }
                }

                return new Response<ExchangeRate>()
                {
                    Status = true,
                    Message = "Operation successfully completed",
                    Data = exchangeRate
                };
            }
            catch (Exception ex)
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
