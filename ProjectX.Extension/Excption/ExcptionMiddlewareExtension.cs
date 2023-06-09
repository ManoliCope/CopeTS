using ProjectX.Middleware.Excption;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Extension.Excption
{
    public static class ExcptionMiddlewareExtension
    {
        public static IApplicationBuilder UseExcptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExcptionMiddleware>();
        }
    }
}
