// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Paste.cs" company="Simon Walker">
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

using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Stwalkerster.SharphConduit.Applications.Paste
{
    public class Paste : ApplicationEditorApi<PasteItem, int>
    {
        public Paste(ConduitClient client)
            : base(client)
        {
        }

        protected override PasteItem NewFromSearch(dynamic data)
        {
            var subscribers = new List<string>();
            var projects = new List<string>();

            string content = null;

            if (data.attachments != null)
            {
                if (data.attachments.projects != null)
                {
                    var jArray = (JArray) data.attachments.projects.projectPHIDs;
                    projects = new List<string>(jArray.Values<string>());
                }

                if (data.attachments.subscribers != null)
                {
                    var jArray = (JArray) data.attachments.subscribers.subscriberPHIDs;
                    subscribers = new List<string>(jArray.Values<string>());
                }

                if (data.attachments.content != null)
                {
                    content = (string) data.attachments.content.content;
                }
            }

            return new PasteItem(
                (string) data.phid,
                (int) data.id,
                (string) data.fields.title,
                content,
                (string) data.fields.authorPHID,
                (string) data.fields.status,
                (string) data.fields.language,
                (string) data.fields.spacePHID,
                (string) data.fields.policy.view,
                (string) data.fields.policy.edit,
                (int) data.fields.dateCreated,
                (int) data.fields.dateModified,
                projects,
                subscribers);
        }

        protected override string GetApplicationName()
        {
            return "paste";
        }
    }
}