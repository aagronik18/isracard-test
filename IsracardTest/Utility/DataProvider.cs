using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsracardTest.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;

namespace IsracardTest.Utility
{
    public class DataProvider : IDataProvider
    {
        private string _dataAccessUrl = "https://api.github.com/search/repositories?page=1&per_page=16&q=";

        public IEnumerable<CustomCard> GetBySearchString(string searchString)
        {
            var data = GetRemoteData(searchString);

            if (String.IsNullOrWhiteSpace(data))
                return Enumerable.Empty<CustomCard>();

            try
            {
                var json = JObject.Parse(data);

                return json.GetValue("items").ToObject<List<JObject>>().Select(item =>
                    new CustomCard
                    {
                        Id = item?.GetValue("id")?.Value<int>() ?? 0,
                        Title = item?.GetValue("name")?.Value<string>() ?? String.Empty,
                        Description = item?.GetValue("description")?.Value<string>() ?? String.Empty,
                        OwnerAvatarUrl = item?.GetValue("owner")?.Value<JObject>()?.GetValue("avatar_url")?.Value<string>() ?? String.Empty
                });
            }
            catch(Exception exp)
            {
                return Enumerable.Empty<CustomCard>();
            }
        }

        private static bool ValidateRemoteCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private string GetRemoteData(string param)
        {
            if (String.IsNullOrWhiteSpace(param))
                return String.Empty;

            try
            {
                var request = WebRequest.Create(_dataAccessUrl + param) as HttpWebRequest;
                request.UserAgent = "definitely-not-a-screen-scraper";
                request.Method = "GET";

                ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateRemoteCertificate);

                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream, Encoding.UTF8, true))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch(Exception exp)
            {
                return String.Empty;
            }
        }
    }
}
