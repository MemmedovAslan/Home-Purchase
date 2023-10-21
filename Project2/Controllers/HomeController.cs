using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project2.Controllers
{
    public class HomeController : Controller
    {
        private readonly Project2Context _sql;
        public HomeController(Project2Context sql)
        {
            _sql = sql;
        }

        public class Nov
        {
            public int usercount { get; set; }
            public List<Elan> normal { get; set; }
            public List<Elan> vip { get; set; }
            public List<Person> adam { get; set; }
        }
        public IActionResult Index(int id, int page = 0)
        {
            Nov n = new Nov();

            ViewBag.SK = new SelectList(_sql.Sks.ToList(), "Skid", "Skad");
            ViewBag.Tip = new SelectList(_sql.Tips.ToList(), "TipId", "TipAd");
            ViewBag.Seher = new SelectList(_sql.Sehers.ToList(), "SeherId", "SeherAd");

            ViewBag.SehifeSayi = Math.Ceiling((decimal)_sql.Elans.ToList().Count / 1);

            n.usercount = _sql.People.Count();
            n.adam = _sql.People.ToList(); ;

            //if (id == 0)
            //{
                n.normal = _sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Include(x => x.ElanRayon).Include(x => x.ElanPerson).Where(x => x.ElanStatus == true && x.ElanAktivlik == true && x.ElanPerson.PersonStatus == true).Skip(page * 4).Take(4).ToList();
                n.vip = _sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Include(x => x.ElanRayon).Where(x => x.ElanNovId == 1 && x.ElanStatus == true && x.ElanAktivlik == true && x.ElanPerson.PersonStatus == true).Skip(page * 4).Take(4).ToList();
            //}
            //else
            //{
            //    n.normal = _sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Include(x => x.ElanRayon).Include(x => x.ElanPerson).Where(x => x.ElanSkid == id && x.ElanStatus == true && x.ElanAktivlik == true && x.ElanPerson.PersonStatus == true).Skip(page * 4).Take(4).ToList();
            //    n.vip = _sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Include(x => x.ElanRayon).Where(x => x.ElanNovId == 1 && x.ElanSkid == id && x.ElanStatus == true && x.ElanAktivlik == true && x.ElanPerson.PersonStatus == true).Skip(page * 4).Take(4).ToList();
            //}

            //n.normal.Skip(page * 1).Take(1).ToList();
            //n.vip = n.vip.Skip(page * 1).Take(1).ToList();

            return View(n);
        }



        public IActionResult ElanEtrafliAxtaris()
        {
            ViewBag.SK = _sql.Sks.ToList();
            ViewBag.Tip = _sql.Tips.ToList();
            ViewBag.Seher = _sql.Sehers.ToList();
            ViewBag.Rayon = _sql.Rayons.ToList();
            return View(); ;
        }


        [Authorize]

        public IActionResult ElanYerlesdir()
        {
            //serti burda yazarsan eger bloka atilibsa...
            var a = int.Parse(User.Claims.SingleOrDefault(x => x.Type == "Id").Value);
            if (_sql.People.SingleOrDefault(x => x.PersonId == a).PersonStatus == true)
            {
                ViewBag.SK = new SelectList(_sql.Sks.ToList(), "Skid", "Skad");
                ViewBag.Tip = new SelectList(_sql.Tips.ToList(), "TipId", "TipAd");
                ViewBag.Seher = _sql.Sehers.ToList();
                ViewBag.Rayon = _sql.Rayons.ToList();
                return View();
            }
            else
                return RedirectToAction("ElanYerlesdirFalse");
        }


        [Authorize]
        [HttpPost]
        public IActionResult ElanYerlesdir(Elan elan, List<IFormFile> Sekil)
        {
            elan.ElanPersonId = int.Parse(User.Claims.SingleOrDefault(x => x.Type == "Id").Value);
            elan.ElanNovId = 2;
            elan.ElanVaxt = DateTime.Now;
            elan.ElanStatus = false;
            elan.ElanAktivlik = true;

            _sql.Elans.Add(elan);
            _sql.SaveChanges();


            if (Sekil != null)
            {
                for (int i = 0; i < Sekil.Count; i++)
                {
                    Sekil sekil = new Sekil();
                    string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(Sekil[i].FileName);
                    using (Stream stream = new FileStream("wwwroot/img/" + filename, FileMode.Create))
                    {
                        Sekil[i].CopyTo(stream);
                    }
                    sekil.SekilAd = filename;
                    sekil.SekilElanId = elan.ElanId;
                    _sql.Add(sekil);
                    _sql.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult ElanYerlesdirFalse()
        {
            return View();
        }

        public class Elanss
        {
            public List<Elan> elans { get; set; }
            public Elan elan { get; set; }
            public List<Rey> rey { get; set; }

        }

        public IActionResult Elan(int id)
        {

            Elanss s = new Elanss();

            if (id != 0)
            {
                s.elan = _sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Include(x => x.ElanTip).Include(x => x.ElanRayon).
                Include(x => x.ElanPerson).Include(x => x.Reys).SingleOrDefault(x => x.ElanId == id);

                s.elans = _sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Where(x => (
                  (x.ElanSkid == s.elan.ElanSkid) &&
                  (x.ElanSeherId == s.elan.ElanSeherId) &&
                  (x.ElanRayonId == s.elan.ElanRayonId) &&
                  (x.ElanOtaqsayi == s.elan.ElanOtaqsayi) &&
                  (x.ElanTipId == s.elan.ElanTipId) &&
                  (x.ElanId != id))).ToList();

                s.rey = _sql.Reys.Include(x => x.ReyPerson).Where(x => x.ReyElanId == id).ToList();

                return View(s);
            }
            else
                return RedirectToAction("Index");

        }



        [Authorize]
        public IActionResult ElanSil(int id)
        {
            //ya Sql də Elanın istifadə olunduğu cədvəl yəni Sekiller də SekilElanId nin qabağına "on delete cascade" yazmalısan
            //ya da burda birinci Sekiller cədvəlini silib sonra Elan cədvəlini silməsilən

            _sql.Elans.Remove(_sql.Elans.SingleOrDefault(x => x.ElanId == id));
            _sql.SaveChanges();
            return RedirectToAction("Index");
        }



        [Authorize]
        public IActionResult ElanDuzelt(int id)
        {
            if (int.Parse(User.Claims.SingleOrDefault(x => x.Type == "Id").Value) == _sql.Elans.SingleOrDefault(x => x.ElanId == id).ElanPersonId || User.Claims.SingleOrDefault(x=>x.Type == System.Security.Claims.ClaimTypes.Role).Value == "Admin" )
            {
                ViewBag.SK = _sql.Sks.ToList();
                ViewBag.Tip = _sql.Tips.ToList();
                ViewBag.Seher = _sql.Sehers.ToList();
                ViewBag.Rayon = _sql.Rayons.ToList();
                return View(_sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSk).Include(x => x.ElanTip).Include(x => x.ElanSeher).Include(x => x.ElanRayon)
                   .SingleOrDefault(x => x.ElanId == id));
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public IActionResult ElanDuzelt(int id, Elan e, List<IFormFile> Sekil)
        {
            if (int.Parse(User.Claims.SingleOrDefault(x => x.Type == "Id").Value) == _sql.Elans.SingleOrDefault(x => x.ElanId == id).ElanPersonId || User.Claims.SingleOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Role).Value == "Admin")
            {
                Elan ekohne = _sql.Elans.SingleOrDefault(x => x.ElanId == id);

                if (Sekil != null)
                {
                    for (int i = 0; i < Sekil.Count; i++)
                    {
                        Sekil sekil = new Sekil();
                        string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(Sekil[i].FileName);
                        using (Stream stream = new FileStream("wwwroot/img/" + filename, FileMode.Create))
                        {
                            Sekil[i].CopyTo(stream);
                        }
                        sekil.SekilAd = filename;
                        sekil.SekilElanId = id;
                        _sql.Add(sekil);
                        _sql.SaveChanges();
                    }
                }
                ekohne.ElanSkid = e.ElanSkid;
                ekohne.ElanTipId = e.ElanTipId;
                ekohne.ElanOtaqsayi = e.ElanOtaqsayi;
                ekohne.ElanMertebesayi = e.ElanMertebesayi;
                ekohne.ElanUmumiMertebesayi = e.ElanUmumiMertebesayi;
                ekohne.ElanSeherId = e.ElanSeherId;
                ekohne.ElanRayonId = e.ElanRayonId;
                ekohne.ElanUnvan = e.ElanUnvan;
                ekohne.ElanQiymet = e.ElanQiymet;
                ekohne.ElanSahe = e.ElanSahe;
                ekohne.ElanMelumat = e.ElanMelumat;

                ekohne.ElanInternet = e.ElanInternet;
                ekohne.ElanLift = e.ElanLift;
                ekohne.ElanKupça = e.ElanKupça;
                ekohne.ElanKondisioner = e.ElanKondisioner;
                ekohne.ElanKombi = e.ElanKombi;
                ekohne.ElanQaz = e.ElanQaz;
                ekohne.ElanTemir = e.ElanTemir;
                ekohne.ElanTelefon = e.ElanTelefon;

                _sql.SaveChanges();
                if(User.Claims.SingleOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Role).Value == "Admin")
                {
                    return RedirectToAction("Elan","Admin",new { id = id });
                }
                else
                    return RedirectToAction("Elan","Home",new { id = id });

            }
            return RedirectToAction("Index");


        }

        public IActionResult ElanVipEt(int id)
        {
            _sql.Elans.SingleOrDefault(x => x.ElanId == id).ElanNovId = 1;
            _sql.SaveChanges();

            return RedirectToAction("Elan", new { id = id });
        }

        public IActionResult SekilSil(int id)
        {
            var filename = _sql.Sekils.SingleOrDefault(x => x.SekilId == id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", filename.SekilAd);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _sql.Sekils.Remove(filename);
            _sql.SaveChanges();
            //string url = this.Request.UrlReferrer.AbsolutePath;
            return RedirectToAction("ElanDuzelt", new { id = filename.SekilElanId });
        }

        public class Elave
        {
            public bool ElanInternet { get; set; }
            public bool ElanLift { get; set; }
            public bool ElanQaz { get; set; }
            public bool ElanKupça { get; set; }
            public bool ElanTelefon { get; set; }
            public bool ElanKombi { get; set; }
            public bool ElanKondisioner { get; set; }
            public bool ElanTemir { get; set; }
        }
        public IActionResult Axtaris(Elave e, int ElanSKId, int ElanSeherId, int ElanOtaqSayi, int ElanTipId, int MinQiymet, int MaxQiymet)
        {
            Nov a = new Nov();

            a.normal = _sql.Elans.Include(x => x.Sekils).Include(x => x.ElanRayon).Include(x => x.ElanSeher).Where(x =>

               (x.ElanLift == e.ElanLift || e.ElanLift == false) &&
               (x.ElanKombi == e.ElanKombi || e.ElanKombi == false) &&
               (x.ElanInternet == e.ElanInternet || e.ElanInternet == false) &&
               (x.ElanQaz == e.ElanQaz || e.ElanQaz == false) &&
               (x.ElanKupça == e.ElanKupça || e.ElanKupça == false) &&
               (x.ElanTemir == e.ElanTemir || e.ElanTemir == false) &&
               (x.ElanTelefon == e.ElanTelefon || e.ElanTelefon == false) &&
               (x.ElanKondisioner == e.ElanKondisioner || e.ElanKondisioner == false) &&

               ((x.ElanSkid == ElanSKId || ElanSKId == 0) &&
               (x.ElanSeherId == ElanSeherId || ElanSeherId == 0) &&
               (x.ElanOtaqsayi == ElanOtaqSayi || ElanOtaqSayi == 0) &&
               (x.ElanTipId == ElanTipId || ElanTipId == 0) &&
               ((x.ElanQiymet > MinQiymet && x.ElanQiymet < MaxQiymet) || (x.ElanQiymet > MinQiymet && MaxQiymet == 0) || (x.ElanQiymet < MaxQiymet && MinQiymet == 0) || (MinQiymet == 0 && MaxQiymet == 0)))).ToList();

            a.vip = _sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Include(x => x.ElanRayon).Where(x => x.ElanNovId == 1).ToList();


            ViewBag.SK = new SelectList(_sql.Sks.ToList(), "Skid", "Skad");
            ViewBag.Tip = new SelectList(_sql.Tips.ToList(), "TipId", "TipAd");
            ViewBag.Seher = new SelectList(_sql.Sehers.ToList(), "SeherId", "SeherAd");


            return View("Index", a);
        }

        public IActionResult MenimElanlarim()
        {
            var personid = int.Parse(User.Claims.SingleOrDefault(x => x.Type == "Id").Value);
            Nov my = new Nov();
            my.normal = _sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Include(x => x.ElanRayon).
                Where(x => x.ElanPersonId == personid).ToList();

            my.vip = _sql.Elans.Include(x => x.Sekils).Include(x => x.ElanSeher).Include(x => x.ElanRayon).
                Where(x => x.ElanPersonId == personid && x.ElanNovId == 1).ToList();

            return View("MenimElanlarim", my);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ReyBildir(int id, Rey r)
        {
            r.ReyPersonId = int.Parse(User.Claims.SingleOrDefault(x => x.Type == "Id").Value);
            r.ReyElanId = id;
            _sql.Add(r);
            _sql.SaveChanges();

            return RedirectToAction("Elan", new { id = id });
        }

        public IActionResult ReyDuzelt(int id)
        {
            return View(_sql.Reys.SingleOrDefault(x => x.ReyId == id));
        }

        [HttpPost]
        public IActionResult ReyDuzelt(int id, Rey r)
        {
            Rey kohnerey = _sql.Reys.SingleOrDefault(x => x.ReyId == id);
            kohnerey.ReyOzu = r.ReyOzu;
            _sql.SaveChanges();
            return RedirectToAction("Elan", new { id = kohnerey.ReyElanId });
        }

        public IActionResult ReySil(int id)
        {
            Rey kohnerey = _sql.Reys.SingleOrDefault(x => x.ReyId == id);

            _sql.Remove(_sql.Reys.SingleOrDefault(x => x.ReyId == id));
            _sql.SaveChanges();
            return RedirectToAction("Elan", new { id = kohnerey.ReyElanId });

        }
    }
}
