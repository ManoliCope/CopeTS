using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Business.Email
{
    public interface IEmailBusiness
    {
        public void ToSendEmail(object dataObject, string emailTemplateCode, byte[] attachment);
        public Task<bool> SendPolicyByEmail(string to, string cc, string emailTemplateCode, byte[] attachment, Entities.Models.Production.PolicyDetail? dataObject, int userID);
    }
}
