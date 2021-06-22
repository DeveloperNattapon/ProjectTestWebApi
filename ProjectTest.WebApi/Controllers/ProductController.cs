using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectTest.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [HttpGet]
        [Route("GetProduct")]
        public IActionResult GetProduct()
        {
            IActionResult result = null;
            var product =_productServices.GetProduct() ;
            result = WrapResponse.ResponseOK(product);

            return result;
        }
    }
}
