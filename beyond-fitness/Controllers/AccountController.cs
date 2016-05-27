using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebHome.Models.ViewModel;

namespace WebHome.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    result = false,
                    errors = ModelState.Keys.Where(k => ModelState[k].Errors.Count > 0)
                            .Select(k => new { name = k, message = ModelState[k].Errors.Select(r => r.ErrorMessage) }).ToArray()
                });
            }

            return View();
        }
    }
}