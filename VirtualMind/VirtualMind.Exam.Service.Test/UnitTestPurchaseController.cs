using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using VirtualMind.Exam.Domain.Interface;
using VirtualMind.Exam.Service.API.Controllers;
using VirtualMind.Exam.Service.Entity.Model.Input;
using VirtualMind.Exam.Transversal;
using Xunit;

namespace VirtualMind.Exam.Service.Test
{
    public class UnitTestPurchaseController
    {
        [Fact]
        public async Task Save_InvalidCurrencyCode_fail()
        {
            // Arrange
            var _purchaseDomain = new Mock<InterfacePurchaseDomain>();
            var _logger = new Mock<ILogger<QuoteController>>();
            var _mapper = new Mock<IMapper>();

            var controller = new PurchaseController(_mapper.Object,_logger.Object, _purchaseDomain.Object);

            // Act
            PurchaseInput purchase = new PurchaseInput()
            {
               CurrencyCode = "XXX"
            };
            var actionResult = await controller.Purchase(purchase);
            var requestResult = actionResult as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, requestResult.StatusCode);
        }
    }
}
