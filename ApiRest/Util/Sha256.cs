using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiRest.Util
{
    public static class Sha256
    {
        public static string computeSha256(string text)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            var byteshash = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);
            string hash = BitConverter.ToString(byteshash).Replace("-", String.Empty);
            return hash;
        }
    }
}