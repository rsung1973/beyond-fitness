using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebHome.Helper;
using WebHome.Models.DataEntity;
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
            models.Items = models.Items.OrderByDescending(a => a.DocID)
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


    }
}