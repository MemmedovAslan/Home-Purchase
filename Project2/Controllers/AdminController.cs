using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    public class AdminController : Controller
    {
        private readonly Project2Context _sql;
        public AdminController(Project2Context sql)
        {
            _sql = sql;
        }




        public IActionResult Index()
        {
            return View(_sql.People.Where(x => x.PersonRolId == 3).ToList());
        }




        public IActionResult IstifadeciDuzelt(int id)
        {
            return View(_sql.People.SingleOrDefault(x => x.PersonId == id));
        }




        [HttpPost]
        public IActionResult IstifadeciDuzelt(int id, Person p)
        {
            Person pkohne = _sql.People.SingleOrDefault(x => x.PersonId == id);
            pkohne.PersonIstifadeciAdi = p.PersonIstifadeciAdi;
            pkohne.PersonAd = p.PersonAd;
            pkohne.PersonSoyad = p.PersonSoyad;
            pkohne.PersonNomre = p.PersonNomre;
            pkohne.PersonParol = p.PersonParol;
            pkohne.PersonEmail = p.PersonEmail;

            _sql.SaveChanges();
            return RedirectToAction("Index");
        }




        public IActionResult IstifadeciBlokla(int id)
        {
            _sql.People.SingleOrDefault(x => x.PersonId == id).PersonStatus = false;
            _sql.SaveChanges();
            return RedirectToAction("Index");
        }




        public IActionResult IstifadeciBlokdanCixart(int id)
        {
            _sql.People.SingleOrDefault(x => x.PersonId == id).PersonStatus = true;
            _sql.SaveChanges();
            return RedirectToAction("Index");
        }




        [Authorize(Roles = "Admin")]
        public IActionResult Moderator()
        {
            return View(_sql.People.Where(x => x.PersonRolId == 2 && x.PersonStatus == true).ToList());
        }



        public IActionResult ModeratorElaveEt()
        {
            return View();
        }



        [HttpPost]
        public IActionResult ModeratorElaveEt(Person moderator)
        {
            moderator.PersonRolId = 2;
            moderator.PersonStatus = true;
            _sql.People.Add(moderator);
            _sql.SaveChanges();
            return RedirectToAction("Moderator");
        }



        //[HttpPost]
        public IActionResult ModeratorSil(int id)
        {
            _sql.People.SingleOrDefault(x => x.PersonId == id).PersonStatus = false;
            _sql.SaveChanges();
            return RedirectToAction("Moderator");
        }


        public class Nov
        {
            public int usercount { get; set; }
            public List<Elan> normal { get; set; }
            public List<Elan> vip { get; set; }
        }


        public IActionResult Elanlar()
        {
            return View();
        }


        public class Elanss
        {
            public List<Elan> elans { get; set; }
            public Elan elan { get; set; }
            public List<Rey> rey { get; set; }

        }


        public IActionResult Elan(int id, int ElanId)
        {
            if (ElanId != 0 || (ElanId == 0 && id != 0))
            {
                Elanss s = new Elanss();

                s.elan = _sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Include(x => x.ElanTip).Include(x => x.ElanRayon).
                    Include(x => x.ElanPerson).Include(x => x.Reys).SingleOrDefault(x => x.ElanId == (ElanId == 0 ? id : ElanId));

                s.rey = _sql.Reys.Include(x => x.ReyPerson).Where(x => x.ReyElanId == (ElanId == 0 ? id : ElanId)).ToList();
                if (s.elan!=null)
                {
                    return View(s);

                }
                else
                {
                    return View("Elanlar", "Elan tapilmadi");
                }

            }
            else
                return View("Elanlar", "Elan tapilmadi");
        }



        public IActionResult ElanTesdiqEt()
        {
            return View(_sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Include(x => x.ElanRayon).Where(x => x.ElanStatus == false && x.ElanAktivlik == true).ToList());
        }


        [HttpPost]
        public IActionResult ElanTesdiqEt(int id)
        {
            _sql.Elans.SingleOrDefault(x => x.ElanId == id).ElanStatus = true;
            _sql.Elans.SingleOrDefault(x => x.ElanId == id).ElanAktivlik = true;
            _sql.SaveChanges();
            return RedirectToAction("ElanTesdiqEt");
        }


        [HttpPost]
        public IActionResult ElanTesdiqEtme(int id)
        {
            _sql.Elans.SingleOrDefault(x => x.ElanId == id).ElanStatus = false;
            _sql.Elans.SingleOrDefault(x => x.ElanId == id).ElanAktivlik = false;
            _sql.SaveChanges();
            return RedirectToAction("ElanTesdiqEt");
        }


        [HttpPost]
        public IActionResult ElanBlokEt(int id)
        {
            _sql.Elans.SingleOrDefault(x => x.ElanId == id).ElanAktivlik = false;
            _sql.SaveChanges();
            return RedirectToAction("Elan", new { id = id });
        }


        public IActionResult Tesdiqlenmemis(int id)
        {
            return View(_sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Include(x => x.ElanRayon).Where(x => x.ElanStatus == false && x.ElanAktivlik == false).ToList());

        }
    }
}
