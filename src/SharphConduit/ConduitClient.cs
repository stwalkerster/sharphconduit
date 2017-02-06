// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConduitClient.cs" company="Simon Walker">
//   Copyright (c) 2016 Simon Walker
//   -
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation
//   the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above 
//   copyright notice and this permission notice shall be included in all copies or substantial portions of the 
//   Software.
//   -
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//   THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
//   CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
//   IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stwalkerster.SharphConduit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ConduitClient
    {
        private readonly string token;

        private readonly string url;

        public ConduitClient(string url, string token)
        {
            this.url = url;
            this.token = token;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public dynamic CallMethod(string method, IDictionary<string, dynamic> parameters)
        {
            // clone the dictionary
            var parameterDictionary = new Dictionary<string, dynamic>(parameters);

            parameterDictionary.Add("__conduit__", new { this.token });
            var json = JsonConvert.SerializeObject(parameterDictionary);

            var webRequest = (HttpWebRequest)WebRequest.Create(string.Format("{0}api/{1}", this.url, method));
            webRequest.Method = "POST";

            IAsyncResult getRequestHandle = webRequest.BeginGetRequestStream(x => { }, new object());

            string postData = string.Format("params={0}&format=json&__conduit__=1", Uri.EscapeDataString(json));
            
            var requestStream = new StreamWriter(webRequest.EndGetRequestStream(getRequestHandle));
            requestStream.Write(postData);
            requestStream.Flush();

            var getResponseHandle = webRequest.BeginGetResponse(x => { }, new object());
            var response = (HttpWebResponse)webRequest.EndGetResponse(getResponseHandle);

            var responseRawStream = response.GetResponseStream();

            if (responseRawStream == null)
            {
                throw new Exception("Umm... we didn't get a response from Conduit.");
            }

            var responseStream = new StreamReader(responseRawStream);
            var responseData = responseStream.ReadToEnd();

            JObject result = (JObject)JsonConvert.DeserializeObject(responseData);

            if (result["error_code"].Type != JTokenType.Null)
            {
                throw new ConduitException((string)result["error_code"], (string)result["error_info"]);
            }

            return result;
        }
    }
}