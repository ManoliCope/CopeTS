using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using static ProjectX.Controllers.PdfController;

namespace ProjectX.Interfaces
{
    public interface IDocumentService
    {
        byte[] GeneratePdfFromRazorView(int policyid, string fileqr,string requesturl);

         string ConvertImageToBase64(string imagePath);

        QRCodeModel GenerateQRCodeImage(string imagePath);
        byte[] BitmapToBytes(Bitmap bitmap);
    }
}
