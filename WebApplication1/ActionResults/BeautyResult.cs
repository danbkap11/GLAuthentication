using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.ActionResults
{
    public class BeautyResult : IActionResult
    {
        public string InnerHtml { set; get; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            string html = $@"
                <!DOCTYPE html><html><head>
                <title>Главная страница</title>
                <meta charset=utf-8 />
                </head> <body> {InnerHtml} </body></html>";

            var bytes = Encoding.UTF8.GetBytes(html);
            await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }

    class RedBeautyFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result as BeautyResult;
            result.InnerHtml = $"<div style='color: red'>{result.InnerHtml}</div>";
        }

        public void OnResultExecuted(ResultExecutedContext context) { }
    }

    public sealed class RedBeautyAttribute : Attribute, IFilterFactory
    {
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) =>
            new RedBeautyFilter();

        public bool IsReusable => false;
    }

    public class ForStudentFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Claims.Any(c => c.Type == "studentId"))
            {
                context.Result = new ForbidResult();
            }
        }
    }

    public class ForStudentAttribute : Attribute, IFilterFactory
    {
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) =>
            new ForStudentFilter();

        public bool IsReusable => false;
    }   
}
