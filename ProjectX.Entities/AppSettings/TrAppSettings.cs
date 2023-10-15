using System;

namespace ProjectX.Entities.AppSettings
{
    public class TrAppSettings
    {
        public ConnectionStrings connectionStrings { get; set; }
        public Jwt jwt { get; set; }
        public WebPagesHosting webPagesHosting { get; set; }
        public EmailSettings emailSettings { get; set; }
        public WebsiteLogging WebsiteLogging { get; set; }
        public WwwRoot WwwRoot { get; set; }
        public EmailXMLSettings EmailXMLSettings { get; set; }
        public UploadUsProduct UploadUsProduct { get; set; }
        public UploadLogo UploadLogo { get; set; }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpiryInMinutes { get; set; }
    }

    public class UploadUsProduct
    {
        public string UploadsDirectory { get; set; }
    }
    public class UploadLogo
    {
        public string UploadsDirectory { get; set; }
    }
    public class ConnectionStrings
    {
        public string ccContext { get; set; }
    }

    public class WebPagesHosting
    {
        public string Host { get; set; }
    }

    public class EmailSettings
    {
        public string Sender { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
        public string DisplayName { get; set; }
    }

    public class WebsiteLogging
    {
        public bool OnlyExceptions { get; set; }
    }

    public class WwwRoot
    {
        public string pathname { get; set; }
        public string signaturelogo { get; set; }
    }
    public class EmailXMLSettings
    {
        public string baseUri { get; set; }
        public string ApiKey { get; set; }
        public string ProjectId { get; set; }
    }
}
