// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Phrequent.cs" company="Simon Walker">
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

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Stwalkerster.SharphConduit.Applications.Maniphest;

namespace Stwalkerster.SharphConduit.Applications.Phrequent
{
    public class Phrequent : ConduitApplicationBase
    {
        public Phrequent(ConduitClient client) : base(client)
        {
        }

        public void Push(ManiphestTask task, DateTime startTime)
        {
            this.Push(task.ObjectPHID, startTime);
        }

        public void Push(string objectPHID, DateTime startTime)
        {
            int seconds = (int) (startTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

            this.ConduitClient.CallMethod(
                "phrequent.push",
                new Dictionary<string, dynamic> {{"objectPHID", objectPHID}, {"startTime", seconds}});
        }

        public void Pop()
        {
            this.ConduitClient.CallMethod("phrequent.pop", new Dictionary<string, dynamic>());
        }

        public void Pop(ManiphestTask task, DateTime? stopTime = null, string note = null)
        {
            this.Pop(task.ObjectPHID, stopTime, note);
        }

        public void Pop(string objectPHID, DateTime? stopTime = null, string note = null)
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic> {{"objectPHID", objectPHID}};

            if (stopTime.HasValue)
            {
                int time = (int) (stopTime.Value - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
                parameters.Add("stopTime", time);
            }

            if (note != null)
            {
                parameters.Add("note", note);
            }

            this.ConduitClient.CallMethod("phrequent.pop", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Dictionary of PHID to int seconds
        /// </returns>
        public Dictionary<string, int> Tracking()
        {
            dynamic callMethod = this.ConduitClient.CallMethod("phrequent.tracking", new Dictionary<string, dynamic>());

            JArray data = callMethod["result"]["data"];

            var result = new Dictionary<string, int>();

            foreach (JToken obj in data)
            {
                result.Add(obj["phid"].Value<string>(), obj["time"].Value<int>());
            }

            return result;
        }
    }
}