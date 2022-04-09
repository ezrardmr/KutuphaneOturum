using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace KutuphaneOturum.Models
{
    public static class Hash
    {
        public static string md5(this string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                using (var md = MD5.Create())
                {
                    var bytes = Encoding.UTF8.GetBytes(password);
                    var hash = md.ComputeHash(bytes);

                    return Convert.ToBase64String(hash);
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}