using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ProjectX.Interfaces;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProjectX.Controllers
{
    public class PdfController : Controller
    {
        private readonly IDocumentService _documentService;

        public PdfController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public IActionResult Get(int polid)
        {
            var pdfFile = _documentService.GeneratePdfFromString();
            return File(pdfFile, "application/octet-stream", "SimplePdf.pdf");
        }
        [HttpGet]
        public ActionResult GeneratePdfFromRazorView(int ii)
        {
            string requesturl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            string printingdirection = GenerateQRCodeImage(requesturl + "/Pdf/GeneratePdfFromRazorView?ii=" + ii).Base64Image;
            var pdfFile = _documentService.GeneratePdfFromRazorView(ii, printingdirection);
            return File(pdfFile, "application/octet-stream", "RazorPdf.pdf");
        }

        [HttpGet]
        public ActionResult DownloadPdfFromRazorView(int ii)
        {
            string requesturl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            string printingdirection = GenerateQRCodeImage(requesturl + "/Pdf/DownloadPdfFromRazorView?ii=" + ii).Base64Image;

            var pdfFile = _documentService.GeneratePdfFromRazorView(ii, printingdirection);
            return File(pdfFile, "application/octet-stream", "RazorPdf.pdf");
        }

        private QRCodeModel GenerateQRCodeImage(string txtCode)
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
        private byte[] BitmapToBytes(Bitmap bitmap)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public class QRCodeModel
        {
            public string Base64Image { get; set; }
            // You can include additional properties as needed.
        }

    }
}
