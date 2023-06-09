using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProjectX.Entities.Resources
{
    public static class ResourcesManager
    {
        public static StatusCode getStatusCode(Languages language, StatusCodeValues statusCodeValues)
        {
            return new StatusCode
            {
                code = (int)statusCodeValues,
                message = ResourcesManager.getMessage(language, (int)statusCodeValues)
            };
        }

        public static StatusCode getStatusCode(string IdLanguage, StatusCodeValues statusCodeValues)
        {
            return new StatusCode
            {
                code = (int)statusCodeValues,
                message = ResourcesManager.getMessage((Languages)Convert.ToInt32(IdLanguage), (int)statusCodeValues)
            };
        }

        public static StatusCode getStatusCode(int IdLanguage, StatusCodeValues statusCodeValues)
        {
            return new StatusCode
            {
                code = (int)statusCodeValues,
                message = ResourcesManager.getMessage((Languages)IdLanguage, (int)statusCodeValues)
            };
        }

        public static string getStatusCodeJson(Languages language, StatusCodeValues statusCodeValues)
        {
            return JsonConvert.SerializeObject(new StatusCode
            {
                code = (int)statusCodeValues,
                message = ResourcesManager.getMessage(language, (int)statusCodeValues)
            });
        }

        private static string getMessage(Languages language, int value)
        {
            if (language == Languages.english)
                return Resources.CodeDescription_English.ResourceManager.GetString(value.ToString());
            else if (language == Languages.arabic)
                return Resources.CodeDescription_Arabic.ResourceManager.GetString(value.ToString());
            else
                return Resources.CodeDescription_English.ResourceManager.GetString(value.ToString());
        }

        public static StatusCode getStatusCode(Languages language, StatusCodeValues statusCodeValues, SuccessCodeValues successCodeValues, string msg)
        {
            StatusCode statusCode = new StatusCode
            {
                code = (int)statusCodeValues,
                message = ResourcesManager.getMessage(language, (int)statusCodeValues)
            };

            if (statusCodeValues == StatusCodeValues.success)
                statusCode.message = ResourcesManager.getSuccesMessage(language, (int)successCodeValues).Replace("%msg%", msg);

            return statusCode;
        }

        private static string getSuccesMessage(Languages language, int value)
        {
            if (language == Languages.english)
                return Resources.SuccessDescription_English.ResourceManager.GetString(value.ToString());
            else if (language == Languages.arabic)
                return Resources.SuccessDescription_Arabic.ResourceManager.GetString(value.ToString());
            else
                return Resources.SuccessDescription_English.ResourceManager.GetString(value.ToString());
        }
    }
}
