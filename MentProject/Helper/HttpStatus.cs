using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MentProject.Helper
{
    public static class HttpStatus
    {
        public static HttpStatusCodeResult BadStatus()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}