using CommonLib.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebHome.Models.DataEntity;

namespace WebHome.Controllers
{

    public class SampleController<TEntity> : Controller
        where TEntity : class, new()
    {
        protected internal ModelSource<TEntity> _dataSource;

        protected internal bool _dbInstance;
        protected internal GenericManager<BFDataContext> models;

        protected SampleController() : base()
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            models.Dispose();
        }

        public ModelSource<TEntity> DataSource => _dataSource;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            models = HttpContext.Items["__DB_Instance"] as GenericManager<BFDataContext>;
            if (models == null)
            {
                models = new GenericManager<BFDataContext>();
                _dbInstance = true;
                HttpContext.Items["__DB_Instance"] = models;
            }

            _dataSource = new ModelSource<TEntity>(models);
            HttpContext.Items["Models"] = DataSource;

            var lang = Request.Cookies["cLang"];
            if (lang != null)
            {
                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                ViewBag.Lang = lang;
            }
        }
    }
}