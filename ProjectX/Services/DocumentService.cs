using DinkToPdf;
using DinkToPdf.Contracts;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Business.Production;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Production;
using ProjectX.Interfaces;
using ProjectX.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

using System.IO;
using ProjectX.Entities.AppSettings;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Options;
using DocumentFormat.OpenXml.Wordprocessing;


//using iTextSharp.text;
//using iTextSharp.text.pdf;
using iText.Kernel.Pdf;
using iText.Layout;
using DocumentFormat.OpenXml.InkML;
using ProjectX.Entities.bModels;
using ProjectX.Business.General;
using ProjectX.Entities.Models.Users;
using ProjectX.Business.Users;
using Microsoft.AspNetCore.Components.Web;
using static ProjectX.Controllers.PdfController;

namespace ProjectX.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IConverter _converter;
        private readonly IRazorRendererHelper _razorRendererHelper;
        private TR_Users _user;
        private IProductionBusiness _productionBusiness;
        private readonly TrAppSettings _appSettings;
        private IGeneralBusiness _generalBusiness;
        private IUsersBusiness _usersBusiness;

        public DocumentService(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor,
            IConverter converter, IProductionBusiness productionBusiness, IGeneralBusiness generalBusiness, IUsersBusiness usersBusiness,
            IRazorRendererHelper razorRendererHelper)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
            _converter = converter;
            _razorRendererHelper = razorRendererHelper;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
            _productionBusiness = productionBusiness;
            _generalBusiness = generalBusiness;
            _usersBusiness = usersBusiness;

        }


        public byte[] GeneratePdfFromRazorView(int policyid, int prttyp, string fileqrurl, string requesturl)
        {
            var uploadsDirectory = _appSettings.UploadUsProduct.UploadsDirectory;
            ProductionPolicy policyreponse = new ProductionPolicy();
            policyreponse = _productionBusiness.GetPolicy(policyid, 0, true);

            int userid = Convert.ToInt16(policyreponse.CreatedById);

            GetUserReq thisuser = new GetUserReq();
            thisuser.idUser = userid;
            var prodcutionuser = _usersBusiness.GetUserAuth(thisuser);

            policyreponse.QrCodebit = fileqrurl;
            policyreponse.prttyp = prttyp;

            string mainheader = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "assets", "images", "backcope.jpg");
            string mainfooter = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "assets", "images", "copelogo.png");

            if (prodcutionuser.user.U_PrintLayout != null && prodcutionuser.user.U_PrintLayout != "")
                policyreponse.Layout = ConvertImageToBase64(Path.Combine(uploadsDirectory, userid.ToString(), "Layout", prodcutionuser.user.U_PrintLayout ?? ""));
            else
                policyreponse.Layout = ConvertImageToBase64(mainheader);


            if (prodcutionuser.user.U_Signature != null && prodcutionuser.user.U_Signature != "")
                policyreponse.Signature = ConvertImageToBase64(Path.Combine(uploadsDirectory, userid.ToString(), "Signature", prodcutionuser.user.U_Signature ?? string.Empty));
            else
                policyreponse.Signature = ConvertImageToBase64(mainfooter);

            var partialName = "/Views/PdfTemplate/PrintPolicy.cshtml";
            if (policyreponse.PolicyDetails.Count > 1)
                partialName = "/Views/PdfTemplate/PrintPolicyGroup.cshtml";

            var htmlContent = _razorRendererHelper.RenderPartialToString(partialName, policyreponse);
            byte[] pdfBytes = ConvertHtmlToPDF(htmlContent);
            //byte[] pdfBytes2 = ConvertHtmlToPDF(htmlContent);
            //byte[] combinedPdf1 = CombinePdfFiles(pdfBytes, pdfBytes2);
            //return combinedPdf1;
           return pdfBytes;

            try
            {
                var response = new UsProSearchResp();
                response.usersproduct = _usersBusiness.GetUsersProduct(userid);

                string UploadedCondition = response.usersproduct.OrderByDescending(product => product.UP_CreationDate).FirstOrDefault(product => product.PR_Id == policyreponse.ProductID).UP_UploadedFile;
                if (UploadedCondition == null)
                    UploadedCondition = "";

                var userFullPath = Path.Combine(uploadsDirectory, userid.ToString(), "Conditions", UploadedCondition);

                byte[] combinedPdf = CombinePdfFiles(pdfBytes, userFullPath);
                return combinedPdf;
            }
            catch (Exception ex)
            {
                var logData = new LogData
                {
                    Timestamp = DateTime.UtcNow,
                    Controller = "test",
                    Action = "test",
                    ErrorMessage = ex.Message,
                    Type = "Error",
                    Message = ex.Message,
                    RequestPath = "here",
                    Response = "Response content",
                    Exception = ex.ToString(),
                    ExecutionTime = 0,
                    Userid = 1
                };
                _generalBusiness.LogErrorToDatabase(logData);

                return pdfBytes;
            }

        }
        public byte[] CombinePdfFiles(byte[] pdfBytes1, byte[] pdfBytes2 )
        {

            using (MemoryStream combinedPdfStream = new MemoryStream())
            {
                // Load the PDF from a byte[] stream
                PdfDocument pdfDocument1 = new PdfDocument(new PdfReader(new MemoryStream(pdfBytes1)));
                PdfDocument pdfDocument2 = new PdfDocument(new PdfReader(new MemoryStream(pdfBytes2)));

                using (PdfWriter writer = new PdfWriter(combinedPdfStream))
                using (PdfDocument combinedDocument = new PdfDocument(writer))
                {
                    iText.Kernel.Geom.Rectangle pageSize1 = pdfDocument1.GetPage(1).GetPageSize();
                    //iText.Kernel.Geom.PageSize pageSize1 = (iText.Kernel.Geom.PageSize)(pdfDocument1.GetPage(1).GetPageSize());

                    pdfDocument2.GetPage(1).GetPdfObject().Put(PdfName.MediaBox, new PdfArray(new float[] { pageSize1.GetLeft(), pageSize1.GetBottom(), pageSize1.GetRight(), pageSize1.GetTop() }));

                    for (int page = 1; page <= pdfDocument1.GetNumberOfPages(); page++)
                    {
                        PdfPage pdfPage = pdfDocument1.GetPage(page);
                        combinedDocument.AddPage(pdfPage.CopyTo(combinedDocument));
                    }

                    for (int page = 1; page <= pdfDocument2.GetNumberOfPages(); page++)
                    {
                        pdfDocument2.GetPage(page).GetPdfObject().Put(PdfName.MediaBox, new PdfArray(new float[] { pageSize1.GetLeft(), pageSize1.GetBottom(), pageSize1.GetRight(), pageSize1.GetTop() }));
                        PdfPage pdfPage = pdfDocument2.GetPage(page);
                        combinedDocument.AddPage(pdfPage.CopyTo(combinedDocument));
                    }
                }
                return combinedPdfStream.ToArray();
            }
        }


        public byte[] CombinePdfFiles(byte[] pdfBytes, string pdfPath)
        {

            using (MemoryStream combinedPdfStream = new MemoryStream())
            {
                // Load the PDF from a byte[] stream
                PdfDocument pdfDocument1 = new PdfDocument(new PdfReader(new MemoryStream(pdfBytes)));
                PdfDocument pdfDocument2 = new PdfDocument(new PdfReader(pdfPath));

                using (PdfWriter writer = new PdfWriter(combinedPdfStream))
                using (PdfDocument combinedDocument = new PdfDocument(writer))
                {
                    iText.Kernel.Geom.Rectangle pageSize1 = pdfDocument1.GetPage(1).GetPageSize();
                    //iText.Kernel.Geom.PageSize pageSize1 = (iText.Kernel.Geom.PageSize)(pdfDocument1.GetPage(1).GetPageSize());

                    pdfDocument2 = new PdfDocument(new PdfReader(pdfPath));
                    pdfDocument2.GetPage(1).GetPdfObject().Put(PdfName.MediaBox, new PdfArray(new float[] { pageSize1.GetLeft(), pageSize1.GetBottom(), pageSize1.GetRight(), pageSize1.GetTop() }));

                    for (int page = 1; page <= pdfDocument1.GetNumberOfPages(); page++)
                    {
                        PdfPage pdfPage = pdfDocument1.GetPage(page);
                        combinedDocument.AddPage(pdfPage.CopyTo(combinedDocument));
                    }

                    for (int page = 1; page <= pdfDocument2.GetNumberOfPages(); page++)
                    {
                        pdfDocument2.GetPage(page).GetPdfObject().Put(PdfName.MediaBox, new PdfArray(new float[] { pageSize1.GetLeft(), pageSize1.GetBottom(), pageSize1.GetRight(), pageSize1.GetTop() }));
                        PdfPage pdfPage = pdfDocument2.GetPage(page);
                        combinedDocument.AddPage(pdfPage.CopyTo(combinedDocument));
                    }
                }
                return combinedPdfStream.ToArray();
            }
        }


        public byte[] ConvertHtmlToPDF(string html, string Title = "",
         string paperSize = "A4", double Margins = 1.25, bool Landscape = false, bool hasPageNumber = false,
         string HeaderUrl = "", double HeaderSpacing = -20, string FooterUrl = "", double FooterSpacing = -15)
        {
            Margins = 0;
            //var converter = new SynchronizedConverter(new PdfTools());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = DinkToPdf.ColorMode.Color,
                    DocumentTitle = Title,
                    Margins = new MarginSettings()
                    {
                        Unit = Unit.Centimeters,
                        Bottom = Margins,
                        Left = Margins,
                        Right = -1,
                        Top = Margins
                    }
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            if (!string.IsNullOrWhiteSpace(HeaderUrl))
            {
                doc.Objects.FirstOrDefault().HeaderSettings = new HeaderSettings() { HtmUrl = HeaderUrl, Spacing = HeaderSpacing };
            }
            if (!string.IsNullOrWhiteSpace(FooterUrl))
            {
                doc.Objects.FirstOrDefault().FooterSettings = new FooterSettings() { HtmUrl = FooterUrl, Spacing = FooterSpacing };
            }
            if (hasPageNumber)
            {
                doc.Objects.FirstOrDefault().FooterSettings = new FooterSettings() { Right = "Page [page] of [toPage]" };
            }
            if (Landscape)
            {
                doc.GlobalSettings.Orientation = Orientation.Landscape;
            }
            else
            {
                doc.GlobalSettings.Orientation = Orientation.Portrait;
            }
            switch (paperSize.ToLower())
            {
                case "a4":
                    doc.GlobalSettings.PaperSize = DinkToPdf.PaperKind.A4;
                    break;
                case "a5":
                    doc.GlobalSettings.PaperSize = DinkToPdf.PaperKind.A5;
                    break;
                default:
                    doc.GlobalSettings.PaperSize = DinkToPdf.PaperKind.A4;
                    break;
            }
            try
            {

                return _converter.Convert(doc);
                //byte[] pdf = _converter.Convert(doc);
                //string base64String = Convert.ToBase64String(pdf);
                //return base64String;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string ConvertImageToBase64(string imagePath)
        {
            string base64String = string.Empty;
            try
            {
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                base64String = Convert.ToBase64String(imageBytes);
            }
            catch
            {
            }

            return base64String;
        }

        public QRCodeModel GenerateQRCodeImage(string txtCode)
        {
            QRCodeGenerator qrcodegenerator = new QRCodeGenerator();
            QRCodeData QRCodeData = qrcodegenerator.CreateQrCode(txtCode, QRCodeGenerator.ECCLevel.Q);
            QRCode QRCode = new QRCode(QRCodeData);
            Bitmap bitmap = QRCode.GetGraphic(15);
            var bitmapbytes = BitmapToBytes(bitmap);
            var base64Image = Convert.ToBase64String(bitmapbytes);

            return new QRCodeModel
            {
                Base64Image = base64Image
            };
        }
        public byte[] BitmapToBytes(Bitmap bitmap)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
