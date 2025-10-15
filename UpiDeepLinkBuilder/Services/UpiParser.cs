namespace TechTonix.UpiDeepLinkBuilder.Services
{
    using System;
    using System.Collections.Generic;

    public sealed class UpiParser
    {
        public Dictionary<string, string> ParseUpiDeeplink(string upiUrl)
        {
            Uri uri = new Uri(upiUrl);
            List<string> queryParams = uri.Query.TrimStart('?').Split('&').ToList();
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (string param in queryParams)
            {
                string[] keyValue = param.Split('=');
                if (keyValue.Length == 2)
                {
                    result[keyValue[0]] = Uri.UnescapeDataString(keyValue[1]);
                }
            }

            return result;
        }
    }
}
