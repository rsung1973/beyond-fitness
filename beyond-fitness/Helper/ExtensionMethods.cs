using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CommonLib.DataAccess;
using Utility;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Properties;

namespace WebHome.Helper
{
    public static partial class ExtensionMethods
    {
        public static void SetModelSource<TEntity>(this TempDataDictionary tempData, ModelSource<TEntity> models)
            where TEntity : class, new()
        {
            tempData["modelSource"] = models;
        }

        public static ModelSource<TEntity> GetModelSource<TEntity>(this TempDataDictionary tempData)
            where TEntity : class, new()
        {
            return (ModelSource<TEntity>)tempData["modelSource"];
        }

        public static GenericManager<BFDataContext> GetGenericModelSource(this TempDataDictionary tempData)
        {
            return (GenericManager<BFDataContext>)tempData["modelSource"];
        }


        public static ModelSource<TEntity> InvokeModelSource<TEntity>(this TempDataDictionary tempData)
            where TEntity : class, new()
        {
            GenericManager<BFDataContext> models = tempData.GetGenericModelSource();
            if (models == null)
            {
                models = new ModelSource<TEntity>();
                tempData.SetModelSource<TEntity>((ModelSource<TEntity>)models);
                return (ModelSource<TEntity>)models;
            }
            else if (models is ModelSource<TEntity>)
            {
                return (ModelSource<TEntity>)models;
            }
            else
            {
                return new ModelSource<TEntity>(models);
            }
        }

        public static IQueryable<Article> GetNormalArticles(this BFDataContext context, IQueryable<Article> items)
        {
            return items.Where(a => a.Document.CurrentStep == (int)Naming.DocumentLevelDefinition.正常);
        }

        public static void NotifyResetPassword(this ResetPassword item)
        {
            ThreadPool.QueueUserWorkItem(t => {

                try
                {

                    StringBuilder body = new StringBuilder();
                    MailMessage message = new MailMessage();
                    message.ReplyToList.Add(Settings.Default.WebMaster);
                    message.From = new MailAddress(Settings.Default.WebMaster);
                    message.To.Add(item.UserProfile.EMail);
                    message.Subject = "Beyond-fitness會員密碼重設通知";
                    message.IsBodyHtml = true;

                    body.Append("您好，請由下列連結重設您的密碼，謝謝。<br/>")
                        .Append("<a href=").Append(Settings.Default.HostDomain).Append(VirtualPathUtility.ToAbsolute("~/Account/ResetPass"))
                        .Append("/").Append(item.ResetID)
                        .Append(">會員重設密碼</a>");

                    message.Body = body.ToString();

                    SmtpClient smtpclient = new SmtpClient(Settings.Default.SmtpServer);
                    //smtpclient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    smtpclient.Send(message);
                }
                catch(Exception ex)
                {
                    Logger.Error(ex);
                }
            });
        }
    }
}