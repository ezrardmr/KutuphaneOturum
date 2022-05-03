//using KutuphaneOturum.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneOturum.Controllers
{
    public class YardimlasmaModel
    {
        public kullanicilar k1 { get; set; }

    }
    public class YardimlasmaController : Controller
    {
       // [Audit]
        // GET: Yardimlasma
        public ActionResult Yardimlasma()
        {
            KullaniciEntities db = new KullaniciEntities();
            Session["SayfaAdi"] = "Yardimlasma";

            string username = Session["USERNAME"].ToString();
            if (username == null)
            {
                return RedirectToAction("Giris", "Giris");
            }
            kullanicilar user = db.kullanicilar.Where(w => w.username == username).FirstOrDefault();
            return View();
        }
    }
}