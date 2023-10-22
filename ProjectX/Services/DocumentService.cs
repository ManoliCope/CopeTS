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

        public byte[] GeneratePdfFromString()
        {
            var htmlContent = $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <style>
                p{{
                    width: 80%;
                }}
                </style>
            </head>
            <body>
                <h1>Some heading</h1>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            </body>
            </html>
            ";

            return GeneratePdf(htmlContent);
        }

        public byte[] GeneratePdfFromRazorView(int policyid, string fileqrurl)
        {
            ProductionPolicy policyreponse = new ProductionPolicy();

            policyreponse = _productionBusiness.GetPolicy(policyid, 0, true);
            policyreponse.QrCodebit = fileqrurl;
            var partialName = "/Views/PdfTemplate/PrintPolicy.cshtml";

            if (policyreponse.PolicyDetails.Count > 1)
                partialName = "/Views/PdfTemplate/PrintPolicyGroup.cshtml";

            var htmlContent = _razorRendererHelper.RenderPartialToString(partialName, policyreponse);
            byte[] pdfBytes = GeneratePdf(htmlContent); ;

            try
            {
                int userid = Convert.ToInt16(policyreponse.CreatedById);
                var response = new UsProSearchResp();
                response.usersproduct = _usersBusiness.GetUsersProduct(userid);

                string UploadedCondition = response.usersproduct.OrderByDescending(product => product.UP_CreationDate).FirstOrDefault(product => product.PR_Id == policyreponse.ProductID).UP_UploadedFile;
                if (UploadedCondition == null)
                    UploadedCondition = "";


                var uploadsDirectory = _appSettings.UploadUsProduct.UploadsDirectory;
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
                    Userid = 14
                };
                _generalBusiness.LogErrorToDatabase(logData);

                return pdfBytes;
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

        //public byte[] CombinePdfFiles(byte[] pdfBytes, string pdfPath)
        //{
        //    using (MemoryStream combinedPdfStream = new MemoryStream())
        //    {
        //        // Load the PDF from a byte[] stream
        //        PdfReader pdfReader1 = new PdfReader(pdfBytes);
        //        PdfReader pdfReader2 = new PdfReader(pdfPath);

        //        using (Document document = new Document())
        //        {
        //            iTextSharp.text.Rectangle pageSize1 = pdfReader1.GetPageSize(1);

        //            pdfReader2 = new PdfReader(pdfPath);
        //            pdfReader2.GetPageN(1).Put(PdfName.MEDIABOX, new PdfRectangle(pageSize1));

        //            PdfCopy pdfCopy = new PdfCopy(document, combinedPdfStream);
        //            document.Open();

        //            for (int page = 1; page <= pdfReader1.NumberOfPages; page++)
        //            {
        //                PdfImportedPage importedPage = pdfCopy.GetImportedPage(pdfReader1, page);
        //                pdfCopy.AddPage(importedPage);
        //            }

        //            for (int page = 1; page <= pdfReader2.NumberOfPages; page++)
        //            {
        //                pdfReader2.GetPageN(page).Put(PdfName.MEDIABOX, new PdfRectangle(pageSize1));
        //                PdfImportedPage importedPage = pdfCopy.GetImportedPage(pdfReader2, page);
        //                pdfCopy.AddPage(importedPage);
        //            }
        //        }
        //        return combinedPdfStream.ToArray();
        //    }
        //}

        private byte[] GeneratePdf(string htmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = DinkToPdf.ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 18, Bottom = 18 },
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                //HeaderSettings = { FontSize = 10, Right = "Page [page] of [toPage]", Line = true },
                //FooterSettings = { FontSize = 8, Center = "PDF demo from JeminPro", Line = true },
            };

            var htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            return _converter.Convert(htmlToPdfDocument);
        }


    }
}
