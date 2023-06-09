using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public static class ValueChecker
    {
        public static bool IsWhiteSpace(string inputdata)
        {
            try
            {
                string whiteSpace = " \t\n\r";
                string sChar;

                for (int i = 0; i < inputdata.Trim().Length; i++)
                {
                    sChar = inputdata.Substring(i, 1);

                    if (whiteSpace.IndexOf(sChar) != -1)
                        return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public static bool IsControlCharacter(string inputData)
        {
            try
            {
                string CtrlChr = "$()/|?,;:'~<>\\+=.[]{}";
                string sChar;

                for (int i = 0; i < inputData.Trim().Length; i++)
                {
                    sChar = inputData.Substring(i, 1);

                    if (CtrlChr.IndexOf(sChar) != -1)
                        return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public static bool IsNullValue(object Value)
        {
            if (Value == null)
                return true;
            if (string.IsNullOrEmpty(Value.ToString()))
                Value = "";
            if (Value.ToString() == "0" || Value.ToString() == "-1" || Value.ToString() == "" || Value.ToString().ToLower() == "null" || Value.ToString().ToLower() == "nil" || Value.ToString().ToLower() == "(nil)" || Value.ToString().ToLower() == "(null)")
                return true;
            else if (Value.Equals(null) || string.IsNullOrWhiteSpace(Value.ToString()))
                return true;
            else
                return false;
        }

        public static bool IsZeroValue(object Value)
        {
            if (Value == null)
                return true;
            else if (!DataTypeChecker.IsDouble(Value.ToString()))
                return true;
            else if (Convert.ToDouble(Value) <= 0)
                return true;
            else
                return false;
        }

        public static bool IsNullDate(object Value)
        {
            DateTime datetime = DateTime.Now;
            if (IsNullValue(Value))
                return true;
            else
                return !DateTime.TryParse(Value.ToString(), out datetime);
        }

        public static bool IsNullOrEmptyGuid(Guid Value)
        {
            if (Value == null || Value == Guid.Empty)
                return true;
            else
                return false;
        }

        public static bool IsNullOrEmptyGuid(Guid? Value)
        {
            if (Value == null || Value == Guid.Empty)
                return true;
            else
                return false;
        }

        public static bool IsPositive(object sText)
        {
            if (sText == null)
                return false;
            else
            {
                double result = 0;
                double.TryParse(sText.ToString(), out result);
                return result >= 0;
            }
        }

        public static bool IsPositivePercentange(object sText)
        {
            if (sText == null)
                return false;
            else
            {
                double result = 0;
                double.TryParse(sText.ToString(), out result);
                return ((result >= 0) && (result <= 100));
            }
        }
    }
}
