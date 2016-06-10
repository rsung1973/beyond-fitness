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

            DataModelCache caching = new DataModelCache(context);
            caching["userProfile"] = profile;

        }

        public static String MakePassword(this String password)
        {
            return Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.Default.GetBytes(password)));
        }

        public static void Logout(this HttpContextBase context)
        {
            context.Response.SetCookie(new HttpCookie(FormsAuthentication.FormsCookieName, ""));
            DataModelCache caching = new DataModelCache(context);
            caching["userProfile"] = null;
        }

        public static UserProfile GetUser(this HttpContextBase context)
        {
            DataModelCache caching = new DataModelCache(context);
            return (UserProfile)caching["userProfile"];
        }

        public static UserProfile GetUser(this HttpContext context)
        {
            HttpContextDataModelCache caching = new HttpContextDataModelCache(context);
            return (UserProfile)caching["userProfile"];
        }
    }
}