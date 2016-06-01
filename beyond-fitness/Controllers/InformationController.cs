using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.ViewModel;

namespace WebHome.Controllers
{
    public class InformationController : Controller
    {
        // GET: Information
        public ActionResult Blog(PagingIndexViewModel viewModel)
        {
            ViewBag.PagingModel = viewModel;
            ModelSource<Article> models = new ModelSource<Article>();
            TempData.SetModelSource(models);
            models.Items = models.Items.Where(a => a.Document.CurrentStep == (int)Naming.DocumentLevelDefinition.正常)
                .OrderByDescending(a => a.DocID)
                .Skip(viewModel.CurrentIndex * viewModel.PageSize)
                .Take(viewModel.PageSize);

            return View(models.Items);
        }

        public ActionResult BlogDetail(int id)
        {
            ModelSource<Article> models = new ModelSource<Article>();
            TempData.SetModelSource(models);

            var item = models.Items.Where(a => a.DocID == id).FirstOrDefault();
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
            ModelSource<Article> models = new ModelSource<Article>();
            TempData.SetModelSource(models);

            var item = models.Items.Where(a => a.DocID == docID).FirstOrDefault();
            if (item == null)
            {
                return Redirect("~/Views/Shared/Error.aspx");
            }

            item.Document.CurrentStep = (int)Naming.DocumentLevelDefinition.已刪除;
            models.SubmitChanges();

            return Json(new { result = true, message = "文件已刪除!!" });

        }

        public ActionResult Publish(PagingIndexViewModel viewModel)
        {
            ViewBag.PagingModel = viewModel;
            ModelSource<Article> models = new ModelSource<Article>();
            TempData.SetModelSource(models);
            models.Items = models.Items.Where(a => a.Document.CurrentStep == (int)Naming.DocumentLevelDefinition.正常)
                .OrderByDescending(a => a.DocID)
                .Skip(viewModel.CurrentIndex * viewModel.PageSize)
                .Take(viewModel.PageSize);

            return View(models.Items);
        }

        public ActionResult CreateNew()
        {
            ModelSource<Article> models = new ModelSource<Article>();
            TempData.SetModelSource(models);

            var item = models.Items.Where(a => a.Document.CurrentStep == (int)Naming.DocumentLevelDefinition.暫存).FirstOrDefault();
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
                models.EntityList.InsertOnSubmit(item);
                models.SubmitChanges();
            }
            return View("EditBlog",item);
        }

        public ActionResult EditBlog(int id)
        {
            ModelSource<Article> models = new ModelSource<Article>();
            TempData.SetModelSource(models);

            var item = models.Items.Where(a => a.DocID == id).FirstOrDefault();
            if (item == null)
            {
                return Redirect("~/Views/Shared/Error.aspx");
            }
            return View(item);
        }

        public ActionResult Resource(int id)
        {
            ModelSource<Article> models = new ModelSource<Article>();
            TempData.SetModelSource(models);

            var item = models.Items.Where(a => a.DocID == id).FirstOrDefault();
            return View(item);
        }

        public ActionResult UploadResource(int id)
        {
            using (ModelSource<Article> models = new ModelSource<Article>())
            {
                TempData.SetModelSource(models);

                var item = models.Items.Where(a => a.DocID == id).FirstOrDefault();
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
        }

        public ActionResult RetrieveResource(int docID,String imgUrl)
        {
            using (ModelSource<Article> models = new ModelSource<Article>())
            {
                TempData.SetModelSource(models);

                var item = models.Items.Where(a => a.DocID == docID).FirstOrDefault();
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
        }

        public ActionResult MakeTheme(int docID, int attachmentID)
        {
            using (ModelSource<Article> models = new ModelSource<Article>())
            {
                TempData.SetModelSource(models);

                var item = models.Items.Where(a => a.DocID == docID).FirstOrDefault();
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
        }

        public ActionResult UpdateArticle(int docID, int docType,String title,String docDate,String content)
        {
            using (ModelSource<Article> models = new ModelSource<Article>())
            {
                TempData.SetModelSource(models);

                var item = models.Items.Where(a => a.DocID == docID).FirstOrDefault();
                if (item == null)
                {
                    return Json(new { result = false, message = "文章不存在!!" });
                }

                try
                {
                    item.Document.DocType = docType;
                    item.Document.DocDate = DateTime.ParseExact(docDate, "yyyy/MM/dd", System.Globalization.CultureInfo.CurrentCulture);
                    item.Title = title;
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
        }

        public ActionResult DeleteResource(int docID, int attachmentID)
        {
            using (ModelSource<Article> models = new ModelSource<Article>())
            {
                TempData.SetModelSource(models);

                var item = models.Items.Where(a => a.DocID == docID).FirstOrDefault();
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
        }

        public ActionResult GetResource(int id)
        {
            using (ModelSource<Attachment> models = new ModelSource<Attachment>())
            {
                TempData.SetModelSource(models);

                var item = models.Items.Where(a => a.AttachmentID == id).FirstOrDefault();
                if (item != null)
                {
                    return File(item.StoredPath, "application/octet-stream");
                }
                return new EmptyResult();
            }
        }


        public ActionResult Footer()
        {
            return View();
        }

        public ActionResult Rental()
        {
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult Cooperation()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Vip()
        {
            return View();
        }
        public ActionResult Professional(String content)
        {
            if (String.IsNullOrEmpty(content))
            {
                return View();
            }

            return View(content);
        }

        public ActionResult Error()
        {
            return View();
        }


    }
}