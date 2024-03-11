using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using ProjectX.Business.Production;
using ProjectX.Business.Users;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.Models.Production;
using ProjectX.Entities.Models.Users;
using ProjectX.Interfaces;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProjectX.Controllers
{
    public class PdfController : Controller
    {
        private readonly IDocumentService _documentService;
        private IProductionBusiness _productionBusiness;
        private readonly TrAppSettings _appSettings;
        private IUsersBusiness _usersBusiness;

        public PdfController(IOptions<TrAppSettings> appIdentitySettingsAccessor, IUsersBusiness usersBusiness, IDocumentService documentService, IProductionBusiness productionBusiness)
        {
            _documentService = documentService;
            _productionBusiness = productionBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _usersBusiness = usersBusiness;
        }

        //[HttpGet]
        //public IActionResult Get(int polid)
        //{
        //    var pdfFile = _documentService.GeneratePdfFromString();
        //    return File(pdfFile, "application/octet-stream", "SimplePdf.pdf");
        //}

        public IActionResult Main(int polid)
        {
            polid = 639;
            var uploadsDirectory = _appSettings.UploadUsProduct.UploadsDirectory;
            string requesturl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            string printingdirection = _documentService.GenerateQRCodeImage(requesturl + "/Pdf/GeneratePdfFromRazorView?ii=" + polid).Base64Image;

            ProductionPolicy policyreponse = new ProductionPolicy();
            policyreponse = _productionBusiness.GetPolicy(polid, 0, true);

            int userid = Convert.ToInt16(policyreponse.CreatedById);

            GetUserReq thisuser = new GetUserReq();
            thisuser.idUser = userid;
            var prodcutionuser = _usersBusiness.GetUserAuth(thisuser);

            string test = Path.Combine(uploadsDirectory, userid.ToString(), "Header", prodcutionuser.user.U_PrintLayout ?? string.Empty);

            policyreponse.QrCodebit = printingdirection;

            string mainheader = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "assets", "images", "copelogo.png");
            string mainfooter = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "assets", "images", "copelogo.png");

            if (prodcutionuser.user.U_PrintLayout != null || prodcutionuser.user.U_PrintLayout != "")
                policyreponse.Layout = _documentService.ConvertImageToBase64(mainheader);
            else
                policyreponse.Layout = _documentService.ConvertImageToBase64(Path.Combine(uploadsDirectory, userid.ToString(), "Header", prodcutionuser.user.U_PrintLayout ?? ""));


            if (prodcutionuser.user.U_Signature != null || prodcutionuser.user.U_Signature != "")
                policyreponse.Signature = _documentService.ConvertImageToBase64(mainfooter);
            else
                policyreponse.Signature = _documentService.ConvertImageToBase64(Path.Combine(uploadsDirectory, userid.ToString(), "Footer", prodcutionuser.user.U_Signature ?? string.Empty));



            return View("/Views/PdfTemplate/PrintPolicy.cshtml", policyreponse);
        }
        //public IActionResult Header(int userid)
        //{
        //    var uploadsDirectory = _appSettings.UploadUsProduct.UploadsDirectory;
        //    GetUserReq thisuser = new GetUserReq();
        //    thisuser.idUser = userid;
        //    var prodcutionuser = _usersBusiness.GetUserAuth(thisuser);
        //    ViewData["headerimg"] = _documentService.ConvertImageToBase64(Path.Combine(uploadsDirectory, userid.ToString(), "Header", prodcutionuser.user.U_PrintLayout ?? string.Empty));
        //    return PartialView("~/Views/PdfTemplate/Printheader.cshtml");
        //}
        //public IActionResult Footer(int userid)
        //{
        //    var uploadsDirectory = _appSettings.UploadUsProduct.UploadsDirectory;
        //    GetUserReq thisuser = new GetUserReq();
        //    thisuser.idUser = userid;
        //    var prodcutionuser = _usersBusiness.GetUserAuth(thisuser);
        //    ViewData["footerimg"] = _documentService.ConvertImageToBase64(Path.Combine(uploadsDirectory, userid.ToString(), "Footer", prodcutionuser.user.U_Signature ?? string.Empty));
        //    string testhhh = _documentService.ConvertImageToBase64(Path.Combine(uploadsDirectory, userid.ToString(), "Footer", prodcutionuser.user.U_Signature ?? string.Empty));
        //    return PartialView("~/Views/PdfTemplate/Printfooter.cshtml");
        //}


        [HttpGet]
        public ActionResult GeneratePdfFromRazorView(int ii, int prttyp)
        {
            string requesturl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            string printingdirection = _documentService.GenerateQRCodeImage(requesturl + "/Pdf/GeneratePdfFromRazorView?ii=" + ii + "&prttyp=" + prttyp).Base64Image;

            var pdfFile = _documentService.GeneratePdfFromRazorView(ii, prttyp, printingdirection, requesturl);
            return File(pdfFile, "application/octet-stream", "PrintPolicy.pdf");
        }

        [HttpGet]
        public ActionResult DownloadPdfFromRazorView(int ii, int prttyp)
        {
            string requesturl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            string printingdirection = _documentService.GenerateQRCodeImage(requesturl + "/Pdf/DownloadPdfFromRazorView?ii=" + ii).Base64Image;

            var pdfFile = _documentService.GeneratePdfFromRazorView(ii, prttyp, printingdirection, requesturl);
            return File(pdfFile, "application/octet-stream", "PrintPolicy.pdf");
        }
        public byte[] getPolicyAttachmentByte(int ii, int prttyp)
        {
            string requesturl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            string printingdirection = _documentService.GenerateQRCodeImage(requesturl + "/Pdf/GeneratePdfFromRazorView?ii=" + ii + "&prttyp=" + prttyp).Base64Image;

            var pdfFile = _documentService.GeneratePdfFromRazorView(ii, prttyp, printingdirection, requesturl);

            return pdfFile;
        }


        public class QRCodeModel
        {
            public string Base64Image { get; set; }
            // You can include additional properties as needed.
        }

    }
}
