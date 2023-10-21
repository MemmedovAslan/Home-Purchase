using Microsoft.AspNetCore.Mvc;
using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project2.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        private readonly Project2Context _sql;
        public UserViewComponent(Project2Context sql)
        {
            _sql = sql;
        }

        public IViewComponentResult Invoke() {
            var u = (ClaimsPrincipal)User;
            int userid = int.Parse(u.Claims.SingleOrDefault(x => x.Type == "Id").Value);
            var user = _sql.People.SingleOrDefault(x=>x.PersonId == userid);
            return View(user);
        }
    }
}
