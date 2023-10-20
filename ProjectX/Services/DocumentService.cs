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

namespace ProjectX.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IConverter _converter;
        private readonly IRazorRendererHelper _razorRendererHelper;
        private TR_Users _user;
        private IProductionBusiness _productionBusiness;

        public DocumentService(IHttpContextAccessor httpContextAccessor,
            IConverter converter, IProductionBusiness productionBusiness,
            IRazorRendererHelper razorRendererHelper)
        {
            _converter = converter;
            _razorRendererHelper = razorRendererHelper;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
            _productionBusiness = productionBusiness;
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

        public byte[] GeneratePdfFromRazorView(int policyid,string fileqrurl)
        {
            ProductionPolicy policyreponse = new ProductionPolicy();
            policyreponse = _productionBusiness.GetPolicy(policyid, _user.U_Id, true);
            policyreponse.QrCodebit = fileqrurl;
            var partialName = "/Views/PdfTemplate/PrintPolicy.cshtml";

            if (policyreponse.PolicyDetails.Count > 1)
                partialName = "/Views/PdfTemplate/PrintPolicyGroup.cshtml";

            var htmlContent = _razorRendererHelper.RenderPartialToString(partialName, policyreponse);

            return GeneratePdf(htmlContent);
        }

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

        private InvoiceViewModel GetInvoiceModel()
        {
            var invoiceViewModel = new InvoiceViewModel
            {
                OrderDate = DateTime.Now,
                OrderId = 1234567890,
                DeliveryDate = DateTime.Now.AddDays(10),
                Products = new List<Product>()
                {
                    new Product
                    {
                        ItemName = "Hosting (12 months)",
                        Price = 200
                    },
                    new Product
                    {
                        ItemName = "Domain name (1 year)",
                        Price = 12
                    },
                    new Product
                    {
                        ItemName = "Website design",
                        Price = 1000

                    },
                    new Product
                    {
                        ItemName = "Maintenance",
                        Price = 300
                    },
                    new Product
                    {
                        ItemName = "Customization",
                        Price = 400
                    },
                }
            };

            invoiceViewModel.TotalAmount = invoiceViewModel.Products.Sum(p => p.Price);

            return invoiceViewModel;
        }

    }
}
