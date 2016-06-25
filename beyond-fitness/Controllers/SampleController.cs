using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.ViewModel;

namespace WebHome.Controllers
{
    public class SampleController<TEntity> : Controller
        where TEntity : class, new()
    {
        protected ModelSource<TEntity> models;

        protected SampleController() :base()
        {
            models = new ModelSource<TEntity>();
            
        }

        public ModelSource<TEntity> DataSource
        {
            get
            {
                return models;
            }
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            models.Dispose();
        }
    }
}