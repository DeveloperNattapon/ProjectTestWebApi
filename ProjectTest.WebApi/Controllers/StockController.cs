using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTest.Models;
using ProjectTest.WebApi.Interfaces;
using ProjectTest.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockServices _stockServices;

        public StockController(IStockServices stockServices)
        {
            _stockServices = stockServices;
        }

        [HttpGet]
        [Route("GetStock")]
        public IActionResult GetStock()
        {
            IActionResult result = null;
            var stock = new { stock = _stockServices.GetStock() };
            result = WrapResponse.ResponseOK(stock);

            return result;
        }

        [HttpPost]
        [Route("AddStock")]
        public IActionResult AddStock([FromBody]StockEntity request) 
        {
            IActionResult result = null;
            var stock = new { stock = _stockServices.AddStock(request) };
            result = WrapResponse.ResponseOK(stock);

            return result;
        }
    }
}
