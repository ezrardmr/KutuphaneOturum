//using KutuphaneOturum.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KutuphaneOturum.Controllers
{
    public class YardimlasmaModel
    {
        public kullanicilar k1 { get; set; }
        public List<yardim> yrdm { get; set; }
        public List<masalar> msl { get; set; }
    }
    public class YardimlasmaController : Controller
    {
        // [Audit]
        KullaniciEntities db = new KullaniciEntities();
        
        // GET: Yardimlasma
        public ActionResult Yardimlasma()
        {
           
            Session["SayfaAdi"] = "Yardimlasma";

            string username = Session["USERNAME"].ToString();
            if (username == null)
            {
                return RedirectToAction("Giris", "Giris");
            }
            kullanicilar user = db.kullanicilar.Where(w => w.username == username).FirstOrDefault();
            YardimlasmaModel ym = new YardimlasmaModel();
            List<masalar> yrdmlsm = new List<masalar>();
            yrdmlsm = db.masalar.Where(w => w.user_id == user.id).ToList();
            ym.k1 = user;
            ym.msl = yrdmlsm;

            return View(ym);
        }
        [HttpPost]
        public ActionResult YardimlasmaAction()
        {
            string usernamea = Session["USERNAME"].ToString();
            if (usernamea == null)
            {
                return RedirectToAction("Giris", "Giris");
            }
            
            return RedirectToAction("Anasayfa", "Anasayfa");
        }
    }
}