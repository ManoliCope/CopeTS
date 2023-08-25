using DinkToPdf;
using DinkToPdf.Contracts;
using ProjectX.Business.Production;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Production;
using ProjectX.Interfaces;
using ProjectX.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectX.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IConverter _converter;
        private readonly IRazorRendererHelper _razorRendererHelper;
        private readonly IProductionBusiness _productionBusiness;


        public DocumentService(
            IConverter converter,
            IRazorRendererHelper razorRendererHelper, IProductionBusiness productionBusiness)
        {
            _converter = converter;
            _productionBusiness = productionBusiness;
            _razorRendererHelper = razorRendererHelper;
        }

        public byte[] GeneratePdfFromString()
        {
            string htmlContent = "<p>This is a sample HTML content.</p>";

            //var htmlContent = "test";
            return GeneratePdf(htmlContent);
        }

        public byte[] GeneratePdfFromRazorView(dynamic model)
        {
            int policyid = 84;
            ProductionPolicy getpolicy = _productionBusiness.GetPolicy(policyid, 0);

            var invoiceViewModel = GetInvoiceModel();
            var partialName = "/Views/PdfTemplate/test.cshtml";
            //var htmlContent = _razorRendererHelper.RenderPartialToStringwithout(partialName);
            var htmlContent = _razorRendererHelper.RenderPartialToString(partialName, getpolicy);
            return GeneratePdf(htmlContent);
        }

        private byte[] GeneratePdf(string htmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                //Margins = new MarginSettings { Top = 18, Bottom = 18 },
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
