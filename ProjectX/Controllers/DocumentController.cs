﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ProjectX.Models;
using ProjectX.Interfaces;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Product;
using ProjectX.Entities.Models.Production;

namespace WebApplication2.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        //public IActionResult Get()
        //{

        //    var pdfFiles = _documentService.GeneratePdfFromString();
        //    return File(pdfFiles, "application/octet-stream", "SimplePdf.pdf");
        //}

      
        [HttpPost]
        public IActionResult GetPdfFromRazor([FromBody] ProductionSaveResp requestData)
        {
            var pdfFile = _documentService.GeneratePdfFromRazorView(requestData);
            return File(pdfFile, "application/octet-stream", "RazorPdf.pdf");
        }

    }
}