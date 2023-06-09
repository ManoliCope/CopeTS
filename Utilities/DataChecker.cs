using System;
using System.Globalization;

namespace Utilities
{
    public static class DataChecker
    {
        public static bool validDate(string dateValue, out string checkedDate)
        {
            checkedDate = string.Empty;

            try
            {
                dateValue = ArabicCulture.ConvertNumbersArabicToEnglish(dateValue);
                checkedDate = DateTime.ParseExact(dateValue, "yyyy/MM/dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            }
            catch
            {
                try
                {
                    checkedDate = DateTime.ParseExact(dateValue, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                }
                catch
                {
                    try
                    {
                        checkedDate = DateTime.ParseExact(dateValue, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    }
                    catch
                    {
                        try
                        {
                            checkedDate = DateTime.ParseExact(dateValue, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            try
                            {
                                checkedDate = DateTime.ParseExact(dateValue, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                            }
                            catch
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }
}
