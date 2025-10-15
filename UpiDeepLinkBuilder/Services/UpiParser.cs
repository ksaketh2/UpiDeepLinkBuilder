namespace TechTonix.UpiDeepLinkBuilder.Services
{
    using System;
    using System.Collections.Generic;

    public sealed class UpiParser
    {
        /// <summary>
        /// Parses a UPI deeplink URL and extracts its query parameters into a dictionary.
        /// </summary>
        /// <remarks>This method assumes that the query parameters in the UPI deeplink are formatted as
        /// key-value pairs separated by an equals sign ('=') and delimited by an ampersand ('&').</remarks>
        /// <param name="upiUrl">The UPI deeplink URL to parse. Must be a valid URI.</param>
        /// <returns>A dictionary containing the query parameters as key-value pairs. The keys represent the parameter names, and
        /// the values represent the decoded parameter values. If the URL contains no query parameters, an empty
        /// dictionary is returned.</returns>
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
