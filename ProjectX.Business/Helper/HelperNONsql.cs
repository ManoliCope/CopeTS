//using BarcodeLib;
using BarcodeStandard;
using Dapper;
using Microsoft.Extensions.Options;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace ProjectX.Business.Helper
{
    public class HelperNONsql: IHelperNONsql
    {
        //private readonly ErpAppSettings _appSettings;

        //public HelperNONsql(IOptions<ErpAppSettings> appIdentitySettingsAccessor)
        //{
        //    _appSettings = appIdentitySettingsAccessor.Value;
        //}
        public string checkPolNum(string polNum)
        {
            string str = "";
            string strResult = "";
            str = polNum.Replace(" ", "");
            str = str.Replace("-", "");
            str = str.Replace("/", "");
            if (str.Length == 14)
            {
                strResult = str.Substring(0, 2) + " " + str.Substring(2, 3) + " " + str.Substring(5, 6) + " " + str.Substring(11, 3);
            }
            else if (str.Length == 18)
            {
                strResult = str.Substring(0, 2) + " " + str.Substring(2, 3) + " " + str.Substring(9, 6) + " " + str.Substring(15, 3);
            }
            else if (str.Length == 11)
            {
                strResult = str.Substring(0, 6) + "-" + str.Substring(6, 3) + "/" + str.Substring(9);
            }
            else if (str.Length == 15)
            {
                strResult = str.Substring(0, 2) + " " + str.Substring(2, 3) + " " + str.Substring(5, 6) + "-" + str.Substring(11, 4);
            }
            else if (str.Length == 9)
            {
                strResult = str.Substring(0, 6) + "-" + str.Substring(6);
            }
            else if (str.Length == 19)
            {
                strResult = str.Substring(0, 2) + " " + str.Substring(2, 3) + " " + str.Substring(9, 6) + "-" + str.Substring(15, 4);
            }
            else
            {
                strResult = "";
            }
            return strResult;
        }

        //this function inputs a polNum and returns its corresponding GetPolicy Api Url       

        public List<string> GetListOfYears()
        {
            List<string> Lyrs = new List<string>();
            Lyrs.Add(DateTime.Now.Year.ToString());
            for (int i = -1; i >= -60; --i)
            {
                Lyrs.Add(DateTime.Now.AddYears(i).ToString("yyyy"));
            }
            return Lyrs;
        }

        public bool SaveHtmlAsPDF(string html, string fullName)
        { 
            try
            {
                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertHtmlString(html);
                doc.Save(fullName);
                doc.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string ConvertHtmlToPDF(string html, string Title = "", string paperSize = "A4", double Margins = 1.25, bool Landscape = false)
        {
            PaperSize size = null;

            switch (paperSize.ToLower())
            {
                case "a4":
                    size = PaperSize.A4;
                    break;
                case "a5":
                    size = PaperSize.A5;
                    break;
                default:
                    size = PaperSize.A4;
                    break;
            }

            var PDFDocument = Pdf.From(html).OfSize(size).WithTitle(Title).WithoutOutline().WithMargins(Margins.Centimeters());

            byte[] result = null;

            if (!Landscape)
            {
                result = PDFDocument.Portrait().Comressed().Content();
            }
            else
            {
                result = PDFDocument.Landscape().Comressed().Content();
            }


            var baseResult = System.Convert.ToBase64String(result);

            return baseResult;
        }

        //public string GenerateBarCode(string label, int width, int height)
        //{
            
        //    Barcode b = new Barcode();
        //    Image barcodeResult = b.Encode(TYPE.CODE39Extended, label, Color.Black, Color.Transparent, width, height);

        //    var memoryStream = new MemoryStream();
        //    barcodeResult.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
        //    byte[] bytecode = memoryStream.ToArray();
        //    return Convert.ToBase64String(bytecode);

        //}

    }
}

