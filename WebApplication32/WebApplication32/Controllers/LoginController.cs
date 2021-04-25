using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using WebApplication32.Models;
using Microsoft.AspNetCore.Authentication;

namespace WebApplication32.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();
        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }


		public async Task<IActionResult> GirisYap(Admin p)
		{

			var bilgiler = c.Admins.FirstOrDefault(x => x.Kullanici == p.Kullanici && x.Sifre == p.Sifre);
			if (bilgiler != null)
			{
				var claims = new List<Claim>{
			new Claim(ClaimTypes.Name,p.Kullanici)
		};

				var useridentity = new ClaimsIdentity(claims, "Login");
				ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
				await HttpContext.SignInAsync(principal);
				return RedirectToAction("Index", "Personelim");
			}
			return View();
		}
	}
}
