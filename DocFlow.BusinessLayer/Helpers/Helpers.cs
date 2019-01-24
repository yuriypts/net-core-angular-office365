using System;

namespace DocFlow.BusinessLayer.Helpers
{
    public static class Helpers
    {
        public static DateTime GetDateTime()
        {
            DateTime date = DateTime.Now;
            string newFormatDate = date.ToString("MM/dd/yyyy h:mm:ss");

            return Convert.ToDateTime(newFormatDate);
        }

        public static DateTime ConvertToEnUsDateFormat(DateTime date)
        {
            string newFormatDate = date.ToString("MM/dd/yyyy h:mm:ss");
            return Convert.ToDateTime(newFormatDate);
        }

        public static string ConvertToEnUsShortDateFormat(DateTime date)
        {
            return date.ToString("MM/dd/yyyy");
        }

        public static string GetNameFileWithCurrentDate(string value)
        {
            return $"{value}-{GetDateTime().ToString()}";
        }

        public static string GetSignedNameFileWithCurrentDate(string value)
        {
            return $"Signed-{value}-{GetDateTime().ToString()}";
        }
    }
}
