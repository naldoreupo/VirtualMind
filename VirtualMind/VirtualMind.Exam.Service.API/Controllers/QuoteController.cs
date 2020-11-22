using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualMind.Exam.Domain.Entity;
using VirtualMind.Exam.Domain.Interface;
using VirtualMind.Exam.Transversal;

namespace VirtualMind.Exam.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly InterfaceQuoteDomain _quoteDomain;
        private readonly ILogger<QuoteController> _logger;
        public QuoteController(ILogger<QuoteController> logger
            , InterfaceQuoteDomain quoteDomain)
        {
            _quoteDomain = quoteDomain;
            _logger = logger;
        }

        [HttpGet("{currencyCode}")]
        public async Task<IActionResult> ExchangeRate(string currencyCode)
        {
            try
            {
                _logger.LogDebug($"Init GetExchangeRate {currencyCode} ");

                if (currencyCode == CurrencyConstants.CurrencyCanadianCode)
                    return new StatusCodeResult(StatusCodes.Status501NotImplemented);

                if (currencyCode != CurrencyConstants.CurrencyDolarCode && currencyCode != CurrencyConstants.CurrencyBrasilianCode)
                    return BadRequest($"{currencyCode} is not currently supported");

                var exchangeRate = await _quoteDomain.GetExchangeRate(currencyCode);

                if (exchangeRate.Status)
                    return Ok(exchangeRate);
                else
                    return NotFound();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> ExchangeRates()
        {
            try
            {
                _logger.LogDebug($"Init GetExchangeRate ");

                var exchangeRates = await _quoteDomain.GetAll();

                if (exchangeRates.Status)
                    return Ok(exchangeRates);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
