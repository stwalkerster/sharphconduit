// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConduitLookupBase.cs" company="Simon Walker">
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

namespace Stwalkerster.Bot.PhabricatorLib
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ConduitLookupBase<T>
        where T : ConduitLookupBase<T>
    {
        protected static IDictionary<string, T> LookupMap;

        private readonly string apiName;

        protected ConduitLookupBase(string apiName)
        {
            this.apiName = apiName;
        }

        public string ApiName
        {
            get
            {
                return this.apiName;
            }
        }

        public static T FromApiName(string apiName)
        {
            T col;
            if (apiName != null && LookupMap.TryGetValue(apiName, out col))
            {
                return col;
            }

            return null;
        }

        protected static void SetupLookupMap(IEnumerable<T> items)
        {
            LookupMap = items.ToDictionary(x => x.ApiName);
        }
    }
}