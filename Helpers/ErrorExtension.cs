using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Personaltool.Models;

namespace Personaltool.Helpers
{
    public static class ErrorExtension {
        public static IActionResult StatusCodeMessage(this Controller controller, int statusCode, String message = null) {
            message ??= ((HttpStatusCode)statusCode).ToString();
            controller.HttpContext.Response.StatusCode = statusCode;
            return controller.View("Error", new ErrorViewModel { StatusCode = statusCode, ErrorMsg = message });
        }
    }
}