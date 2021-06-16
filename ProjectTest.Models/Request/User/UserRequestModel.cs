using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTest.Models.Request
{
    public class UserRequestModel
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
