using ProjectX.Business.Jwt;
using ProjectX.Entities.AppSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectX.Entities.dbModels;
using ProjectX.Entities;
using ProjectX.Business.Users;
using ProjectX.Entities.Resources;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using ProjectX.Entities.bModels;
using ProjectX.Business.General;
using ProjectX.Entities.Models;

namespace ProjectX.Middleware.Jwt
{
    public class JwtMiddleware
    {
        private readonly TrAppSettings _appSettings;
        private readonly RequestDelegate _next;
        private IJwtBusiness _jwtbusiness;
        private IUsersBusiness _userBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness, IJwtBusiness jwtbusiness, IUsersBusiness userBusiness/*, IRouter router*/, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _jwtbusiness = jwtbusiness;
            _userBusiness = userBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //if (!context.Request.Path.Value.Contains("ProjectX"))
            //{
            //    context.Request.Path = "/ProjectX" + context.Request.Path;
            //    context.Response.Redirect(context.);
            //}

            string Controller = string.Empty;
            string Action = string.Empty;
            try
            {
                var routeData = context.GetRouteData();
                Controller = routeData?.Values["controller"]?.ToString().ToLower();
                Action = routeData?.Values["action"]?.ToString().ToLower();

                if (!string.IsNullOrEmpty(Controller))
                {
                    if (Controller == "login" || Controller == "content" || Controller == "errorf" || Controller == "user" || Action == "display" || Action == "drawpdf")
                    {
                        if (Controller == "login")
                            context.Response.Cookies.Delete("token");
                        await _next(context);
                    }
                    else
                    {
                        string token = context.Request.Cookies["token"].ToString();

                        if (string.IsNullOrEmpty(token))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return;
                        }
                        else
                        {
                            CookieUser cookieUser = _jwtbusiness.getUserFromToken(token, _appSettings.jwt);
                            if (cookieUser != null)
                            {
                                var user = _userBusiness.GetUserAuth(new Entities.Models.Users.GetUserReq
                                {
                                    idUser = cookieUser.UserId
                                }).user;

                                if (user != null)
                                {
                                    // save to cookie 
                                    bool isPersistent = true;

                                    CookieOptions options = new CookieOptions
                                    {
                                        Secure = false
                                    };

                                    if (isPersistent)
                                        options.Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_appSettings.jwt.ExpiryInMinutes));
                                    else
                                        options.Expires = DateTime.UtcNow.AddMilliseconds(1);

                                    context.Response.Cookies.Append("token", cookieUser.refreshedtoken, options);
                                    context.Items["User"] = user;
                                    context.Items["Userid"] = user.U_Id;
                                    context.Items["Username"] = user.U_First_Name + " " + user.U_Last_Name;

                                }
                                else
                                {
                                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                    return;
                                }
                            }
                            else
                            {
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                return;
                            }
                        }

                        if (Controller == "avaya")
                        {
                            string ParmPath = context.Request.QueryString.Value;

                            if (context.Request.Host.Host.Contains("localhost"))
                                context.Response.Redirect("/Home" + ParmPath);
                            else
                                context.Response.Redirect("/Home" + ParmPath);

                            return;
                        }




                        await _next(context);
                    }
                }
                else
                {
                    //context.Response.Redirect(@"/login");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "REQUEST/RESPONSE");

                loggertest logger = new loggertest();
                logger.Log("Error middleware: " + ex.Message);
                logger.Log("Error middleware: " + ex.StackTrace.ToString());

                var logData = new LogData
                {
                    Timestamp = DateTime.UtcNow,
                    Controller = Controller,
                    Action = Action,
                    ErrorMessage = ex.Message,
                    Type = "Error", 
                    Message = "Additional error message",
                    RequestPath = context.Request.Path,
                    Response = "Response content",
                    Exception = ex.ToString(), 
                    ExecutionTime = 0, 
                    Userid = ((TR_Users)context.Items["User"]).U_Id 
                };

                _generalBusiness.LogErrorToDatabase(logData);


                context.Response.StatusCode = -1000;

                string ttt = Controller;
                string aaa = Action;

                //context.Response.Redirect("/");
                context.Response.Redirect("/login");
            }
        }
    }
}
