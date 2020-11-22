using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualMind.Exam.Domain.Entity;
using VirtualMind.Exam.Domain.Interface;
using VirtualMind.Exam.Service.Entity.Model.Input;
using VirtualMind.Exam.Transversal;
using Newtonsoft;

namespace VirtualMind.Exam.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly InterfacePurchaseDomain _purchaseDomain;
        private readonly ILogger<QuoteController> _logger;
        private readonly IMapper _mapper;
        public PurchaseController(IMapper mapper, ILogger<QuoteController> logger
            , InterfacePurchaseDomain purchaseDomain)
        {
            _purchaseDomain = purchaseDomain;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Purchase([FromBody] PurchaseInput purchase)
        {
            try
            {
                _logger.LogDebug($"Init Purchase {purchase.CurrencyCode} ");

                if (purchase.CurrencyCode == CurrencyConstants.CurrencyCanadianCode)
                    return new StatusCodeResult(StatusCodes.Status501NotImplemented);

                if (purchase.CurrencyCode != CurrencyConstants.CurrencyDolarCode
                    && purchase.CurrencyCode != CurrencyConstants.CurrencyBrasilianCode)
                    return BadRequest($"{purchase.CurrencyCode} is not currently supported");

                var isvalidAmount = await _purchaseDomain.ValidateMaxLimit(_mapper.Map<PurchaseDTO>(purchase));

                if (!isvalidAmount)
                    return BadRequest($"Limit for {purchase.CurrencyCode} exceeded");

                var purchaseDTO = await _purchaseDomain.SaveTransacction(_mapper.Map<PurchaseDTO>(purchase));

                return Ok(purchaseDTO);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
