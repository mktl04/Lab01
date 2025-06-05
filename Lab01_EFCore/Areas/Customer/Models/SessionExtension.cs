using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01_EFCore.Areas.Customer.Models
{
    public static class SessionExtension
    {
        public static void SetJSon(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetJSon<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
