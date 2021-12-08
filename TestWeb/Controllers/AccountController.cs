using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomPolicyProvider.Controllers
{
    public class AccountController: Controller
    {
        [HttpGet]
        public IActionResult Signin(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(string userName, DateTime? birthDate, string returnUrl = null)
        {
            var task = SaveAsAsync("G:\\temp\\request.txt");
            if (string.IsNullOrEmpty(userName)) return BadRequest("A user name is required");

            // In a real-world application, user credentials would need validated before signing in
            var claims = new List<Claim>();
            // Add a Name claim and, if birth date was provided, a DateOfBirth claim
            claims.Add(new Claim(ClaimTypes.Name, userName));
            if (birthDate.HasValue)
            {
                claims.Add(new Claim(ClaimTypes.DateOfBirth, birthDate.Value.ToShortDateString()));
            }

            // Create user's identity and sign them in
            var identity = new ClaimsIdentity(claims, "UserSpecified");
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return Redirect(returnUrl ?? "/");
        }

        async Task SaveAsAsync(String fileName)
        {
            Request.EnableBuffering();
            Request.Body.Position = 0;
            using var streamReader = new StreamReader(Request.Body);
            string bodyContent = await streamReader.ReadToEndAsync();

            Request.Body.Position = 0;
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                using(StreamWriter writer = new StreamWriter(fs,leaveOpen:true))
                {
                    foreach(var h in Request.Headers)
                    {
                        writer.WriteLine($"{h.Key}: {h.Value}");
                    }
                    writer.WriteLine();
                }
                await Request.Body.CopyToAsync(fs);
            }
            Request.Body.Position = 0;
        }

        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}
