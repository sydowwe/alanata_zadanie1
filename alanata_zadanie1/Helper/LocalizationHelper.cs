using System.Web;

namespace alanata_zadanie1.Helper
{
    public class LocalizationHelper
    {
        public static string GetCurrentLocale(HttpContext context)
        {
            HttpCookie cookie = context.Request.Cookies["Culture"];
            return cookie?.Value ?? "en";
        }
    }
}