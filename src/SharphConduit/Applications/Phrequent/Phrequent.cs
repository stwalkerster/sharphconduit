using System;
using System.Collections.Generic;

namespace Stwalkerster.SharphConduit.Applications.Phrequent
{
    using Newtonsoft.Json.Linq;

    using Stwalkerster.SharphConduit.Applications.Maniphest;

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
            int seconds = (int)(startTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

            this.client.CallMethod(
                "phrequent.push",
                new Dictionary<string, dynamic> { { "objectPHID", objectPHID }, { "startTime", seconds } });
        }

        public void Pop()
        {
            this.client.CallMethod("phrequent.pop", new Dictionary<string, dynamic>());
        }

        public void Pop(ManiphestTask task, DateTime? stopTime = null, string note = null)
        {
            this.Pop(task.ObjectPHID, stopTime, note);    
        }

        public void Pop(string objectPHID, DateTime? stopTime = null, string note = null)
        {
            Dictionary<string, dynamic> parameters = new Dictionary<string, dynamic>();
            parameters.Add("objectPHID", objectPHID);

            if (stopTime.HasValue)
            {
                int time = (int)(stopTime.Value - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
                parameters.Add("stopTime", time);
            }

            if (note != null)
            {
                parameters.Add("note", note);
            }

            this.client.CallMethod("phrequent.pop", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Dictionary of PHID to int seconds
        /// </returns>
        public Dictionary<string, int> Tracking()
        {
            dynamic callMethod = this.client.CallMethod("phrequent.tracking", new Dictionary<string, dynamic>());

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
