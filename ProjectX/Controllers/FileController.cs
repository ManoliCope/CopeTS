using ProjectX.Business.Attachment;
using ProjectX.Entities;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.File;
using ProjectX.Entities.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ProjectX.Controllers
{
    public class FileController : Controller
    {
        private IAttachmentBusiness _attachmentBusiness;
        public User _user;
        //private readonly ILogger<FileController> _logger;

        public FileController(IHttpContextAccessor context, IAttachmentBusiness attachmentBusiness/*, ILogger<FileController> logger*/)
        {
            try
            {
                _user = (User)context.HttpContext.Items["User"];
            }
            catch { }
            _attachmentBusiness = attachmentBusiness;
            //_logger = logger;
        }

        [HttpPost]
        //[Consumes("multipart/form-data")]
        //public async Task<FileUploadResp> Upload([FromForm(Name = "files")] IFormFileCollection files, [FromForm(Name = "fd")] int IdFileDirectory, [FromForm(Name = "ref")] string IdReference, [FromForm(Name = "dt")] int IdDocumentType)
        public async Task<FileUploadResp> Upload(IList<IFormFile> files, int fd, string reference, string dt)
        {
            //files = HttpContext.Request.Form.Files;
            //IdDocumentType = Convert.ToInt32(HttpContext.Request.Form["dt"]);
            //IdFileDirectory = Convert.ToInt32(HttpContext.Request.Form["fd"]);
            //IdReference = HttpContext.Request.Form["ref"];
            if (dt == "undefined")
                dt = "0";

            return await _attachmentBusiness.SaveFile(files, Convert.ToInt32(fd), reference, _user.UserId, Convert.ToInt32(dt));

        }

        [HttpPost]
        public GlobalResponse Clear(ClearFileReq req)
        {
            return _attachmentBusiness.ClearFile(req.idAttachment, req.IdFileDirectory, req.IdReference, req.idDocumentType);
        }

        [HttpGet]
        public object Display([FromQuery] DisplayFileReq req)
        {
            try
            {
                DisplayFile displayFile = _attachmentBusiness.DisplayFile(req);

                if (displayFile != null)
                {
                    if (!string.IsNullOrEmpty(displayFile.FilePath))
                    {
                        //displayFile.FilePath = displayFile.FilePath.Replace("H:", "D:");
                        if (displayFile.AllowDownload)
                        {
                            WebClient webClient = new WebClient();
                            byte[] dataPath = webClient.DownloadData(displayFile.FilePath);
                            var cd = new System.Net.Mime.ContentDisposition
                            {
                                //FileName = displayFile.FileName, //System.IO.Path.GetFileName(displayFile.FilePath),
                                FileName = System.IO.Path.GetFileName(displayFile.FilePath),
                                Inline = false,
                            };
                            Response.Headers.Add("Access-Control-Allow-Origin", "*");
                            Response.Headers.Add("Content-Disposition", cd.ToString());
                            return File(dataPath, displayFile.ContentType, System.IO.Path.Combine(displayFile.FileName, displayFile.FileExtension), true);
                        }
                        else
                            return File(System.IO.File.ReadAllBytes(displayFile.FilePath), displayFile.ContentType, System.IO.Path.Combine(displayFile.FileName, displayFile.FileExtension), true);
                    }
                    else
                        return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<GetHtmlResp> GetHtmlGOP([FromForm(Name = "files")] IFormFile file)
        {
            GetHtmlResp response = new GetHtmlResp();

            if (file.Length > 0)
            {
                SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();

                f.HtmlOptions.DetectTables = true;
                f.HtmlOptions.IncludeImageInHtml = true;
                f.HtmlOptions.KeepCharScaleAndSpacing = true;

                f.OpenPdf(file.OpenReadStream());
                if (f.PageCount > 0)
                    response.htmlString = f.ToHtml().Replace("</span>", "&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;</span>");
                else
                {
                    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidFile);
                    return response;
                }

                f.ClosePdf();
            }
            else
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.NoFileDetected);
                return response;
            }
            return response;
        }
    }
}
