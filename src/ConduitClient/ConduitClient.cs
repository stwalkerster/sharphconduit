namespace Stwalkerster.ConduitClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Web;

    using Newtonsoft.Json;

    public class ConduitClient
    {
        private readonly string url;

        private readonly string token;

        public ConduitClient(string url, string token)
        {
            this.url = url;
            this.token = token;
        }

        public dynamic CallMethod(string method, IDictionary<string, dynamic> parameters)
        {
            parameters.Add("__conduit__", new { this.token });
            var json = JsonConvert.SerializeObject(parameters);
            
            var webRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}api/{1}", this.url, method));
            webRequest.Method = "POST";
            string postData = string.Format("params={0}&format=json&__conduit__=1", HttpUtility.UrlEncode(json));

            var requestStream = new StreamWriter(webRequest.GetRequestStream());
            requestStream.Write(postData);
            requestStream.Flush();
            requestStream.Close();
            
            var response = (HttpWebResponse)webRequest.GetResponse();

            var responseRawStream = response.GetResponseStream();

            if (responseRawStream == null)
            {
                throw new Exception("Umm... we didn't get a response from Conduit.");
            }

            var responseStream = new StreamReader(responseRawStream);
            var responseData = responseStream.ReadToEnd();

            return JsonConvert.DeserializeObject(responseData);
        }
    }
}
