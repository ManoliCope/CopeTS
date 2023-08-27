using Microsoft.AspNetCore.Mvc;
using ProjectX.Interfaces;

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
        public IActionResult Get()
        {
            var pdfFile = _documentService.GeneratePdfFromString();
            return File(pdfFile, "application/octet-stream", "SimplePdf.pdf");
        }

        [HttpGet("GetPdfFromRazor")]
        public IActionResult GetPdfFromRazor()
        {
            var pdfFile = _documentService.GeneratePdfFromRazorView();
            return File(pdfFile, "application/octet-stream", "RazorPdf.pdf");
        }

    }
}
