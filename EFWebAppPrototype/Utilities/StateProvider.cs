using System.Collections;
using System.Web;

namespace EFWebAppPrototype.Utilities
{
    public static class StateProvider
    {

        public static IDictionary Items
        {
            get { return HttpContext.Current.Items; }
        }

        public static object GetObject(string key, bool useApplication)
        {
            if (useApplication)
            {
                return HttpContext.Current.Application[key];
            }
            return HttpContext.Current.Session[key];
        }

        public static void SetObject(string key, object value, bool useApplication)
        {
            if (useApplication)
            {
                HttpContext.Current.Application[key] = value;
            }
            else
            {
                HttpContext.Current.Session[key] = value;
            }
        }

        public static void SetCacheObject(string key, object value)
        {
            HttpContext.Current.Cache[key] = value;
        }

        public static object GetCacheObject(string key)
        {
            return HttpContext.Current.Cache[key];
        }
    }
}