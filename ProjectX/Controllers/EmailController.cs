using Microsoft.AspNetCore.Mvc;
using ProjectX.Entities.Models.Emails;

namespace ProjectX.Controllers
{
	public class EmailController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
        public SendEmailResp SendEmail(SendEmailReq req, int IdCaseEmailOption)
        {
            //SendEmailResp response = new SendEmailResp();

            //CaseEmailOptions caseEmailOptions = (Entities.CaseEmailOptions)IdCaseEmailOption;

            //if (!Enum.IsDefined(typeof(CaseEmailOptions), caseEmailOptions) || ValueChecker.IsNullValue(req.idCase))
            //{
            //    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
            //    return response;
            //}

            //req.userName = _user.UserFullName;
            //return _caseBusiness.SendEmail(req, _user.UserId, caseEmailOptions);
            return null;
        }

    }
}
