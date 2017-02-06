// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Diffusion.cs" company="Simon Walker">
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

using Newtonsoft.Json.Linq;

namespace Stwalkerster.SharphConduit.Applications.Diffusion
{
    /// <summary>
    /// The diffusion.
    /// </summary>
    public class Diffusion
    {
        public DiffusionRepository Repositories { get; private set; }

        public Diffusion(ConduitClient client)
        {
            this.Repositories = new DiffusionRepository(client);
        }

        public class DiffusionRepository : ApplicationEditorApi<Repository, int>
        {
            internal DiffusionRepository(ConduitClient client) : base(client)
            {   
            }

            protected override string GetApplicationName()
            {
                return "diffusion.repository";
            }

            protected override Repository NewFromSearch(dynamic data)
            {
                var repo = new Repository(
                    (string) data.phid,
                    (int) data.id,
                    null,
                    (string) data.fields.name,
                    (string) data.fields.vcs,
                    (string) data.fields.callsign,
                    (string) data.fields.shortName,
                    (string) data.fields.status,
                    (bool) data.fields.isImporting,
                    (string) data.fields.spacePHID,
                    (int) data.fields.dateCreated,
                    (int) data.fields.dateModified,
                    (string) data.fields.policy.view,
                    (string) data.fields.policy.edit,
                    (string) ((JObject) data.fields.policy)["diffusion.push"]);

                return repo;
            }
        }
    }
}