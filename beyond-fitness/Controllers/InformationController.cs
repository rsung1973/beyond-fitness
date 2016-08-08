using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.ViewModel;
using WebHome.Properties;

namespace WebHome.Controllers
{
    [Authorize]
    public class InformationController : SampleController<UserProfile>
    {
        public InformationController() : base() { }
        // GET: Information
        [AllowAnonymous]
        public ActionResult Blog(PagingIndexViewModel viewModel)
        {
            ViewBag.PagingModel = viewModel;


            var items = models.GetDataContext().GetNormalArticles(models.GetTable<Article>())
                .OrderByDescending(a => a.Document.DocDate)
                .Skip(viewModel.CurrentIndex * viewModel.PageSize)
                .Take(viewModel.PageSize);

            return View(items);
        }

        [AllowAnonymous]
        public ActionResult BlogDetail(int id)
        {



            var item = models.GetTable<Article>().Where(a => a.DocID == id).FirstOrDefault();
            if (item == null)
            {
                return Redirect("~/Views/Shared/Error.aspx");
            }
            return View(item);
        }

        public ActionResult Preview(int id)
        {
            return BlogDetail(id);
        }

        public ActionResult DeleteBlog(int docID)
        {



            var item = models.GetTable<Article>().Where(a => a.DocID == docID).FirstOrDefault();
            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return Publish(null);
            }

            item.Document.CurrentStep = (int)Naming.DocumentLevelDefinition.已刪除;
            models.SubmitChanges();

            ViewBag.Message = "文件已刪除!!";
            return Publish(null);

        }

        public ActionResult Publish(PagingIndexViewModel viewModel)
        {
            ViewBag.PagingModel = viewModel;


            var items = models.GetDataContext().GetNormalArticles(models.GetTable<Article>())
                .OrderByDescending(a => a.Document.DocDate);
            //.Skip(viewModel.CurrentIndex * viewModel.PageSize)
            //.Take(viewModel.PageSize);

            return View("Publish",items);
        }

        public ActionResult CreateNew()
        {



            var item = models.GetTable<Article>().Where(a => a.Document.CurrentStep == (int)Naming.DocumentLevelDefinition.暫存).FirstOrDefault();
            if (item == null)
            {
                item = new Article
                {
                    Document = new Document
                    {
                        DocDate = DateTime.Now,
                        CurrentStep = (int)Naming.DocumentLevelDefinition.暫存,
                        DocType = (int)Naming.DocumentTypeDefinition.Knowledge
                    }
                };
                models.GetTable<Article>().InsertOnSubmit(item);
                models.SubmitChanges();
            }
            return View("EditBlog", item);
        }

        public ActionResult EditBlog(int id)
        {

            var item = models.GetTable<Article>().Where(a => a.DocID == id).FirstOrDefault();
            if (item == null)
            {
                ViewBag.Message = "文章資料不存在!!";
                return RedirectToAction("Publish");
            }
            return View(item);
        }

        [AllowAnonymous]
        public ActionResult Resource(int id)
        {



            var item = models.GetTable<Article>().Where(a => a.DocID == id).FirstOrDefault();
            return View(item);
        }

