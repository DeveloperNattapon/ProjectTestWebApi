using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTest.Models;
using ProjectTest.Models.Request;
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
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost]
        [Route("SaveUserAccount")]
        public IActionResult SaveUserAccount([FromBody]UserRequestModel request)
        {
            
            IActionResult result = null;
            try
            {
                var account =   _userServices.SaveUserAccount(request) ;
                result = WrapResponse.ResponseOK(account);

            }
            catch (Exception ex)
            {

                result = WrapResponse.ResponseError(null,ex.Message);
            }
           
            return result;
        }

        [HttpPost]
        [Route("UserCheck")]
        public IActionResult UserCheck([FromBody] UserRequestModel request)
        {

            IActionResult result = null;
            try
            {
                var account = _userServices.UserCheck(request);
                result = WrapResponse.ResponseOK(account);

            }
            catch (Exception ex)
            {

                result = WrapResponse.ResponseError(null, ex.Message);
            }

            return result;
        }
    }
}
