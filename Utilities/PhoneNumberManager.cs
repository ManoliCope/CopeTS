using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public static class PhoneNumberManager
    {
        static PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();

        public static object GetInternationalPhoneNumber(object CountryDialCode, object phoneNumber)
        {
            object Result = null;
            if (!ValueChecker.IsNullValue(CountryDialCode) && !ValueChecker.IsNullValue(phoneNumber))
            {
                PhoneNumber PhoneNumber;
                PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
                PhoneNumber = phoneUtil.Parse(phoneNumber.ToString(), phoneUtil.GetRegionCodeForCountryCode(Convert.ToInt32(CountryDialCode)));
                Result = phoneUtil.Format(PhoneNumber, PhoneNumberFormat.INTERNATIONAL).TrimStart('+');
            }
            return Result;
        }

        public static string NormalizeNumber(string phoneNumber)
        {
            return PhoneNumberUtil.NormalizeDigitsOnly(phoneNumber).TrimStart('+').TrimStart(new Char[] { '0' });
        }

        public static PhoneNumberDetails CheckInternational(string InternationalNumber)
        {
            PhoneNumberDetails phoneNum = new PhoneNumberDetails();
            try
            {
                PhoneNumber phoneNumber = phoneUtil.ParseAndKeepRawInput(string.Concat('+', NormalizeNumber(InternationalNumber)), "");
                if (!phoneUtil.IsValidNumber(phoneNumber))
                    phoneNum.IsValid = false;
                else
                {
                    phoneNum.IsValid = true;
                    phoneNum.CC = phoneNumber.CountryCode.ToString();
                    phoneNum.IntNum = NormalizeNumber(phoneNumber.RawInput.ToString());
                    phoneNum.Local = phoneNumber.NationalNumber.ToString();
                }
            }
            catch
            {
                phoneNum.IsValid = false;
            }

            return phoneNum;
        }

        public static PhoneNumberDetails CheckLocal(string CountryCode, string LocalNumber)
        {
            PhoneNumberDetails phoneNum = new PhoneNumberDetails();

            try
            {
                HashSet<int> SupportedCallingCodes = phoneUtil.GetSupportedCallingCodes();

                if (!SupportedCallingCodes.Contains(Convert.ToInt32(CountryCode)))
                    phoneNum.IsValid = false;
                else
                {
                    PhoneNumber phoneNumber = phoneUtil.ParseAndKeepRawInput(string.Concat('+', CountryCode, NormalizeNumber(LocalNumber)), "");
                    if (!phoneUtil.IsValidNumber(phoneNumber))
                        phoneNum.IsValid = false;

                    else
                    {
                        phoneNum.IsValid = true;
                        phoneNum.CC = phoneNumber.CountryCode.ToString();
                        phoneNum.IntNum = NormalizeNumber(phoneNumber.RawInput.ToString());
                        phoneNum.Local = phoneUtil.Format(phoneNumber, PhoneNumberFormat.NATIONAL).Replace(" ", string.Empty);
                        //phoneNum.Local = phoneNumber.NationalNumber.ToString();
                    }
                }
            }
            catch
            {
                phoneNum.IsValid = false;
            }
            return phoneNum;
        }

        public static PhoneNumberDetails CheckLebaneseNumber(string PhoneNumber, bool CheckRoaming = false)
        {
            PhoneNumberDetails phoneNum = CheckInternational(PhoneNumber);

            if (phoneNum.IsValid)
            {
                if (!CheckRoaming)
                {
                    if (phoneNum.CC != "961")
                        phoneNum.IsValid = false;
                }
            }
            else
                phoneNum = CheckLocal("961", PhoneNumber);

            return phoneNum;
        }

        public static PhoneNumberDetails CheckPhoneNumber(string IntCode, string PhoneNumber)
        {
            PhoneNumberDetails phoneNum = CheckInternational(PhoneNumber);
            if (!phoneNum.IsValid)
                phoneNum = CheckLocal(IntCode, PhoneNumber);
            return phoneNum;
        }

        public static PhoneNumberDetails CheckAvayaNumber(string PhoneNumber)
        {
            PhoneNumberDetails phoneNum = CheckInternational(PhoneNumber);

            if (!phoneNum.IsValid)
                phoneNum = CheckLocal("961", PhoneNumber);

            return phoneNum;
        }
    }

    public class PhoneNumberDetails
    {
        public string CC { get; set; }
        public string IntNum { get; set; }
        public string Local { get; set; }
        public bool IsValid { get; set; }
    }
}
