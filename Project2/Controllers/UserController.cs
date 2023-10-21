using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project2.Controllers
{
    public class UserController : Controller
    {
        private readonly Project2Context _sql;
        public UserController(Project2Context sql)
        {
            _sql = sql;
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Person p)
        {
            var getUser = _sql.People.Include(x => x.PersonRol).SingleOrDefault(x => (x.PersonIstifadeciAdi == p.PersonIstifadeciAdi && x.PersonParol == p.PersonParol));
            if (getUser != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, p.PersonIstifadeciAdi));
                claims.Add(new Claim("Id", getUser.PersonId.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, getUser.PersonRol.RolAd));

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var princitial = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princitial, props).Wait();
                if (getUser.PersonRolId == 1 || getUser.PersonRolId == 2)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Login", "User");
        }

        public IActionResult Qeydiyyat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Qeydiyyat(Person p)
        {
            p.PersonRolId = 3;
            _sql.Add(p);
            _sql.SaveChanges();

            var getUser = _sql.People.Include(x => x.PersonRol).SingleOrDefault(x => (x.PersonIstifadeciAdi == p.PersonIstifadeciAdi && x.PersonParol == p.PersonParol));
            if (getUser != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, p.PersonIstifadeciAdi));
                claims.Add(new Claim("Id", getUser.PersonId.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, getUser.PersonRol.RolAd));


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var princitial = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princitial, props).Wait();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Qeydiyyat", "User");
        }
    }
}
