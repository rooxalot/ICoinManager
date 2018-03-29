using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICoinManager.Common.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static Guid ToGuid(this object obj)
        {
            Guid ID;
            var parseSuccess = Guid.TryParse(obj.ToString(), out ID);
            return parseSuccess ? ID : Guid.Empty;
        }

        public static string ToJson(this object obj)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(obj, jsonSettings);
            return json;
        }
    }
}
