using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace LibraryManagement.Helpers
{
    public class CustomExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var res = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An Unexpected Error occured"),
                ReasonPhrase = "Internal Server Error"
            };
            actionExecutedContext.Response = res;
        }
    }
}