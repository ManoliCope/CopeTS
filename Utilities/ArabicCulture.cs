using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class ArabicCulture
    {
        public static string ConvertNumbersEnglishToArabic(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Replace("0", "٠");
                s = s.Replace("1", "١");
                s = s.Replace("2", "٢");
                s = s.Replace("3", "٣");
                s = s.Replace("4", "٤");
                s = s.Replace("5", "٥");
                s = s.Replace("6", "٦");
                s = s.Replace("7", "٧");
                s = s.Replace("8", "٨");
                s = s.Replace("9", "٩");
            }
            return s;
        }

        public static string ConvertNumbersArabicToEnglish(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Replace("٠", "0");
                s = s.Replace("١", "1");
                s = s.Replace("٢", "2");
                s = s.Replace("٣", "3");
                s = s.Replace("٤", "4");
                s = s.Replace("٥", "5");
                s = s.Replace("٦", "6");
                s = s.Replace("٧", "7");
                s = s.Replace("٨", "8");
                s = s.Replace("٩", "9");
            }
            return s;
        }
    }
}
