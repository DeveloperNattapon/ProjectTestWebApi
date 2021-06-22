using ProjectTest.Models;
using ProjectTest.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.WebApi.Interfaces
{
    public interface IUserServices
    {
        public ResponseModel SaveUserAccount(UserRequestModel request);
        public ResponseModel UserCheck(UserRequestModel request);
    }
}
