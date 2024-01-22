using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebDuLich.Common
{
    public class Base
    {

        // conver chuỗi html sang dạng chuỗi
        public static string StripHTML(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Regex.Replace(input, "<.*?>", String.Empty);
            }
            return input;
        }


        public static string FormatNumberVND(int? val)
        {
            return string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VNĐ", val);
        }
    }
}