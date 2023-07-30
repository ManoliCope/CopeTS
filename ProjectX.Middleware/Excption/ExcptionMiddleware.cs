using ProjectX.Business.Jwt;
using ProjectX.Entities.AppSettings;
using ProjectX.Repository.UserRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectX.Entities.dbModels;
using ProjectX.Entities;
using ProjectX.Business.User;
using ProjectX.Entities.Resources;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Diagnostics;
using NLog;
using System.Net.Http;
using System.Net;

namespace ProjectX.Middleware.Excption
{
    public class ExcptionMiddleware
    {
        private readonly TrAppSettings _appSettings;
        private readonly RequestDelegate _next;
        private readonly ILogger<ExcptionMiddleware> _logger;
        private Stopwatch stopwatch;
        private string IP = string.Empty;
        public TR_Users _user;

        public ExcptionMiddleware(RequestDelegate next, IOptions<TrAppSettings> appIdentitySettingsAccessor, ILogger<ExcptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _appSettings = appIdentitySettingsAccessor.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            Exception _ex = null;
            string RequestPath = context.Request.Path.Value;
            //string jsonResponse = string.Empty; /product/
            //Stream originBody = null;
            _user = (TR_Users)context.Items["User"];

            try
            {
                if (context.Request.ContentType != null && context.Request.ContentType.Contains("multipart/form-data"))
                {
                    var files = context.Request.Form.Files;
                }

                //Stream stream = context.Request.Body;
                //originBody = context.Response.Body;

                stopwatch = new Stopwatch();
                stopwatch.Start();

                //context.Response.Body = new MemoryStream();

                //string _originalContent = string.Empty;
                //_originalContent = new StreamReader(stream).ReadToEnd();

                MappedDiagnosticsLogicalContext.Set("RequestMethod", context.Request.Method);
                MappedDiagnosticsLogicalContext.Set("RequestContentType", context.Request.ContentType);
                MappedDiagnosticsLogicalContext.Set("RequestPath", RequestPath);
                //MappedDiagnosticsLogicalContext.Set("RequestBody", _originalContent);
                MappedDiagnosticsLogicalContext.Set("BaseLink", string.Concat(context.Request.Scheme, "://", context.Request.Host.ToUriComponent()) + context.Request.PathBase.ToUriComponent());

                //var requestContent = new StringContent(_originalContent);
                //context.Request.Body = await requestContent.ReadAsStreamAsync();
                //var newBody = new MemoryStream();
                //context.Response.Body = newBody;

                //if (context.Request.ContentType != null && !context.Request.ContentType.Contains("multipart/form-data"))
                //    context.Request.ContentType = "application/json";

                if (_user != null)
                    MappedDiagnosticsLogicalContext.Set("Username", _user.U_User_Name);

                await _next(context);

                //if (context.Response.StatusCode != (int)HttpStatusCode.OK)
                //{
                //    //redirect
                //    //jsonResponse = ResourcesManager.getStatusCodeJson(Entities.Language.English, Entities.StatusCodeValues.ServerError);
                //}
                //else
                //{
                //    var originalResponse = context.Response.Body;
                //    originalResponse.Seek(0, SeekOrigin.Begin);
                //    jsonResponse = new StreamReader(originalResponse).ReadToEnd();
                //}
            }
            catch (Exception ex)
            {
                _ex = ex;
                MappedDiagnosticsLogicalContext.Set("Exception", ex.Message + Environment.NewLine + ex.StackTrace);
                StackFrame stackFrame = new StackTrace(ex, true).GetFrame(0);
                MappedDiagnosticsLogicalContext.Set("Stacktrace", string.Join(" - ", stackFrame.GetMethod().Name, string.Concat("Line: ", stackFrame.GetFileLineNumber().ToString())));

                //jsonResponse = ResourcesManager.getStatusCodeJson(Entities.Language.English, Entities.StatusCodeValues.ServerError);
            }
            finally
            {
                //if (RequestPath.ToLower().Contains("export"))
                //    context.Response.ContentType = "text/csv";
                //else
                //    context.Response.ContentType = "application/json";
                //context.Response.Body = originBody;

                MappedDiagnosticsLogicalContext.Set("IP", IP);
                stopwatch.Stop();
                MappedDiagnosticsLogicalContext.Set("ExecutionTime", stopwatch.ElapsedMilliseconds.ToString());
                //MappedDiagnosticsLogicalContext.Set("Response", jsonResponse);

                if (_ex != null)
                {
                    _logger.LogError(_ex, "REQUEST/RESPONSE");
                    context.Response.Redirect(@"/error");
                }
                else
                {
                    if (!_appSettings.WebsiteLogging.OnlyExceptions)
                        _logger.LogInformation("REQUEST/RESPONSE");
                }
            }
        }
    }
}