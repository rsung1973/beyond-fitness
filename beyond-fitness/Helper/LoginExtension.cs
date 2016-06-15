using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using CommonLib.Web;
using WebHome.Models.DataEntity;
using WebHome.Models.Security;
using WebHome.Properties;

namespace WebHome.Helper
{
    public static class LoginExtension
    {
        public static void SignOn(this HttpContextBase context,UserProfile profile)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(profile.PID, false, Settings.Default.UserTimeoutInMinutes);
            context.Response.SetCookie(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));
            context.ClearCache();
            context.SetCacheValue("userProfile",profile);
        }

        public static void ClearCache(this HttpContextBase context)
        {
            DataModelCache caching = new DataModelCache(context);
            caching.Clear();
        }


        public static void SetCacheValue(this HttpContextBase context,String keyName,Object value)
        {
            DataModelCache caching = new DataModelCache(context);
            caching[keyName] = value;
        }

        public static Object GetCacheValue(this HttpContextBase context, String keyName)
        {
            DataModelCache caching = new DataModelCache(context);
            return caching[keyName];
        }


        public static String MakePassword(this String password)
        {
            return Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.Default.GetBytes(password)));
        }

        public static void Logout(this HttpContextBase context)
        {
            context.Response.SetCookie(new HttpCookie(FormsAuthentication.FormsCookieName, ""));
            context.ClearCache();
        }

        public static UserProfile GetUser(this HttpContextBase context)
        {
            return (UserProfile)context.GetCacheValue("userProfile");
        }

        public static UserProfile GetUser(this HttpContext context)
        {
            HttpContextDataModelCache caching = new HttpContextDataModelCache(context);
            return (UserProfile)caching["userProfile"];
        }
    }
}