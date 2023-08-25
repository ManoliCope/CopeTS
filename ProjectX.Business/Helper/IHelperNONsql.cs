using System.Collections.Generic;

namespace ProjectX.Business.Helper
{
    public interface IHelperNONsql
    {
        public string checkPolNum(string polNum);

        public List<string> GetListOfYears();

        public bool SaveHtmlAsPDF(string html, string fullName);

        /// <summary>
        /// This Method is used to render Html into PDF Base64 string
        /// </summary>
        /// <param name="html">HTML to be converted to PDF.</param>
        /// <param name="Title">PDF Title.</param>
        /// <param name="paperSize">PDF Papersize. Either "A4" or "A5".</param>
        /// <param name="Margins">PDF Margins.</param>
        /// <param name="Landscape">PDF Portrait/Lanscape Mode.</param>
        /// <returns></returns>
        public string ConvertHtmlToPDF(string html, string Title = "", string paperSize = "A4", double Margins = 1.25, bool Landscape = false);

        //public string GenerateBarCode(string label, int width, int height);
    }
}

