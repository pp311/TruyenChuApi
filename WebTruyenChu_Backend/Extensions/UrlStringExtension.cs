using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace WebTruyenChu_Backend.Extensions;

public static class UrlStringExtension
{
    public static string Base64ForUrlEncode(this string str)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            return WebEncoders.Base64UrlEncode(encbuff);
        }

        public static string Base64ForUrlDecode(this string str)
        {
            byte[] decbuff = WebEncoders.Base64UrlDecode(str);
            return Encoding.UTF8.GetString(decbuff);
        }
}