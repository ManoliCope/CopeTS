using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public static class DataTypeChecker
    {
        public static bool IsNumeric(object sText)
        {
            if (sText == null)
                return false;
            else
            {
                int result;
                return int.TryParse(sText.ToString(), out result);
            }
        }

        public static bool IsBoolean(object sText)
        {
            if (sText == null)
                return false;
            else
            {
                bool result;
                return bool.TryParse(sText.ToString(), out result);
            }
        }

        public static bool IsDouble(object sText)
        {
            if (sText == null)
                return false;
            else
            {
                double result = 0;
                return double.TryParse(sText.ToString(), out result);
            }
        }
    }

}
