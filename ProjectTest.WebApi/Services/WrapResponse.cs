﻿using Microsoft.AspNetCore.Mvc;
using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.WebApi.Services
{
    public static class WrapResponse
    {
        public static ObjectResult ResponseOK(object data)
        {
            return new ObjectResult(new ResponseModel
            {
                data = data
            });
        }

        public static ObjectResult ResponseOK(object data, bool success, string message)
        {
            return new ObjectResult(new ResponseModel
            {
                data = data,
                status = 200,
                success = success,
                message = message,
            });
        }

        public static ObjectResult ResponseError(object data, string message, int statusCode = 200)
        {
            var obj = new ObjectResult(new ResponseModel
            {
                status = statusCode,
                success = false,
                message = message,
                data = data
            });
            obj.StatusCode = statusCode;
            return obj;
        }

        public static ObjectResult Response(ResponseModel data)
        {
            var obj = new ObjectResult(data);
            obj.StatusCode = data.status;
            return obj;
        }
    }
}
