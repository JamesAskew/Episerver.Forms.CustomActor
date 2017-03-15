using System.Collections.Generic;
using System.Collections.Specialized;

namespace Episerver.Forms.CustomActor.Business.Extensions
{
    public static class DictionaryExtensions
    {
        public static NameValueCollection ToNameValueCollection<tValue>(this IEnumerable<KeyValuePair<string, tValue>> dictionary)
        {
            var collection = new NameValueCollection();
            foreach (var pair in dictionary)
                collection.Add(pair.Key, pair.Value.ToString());
            return collection;
        }
    }
}