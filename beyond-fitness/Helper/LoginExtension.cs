using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using CommonLib.Web;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
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

            /// process sign-on user profile
            /// 
            var roles = profile.UserRole.Select(r => r.UserRoleDefinition).ToArray();
        }

        public static void ClearCache(this HttpContextBase context)
        {
            DataModelCache caching = new DataModelCache(context);
            caching.Clear();
        }
        public static void ClearCache(this HttpContext context)
        {
            HttpContextDataModelCache caching = new HttpContextDataModelCache(context);
            caching.Clear();
        }


        public static void SetCacheValue(this HttpContextBase context, CachingKey keyName, Object value)
        {
            context.SetCacheValue(keyName.ToString(), value);
        }

        public static void RemoveCache(this HttpContextBase context, CachingKey keyName)
        {
            context.SetCacheValue(keyName.ToString(), null);
        }


        public static Object GetCacheValue(this HttpContextBase context, CachingKey keyName)
        {
            return context.GetCacheValue(keyName.ToString());
        }

        public static void SetCacheValue(this HttpContextBase context,String keyName,Object value)
        {
            DataModelCache caching = new DataModelCache(context);
            caching[keyName] = value;
        }

        public static void SetCacheValue(this HttpContext context, String keyName, Object value)
        {
            HttpContextDataModelCache caching = new HttpContextDataModelCache(context);
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
            UserProfile profile = (UserProfile)context.GetCacheValue("userProfile");
            if (profile == null)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    profile = context.User.Identity.Name.getLoginUser();
                    context.SetCacheValue("userProfile", profile);
                }
            }
            return profile;
        }

        private static UserProfile getLoginUser(this String pid)
        {
            using (ModelSource<UserProfile> Models = new ModelSource<UserProfile>())
            {
                UserProfile profile = Models.EntityList.Where(u => u.PID == pid).FirstOrDefault();
                if (profile != null)
                {
                    var roles = profile.UserRole.Select(r => r.UserRoleDefinition).ToArray();
                }
                return profile;
            }
        }

        public static UserProfile GetUser(this HttpContext context)
        {
            HttpContextDataModelCache caching = new HttpContextDataModelCache(context);
            UserProfile profile =  (UserProfile)caching["userProfile"];
            if (profile == null)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    profile = context.User.Identity.Name.getLoginUser();
                    context.SetCacheValue("userProfile", profile);
                }
            }
            return profile;
        }

        public static bool IsFreeAgent(this UserProfile profile)
        {
            return profile.CurrentUserRole.RoleID == (int)Naming.RoleID.FreeAgent;
        }
    }
}