        [AllowAnonymous]
        public ActionResult UploadResource(int id)
        {


            var item = models.GetTable<Article>().Where(a => a.DocID == id).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "文章不存在!!" });
            }
            if (Request.Files.Count <= 0)
            {
                return Json(new { result = false, message = "檔案上載失敗!!" });
            }

            try
            {
                String storePath = Path.Combine(Logger.LogDailyPath, Guid.NewGuid().ToString() + Path.GetExtension(Request.Files[0].FileName));
                Request.Files[0].SaveAs(storePath);

                models.GetTable<Attachment>().InsertOnSubmit(new Attachment
                {
                    DocID = item.DocID,
                    StoredPath = storePath
                });
                models.SubmitChanges();
                return Json(new { result = true });

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }

        }

        [AllowAnonymous]
        public ActionResult RetrieveResource(int docID, String imgUrl)
        {


            var item = models.GetTable<Article>().Where(a => a.DocID == docID).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "文章不存在!!" });
            }

            if (String.IsNullOrEmpty(imgUrl))
            {
                return Json(new { result = false, message = "來源網址錯誤!!" });
            }

            try
            {
                String storePath = Path.Combine(Logger.LogDailyPath, Guid.NewGuid().ToString() + ".dat");
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(imgUrl, storePath);
                }

                models.GetTable<Attachment>().InsertOnSubmit(new Attachment
                {
                    DocID = item.DocID,
                    StoredPath = storePath
                });
                models.SubmitChanges();
                return Json(new { result = true, message = "擷取完成!!" });

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }

        }

        public ActionResult MakeTheme(int docID, int attachmentID)
        {


            var item = models.GetTable<Article>().Where(a => a.DocID == docID).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "文章不存在!!" });
            }

            var attachment = models.GetTable<Attachment>().Where(a => a.AttachmentID == attachmentID).FirstOrDefault();
            if (attachment == null)
            {
                return Json(new { result = false, message = "圖檔不存在!!" });
            }


            try
            {
                item.Illustration = attachment.AttachmentID;
                models.SubmitChanges();
                return Json(new { result = true, message = "資料已更新!!" });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }

        }

        public ActionResult UpdateArticle(int docID, int docType, String title, String docDate, String content,int? authorID)
        {

            if(String.IsNullOrEmpty(title))
            {
                return Json(new { result = false, message = "請輸入文標題!!" });
            }

            var item = models.GetTable<Article>().Where(a => a.DocID == docID).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "文章不存在!!" });
            }

            try
            {
                item.Document.DocType = docType;
                item.Document.DocDate = DateTime.ParseExact(docDate, "yyyy/MM/dd", System.Globalization.CultureInfo.CurrentCulture);
                item.Document.CurrentStep = (int)Naming.DocumentLevelDefinition.正常;
                item.Title = title;
                item.AuthorID = authorID;
                item.ArticleContent = HttpUtility.HtmlDecode(content);

                models.SubmitChanges();

                return Json(new { result = true, message = "文章已更新!!" });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }

        }

        public ActionResult DeleteResource(int docID, int attachmentID)
        {


            var item = models.GetTable<Article>().Where(a => a.DocID == docID).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "文章不存在!!" });
            }

            var attachment = models.GetTable<Attachment>().Where(a => a.AttachmentID == attachmentID).FirstOrDefault();
            if (attachment == null)
            {
                return Json(new { result = false, message = "圖檔不存在!!" });
            }

            try
            {
                if (item.Illustration == attachment.AttachmentID)
                {
                    item.Illustration = null;
                }


                models.GetTable<Attachment>().DeleteOnSubmit(attachment);
                if (System.IO.File.Exists(attachment.StoredPath))
                    System.IO.File.Delete(attachment.StoredPath);

                models.SubmitChanges();

                return Json(new { result = true, message = "圖片已更新!!" });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }

        }

        [AllowAnonymous]
        public ActionResult GetResource(int id)
        {
            var item = models.GetTable<Attachment>().Where(a => a.AttachmentID == id).FirstOrDefault();
            if (item != null)
            {
                return File(item.StoredPath, "application/octet-stream");
            }
            return new EmptyResult();

        }

        [AllowAnonymous]
        public ActionResult Footer()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Rental()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Products()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Cooperation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Location()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ContactUs(String email,String userName,String subject,String comment)
        {
            ThreadPool.QueueUserWorkItem(t => {

                try
                {

                    StringBuilder body = new StringBuilder();
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    message.ReplyToList.Add(Settings.Default.WebMaster);
                    message.From = new System.Net.Mail.MailAddress(email,userName);
                    message.To.Add(Settings.Default.WebMaster);
                    message.Subject = subject;
                    message.IsBodyHtml = true;

                    message.Body = HttpUtility.HtmlDecode(comment);

                    System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient(Settings.Default.SmtpServer);
                    //smtpclient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    smtpclient.Send(message);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            });

            ViewBag.Success = "Your comment was successfully added!";
            return View("Success");
        }

        public ActionResult Vip()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Professional(String content)
        {
            if (String.IsNullOrEmpty(content))
            {
                return View();
            }

            return View(content);
        }

        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }

    }
}