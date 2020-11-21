using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualMind.Exam.Domain.Interface;
using VirtualMind.Exam.Transversal;

namespace VirtualMind.Exam.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly InterfaceCurrencyFactoryDomain _currencyFactoryDomain;
        private readonly ILogger<CurrencyController> _logger;
        public CurrencyController(ILogger<CurrencyController> logger
            , InterfaceCurrencyFactoryDomain currencyFactoryDomain)
        {
            _currencyFactoryDomain = currencyFactoryDomain;
            _logger = logger;
        }

        [HttpGet("ExchangeRate/{currencyCode}")]
        public async Task<IActionResult> ExchangeRate(string currencyCode)
        {
            try
            {
                _logger.LogDebug($"Init GetExchangeRate {currencyCode} ");

                if (currencyCode == CurrencyConstants.CurrencyCanadianCode)
                    return new StatusCodeResult(StatusCodes.Status501NotImplemented);

                if (currencyCode != CurrencyConstants.CurrencyDolarCode && currencyCode != CurrencyConstants.CurrencyBrasilianCode)
                    return BadRequest($"{currencyCode} is not currently supported");

                var _currencyDomain = _currencyFactoryDomain.Build(currencyCode);
                var exchangeRate = await _currencyDomain.GetExchangeRate();

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

    }
}
