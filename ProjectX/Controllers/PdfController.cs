using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        public IActionResult Get(int polid)
        {
            var pdfFile = _documentService.GeneratePdfFromString();
            return File(pdfFile, "application/octet-stream", "SimplePdf.pdf");
        }
        [HttpGet]
        public ActionResult GeneratePdfFromRazorView(int policyId)
        { 
            var pdfFile = _documentService.GeneratePdfFromRazorView(policyId);
            return File(pdfFile, "application/octet-stream", "RazorPdf.pdf");
        }

    }
}
